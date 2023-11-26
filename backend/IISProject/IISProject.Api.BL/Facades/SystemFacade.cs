using AutoMapper;
using IISProject.Api.BL.Enums;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.Device;
using IISProject.Api.BL.Models.System;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;
using Microsoft.IdentityModel.Tokens;

namespace IISProject.Api.BL.Facades;

public class SystemFacade: FacadeBase<SystemEntity, SystemListModel, SystemDetailModel, SystemCreateUpdateModel>, ISystemFacade
{
    private readonly DeviceFacade _deviceFacade;

    public SystemFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, DeviceFacade deviceFacade) : base(unitOfWorkFactory, mapper)
    {
        _deviceFacade = deviceFacade;
    }
    
    public async Task<SystemSearchModel> SearchAsync(SearchSystemParams parameters)
    {
        var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<SystemEntity>();
        var systemQuery = repository.GetAll();
        IncludeNavigationPathDetails(ref systemQuery);
        
        IEnumerable<SystemEntity> filteredSystems;
        if (parameters.Query.IsNullOrEmpty())
        {
            filteredSystems = systemQuery.OrderBy(x => x.Name);
        }
        else
        {
            filteredSystems = systemQuery
                .Where(x => x.Name.ToLower().Contains(parameters.Query.ToLower()) ||
                            x.Description.ToLower().Contains(parameters.Query.ToLower()));
        }

        var systems = filteredSystems
            .Skip(parameters.PageIndex * parameters.PageSize)
            .Take(parameters.PageSize).ToList();
        
        var totalCount = filteredSystems.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / parameters.PageSize);

        var systemModels = Mapper.Map<IEnumerable<SystemListModel>>(systems);
        
        systemModels = GetSystemsStatus(systemModels.ToList());


        var result = new SystemSearchModel
        {
            PageIndex = parameters.PageIndex,
            PageSize = parameters.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Systems = systemModels
        };
        
        return result;
    }

    private IEnumerable<SystemListModel> GetSystemsStatus(List<SystemListModel> systems)
    {
        var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<DeviceEntity>();
        var deviceQuery = repository.GetAll();

        var result = new List<SystemListModel>();
        foreach (var system in systems)
        {
            var devicesInSystem = deviceQuery.Where(x => x.SystemId == system.Id).ToList();
            var devicesWithStatus = _deviceFacade.GetDevicesWithStatus(devicesInSystem);
            var systemStatus = CheckSystemStatus(devicesWithStatus.ToList());
            var systemModel = Mapper.Map<SystemListModel>(system);
            systemModel.Status = systemStatus;
            result.Add(systemModel);
        }

        return result;   
    }
    
    private SystemStatus CheckSystemStatus(List<DeviceStatusListModel> devices)
    {
        if (devices.Count == 0)
        {
            return SystemStatus.Okay;
        }
        
        bool hasError = devices.Any(x => x.Status == DeviceStatus.Critical || x.Status == DeviceStatus.Warning);
        bool allError = devices.All(x => x.Status == DeviceStatus.Critical);

        if (allError)
        {
            return SystemStatus.Critical;
        } 
        
        if (hasError)
        {
            return SystemStatus.Warning;
        }

        return SystemStatus.Okay;
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(SystemEntity.Devices)}",
        $"{nameof(SystemEntity.UsersInSystem)}",
        $"{nameof(SystemEntity.UsersInSystem)}.{nameof(UserInSystemEntity.User)}",
        $"{nameof(SystemEntity.Creator)}",
        $"{nameof(SystemEntity.Devices)}.{nameof(DeviceEntity.DeviceType)}"
    };
}
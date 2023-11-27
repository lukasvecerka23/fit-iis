using System.Security.Claims;
using AutoMapper;
using IISProject.Api.BL.Enums;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.Device;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.Repositories;
using IISProject.Api.DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace IISProject.Api.BL.Facades;

public class DeviceFacade: FacadeBase<DeviceEntity, DeviceListModel, DeviceDetailModel, DeviceCreateUpdateModel>, IDeviceFacade
{
    private readonly IHttpContextAccessor _contextAccessor;

    public DeviceFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IHttpContextAccessor contextAccessor) : base(unitOfWorkFactory, mapper)
    {
        _contextAccessor = contextAccessor;
    }
    
    public async Task<DeviceSearchModel> SearchAsync(SearchDeviceParams parameters)
    {
        var userId = _contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdminOrBroker = _contextAccessor.HttpContext!.User.IsInRole("Admin") || _contextAccessor.HttpContext!.User.IsInRole("Broker");
        
        var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<DeviceEntity>();
        var userInSystemRepository = uow.GetRepository<UserInSystemEntity>();
        var deviceQuery = repository.GetAll();
        IncludeNavigationPathDetails(ref deviceQuery);

        if (!isAdminOrBroker)
        {
            var userSystems = userInSystemRepository.GetAll()
                .Where(x => x.UserId == Guid.Parse(userId))
                .Select(x => x.SystemId).ToList();
            
            deviceQuery = deviceQuery.Where(x => x.CreatorId == Guid.Parse(userId) || userSystems.Contains(x.SystemId ?? Guid.Empty));
        }

        
        IEnumerable<DeviceEntity> filteredDevices;
        if (parameters.SystemId != Guid.Empty)
        {
            deviceQuery = deviceQuery.Where(x => x.SystemId == parameters.SystemId);
        }
        if (parameters.Query.IsNullOrEmpty())
        {
            filteredDevices = deviceQuery.OrderBy(x => x.UserAlias);
        }
        else
        {
            filteredDevices = deviceQuery
                .Where(x => x.UserAlias.ToLower().Contains(parameters.Query.ToLower()) ||
                            x.Description.ToLower().Contains(parameters.Query.ToLower()));
        }
        
        var devices = filteredDevices
            .Skip(parameters.PageIndex * parameters.PageSize)
            .Take(parameters.PageSize).ToList();

        var totalCount = filteredDevices.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / parameters.PageSize);


        var result = new DeviceSearchModel
        {
            PageIndex = parameters.PageIndex,
            PageSize = parameters.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Devices = GetDevicesWithStatus(devices)
        };

        return result;
    }


    public IEnumerable<DeviceStatusListModel> GetDevicesWithStatus(IEnumerable<DeviceEntity> devices)
    {
        var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<KpiEntity>();
        var kpiQuery = repository.GetAll();
        
        var result = new List<DeviceStatusListModel>();
        foreach (var device in devices)
        {
            var kpis = kpiQuery.Where(x => x.DeviceId == device.Id).ToList();
            var status = CheckDeviceStatus(kpis);
            var deviceStatus = Mapper.Map<DeviceStatusListModel>(device);
            deviceStatus.Status = status;
            result.Add(deviceStatus);
        }
        
        return result;
    }
    
    public DeviceStatus CheckDeviceStatus(List<KpiEntity> kpis)
    {
        if (kpis.Count == 0)
        {
            return DeviceStatus.Okay;
        }
        
        bool hasError = kpis.Any(x => x.Error == true);
        bool allError = kpis.All(x => x.Error == true);

        if (allError)
        {
            return DeviceStatus.Critical;
        } 
        
        if (hasError)
        {
            return DeviceStatus.Warning;
        }

        return DeviceStatus.Okay;
    }
    
    public async Task<IdModel?> CreateWithCheckAsync(DeviceCreateUpdateModel model)
    {
        DeviceEntity entity = Mapper.Map<DeviceEntity>(model);
        
        await using var uow = UnitOfWorkFactory.Create();
        IRepository<DeviceEntity> repository = uow.GetRepository<DeviceEntity>();
        
        var existUserId = repository.GetAll().Any(x => x.UserId == entity.UserId);
        
        if (existUserId)
        {
            return null;
        }
        
        entity.Id = Guid.NewGuid();
        DeviceEntity insertedEntity = await repository.InsertAsync(entity);
        
        await uow.CommitAsync();
        
        var result = Mapper.Map<IdModel>(insertedEntity);
        
        return result;
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(DeviceEntity.DeviceType)}",
        $"{nameof(DeviceEntity.Creator)}",
        $"{nameof(DeviceEntity.System)}",
        $"{nameof(DeviceEntity.DeviceType)}.{nameof(DeviceTypeEntity.Parameters)}",
    };
}
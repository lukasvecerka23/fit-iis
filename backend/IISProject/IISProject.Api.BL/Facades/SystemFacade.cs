using System.Security.Claims;
using AutoMapper;
using IISProject.Api.BL.Enums;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.Device;
using IISProject.Api.BL.Models.System;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace IISProject.Api.BL.Facades;

public class SystemFacade: FacadeBase<SystemEntity, SystemListModel, SystemDetailModel, SystemCreateUpdateModel>, ISystemFacade
{
    private readonly DeviceFacade _deviceFacade;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SystemFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, DeviceFacade deviceFacade, IHttpContextAccessor contextAccessor) : base(unitOfWorkFactory, mapper)
    {
        _httpContextAccessor = contextAccessor;
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
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
        var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<DeviceEntity>();
        var userInSystemRepository = uow.GetRepository<UserInSystemEntity>();
        var assignToSystemRepository = uow.GetRepository<AssignToSystemEntity>();
        var deviceQuery = repository.GetAll();
        var usersSystem = userInSystemRepository.GetAll().Where(x => x.UserId.ToString() == userId);
        var usersAssigns = assignToSystemRepository.GetAll().Where(x => x.UserId.ToString() == userId);

        var result = new List<SystemListModel>();
        foreach (var system in systems)
        {
            var devicesInSystem = deviceQuery.Where(x => x.SystemId == system.Id).ToList();
            var devicesWithStatus = _deviceFacade.GetDevicesWithStatus(devicesInSystem);
            var isUserOfSystem = usersSystem.Any(x => x.SystemId == system.Id);
            var isUserAssignedToSystem = usersAssigns.Any(x => x.SystemId == system.Id);
            
            if (isUserOfSystem && system.CreatorId.ToString() != userId)
            {
                system.AssignStatus = AssignStatus.Leave;
            }
            else if (userRole == "Admin" || system.CreatorId.ToString() == userId)
            {
                system.AssignStatus = AssignStatus.None;
            }
            else if (isUserAssignedToSystem)
            {
                system.AssignStatus = AssignStatus.Processing;
            }

            else
            {
                system.AssignStatus = AssignStatus.CanAssign;
            }
            
            var systemStatus = userRole == "Admin" || isUserOfSystem ? CheckSystemStatus(devicesWithStatus.ToList()) : SystemStatus.None;
            
            system.Status = systemStatus;
            system.CanEdit = userRole == "Admin" || system.CreatorId.ToString() == userId;
            result.Add(system);
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
    
    public async Task<bool> LeaveSystemAsync(Guid systemId)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var uow = UnitOfWorkFactory.Create();
        var userInSystemRepository = uow.GetRepository<UserInSystemEntity>();
        var userInSystem = userInSystemRepository.GetAll().FirstOrDefault(x => x.SystemId == systemId && x.UserId.ToString() == userId);

        if (userInSystem != null)
        {
            await userInSystemRepository.DeleteAsync(userInSystem.Id);
            await uow.CommitAsync();
            return true;
        }

        return false;
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
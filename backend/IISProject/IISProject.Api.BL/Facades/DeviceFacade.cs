using AutoMapper;
using AutoMapper.QueryableExtensions;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.Device;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace IISProject.Api.BL.Facades;

public class DeviceFacade: FacadeBase<DeviceEntity, DeviceListModel, DeviceDetailModel, DeviceCreateUpdateModel>, IDeviceFacade
{
    public DeviceFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
    
    public async Task<DeviceSearchModel> SearchAsync(string query, int index, int size)
    {
        var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<DeviceEntity>();
        var deviceQuery = repository.GetAll();
        IncludeNavigationPathDetails(ref deviceQuery);

        IEnumerable<DeviceEntity> filteredDevices;
        if (query.IsNullOrEmpty())
        {
            filteredDevices = deviceQuery.OrderBy(x => x.UserAlias);
        }
        else
        {
            filteredDevices = deviceQuery
                .Where(x => x.UserAlias.ToLower().Contains(query.ToLower()) ||
                            x.Description.ToLower().Contains(query.ToLower()));
        }

        var systems = filteredDevices
            .Skip(index * size)
            .Take(size).ToList();

        var totalCount = filteredDevices.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / size);


        var result = new DeviceSearchModel
        {
            PageIndex = index,
            PageSize = size,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Devices = Mapper.Map<IEnumerable<DeviceListModel>>(systems)
        };

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
using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.DeviceType;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace IISProject.Api.BL.Facades;

public class DeviceTypeFacade: FacadeBase<DeviceTypeEntity, DeviceTypeListModel, DeviceTypeDetailModel, DeviceTypeCreateUpdateModel>, IDeviceTypeFacade
{
    public DeviceTypeFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
    
    public override async  Task<bool> DeleteAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        
        var deviceTypeRepository = uow.GetRepository<DeviceTypeEntity>();
        
        var deviceRepository = uow.GetRepository<DeviceEntity>();
        
        
        if (!await deviceTypeRepository.ExistsAsync(id))
        {
            return false;
        }

        await deviceRepository.GetAll().Where(x => x.DeviceTypeId == id).ExecuteDeleteAsync();
        
        await deviceTypeRepository.DeleteAsync(id);
        
        await uow.CommitAsync();
        
        return true;
    }
    
    public async Task<DeviceTypeSearchModel> SearchAsync(string query, int index, int size)
    {
        var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<DeviceTypeEntity>();
        var deviceTypeQuery = repository.GetAll();
        IncludeNavigationPathDetails(ref deviceTypeQuery);
        
        IEnumerable<DeviceTypeEntity> filteredDeviceTypes;
        if (query.IsNullOrEmpty())
        {
            filteredDeviceTypes = deviceTypeQuery.OrderBy(x => x.Name);
        }
        else
        {
            filteredDeviceTypes = deviceTypeQuery
                .Where(x => x.Name.ToLower().Contains(query.ToLower()));
        }

        var deviceTypes = filteredDeviceTypes
            .Skip(index * size)
            .Take(size).ToList();
        
        var totalCount = filteredDeviceTypes.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / size);


        var result = new DeviceTypeSearchModel
        {
            PageIndex = index,
            PageSize = size,
            TotalCount = totalCount,
            TotalPages = totalPages,
            DeviceTypes = Mapper.Map<IEnumerable<DeviceTypeListModel>>(deviceTypes)
        };
        
        return result;
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(DeviceTypeEntity.Devices)}",
        $"{nameof(DeviceTypeEntity.Parameters)}"
    };
}
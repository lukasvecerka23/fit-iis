using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.DeviceType;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

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
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(DeviceTypeEntity.Devices)}",
        $"{nameof(DeviceTypeEntity.Parameters)}"
    };
}
using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.AssignToSystem;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.BL.Facades;

public class AssignToSystemFacade: FacadeBase<AssignToSystemEntity, AssignToSystemListModel, AssignToSystemDetailModel, AssignToSystemCreateUpdateModel>, IAssignToSystemFacade
{
    public AssignToSystemFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
    
    public async Task<IEnumerable<AssignToSystemListModel>> GetAllAsync(AssignToSystemParams parameters)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<AssignToSystemEntity> query = uow.GetRepository<AssignToSystemEntity>().GetAll();
        
        if (parameters.SystemId != Guid.Empty)
        {
            query = query.Where(x => x.SystemId == parameters.SystemId);
        }

        IncludeNavigationPathDetails(ref query);

        List<AssignToSystemEntity> entities = await query.ToListAsync();
        
        return Mapper.Map<IEnumerable<AssignToSystemListModel>>(entities);
    }

    public async Task<bool> AssignUserToSystem(Guid assignId)
    {
        IUnitOfWork uow = UnitOfWorkFactory.Create();
        
        var assign = await base.GetByIdAsync(assignId);
        
        if (assign == null)
        {
            return false;
        }
        
        var userInSystemRepository = uow.GetRepository<UserInSystemEntity>();
        
        var userInSystem = userInSystemRepository.GetAll().Where(x => x.SystemId == assign.SystemId && x.UserId == assign.UserId);
        
        if (!userInSystem.Any())
        {
            var newUserInSystem = new UserInSystemEntity
            {
                Id = Guid.NewGuid(),
                SystemId = assign.SystemId,
                UserId = assign.UserId
            };
            
            await userInSystemRepository.InsertAsync(newUserInSystem);
        }

        await base.DeleteAsync(assign.Id);
        
        await uow.CommitAsync();

        return true;
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(AssignToSystemEntity.User)}",
    };
}
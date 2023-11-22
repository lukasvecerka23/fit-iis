using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.RoleOfUser;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.BL.Facades;

public class RoleOfUserFacade: FacadeBase<RoleOfUserEntity, RoleOfUserListModel, RoleOfUserDetailModel, RoleOfUserCreateUpdateModel>, IRoleOfUserFacade
{
    public RoleOfUserFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
    
    public IEnumerable<RoleOfUserListModel> GetByUserIdAsync(Guid userId)
    {
        var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<RoleOfUserEntity>();
        var roleOfUser = repository.GetAll().Where(x => x.UserId == userId);
        IncludeNavigationPathDetails(ref roleOfUser);
        return Mapper.Map<IEnumerable<RoleOfUserListModel>>(roleOfUser.ToList());
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(RoleOfUserEntity.Role)}",
    };
}
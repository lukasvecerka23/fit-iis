using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.User;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.Seeds;
using IISProject.Api.DAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace IISProject.Api.BL.Facades;

public class UserFacade: FacadeBase<UserEntity, UserListModel, UserDetailModel, UserCreateUpdateModel>, IUserFacade
{
    private readonly PasswordHasher<UserEntity> _passwordHasher = new PasswordHasher<UserEntity>();
    
    public UserFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
    
    public async Task<UserDetailModel?> AuthenticateAsync(string username, string password)
    {
        var uow = UnitOfWorkFactory.Create();
        
        var repository = uow.GetRepository<UserEntity>();
        
        var users = repository.GetAll();
        
        IncludeNavigationPathDetails(ref users);
        
        var user = await users.SingleOrDefaultAsync(x => x.Username == username);
        
        if (user == null || !VerifyPassword(user, user.PasswordHash, password))
        {
            return null;
        }

        return Mapper.Map<UserDetailModel>(user);
    }
    
    public async Task<UserDetailModel?> RegisterAsync(UserCreateUpdateModel userCreateUpdateModel)
    { 
        var uow = UnitOfWorkFactory.Create();
        
        var repository = uow.GetRepository<UserEntity>();
        
        var users = repository.GetAll();
        
        var user = await users.SingleOrDefaultAsync(x => x.Username == userCreateUpdateModel.Username);

        if (user != null)
        {
            return null;
        }

        var newUser = Mapper.Map<UserEntity>(userCreateUpdateModel);
        newUser.PasswordHash = HashPassword(newUser, userCreateUpdateModel.Password);
        newUser.RoleId = RoleSeeds.UserRole.Id;
        var createdUser = await repository.InsertAsync(newUser);
        await uow.CommitAsync();
        return Mapper.Map<UserDetailModel>(createdUser);
    }
    
    public string HashPassword(UserEntity user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(UserEntity user, string hashedPassword, string providedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword) != PasswordVerificationResult.Failed;
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(UserEntity.Role)}",
    };
}
using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.Auth;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.BL.Models.User;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.Seeds;
using IISProject.Api.DAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


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
    
    public async Task<UserDetailModel?> RegisterAsync(RegisterModel registerModel)
    { 
        var uow = UnitOfWorkFactory.Create();
        
        var repository = uow.GetRepository<UserEntity>();
        
        var users = repository.GetAll();
        
        var user = await users.SingleOrDefaultAsync(x => x.Username == registerModel.Username);

        if (user != null)
        {
            return null;
        }

        var newUser = Mapper.Map<UserEntity>(registerModel);
        newUser.PasswordHash = HashPassword(newUser, registerModel.Password);
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
    
    public async Task<UserSearchModel> SearchAsync(string query, int index, int size)
    {
        var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<UserEntity>();
        var userQuery = repository.GetAll();
        IncludeNavigationPathDetails(ref userQuery);
        
        IEnumerable<UserEntity> filteredUsers;
        if (query.IsNullOrEmpty())
        {
            filteredUsers = userQuery.OrderBy(x => x.Name);
        }
        else
        {
            filteredUsers = userQuery
                .Where(x => 
                    x.Username.ToLower().Contains(query.ToLower()) ||
                    x.Name.ToLower().Contains(query.ToLower()) || 
                    x.Surname.ToLower().Contains(query.ToLower())
                    );
        }

        var users = filteredUsers
            .Skip(index * size)
            .Take(size).ToList();
        
        var totalCount = filteredUsers.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / size);


        var result = new UserSearchModel
        {
            PageIndex = index,
            PageSize = size,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Users = Mapper.Map<IEnumerable<UserListModel>>(users)
        };
        
        return result;
    }
    
    public override async  Task<IdModel?> UpdateAsync(UserCreateUpdateModel model, Guid id)
    {
        var entity = Mapper.Map<UserEntity>(model);

        await using var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<UserEntity>();

        if (!await repository.ExistsAsync(id))
        {
            return null;
        }
        
        var existingUser = await repository.GetAll().SingleOrDefaultAsync(x => x.Id == id);
        
        entity.Id = id;
        entity.PasswordHash = existingUser!.PasswordHash;
        var updatedEntity = await repository.UpdateAsync(entity);

        await uow.CommitAsync();

        var result = Mapper.Map<IdModel>(updatedEntity);
        return result;
    }
    
    public override async Task<bool> DeleteAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        
        var userRepository = uow.GetRepository<UserEntity>();
        
        var systemRepository = uow.GetRepository<SystemEntity>();
        
        var deviceRepository = uow.GetRepository<DeviceEntity>();
        
        if (!await userRepository.ExistsAsync(id))
        {
            return false;
        }
        
        await systemRepository.GetAll().Where(x => x.CreatorId == id).ExecuteDeleteAsync();
        await deviceRepository.GetAll().Where(x => x.CreatorId == id).ExecuteDeleteAsync();
        
        await userRepository.DeleteAsync(id);
        
        await uow.CommitAsync();
        
        return true;
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(UserEntity.Role)}",
    };
}
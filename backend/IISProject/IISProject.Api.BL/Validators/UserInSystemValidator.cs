using FluentValidation;
using IISProject.Api.BL.Models.UserInSystem;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Validators;

public class UserInSystemValidator: AbstractValidator<UserInSystemCreateUpdateModel>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public UserInSystemValidator(IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;

        RuleFor(x => x.UserId).Must(UserExists)
            .WithMessage(x => $"Question with Id = {x.UserId} doesn't exist!");
        RuleFor(x => x.SystemId).Must(SystemExists)
            .WithMessage(x => $"System with Id = {x.SystemId} doesn't exist!");
    }
    
    private bool UserExists(Guid id)
    {
        var uow = _unitOfWorkFactory.Create();

        return uow.GetRepository<UserEntity>().Exists(id);
    }
    
    private bool SystemExists(Guid id)
    {
        var uow = _unitOfWorkFactory.Create();

        return uow.GetRepository<SystemEntity>().Exists(id);
    }
}
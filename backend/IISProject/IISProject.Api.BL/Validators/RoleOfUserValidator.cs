using FluentValidation;
using IISProject.Api.BL.Models.RoleOfUser;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Validators;

public class RoleOfUserValidator: AbstractValidator<RoleOfUserCreateUpdateModel>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public RoleOfUserValidator(IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;

        RuleFor(x => x.RoleId).Must(RoleExists)
            .WithMessage(x => $"Question with Id = {x.RoleId} doesn't exist!");
        RuleFor(x => x.UserId).Must(UserExists)
            .WithMessage(x => $"System with Id = {x.UserId} doesn't exist!");
    }
    
    private bool RoleExists(Guid id)
    {
        var uow = _unitOfWorkFactory.Create();

        return uow.GetRepository<RoleEntity>().Exists(id);
    }
    
    private bool UserExists(Guid id)
    {
        var uow = _unitOfWorkFactory.Create();

        return uow.GetRepository<UserEntity>().Exists(id);
    }
}
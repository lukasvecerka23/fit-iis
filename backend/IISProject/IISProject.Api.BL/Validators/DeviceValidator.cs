using FluentValidation;
using IISProject.Api.BL.Models.Device;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Validators;

public class DeviceValidator: AbstractValidator<DeviceCreateUpdateModel>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public DeviceValidator(IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;

        RuleFor(x => x.DeviceTypeId).Must(DeviceTypeExists)
            .WithMessage(x => $"Question with Id = {x.DeviceTypeId} doesn't exist!");
        RuleFor(x => x.SystemId).Must(SystemExists)
            .WithMessage(x => $"System with Id = {x.SystemId} doesn't exist!");
        RuleFor(x => x.CreatorId).Must(CreatorExists)
            .WithMessage(x => $"User with Id = {x.CreatorId} doesn't exist!");
    }

    private bool DeviceTypeExists(Guid id)
    {
        var uow = _unitOfWorkFactory.Create();

        return uow.GetRepository<DeviceTypeEntity>().Exists(id);
    }
    
    private bool SystemExists(Guid? id)
    {
        if (id == null) 
        {
            return true;
        }
        
        var uow = _unitOfWorkFactory.Create();

        return uow.GetRepository<SystemEntity>().Exists(id.Value);
    }
    
    private bool CreatorExists(Guid id)
    {
        var uow = _unitOfWorkFactory.Create();

        return uow.GetRepository<UserEntity>().Exists(id);
    }
}
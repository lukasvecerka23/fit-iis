using FluentValidation;
using IISProject.Api.BL.Models.Measurement;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Validators;

public class MeasurementValidator: AbstractValidator<MeasurementCreateUpdateModel>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public MeasurementValidator(IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;

        RuleFor(x => x.DeviceId).Must(DeviceExists)
            .WithMessage(x => $"Question with Id = {x.DeviceId} doesn't exist!");
        RuleFor(x => x.ParameterId).Must(ParameterExists)
            .WithMessage(x => $"System with Id = {x.ParameterId} doesn't exist!");
        RuleFor(x => x.CreatorId).Must(CreatorExists)
            .WithMessage(x => $"User with Id = {x.CreatorId} doesn't exist!");
    }
    
    private bool DeviceExists(Guid id)
    {
        var uow = _unitOfWorkFactory.Create();

        return uow.GetRepository<DeviceEntity>().Exists(id);
    }
    
    private bool ParameterExists(Guid id)
    {
        var uow = _unitOfWorkFactory.Create();

        return uow.GetRepository<ParameterEntity>().Exists(id);
    }
    
    private bool CreatorExists(Guid id)
    {
        var uow = _unitOfWorkFactory.Create();

        return uow.GetRepository<UserEntity>().Exists(id);
    }
}
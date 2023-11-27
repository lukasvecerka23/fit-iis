using FluentValidation;
using IISProject.Api.BL.Models.Kpi;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Validators;

public class KpiValidator: AbstractValidator<KpiCreateUpdateModel>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public KpiValidator(IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;

        RuleFor(x => x.ParameterId).Must(ParameterExists)
            .WithMessage(x => $"Question with Id = {x.ParameterId} doesn't exist!");
        RuleFor(x => x.DeviceId).Must(DeviceExists)
            .WithMessage(x => $"Device with Id = {x.DeviceId} doesn't exist!");
        RuleFor(x => x.CreatorId).Must(CreatorExists)
            .WithMessage(x => $"Device with Id = {x.CreatorId} doesn't exist!");
    }
    
    private bool ParameterExists(Guid id)
    {
        var uow = _unitOfWorkFactory.Create();

        return uow.GetRepository<ParameterEntity>().Exists(id);
    }
    
    private bool DeviceExists(Guid id)
    {
        var uow = _unitOfWorkFactory.Create();

        return uow.GetRepository<DeviceEntity>().Exists(id);
    }
    
    private bool CreatorExists(Guid id)
    {
        var uow = _unitOfWorkFactory.Create();

        return uow.GetRepository<UserEntity>().Exists(id);
    }
}
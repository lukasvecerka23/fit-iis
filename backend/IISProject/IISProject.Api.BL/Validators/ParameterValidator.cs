using FluentValidation;
using IISProject.Api.BL.Models.Parameter;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Validators;

public class ParameterValidator: AbstractValidator<ParameterCreateUpdateModel>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public ParameterValidator(IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;

        RuleFor(x => x.DeviceTypeId).Must(DeviceTypeExists)
            .WithMessage(x => $"Question with Id = {x.DeviceTypeId} doesn't exist!");

    }
    
    private bool DeviceTypeExists(Guid id)
    {
        var uow = _unitOfWorkFactory.Create();

        return uow.GetRepository<DeviceTypeEntity>().Exists(id);
    }
}
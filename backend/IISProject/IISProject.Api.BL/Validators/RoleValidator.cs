using FluentValidation;
using IISProject.Api.BL.Models.Role;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Validators;

public class RoleValidator: AbstractValidator<RoleCreateUpdateModel>
{
}
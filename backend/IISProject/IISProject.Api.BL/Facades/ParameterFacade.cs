using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.Parameter;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Facades;

public class ParameterFacade: FacadeBase<ParameterEntity, ParameterListModel, ParameterDetailModel>, IParameterFacade
{
    public ParameterFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(ParameterEntity.Kpis)}"
    };
}
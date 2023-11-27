using IISProject.Api.BL.Models.Parameter;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.Facades.Interfaces;

public interface IParameterFacade: IFacade<ParameterEntity, ParameterListModel, ParameterDetailModel, ParameterCreateUpdateModel>
{
    
}
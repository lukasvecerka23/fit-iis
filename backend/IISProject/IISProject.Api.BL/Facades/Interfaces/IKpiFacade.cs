using IISProject.Api.BL.Models.Kpi;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.Facades.Interfaces;

public interface IKpiFacade: IFacade<KpiEntity, KpiListModel, KpiDetailModel, KpiCreateUpdateModel>
{
    
}
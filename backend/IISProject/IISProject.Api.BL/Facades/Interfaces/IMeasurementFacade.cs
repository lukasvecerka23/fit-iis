using IISProject.Api.BL.Models.Measurement;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.Facades.Interfaces;

public interface IMeasurementFacade: IFacade<MeasurementEntity, MeasurementListModel, MeasurementDetailModel>
{
    
}
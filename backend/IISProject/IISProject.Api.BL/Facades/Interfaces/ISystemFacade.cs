using IISProject.Api.BL.Models.System;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.Facades.Interfaces;

public interface ISystemFacade: IFacade<SystemEntity, SystemListModel, SystemDetailModel, SystemCreateUpdateModel>
{
    
}
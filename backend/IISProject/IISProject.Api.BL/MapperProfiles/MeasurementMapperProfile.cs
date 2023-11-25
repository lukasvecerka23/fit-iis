using AutoMapper;
using IISProject.Api.BL.Extensions;
using IISProject.Api.BL.Models.Measurement;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class MeasurementMapperProfile: Profile
{
    public MeasurementMapperProfile()
    {
        CreateMap<MeasurementEntity, MeasurementListModel>();
        CreateMap<MeasurementEntity, MeasurementDetailModel>();
        CreateMap<MeasurementCreateUpdateModel, MeasurementEntity>()
            .Ignore(dst => dst.Id)
            .Ignore(dst => dst.Device)
            .Ignore(dst => dst.Parameter);
        
        CreateMap<MeasurementEntity, IdModel>();
    }
}
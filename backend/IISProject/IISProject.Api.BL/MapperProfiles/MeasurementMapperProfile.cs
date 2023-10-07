using AutoMapper;
using IISProject.Api.BL.Models.Measurement;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class MeasurementMapperProfile: Profile
{
    public MeasurementMapperProfile()
    {
        CreateMap<MeasurementEntity, MeasurementListModel>();
        CreateMap<MeasurementEntity, MeasurementDetailModel>();
        CreateMap<MeasurementDetailModel, MeasurementEntity>();
    }
}
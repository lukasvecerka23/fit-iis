using AutoMapper;
using IISProject.Api.BL.Extensions;
using IISProject.Api.BL.Models.Parameter;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class ParameterMapperProfile: Profile
{
    public ParameterMapperProfile()
    {
        CreateMap<ParameterEntity, ParameterListModel>();
        
        CreateMap<ParameterEntity, ParameterDetailModel>()
            .MapMember(src => src.Kpis, dst => dst.Kpis);

        CreateMap<ParameterDetailModel, ParameterEntity>()
            .Ignore(dst => dst.Kpis)
            .Ignore(dst => dst.DeviceType)
            .Ignore(dst => dst.Measurements);
    }
}
using AutoMapper;
using IISProject.Api.BL.Extensions;
using IISProject.Api.BL.Models.Kpi;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class KpiMapperProfile: Profile
{
    public KpiMapperProfile()
    {
        CreateMap<KpiEntity, KpiListModel>()
            .MapMember(dst => dst.ParameterName, src => src.Parameter!.Name)
            .Ignore(dst => dst.LastMeasurement);
        
        CreateMap<KpiEntity, KpiDetailModel>();
        CreateMap<KpiCreateUpdateModel, KpiEntity>()
            .Ignore(dst => dst.Id)
            .Ignore(dst => dst.Creator)
            .Ignore(dst => dst.Device)
            .Ignore(dst => dst.Parameter);
        
        CreateMap<KpiEntity, IdModel>();
    }
}
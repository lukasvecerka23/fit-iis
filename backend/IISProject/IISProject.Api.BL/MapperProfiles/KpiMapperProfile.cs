using AutoMapper;
using IISProject.Api.BL.Models.Kpi;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class KpiMapperProfile: Profile
{
    public KpiMapperProfile()
    {
        CreateMap<KpiEntity, KpiListModel>();
        CreateMap<KpiEntity, KpiDetailModel>();
        CreateMap<KpiDetailModel, KpiEntity>();
    }
}
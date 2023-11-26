using AutoMapper;
using IISProject.Api.Common.Enum;


namespace IISProject.Api.DAL.Entities;

public record KpiEntity : IEntity
{
    public required Guid Id { get; set; }
    public required KpiFunction Function { get; set; }
    public bool? Error { get; set; }
    
    public required double Value { get; set; }
    
    public required Guid CreatorId { get; set; }
    public required Guid DeviceId { get; set; }
    public required Guid ParameterId { get; set; }

    public UserEntity? Creator { get; init; }
    public DeviceEntity? Device { get; init; }
    public ParameterEntity? Parameter { get; init; }
    
    public class KpiEntityProfile : Profile
    {
        public KpiEntityProfile()
        {
            CreateMap<KpiEntity, KpiEntity>();
        }
    }
}
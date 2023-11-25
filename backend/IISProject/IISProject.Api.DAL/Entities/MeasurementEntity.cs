using AutoMapper;

namespace IISProject.Api.DAL.Entities;

public record MeasurementEntity : IEntity
{
    public required Guid Id { get; set; }
    public required double Value { get; set; }
    public required DateTime TimeStamp { get; set; }

    public required Guid DeviceId { get; set; }
    public required Guid ParameterId { get; set; }
    
    public DeviceEntity? Device { get; init; }
    public ParameterEntity? Parameter { get; init; }
    
    public class MeasurementEntityProfile : Profile
    {
        public MeasurementEntityProfile()
        {
            CreateMap<MeasurementEntity, MeasurementEntity>();
        }
    }
}
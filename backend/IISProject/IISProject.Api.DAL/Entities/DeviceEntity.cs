﻿using AutoMapper;

namespace IISProject.Api.DAL.Entities;

public record DeviceEntity : IEntity
{
    public required Guid Id { get; set; }
    
    public required string UserId { get; set; }
    public string? UserAlias { get; set; }
    public string? Description { get; set; }

    public Guid? CreatorId { get; set; }
    public Guid? SystemId { get; set; }
    public Guid? DeviceTypeId { get; set; }
    
    public UserEntity? Creator { get; init; }
    public SystemEntity? System { get; init; }
    public DeviceTypeEntity? DeviceType { get; init; }
    public ICollection<MeasurementEntity> Measurements { get; set; } = new List<MeasurementEntity>();
    public ICollection<KpiEntity> Kpis { get; set; } = new List<KpiEntity>();
    
    public class DeviceEntityProfile : Profile
    {
        public DeviceEntityProfile()
        {
            CreateMap<DeviceEntity, DeviceEntity>();
        }
    }
}
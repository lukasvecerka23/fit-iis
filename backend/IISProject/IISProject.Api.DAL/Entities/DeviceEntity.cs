﻿namespace IISProject.Api.DAL.Entities;

public record DeviceEntity : IEntity
{
    public required Guid Id { get; set; } // Id nebo Designation?
    public required string UserAlias { get; set; }
    public string? Description { get; set; }

    public required Guid CreatorId { get; set; }
    public required Guid SystemId { get; set; }
    public required Guid DeviceTypeId { get; set; }
    
    public UserEntity? Creator { get; init; }
    public SystemEntity? System { get; init; }
    public DeviceTypeEntity? DeviceType { get; init; }
    public ICollection<MeasurementEntity> Measurements { get; set; } = new List<MeasurementEntity>();
    public ICollection<KpiEntity> Kpis { get; set; } = new List<KpiEntity>();
    
}
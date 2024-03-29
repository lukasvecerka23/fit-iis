﻿using AutoMapper;

namespace IISProject.Api.DAL.Entities;

public record DeviceTypeEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<DeviceEntity> Devices { get; set; } = new List<DeviceEntity>();
    public ICollection<ParameterEntity> Parameters { get; set; } = new List<ParameterEntity>();
    
    public class DeviceTypeEntityProfile : Profile
    {
        public DeviceTypeEntityProfile()
        {
            CreateMap<DeviceTypeEntity, DeviceTypeEntity>();
        }
    }
}
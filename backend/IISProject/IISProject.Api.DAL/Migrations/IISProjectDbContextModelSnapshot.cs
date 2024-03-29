﻿// <auto-generated />
using System;
using IISProject.Api.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IISProject.Api.DAL.Migrations
{
    [DbContext(typeof(IISProjectDbContext))]
    partial class IISProjectDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IISProject.Api.DAL.Entities.AssignToSystemEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SystemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SystemId");

                    b.HasIndex("UserId");

                    b.ToTable("AssignsToSystems");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.DeviceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("DeviceTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SystemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserAlias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("DeviceTypeId");

                    b.HasIndex("SystemId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.DeviceTypeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DeviceTypes");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.KpiEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Error")
                        .HasColumnType("bit");

                    b.Property<int>("Function")
                        .HasColumnType("int");

                    b.Property<Guid>("ParameterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("DeviceId");

                    b.HasIndex("ParameterId");

                    b.ToTable("Kpis");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.MeasurementEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ParameterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("ParameterId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.ParameterEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeviceTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("LowerLimit")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("UpperLimit")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DeviceTypeId");

                    b.ToTable("Parameters");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.RoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.SystemEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Systems");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.UserInSystemEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SystemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SystemId");

                    b.HasIndex("UserId");

                    b.ToTable("UserInSystems");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.AssignToSystemEntity", b =>
                {
                    b.HasOne("IISProject.Api.DAL.Entities.SystemEntity", "System")
                        .WithMany("AssignsToSystems")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IISProject.Api.DAL.Entities.UserEntity", "User")
                        .WithMany("AssignsToSystems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("System");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.DeviceEntity", b =>
                {
                    b.HasOne("IISProject.Api.DAL.Entities.UserEntity", "Creator")
                        .WithMany("Devices")
                        .HasForeignKey("CreatorId");

                    b.HasOne("IISProject.Api.DAL.Entities.DeviceTypeEntity", "DeviceType")
                        .WithMany("Devices")
                        .HasForeignKey("DeviceTypeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("IISProject.Api.DAL.Entities.SystemEntity", "System")
                        .WithMany("Devices")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Creator");

                    b.Navigation("DeviceType");

                    b.Navigation("System");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.KpiEntity", b =>
                {
                    b.HasOne("IISProject.Api.DAL.Entities.UserEntity", "Creator")
                        .WithMany("Kpis")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IISProject.Api.DAL.Entities.DeviceEntity", "Device")
                        .WithMany("Kpis")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IISProject.Api.DAL.Entities.ParameterEntity", "Parameter")
                        .WithMany("Kpis")
                        .HasForeignKey("ParameterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Device");

                    b.Navigation("Parameter");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.MeasurementEntity", b =>
                {
                    b.HasOne("IISProject.Api.DAL.Entities.DeviceEntity", "Device")
                        .WithMany("Measurements")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IISProject.Api.DAL.Entities.ParameterEntity", "Parameter")
                        .WithMany("Measurements")
                        .HasForeignKey("ParameterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Parameter");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.ParameterEntity", b =>
                {
                    b.HasOne("IISProject.Api.DAL.Entities.DeviceTypeEntity", "DeviceType")
                        .WithMany("Parameters")
                        .HasForeignKey("DeviceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeviceType");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.SystemEntity", b =>
                {
                    b.HasOne("IISProject.Api.DAL.Entities.UserEntity", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.UserEntity", b =>
                {
                    b.HasOne("IISProject.Api.DAL.Entities.RoleEntity", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.UserInSystemEntity", b =>
                {
                    b.HasOne("IISProject.Api.DAL.Entities.SystemEntity", "System")
                        .WithMany("UsersInSystem")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IISProject.Api.DAL.Entities.UserEntity", "User")
                        .WithMany("UserInSystems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("System");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.DeviceEntity", b =>
                {
                    b.Navigation("Kpis");

                    b.Navigation("Measurements");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.DeviceTypeEntity", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("Parameters");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.ParameterEntity", b =>
                {
                    b.Navigation("Kpis");

                    b.Navigation("Measurements");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.RoleEntity", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.SystemEntity", b =>
                {
                    b.Navigation("AssignsToSystems");

                    b.Navigation("Devices");

                    b.Navigation("UsersInSystem");
                });

            modelBuilder.Entity("IISProject.Api.DAL.Entities.UserEntity", b =>
                {
                    b.Navigation("AssignsToSystems");

                    b.Navigation("Devices");

                    b.Navigation("Kpis");

                    b.Navigation("UserInSystems");
                });
#pragma warning restore 612, 618
        }
    }
}

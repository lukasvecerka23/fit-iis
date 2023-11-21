using AutoMapper;
using FluentValidation.AspNetCore;
using IISProject.Api.BL.Installers;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.Common.Extensions;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.Installers;
using IISProject.Api.DAL.Extensions;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureDependencies(builder.Services, builder.Configuration);
ConfigureAutoMapper(builder.Services);
ConfigureCors(builder.Services);
ConfigureControllers(builder.Services);
builder.Services.AddFluentValidationAutoValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

ValidateAutoMapperConfiguration(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Migrate database
using var scope = app.Services.CreateScope();
if (app.Environment.IsDevelopment())
{
    scope.ServiceProvider.GetRequiredService<IDbMigrator>().Migrate();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureCors(IServiceCollection serviceCollection)
{
    serviceCollection.AddCors(options =>
    {
        options.AddDefaultPolicy(o =>
            o.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
    });
}

void ConfigureDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
{
    var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentException("The connection string is missing");
    serviceCollection.AddInstaller<ApiDALInstaller>(connectionString);
    serviceCollection.AddInstaller<ApiBLInstaller>();
}

void ConfigureAutoMapper(IServiceCollection serviceCollection)
{
    serviceCollection.AddAutoMapper(typeof(IEntity), typeof(ApiBLInstaller));
}

void ValidateAutoMapperConfiguration(IServiceProvider serviceProvider)
{
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

void ConfigureControllers(IServiceCollection serviceCollection)
{
    // Configure better error messages for invalid input
    serviceCollection.AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var errorResponse = new BadRequestModel
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });
}
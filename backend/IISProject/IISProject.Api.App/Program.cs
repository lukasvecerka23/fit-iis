using AutoMapper;
using IISProject.Api.BL.Installers;
using IISProject.Api.Common.Extensions;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.Installers;
using IISProject.Api.DAL.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureDependencies(builder.Services, builder.Configuration);
ConfigureAutoMapper(builder.Services);
builder.Services.AddControllers();
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


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

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
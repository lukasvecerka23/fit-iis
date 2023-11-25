using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.Measurement;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Facades;

public class MeasurementFacade: FacadeBase<MeasurementEntity, MeasurementListModel, MeasurementDetailModel, MeasurementCreateUpdateModel>, IMeasurementFacade
{
    public MeasurementFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
    
    public async Task<MeasurementSearchModel> SearchAsync(Guid deviceId, Guid parameterId, int index, int size)
    {
        var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<MeasurementEntity>();
        var measurementQuery = repository.GetAll();
        IncludeNavigationPathDetails(ref measurementQuery);
        
        IEnumerable<MeasurementEntity> filteredMeasurements;
        if (parameterId == Guid.Empty)
        {
            filteredMeasurements = measurementQuery.Where(x => x.DeviceId == deviceId);
        }
        else
        {
            filteredMeasurements = measurementQuery
                .Where(x => x.ParameterId == parameterId && x.DeviceId == deviceId);
        }

        var kpis = filteredMeasurements
            .OrderByDescending(x => x.TimeStamp)
            .Skip(index * size)
            .Take(size).ToList();
        
        var totalCount = filteredMeasurements.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / size);


        var result = new MeasurementSearchModel
        {
            PageIndex = index,
            PageSize = size,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Measurements = Mapper.Map<IEnumerable<MeasurementListModel>>(kpis)
        };
        
        return result;
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(MeasurementEntity.Parameter)}",
    };
}
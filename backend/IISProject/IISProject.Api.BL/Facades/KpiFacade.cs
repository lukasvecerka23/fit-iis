using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.Kpi;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Facades;

public class KpiFacade: FacadeBase<KpiEntity, KpiListModel, KpiDetailModel, KpiCreateUpdateModel>, IKpiFacade
{
    public KpiFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
    
    public async Task<KpiSearchModel> SearchAsync(Guid deviceId, Guid parameterId, int index, int size)
    {
        var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<KpiEntity>();
        var kpiQuery = repository.GetAll();
        IncludeNavigationPathDetails(ref kpiQuery);
        
        IEnumerable<KpiEntity> filteredKpi;
        if (parameterId == Guid.Empty)
        {
            filteredKpi = kpiQuery.Where(x => x.DeviceId == deviceId);
        }
        else
        {
            filteredKpi = kpiQuery
                .Where(x => x.ParameterId == parameterId && x.DeviceId == deviceId);
        }

        var kpis = filteredKpi
            .Skip(index * size)
            .Take(size).ToList();
        
        var totalCount = filteredKpi.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / size);


        var result = new KpiSearchModel
        {
            PageIndex = index,
            PageSize = size,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Kpis = Mapper.Map<IEnumerable<KpiListModel>>(kpis)
        };
        
        return result;
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(KpiEntity.Parameter)}",
    };
}
using AutoMapper;
using IISProject.Api.BL.Enums;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.Parameter;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.BL.Facades;

public class ParameterFacade: FacadeBase<ParameterEntity, ParameterListModel, ParameterDetailModel, ParameterCreateUpdateModel>, IParameterFacade
{
    public ParameterFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
    
    public async Task<IEnumerable<ParameterStatusListModel>> GetParameterStatus(Guid deviceTypeId, Guid deviceId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ParameterEntity> query = uow.GetRepository<ParameterEntity>().GetAll().Where(x => x.DeviceTypeId == deviceTypeId);

        IncludeNavigationPathDetails(ref query);

        List<ParameterEntity> entities = await query.ToListAsync();
        
        IEnumerable<ParameterStatusListModel> models = Mapper.Map<IEnumerable<ParameterStatusListModel>>(entities).ToList();

        var kpiRepository = uow.GetRepository<KpiEntity>();
        
        foreach (var model in models)
        {
            var kpis = await kpiRepository.GetAll().Where(x => x.DeviceId == deviceId && x.ParameterId == model.Id).ToListAsync();
            model.Status = CheckParameterStatus(kpis);
        }


        return models;
    }

    private ParameterStatus CheckParameterStatus(List<KpiEntity> kpis)
    {
        if (kpis.Count == 0)
        {
            return ParameterStatus.Okay;
        }
        
        bool hasError = kpis.Any(x => x.Error == true);
        bool allError = kpis.All(x => x.Error == true);

        if (allError)
        {
            return ParameterStatus.Critical;
        } 
        
        if (hasError)
        {
            return ParameterStatus.Warning;
        }

        return ParameterStatus.Okay;
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(ParameterEntity.Kpis)}"
    };
}
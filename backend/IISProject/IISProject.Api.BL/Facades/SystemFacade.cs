using AutoMapper;
using AutoMapper.QueryableExtensions;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.System;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace IISProject.Api.BL.Facades;

public class SystemFacade: FacadeBase<SystemEntity, SystemListModel, SystemDetailModel, SystemCreateUpdateModel>, ISystemFacade
{
    public SystemFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
    
    public async Task<SystemSearchModel> SearchAsync(string query, int index, int size)
    {
        var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<SystemEntity>();
        var systemQuery = repository.GetAll();
        IncludeNavigationPathDetails(ref systemQuery);
        
        IEnumerable<SystemEntity> filteredSystems;
        if (query.IsNullOrEmpty())
        {
            filteredSystems = systemQuery.OrderBy(x => x.Name);
        }
        else
        {
            filteredSystems = systemQuery
                .Where(x => x.Name.ToLower().Contains(query.ToLower()) ||
                            x.Description.ToLower().Contains(query.ToLower()));
        }

        var systems = filteredSystems
            .Skip(index * size)
            .Take(size).ToList();
        
        var totalCount = filteredSystems.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / size);


        var result = new SystemSearchModel
        {
            PageIndex = index,
            PageSize = size,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Systems = Mapper.Map<IEnumerable<SystemListModel>>(systems)
        };
        
        return result;
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(SystemEntity.Devices)}",
        $"{nameof(SystemEntity.UsersInSystem)}",
        $"{nameof(SystemEntity.Creator)}"
    };
}
using System.Collections;
using System.Reflection;
using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.Repositories;
using IISProject.Api.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.BL.Facades;

public abstract class FacadeBase<TEntity, TListModel, TDetailModel, TCreateUpdateModel> : IFacade<TEntity, TListModel, TDetailModel, TCreateUpdateModel>
where TEntity : class, IEntity
where TListModel : IModel
where TDetailModel : class, IModel
where TCreateUpdateModel : class
{
    protected readonly IUnitOfWorkFactory UnitOfWorkFactory;
    protected readonly IMapper Mapper;

    protected FacadeBase(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
    {
        UnitOfWorkFactory = unitOfWorkFactory;
        Mapper = mapper;
    }

    public virtual List<string> NavigationPathDetails => new();

    public void IncludeNavigationPathDetails(ref IQueryable<TEntity> query)
    {
        foreach (var navigationPathDetail in NavigationPathDetails)
        {
            query = string.IsNullOrWhiteSpace(navigationPathDetail)
                ? query
                : query.Include(navigationPathDetail);
        }
    }

    public virtual async Task<IEnumerable<TListModel>> GetAllAsync()
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<TEntity> query = uow.GetRepository<TEntity>().GetAll();

        IncludeNavigationPathDetails(ref query);

        List<TEntity> entities = await query.ToListAsync();
        
        return Mapper.Map<IEnumerable<TListModel>>(entities);
    }

    public virtual async Task<TDetailModel?> GetByIdAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        
        IQueryable<TEntity> query = uow.GetRepository<TEntity>().GetAll();

        IncludeNavigationPathDetails(ref query);
        
        TEntity? entity = await query.SingleOrDefaultAsync(e => e.Id == id);
        
        return entity == null ? null : Mapper.Map<TDetailModel>(entity);
    }

    public virtual async Task<IdModel> CreateAsync(TCreateUpdateModel model)
    {
        TEntity entity = Mapper.Map<TEntity>(model);
        
        await using var uow = UnitOfWorkFactory.Create();
        IRepository<TEntity> repository = uow.GetRepository<TEntity>();
        
        entity.Id = Guid.NewGuid();
        TEntity insertedEntity = await repository.InsertAsync(entity);
        
        await uow.CommitAsync();
        
        var result = Mapper.Map<IdModel>(insertedEntity);
        
        return result;
    }
    
    public async Task<IdModel> UpdateAsync(TCreateUpdateModel model, Guid id)
    {
        var entity = Mapper.Map<TEntity>(model);

        await using var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<TEntity>();

        entity.Id = id;
        var updatedEntity = await repository.UpdateAsync(entity);

        await uow.CommitAsync();

        var result = Mapper.Map<IdModel>(updatedEntity);
        return result;
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        
        IRepository<TEntity> repository = uow.GetRepository<TEntity>();
        
        await repository.DeleteAsync(id);
        
        await uow.CommitAsync();
    }
}
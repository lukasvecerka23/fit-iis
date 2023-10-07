using AutoMapper;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.UnitOfWork;

public sealed class UnitOfWork: IUnitOfWork
{
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;
    
    public UnitOfWork(DbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }
    
    public IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class, IEntity
    {
        return new Repository<TEntity>(_dbContext, _mapper);
    }
    
    public async Task CommitAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
    
    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.UnitOfWork;

public class UnitOfWorkFactory: IUnitOfWorkFactory
{
    private readonly IDbContextFactory<IISProjectDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public UnitOfWorkFactory(IDbContextFactory<IISProjectDbContext> dbContextFactory, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public IUnitOfWork Create()
    {
        return new UnitOfWork(_dbContextFactory.CreateDbContext(), _mapper);
    }
}
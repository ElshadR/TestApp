using Domain.Base;
using Domain.Interfaces;
using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFDbContext _dbContext;

        public UnitOfWork(EFDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IAsyncRepository<T> AsyncRepository<T>() where T : BaseEntity
        {
            return new RepositoryBase<T>(_dbContext);
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfRegistration.Domain.Models;
using WpfRegistration.Domain.Services;

namespace WpfRegistration.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObjects
    {
        #region Variable declarations

        private readonly DbContextFactory _dbContextFactory;

        #endregion Variable declarations

        #region Constructor

        public GenericDataService(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        #endregion Constructor

        #region Async Methods

        public async Task<T> Create(T entity)
        {
            using (ApplicationDbContext context = _dbContextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResults = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResults.Entity;
            };
        }

        public async Task<bool> Delete(Guid Id)
        {
            using (ApplicationDbContext context = _dbContextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == Id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            };
        }

        public async Task<T> Get(Guid Id)
        {
            using (ApplicationDbContext context = _dbContextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == Id);
                return entity;
            };
        }

        public async Task<List<T>> Getall()
        {
            using (ApplicationDbContext context = _dbContextFactory.CreateDbContext())
            {
                List<T> entities = await context.Set<T>().Select(x => x).ToListAsync();
                return entities;
            };
        }

        public async Task<T> Update(Guid Id, T entity)
        {
            using (ApplicationDbContext context = _dbContextFactory.CreateDbContext())
            {
                entity.Id = Id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            };
        }

        #endregion Async Methods
    }
}
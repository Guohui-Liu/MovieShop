using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    //IEnumerable is for IEnumerable objects
    //IQAueryable is for entity framework Dbsets,convert this LinQ Query to SQLquery
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly MovieShopDbContext _dbContext;
        //constructor injection
        public EfRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T GetById(int id) //virtual: change the implementation,can override the implementation
        {
            var entity = _dbContext.Set<T>().Find(id); //EfRepository doesn't belong to any table
            return entity;
        }

        public virtual IEnumerable<T> ListAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public virtual IEnumerable<T> List(Expression<Func<T, bool>> filter)
        {
            return _dbContext.Set<T>().Where(filter).ToList();
        }

        public virtual int GetCount(Expression<Func<T, bool>> filter)
        {
            return _dbContext.Set<T>().Where(filter).Count();
        }

        public virtual bool GetExists(Expression<Func<T, bool>> filter)
        {
            return _dbContext.Set<T>().Where(filter).Any();
        }

        public T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }
    }

}
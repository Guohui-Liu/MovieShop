using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.RepositoryInterfaces
{
    //base repository for common crud operation
   public interface IAsyncRepository<T> where T: class 
    {
        T GetById(int id); //select sth by id
        IEnumerable<T> ListAll(); //get all the information about an entity
        IEnumerable<T> List(Expression<Func<T, bool>> filter); //add a condition for list
        int GetCount(Expression<Func<T, bool>> filter); //example: get the count of movies
        bool GetExists(Expression<Func<T, bool>> filter); // figure out the movie is exist or not
        T Add(T entity);     
        T Update(T entity);
        void Delete(T entity);

        //public virtual async Task<T> GetById(int id)
        //{
        //    var entity = await _dbContext.Set<T>().FindAsync(id);
        //    return entity;
        //}

        //public virtual async Task<IEnumerable<T>> ListAll()
        //{
        //    return await _dbContext.Set<T>().ToListAsync();
        //}

        //public virtual async Task<IEnumerable<T>> List(Expression<Func<T, bool>> filter)
        //{
        //    return await _dbContext.Set<T>().Where(filter).ToListAsync();
        //}

        //public virtual async Task<int> GetCount(Expression<Func<T, bool>> filter)
        //{
        //    return await _dbContext.Set<T>().Where(filter).CountAsync();
        //}

        //public virtual async Task<bool> GetExists(Expression<Func<T, bool>> filter)
        //{
        //    return await _dbContext.Set<T>().Where(filter).AnyAsync();
        //}

        //public virtual async Task<T> Add(T entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual async Task<T> Update(T entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual Task Delete(T entity)
        //{
        //    throw new NotImplementedException();
        //}

    }
}

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
        //T GetById(int id); //select sth by id
        //IEnumerable<T> ListAll(); //get all the information about an entity
        //IEnumerable<T> List(Expression<Func<T, bool>> filter); //add a condition for list
        //int GetCount(Expression<Func<T, bool>> filter); //example: get the count of movies
        //bool GetExists(Expression<Func<T, bool>> filter); // figure out the movie is exist or not
        //T Add(T entity);     
        //T Update(T entity);
        //void Delete(T entity);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> ListAll();
        Task<IEnumerable<T>> List(Expression<Func<T, bool>> filter);
        Task<int> GetCount(Expression<Func<T, bool>> filter);
        Task<bool> GetExists(Expression<Func<T, bool>> filter);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);



    }
}

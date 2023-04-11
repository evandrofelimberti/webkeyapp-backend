using System.Collections.Generic;
using System.Linq.Expressions;
using System;
namespace WebAppKey.Services.Interfaces;

public interface IRepositoryBase<T> where T: class
{
    Task<T> GetById(int Id);
    Task<IEnumerable<T>> GetAll();
    Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
   // Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate, Func<T, T> include);    
    
    Task Add(T entity);
    Task Update(T entity);
    void Delete(T entity);
    void DeleteById(int Id);


    /*     
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);                
    */

}
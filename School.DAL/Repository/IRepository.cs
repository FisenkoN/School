using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using School.DAL.Entities;

namespace School.DAL.Repository
{
    public interface IRepository<T>:IDisposable where T : EntityBase, new()
    {
        int Add(T entity);
        
        int Add(IList<T> entities);
        
        int Update (T entity);
        
        int Update(IList<T> entities);
        
        int Delete(int id);
        
        int Delete(T entity);
        
        T GetOne(int? id);
        
        IQueryable<T> GetSome(Expression<Func<T, bool>> where);

        List<T> GetAll();
        
        List<T> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy,
            bool ascending);
        
        List<T> ExecuteQuery(string sql);
        
        List<T> ExecuteQuery(string sql, object[] sqlParametersObjects);
    }
}
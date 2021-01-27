using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq; 

namespace HC.DataAccess.Data.Repository.IRepository
{
    public interface IRepository <T>  where T : class
    {
        T Get(int id);
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includedProperties = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
            ); 
        
        //IncludedProperties must be separated by comma.
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includedProperties = null  
            );

        void Remove(int id);
        void Remove(T entity);

        void Add(T entity); 

        
    }
}

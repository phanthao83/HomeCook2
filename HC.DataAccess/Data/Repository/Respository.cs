using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using HC.DataAccess.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HC.DataAccess.Data.Repository
{
    public class Respository<T> : IRepository<T> where T: class
    {
        protected readonly DbContext Context;
        internal  DbSet<T> dbSet;

        public Respository(DbContext _context) 
        {
            Context = _context;
            dbSet = Context.Set<T>(); 
        }

        public void Add(T entity)
        {
            dbSet.Add(entity); 
        }

        public T Get(int id)
        {
            return dbSet.Find(id); 
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includedProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null) 
            {
                query = query.Where(filter); 
            }
            if (includedProperties != null)
            {
                foreach (string includeProperty in includedProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim()); 
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList(); 
            }

            return query.ToList(); 
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includedProperties = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includedProperties != null)
            {
                foreach (string includeProperty in includedProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault(); 
            }
            return query.FirstOrDefault(); 
        }

        public void Remove(int id)
        {
            T deletedEntity = this.Get(id);
            Remove(deletedEntity); 
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity); 
        }
    }
}

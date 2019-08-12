using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class Repositorybase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext DBContext { get; set; }

        public Repositorybase(RepositoryContext dBContext)
        {
            this.DBContext = dBContext;
        }

        public void Create(T entity)
        {
            this.DBContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            this.DBContext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> FindAll()
        {
            return this.DBContext.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.DBContext.Set<T>().Where(expression);
        }

        public void Save()
        {
            this.DBContext.SaveChanges();
        }

        public void Update(T entity)
        {
            this.DBContext.Set<T>().Update(entity);
        }
    }
}

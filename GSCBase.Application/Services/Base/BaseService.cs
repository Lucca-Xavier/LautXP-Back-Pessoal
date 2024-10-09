using GSCBase.Application.IServices.Base;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Infrastructure.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GSCBase.Application.Services.Base
{
    public abstract class BaseService<T> : IDisposable, IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> repository;
        public BaseService(IBaseRepository<T> _repository)
        {
            repository = _repository;
        }


        public virtual void Save(T obj)
        {
            repository.Save(obj);
        }
        public virtual void Save(List<T> obj)
        {
            repository.Save(obj);
        }
        public virtual T FindById(int id)
        {
            return repository.FindById(id);
        }
        public virtual T FindById(int id, params Expression<Func<T, object>>[] includes)
        {
            return repository.FindById(id,includes);
        }
        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return repository.Find(predicate);
        }
        public virtual T Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return this.Find(predicate, includes);
        }
        public virtual IQueryable<T> Get()
        {
            return repository.Get();
        }
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return repository.Get(predicate);
        }
        public virtual IQueryable<T> Get(params Expression<Func<T, object>>[] includes)
        {
            return this.Get(includes);
        }
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return this.Get(predicate, includes);
        }
        public virtual bool Delete(T obj, ApplicationUser user)
        {
            return repository.Delete(obj, user);
        }
        public virtual bool Delete(int id, ApplicationUser user)
        {
            return repository.Delete(id, user);
        }
        public virtual void Dispose()
        {
            repository.Dispose();
        }
    }
}

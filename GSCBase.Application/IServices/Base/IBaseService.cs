using GSCBase.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GSCBase.Application.IServices.Base
{
    public interface IBaseService<T> where T : class
    {
        void Save(T obj);
        void Save(List<T> obj);
        T FindById(int id);
        T FindById(int id, params Expression<Func<T, object>>[] includes);
        T Find(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Method that centralize all repository get item passing parameters
        /// How to use in the end of document
        /// </summary>
        /// <param name="predicate">Lambda expression for condition</param>
        /// <param name="includes">Include list necessary to user object</param>
        /// <returns>T entity</returns>
        T Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        IQueryable<T> Get();
        IQueryable<T> Get(params Expression<Func<T, object>>[] includes);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Method that centralize all repository list passing parameters
        /// How to use in the end of document
        /// </summary>
        /// <param name="predicate">Lambda expression for condition</param>
        /// <param name="includes">Include list necessary to user object</param>
        /// <returns>IQueryable list of entity</returns>
        IQueryable<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        bool Delete(T obj, ApplicationUser user);
        bool Delete(int id, ApplicationUser user);
        void Dispose();
    }
}

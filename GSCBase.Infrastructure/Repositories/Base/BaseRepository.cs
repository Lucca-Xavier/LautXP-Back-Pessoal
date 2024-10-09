using Microsoft.EntityFrameworkCore;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GSCBase.Infrastructure.Repositories.Base
{
    public abstract class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        private readonly Context _context;
        private IQueryable<T> _query;
        public BaseRepository(Context context)
        {
            _context = context;
            _query = _context.Set<T>().AsNoTracking()
                                      .AsQueryable();
        }

        public virtual void Save(T obj)
        {
            try
            {
                string pk = this.GetKeyName();
                var pkValue = obj.GetType().GetProperty(pk).GetValue(obj, null);

                if ((pkValue is int && Convert.ToInt32(pkValue) > 0) ||
                    (pkValue is string && !string.IsNullOrEmpty(pkValue.ToString())))
                {
                    _context.Set<T>().Update(obj);
                }
                else
                {
                    _context.Set<T>().Add(obj);
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }
        public virtual void Save(List<T> list)
        {
            string pk = this.GetKeyName();

            List<T> saveList = list.Where(obj =>
            {
                var pkValue = obj.GetType().GetProperty(pk).GetValue(obj, null);
                return !((pkValue is int && Convert.ToInt32(pkValue) > 0) ||
                    (pkValue is string && !string.IsNullOrEmpty(pkValue.ToString())));
            }).ToList();

            List<T> updateList = list.Where(obj =>
            {
                var pkValue = obj.GetType().GetProperty(pk).GetValue(obj, null);
                return ((pkValue is int && Convert.ToInt32(pkValue) > 0) ||
                    (pkValue is string && !string.IsNullOrEmpty(pkValue.ToString())));
            }).ToList();

            _context.Set<T>().UpdateRange(updateList);
            _context.Set<T>().AddRange(saveList);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }
        public virtual T FindById(int id)
        {
            return this.FindById(id, null);
        }
        public virtual T FindById(int id, params Expression<Func<T, object>>[] includes)
        {
            string pk = this.GetKeyName();

            Expression<Func<T, bool>> predicate = this.GetPredicate(pk, id);
            return this.Find(predicate, includes);
        }
        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return this.Find(predicate, null);
        }
        public virtual T Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            if (includes != null)
            {
                _query = includes.Aggregate(_query,
                          (current, include) => current.Include(include));
            }

            if (predicate != null)
            {
                return _query.FirstOrDefault(predicate);
            }

            return _query.FirstOrDefault();
        }
        public virtual IQueryable<T> Get()
        {
            return this.Get(null, null);
        }
        public virtual IQueryable<T> Get(params Expression<Func<T, object>>[] includes)
        {
            return this.Get(null, includes);
        }
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return this.Get(predicate, null);
        }
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            if (includes != null)
            {
                _query = includes.Aggregate(_query,
                          (current, include) => current.Include(include));
            }

            if (predicate != null)
            {
                _query = _query.Where(predicate);
            }

            return _query;
        }
        public virtual bool Delete(T obj, ApplicationUser user)
        {
            bool result = false;
            if (obj != null)
            {
                if (obj.GetType().GetProperty("IsActive") != null || obj.GetType().GetProperty("DtDeleted") != null)
                {
                    BaseModel _obj = obj as BaseModel;
                    _obj.SetUserDeleted(user);
                    try
                    {
                        _context.Entry(_obj).State = EntityState.Modified;
                        _context.SaveChanges();
                        result = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        result = false;
                    }
                }
                else
                {
                    try
                    {
                        _context.Remove(obj);
                        _context.SaveChanges();
                        result = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        result = false;
                    }
                }
            }
            return result;
        }
        public virtual bool Delete(int id, ApplicationUser user)
        {
            return this.Delete(this.FindById(id), user);
        }
        private string GetKeyName()
        {
            var keyName = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();

            return keyName;
        }
        private Expression<Func<T,bool>> GetPredicate(string key,int id)
        {
            ParameterExpression argParam = Expression.Parameter(typeof(T), "t");
            Expression keyProperty = Expression.Property(argParam, key);
            //Expression namespaceProperty = Expression.Property(argParam, "Namespace");

            var val = Expression.Constant(id);
            //var val1 = Expression.Constant("Modules");
            //var val2 = Expression.Constant("Namespace");

            Expression exp = Expression.Equal(keyProperty, val);
            //Expression e1 = Expression.Equal(nameProperty, val1);
            //Expression e2 = Expression.Equal(namespaceProperty, val2);
            //var andExp = Expression.AndAlso(e1, e2);

            var lambda = Expression.Lambda<Func<T, bool>>(exp, argParam);
            return lambda;
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(true);
        }
    }
}
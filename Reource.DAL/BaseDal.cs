using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Resource.DAL
{
    public class BaseDal<T> where T : class, new()
    {
        /// <summary>
        /// EF上下文对象
        /// </summary>
        private DbContext dc = DbContextFactory.Create();

        public void Add(T t)
        {
            dc.Set<T>().Add(t);
        }
        public void Update(T t)
        {
            dc.Set<T>().AddOrUpdate(t);
        }
        public void Delete(T t)
        {
            dc.Set<T>().Remove(t);
        }
        public bool SaveChanges()
        {
            return dc.SaveChanges() > 0;
        }
        public IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda)
        {
            return dc.Set<T>().Where(whereLambda);
        }
        public IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc,
            Expression<Func<T, bool>> WhereLambda,
            Expression<Func<T, type>> OrderLambda)
        {
            if (isAsc)
            {
                return dc.Set<T>()
                                    .Where(WhereLambda)
                                    .OrderBy(OrderLambda)
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize);
            }
            else
            {
                return dc.Set<T>()
                                    .Where(WhereLambda)
                                    .OrderByDescending(OrderLambda)
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize);
            }
        }

    }
}

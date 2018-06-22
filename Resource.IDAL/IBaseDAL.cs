using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Resource.IDAL
{
    public partial interface IBaseDal<T> where T : class, new()
    {
        void Add(T t);
        void Update(T t);
        void Delete(T t);
        bool SaveChanges();
        IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc, Expression<Func<T, bool>> whereLambda, Expression<Func<T, type>> orderLambda);
    }
}
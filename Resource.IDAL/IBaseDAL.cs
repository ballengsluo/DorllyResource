using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Data;

namespace Resource.IDAL
{
    public partial interface IBaseDAL<T> where T : class, new()
    {
        void Add(T t);
        void Update(T t);
        int Update(string sqlText, params SqlParameter[] parameter);
        void Delete(T t);
        bool SaveChanges();
        IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda);
        DataSet ExecuteSql(string sqlText, CommandType cmdType, params SqlParameter[] parameters);
        IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc, Expression<Func<T, bool>> whereLambda, Expression<Func<T, type>> orderLambda);
    }
}
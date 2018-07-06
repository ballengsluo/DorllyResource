using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Project.Common.DBUtility;

namespace Resource.DAL
{
    public class BaseDAL<T> where T : class, new()
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
        public int Update(string sqlText, params SqlParameter[] parameter)
        {
            if (parameter != null) return dc.Database.ExecuteSqlCommand(sqlText, parameter);
            else return dc.Database.ExecuteSqlCommand(sqlText);

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
        public DataSet ExecuteSql(string sqlText, CommandType cmdType, params SqlParameter[] parameters)
        {
            return SQLFactory.Create().GetDataSet(sqlText, cmdType, parameters);
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

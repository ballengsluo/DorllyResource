using Resource.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Resource.BLL
{
    public abstract class BaseService<T> where T : class, new()
    {
        public BaseService() { SetDal(); }
        public IBaseDAL<T> Dal;
        public abstract void SetDal();
        public bool Add(T t)
        {
            Dal.Add(t);
            return Dal.SaveChanges();
        }
        public bool Update(T t)
        {
            Dal.Update(t);
            return Dal.SaveChanges();
        }
        public bool Delete(T t)
        {
            Dal.Delete(t);
            return Dal.SaveChanges();
        }
        public IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda)
        {
            return Dal.GetModels(whereLambda);
        }

        public IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc
            , Expression<Func<T, bool>> WhereLambda
            , Expression<Func<T, type>> OrderLambda)
        {
            return Dal.GetModelsByPage(pageSize, pageIndex, isAsc, WhereLambda, OrderLambda);
        }

        public int Update(string sqlText, params SqlParameter[] parameter)
        {
            return Dal.Update(sqlText, parameter);
        }
        public virtual DataSet ExecuteSql(string sqlText, CommandType cmdType, params SqlParameter[] parameters)
        {
            return Dal.ExecuteSql(sqlText, cmdType, parameters);
        }
    }
}

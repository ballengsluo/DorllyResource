using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.DBUtility
{
    public  abstract class DBHelper
    {
        string ConnectionStr = string.Empty;
        public bool _IsTrans = false;
        public DBHelper(string connectiontStr)
        {
            ConnectionStr = connectiontStr;
        }
        protected abstract DbConnection DbConnectionObject { get; }
        protected abstract DbCommand DbCommandObject { get; }
        protected abstract DbDataAdapter DbDataAdapterObject { get; }
        protected DbTransaction DbTransObject;

        #region 核心配置

        public DbConnection CurrentConnection
        {
            get
            {
                return DbConnectionObject;
            }
        }
        void OpenConnect()
        {
            if (DbConnectionObject.State != ConnectionState.Open)
            {
                DbConnectionObject.ConnectionString = ConnectionStr;
                DbConnectionObject.Open();
            }
        }
        /// <summary>
        /// 关闭连接,如果没有开始事务或连接打开时才关闭
        /// </summary>
        void CloseConnect()
        {
            if (!_IsTrans)
            {
                if (DbConnectionObject.State == ConnectionState.Open)
                {
                    DbConnectionObject.Close();
                    DbConnectionObject.Dispose();
                }
            }
        }
        void SetCommandAndOpenConnect(string sqlText, CommandType cmdType, params DbParameter[] param)
        {
            //按说赋值Connection,CommandType,是不用多次赋值的
            DbCommandObject.CommandType = cmdType;
            DbCommandObject.Connection = DbConnectionObject;
            DbCommandObject.Parameters.Clear();
            if (param != null)
            {
                DbCommandObject.Parameters.AddRange(param);
            }
            DbCommandObject.CommandText = sqlText;
            OpenConnect();
        }

        #endregion

        /// <summary>
        /// 执行一条指定命令类型(SQL语句或存储过程等)的SQL语句,返回所影响行数
        /// </summary>
        public int ExecNonQuery(string sqlText, CommandType cmdType, params DbParameter[] param)
        {
            try
            {
                SetCommandAndOpenConnect(sqlText, cmdType, param);
                return DbCommandObject.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnect();
            }
        }
     
        /// <summary>
        /// 获得首行首列
        /// </summary>
        public object GetScalar(string sqlText, CommandType cmdType, params DbParameter[] param)
        {
            try
            {
                SetCommandAndOpenConnect(sqlText, cmdType, param);
                return DbCommandObject.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnect();
            }
        }
        /// <summary>
        /// 执行一条SQL语句返回DataSet对象
        /// </summary>
        public DataSet GetDataSet(string sqlText, CommandType cmdType, params DbParameter[] param)
        {
            try
            {
                SetCommandAndOpenConnect(sqlText, cmdType, param);
                DbDataAdapterObject.SelectCommand = DbCommandObject;
                DataSet ds = new DataSet();
                DbDataAdapterObject.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnect();
            }
        }

        /// <summary>
        /// 获得DataReader对象
        /// </summary>
        public DbDataReader GetDataReader(string sqlText, CommandType cmdType, params DbParameter[] param)
        {
            try
            {
                SetCommandAndOpenConnect(sqlText, cmdType, param);
                CommandBehavior cmdBehavior = CommandBehavior.CloseConnection;
                if (_IsTrans)
                {
                    cmdBehavior = CommandBehavior.Default;
                }
                DbDataReader dbReader = DbCommandObject.ExecuteReader(cmdBehavior);
                return dbReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //DataReader用dbReader对象来关闭
                //CloseConnect();
            }
        }

        #region 事务操作

        /// <summary>
        /// 开始执行事务
        /// </summary>
        public void TransStart()
        {
            OpenConnect();
            DbTransObject = DbConnectionObject.BeginTransaction();
            DbCommandObject.Transaction = DbTransObject;
            _IsTrans = true;
        }
        /// <summary>
        /// 事务提交
        /// </summary>
        public void TransCommit()
        {
            _IsTrans = false;
            DbTransObject.Commit();
            CloseConnect();
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        public void TransRollback()
        {
            _IsTrans = false;
            DbTransObject.Rollback();
            CloseConnect();
        }
        #endregion


    }
}

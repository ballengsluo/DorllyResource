using System;
using System.Data;

namespace Project.Business
{
    public sealed class Business_T_Resource : Project.Business.AbstractPmBusiness
    {
        private Project.Entity.Entity_T_Resource _entity = new Project.Entity.Entity_T_Resource();
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public Business_T_Resource() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public Business_T_Resource(Project.Entity.Entity_T_Resource entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类关联
        /// </summary>
        public Project.Entity.Entity_T_Resource Entity
        {
            get { return _entity as Project.Entity.Entity_T_Resource; }
        }

        /// <summary>
        /// load 方法 pid主键
        /// </summary>
        public int load(string ID)
        {
            int rows = 1;
            DataTable dt = objdata.PopulateDataSet("select * from T_Resource where ID='" + ID + "' and Status=2 and Enable=1").Tables[0];
            DataRow[] dr = dt.Select();
            if (dr.Length > 0)
            {
                _entity.Name = dt.Rows[0]["Name"].ToString();
                _entity.Location = dt.Rows[0]["Location"].ToString();
                _entity.Content = dt.Rows[0]["Content"].ToString();
                _entity.ResourceKindID = dt.Rows[0]["ResourceKindID"].ToString();
                #region 房间 会议室
                _entity.Area = ParseDecimalForString(dt.Rows[0]["Area"].ToString());
                _entity.RentArea = ParseDecimalForString(dt.Rows[0]["RentArea"].ToString());
                _entity.StartArea = ParseDecimalForString(dt.Rows[0]["StartArea"].ToString());
                _entity.RangeArea = dt.Rows[0]["RangeArea"].ToString();
                #endregion
                #region 工位
                _entity.Number = ParseIntForString(dt.Rows[0]["Number"].ToString());
                _entity.RentNum = ParseIntForString(dt.Rows[0]["RentNum"].ToString());
                _entity.StartNum = ParseIntForString(dt.Rows[0]["StartNum"].ToString());
                _entity.RangeNum = dt.Rows[0]["RangeNum"].ToString();
                #endregion
                #region 广告位
                _entity.Size = dt.Rows[0]["Size"].ToString();
                _entity.RentSize = dt.Rows[0]["RentSize"].ToString();
                _entity.StartSize = dt.Rows[0]["StartSize"].ToString();
                _entity.RangeSize = dt.Rows[0]["RangeSize"].ToString();
                #endregion
            }
            else
            {
                rows = 0;
            }
            return rows;
        }
    }
}

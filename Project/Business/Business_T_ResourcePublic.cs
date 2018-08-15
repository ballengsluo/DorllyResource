using System;
using System.Data;

namespace Project.Business
{
    public sealed class Business_T_ResourcePublic : Project.Business.AbstractPmBusiness
    {
        private Project.Entity.Entity_T_ResourcePublic _entity = new Project.Entity.Entity_T_ResourcePublic();
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public Business_T_ResourcePublic() { }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="parkID">园区ID</param>
        /// <param name="kindID">种类ID</param>
        /// <param name="searchName">资源名称</param>
        /// <param name="orderstr">排序字段</param>
        /// <param name="startRow">第几页</param>
        /// <param name="pageSize">每页个数</param>
        /// <returns></returns>
        public System.Collections.ICollection GetT_ResourcePublicListQuery(string parkID, string kindID, string searchName, string releaseData, string orderstr, int startRow, int pageSize)
        {
            return GetListHelper(parkID, kindID, searchName, releaseData, orderstr, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        public System.Collections.ICollection GetListHelper(string parkID, string kindID, string searchName, string releaseData, string orderstr, int startRow, int pageSize)
        {
            string wherestr = " and a.Status='4' and a.ZYPTEnable=1";
            if (parkID != string.Empty)
            {
                string parkIDS = "";
                foreach (string str in parkID.Split(','))
                {
                    if (str == "") continue;
                    parkIDS += "'" + str + "'" + ",";
                }
                parkIDS = parkIDS.Substring(0, parkIDS.Length - 1);
                wherestr = wherestr + " and b.ParkID in (" + parkIDS + ")";
            }
            if (kindID != string.Empty)
            {
                wherestr = wherestr + " and b.ResourceKindID='" + kindID + "'";
            }
            if (searchName != string.Empty)
            {
                wherestr = wherestr + " and b.Name like '%" + searchName + "%'";
            }
            if (releaseData != string.Empty)
            {
                wherestr = wherestr + " and convert(nvarchar(10),a.BeginTime,121)<='" + releaseData + "'";
            }
            if (releaseData != string.Empty)
            {
                wherestr = wherestr + " and convert(nvarchar(10),a.EndTime,121)>='" + releaseData + "'";
            }
            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("T_ResourcePublic a left join T_Resource b on a.ResourceID=b.ID",
                    "b.ID,b.Name,b.OrderPrice,b.ParkID,b.ParentID,b.ResourceKindID",
                    wherestr, startRow, pageSize, orderstr));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("T_ResourcePublic a left join T_Resource b on a.ResourceID=b.ID",
                    "b.ID,b.Name,b.OrderPrice,b.ParkID,b.ParentID,b.ResourceKindID",
                    wherestr, START_ROW_INIT, START_ROW_INIT, orderstr));
            }
            return entitys;
        }

        /// <summary>
        /// Query 方法 dt查询结果
        /// </summary>
        public System.Collections.IList Query(System.Data.DataTable dt)
        {
            System.Collections.IList result = new System.Collections.ArrayList();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Project.Entity.Entity_T_ResourcePublic entity = new Project.Entity.Entity_T_ResourcePublic();
                entity.ResourceID = dr["ID"].ToString();
                entity.ResourceName = dr["Name"].ToString();
                entity.OrderPrice = ParseDecimalForString(dr["OrderPrice"].ToString());
                entity.ParkID = dr["ParkID"].ToString();
                entity.ParentID = dr["ParentID"].ToString();
                entity.ResourceKindID = dr["ResourceKindID"].ToString();
                string CitySql = "select Name from T_City where ID=(select top 1 b.CityID from T_Park a left join T_Region b on a.RegionID=b.ID where a.ID='" + entity.ParkID + "')";
                entity.CityName = objdata.PopulateDataSet(CitySql).Tables[0].Rows[0]["Name"].ToString();

                if (entity.ResourceKindID == "3" || entity.ResourceKindID == "4")//会议室 广告
                { }
                else if (entity.ResourceKindID == "1")//办公室资源
                {
                    string Sql = " select a.Name as BuildingName,b.Name as FloorName from T_Building a left join (select Name,BuildingID from T_Floor where ID='" + entity.ParentID + "') b on a.ID=b.BuildingID where a.ID in(select BuildingID from T_Floor where ID='" + entity.ParentID + "')";
                    DataTable SqlDT = objdata.PopulateDataSet(Sql).Tables[0];

                    entity.BuildingName = SqlDT.Rows[0]["BuildingName"].ToString();
                    entity.FloorName = SqlDT.Rows[0]["FloorName"].ToString();
                }
                else if (entity.ResourceKindID == "2")//工位
                {
                    string parentSql = "select Name,ParentID from T_Resource where ID='" + entity.ParentID + "'";
                    DataTable parentDt = objdata.PopulateDataSet(parentSql).Tables[0];
                    entity.ParentID = parentDt.Rows[0]["ParentID"].ToString();
                    entity.ParentName = parentDt.Rows[0]["Name"].ToString();

                    string Sql = " select a.Name as BuildingName,b.Name as FloorName from T_Building a left join (select Name,BuildingID from T_Floor where ID='" + entity.ParentID + "') b on a.ID=b.BuildingID where a.ID in(select BuildingID from T_Floor where ID='" + entity.ParentID + "')";
                    DataTable SqlDT = objdata.PopulateDataSet(Sql).Tables[0];

                    entity.BuildingName = SqlDT.Rows[0]["BuildingName"].ToString();
                    entity.FloorName = SqlDT.Rows[0]["FloorName"].ToString();

                    entity.ResourceName = "工位";
                }
                else
                { }

                string coverImgSql = "select ImgUrl from T_ResourceImg where ResourceID='" + entity.ResourceID + "' and IsCover=1";
                DataTable coverImgDt = objdata.PopulateDataSet(coverImgSql).Tables[0];
                DataRow[] coverImgRows = coverImgDt.Select();
                if (coverImgRows.Length > 0)
                {
                    entity.CoverImg = coverImgDt.Rows[0]["ImgUrl"].ToString();
                }
                else
                {
                    coverImgSql = "select ImgUrl from T_ResourceImg where ResourceID='" + entity.ResourceID + "' order by ID";
                    coverImgDt = objdata.PopulateDataSet(coverImgSql).Tables[0];
                    coverImgRows = coverImgDt.Select();
                    if (coverImgRows.Length > 0)
                    {
                        entity.CoverImg = coverImgDt.Rows[0]["ImgUrl"].ToString();
                    }
                    else
                    { }
                }

                result.Add(entity);
            }
            return result;
        }
    }
}

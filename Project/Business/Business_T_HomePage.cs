using System;
using System.Data;

namespace Project.Business
{
	 public sealed class Business_T_HomePage : Project.Business.AbstractPmBusiness
	 {
		 private Project.Entity.Entity_T_HomePage _entity=new Project.Entity.Entity_T_HomePage();
         public string orderstr = "OrderNum";
		 Data objdata = new Data();

		 /// <summary>
		 /// 缺省构造函数
		 /// </summary>
		 public Business_T_HomePage() { }

		 /// <summary>
		 /// 按条件查询，不支持分页
		 /// </summary>
         public System.Collections.ICollection GetT_HomePageListQuery(string CityID, string Position, string Status)
		 {
             return GetListHelper(CityID, Position, Status, START_ROW_INIT, START_ROW_INIT);
		 }

		 /// <summary>
		 /// 按条件查询，返回符合条件的集合
		 /// </summary>
		 public System.Collections.ICollection GetListHelper(string CityID, string Position, string Status,int startRow, int pageSize)
		 {
			 string wherestr = "";
             if (CityID != string.Empty)
             {
                 wherestr = wherestr + " and CityID='" + CityID + "'";
             }
             if (Position != string.Empty)
             {
                 wherestr = wherestr + " and Position=" + Position + "";
             }
             if (Status != string.Empty)
             {
                 wherestr = wherestr + " and Status='" + Status + "'";
             }
			 System.Collections.IList entitys = null;
			 if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
			 {
				 entitys = Query(objdata.ExecSelect("T_HomePage", wherestr, startRow, pageSize, orderstr));
			 }
			 else
			 {
				 entitys = Query(objdata.ExecSelect("T_HomePage", wherestr, START_ROW_INIT, START_ROW_INIT, orderstr));
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
				 Project.Entity.Entity_T_HomePage entity = new Project.Entity.Entity_T_HomePage();
				 entity.ID=ParseIntForString(dr["ID"].ToString());
				 entity.CityID=dr["CityID"].ToString();
				 entity.Position=ParseIntForString(dr["Position"].ToString());
				 entity.Title=dr["Title"].ToString();
				 entity.SubTitle=dr["SubTitle"].ToString();
				 entity.OrderNum=ParseIntForString(dr["OrderNum"].ToString());
				 entity.ImgUrl=dr["ImgUrl"].ToString();
				 entity.LinkUrl=dr["LinkUrl"].ToString();
				 entity.Status=ParseIntForString(dr["Status"].ToString());
				 result.Add(entity);
			 }
			 return result;
		 }
	}
}

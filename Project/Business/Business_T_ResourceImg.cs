using System;
using System.Data;

namespace Project.Business
{
    public sealed class Business_T_ResourceImg : Project.Business.AbstractPmBusiness
    {
        private Project.Entity.Entity_T_ResourceImg _entity = new Project.Entity.Entity_T_ResourceImg();
         public string orderstr = "ID";
		 Data objdata = new Data();

		 /// <summary>
		 /// 缺省构造函数
		 /// </summary>
         public Business_T_ResourceImg() { }

         /// <summary>
         /// 按条件查询，不支持分页
         /// </summary>
         public System.Collections.ICollection GetT_ResourceImgListQuery(string ResourceID, string IsCover)
         {
             return GetListHelper(ResourceID, IsCover,START_ROW_INIT, START_ROW_INIT);
         }

         /// <summary>
         /// 按条件查询，返回符合条件的集合
         /// </summary>
         public System.Collections.ICollection GetListHelper(string ResourceID, string IsCover, int startRow, int pageSize)
         {
             string wherestr = "";
             if (ResourceID != string.Empty)
             {
                 wherestr = wherestr + " and ResourceID='" + ResourceID + "'";
             }
             if (IsCover != string.Empty)
             {
                 wherestr = wherestr + " and IsCover="+IsCover+"";
             }
             System.Collections.IList entitys = null;
             if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
             {
                 entitys = Query(objdata.ExecSelect("T_ResourceImg", wherestr, startRow, pageSize, orderstr));
             }
             else
             {
                 entitys = Query(objdata.ExecSelect("T_ResourceImg", wherestr, START_ROW_INIT, START_ROW_INIT, orderstr));
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
                 Project.Entity.Entity_T_ResourceImg entity = new Project.Entity.Entity_T_ResourceImg();
                 entity.ImgUrl = dr["ImgUrl"].ToString();
                 result.Add(entity);
             }
             return result;
         }
    }
}

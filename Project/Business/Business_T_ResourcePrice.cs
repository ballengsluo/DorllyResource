using System;
using System.Data;

namespace Project.Business
{
	 public sealed class Business_T_ResourcePrice : Project.Business.AbstractPmBusiness
	 {
		 private Project.Entity.Entity_T_ResourcePrice _entity=new Project.Entity.Entity_T_ResourcePrice();
		 public string orderstr = "";
		 Data objdata = new Data();

		 /// <summary>
		 /// 缺省构造函数
		 /// </summary>
		 public Business_T_ResourcePrice() { }

		 /// <summary>
		 /// 带参数的构函数
		 /// </summary>
		 /// <param name="entity">实体类</param>
		 public Business_T_ResourcePrice(Project.Entity.Entity_T_ResourcePrice entity)
		 {
			 this._entity = entity;
		 }

		 /// <summary>
		 /// 与实体类关联
		 /// </summary>
		 public Project.Entity.Entity_T_ResourcePrice Entity
		 {
			 get { return _entity as Project.Entity.Entity_T_ResourcePrice; }
		 }

		 /// <summary>
		 /// load 方法 pid主键
		 /// </summary>
         public int loadResourceID(string ResourceID)
		 {
             int rows = 1;
             DataTable dt = objdata.PopulateDataSet("select * from T_ResourcePrice where ResourceID='" + ResourceID + "'").Tables[0];
             DataRow[] dr = dt.Select();
             if (dr.Length > 0)
             {
                 _entity.ID = ParseIntForString(dt.Rows[0]["ID"].ToString());
                 _entity.ResourceID = dt.Rows[0]["ResourceID"].ToString();
                 //年
                 _entity.YearEnable = ParseBoolForString(dt.Rows[0]["YearEnable"].ToString());
                 _entity.YearInPrice = ParseDecimalForString(dt.Rows[0]["YearInPrice"].ToString());
                 _entity.YearOutPrice = ParseDecimalForString(dt.Rows[0]["YearOutPrice"].ToString());
                 //季度
                 _entity.QuaterEnable = ParseBoolForString(dt.Rows[0]["QuaterEnable"].ToString());
                 _entity.QuaterInPrcie = ParseDecimalForString(dt.Rows[0]["QuaterInPrcie"].ToString());
                 _entity.QuaterOutPrice = ParseDecimalForString(dt.Rows[0]["QuaterOutPrice"].ToString());
                 //月
                 _entity.MonthEnable = ParseBoolForString(dt.Rows[0]["MonthEnable"].ToString());
                 _entity.MonthInPrice = ParseDecimalForString(dt.Rows[0]["MonthInPrice"].ToString());
                 _entity.MonthOutPrice = ParseDecimalForString(dt.Rows[0]["MonthOutPrice"].ToString());
                 //星期
                 _entity.WeekEnable = ParseBoolForString(dt.Rows[0]["WeekEnable"].ToString());
                 _entity.WeekInPrice = ParseDecimalForString(dt.Rows[0]["WeekInPrice"].ToString());
                 _entity.WeekOutPrice = ParseDecimalForString(dt.Rows[0]["WeekOutPrice"].ToString());
                 //天
                 _entity.DayEnable = ParseBoolForString(dt.Rows[0]["DayEnable"].ToString());
                 _entity.DayInPrice = ParseDecimalForString(dt.Rows[0]["DayInPrice"].ToString());
                 _entity.DayOutPrice = ParseDecimalForString(dt.Rows[0]["DayOutPrice"].ToString());
                 //半天
                 _entity.HDayEnable = ParseBoolForString(dt.Rows[0]["HDayEnable"].ToString());
                 _entity.HDayInPrice = ParseDecimalForString(dt.Rows[0]["HDayInPrice"].ToString());
                 _entity.HDayOutPrice = ParseDecimalForString(dt.Rows[0]["HDayOutPrice"].ToString());
                 //小时
                 _entity.HourEnable = ParseBoolForString(dt.Rows[0]["HourEnable"].ToString());
                 _entity.HourInPrice = ParseDecimalForString(dt.Rows[0]["HourInPrice"].ToString());
                 _entity.HourOutPrice = ParseDecimalForString(dt.Rows[0]["HourOutPrice"].ToString());
                 //单个
                 _entity.SingleEnable = ParseBoolForString(dt.Rows[0]["SingleEnable"].ToString());
                 _entity.SingleInPrice = ParseDecimalForString(dt.Rows[0]["SingleInPrice"].ToString());
                 _entity.SingleOutPrice = ParseDecimalForString(dt.Rows[0]["SingleOutPrice"].ToString());
                 //延迟价
                 _entity.DelayEnable = ParseBoolForString(dt.Rows[0]["DelayEnable"].ToString());
                 _entity.DelayInPrice = ParseDecimalForString(dt.Rows[0]["DelayInPrice"].ToString());
                 _entity.DelayOutPrice = ParseDecimalForString(dt.Rows[0]["DelayOutPrice"].ToString());
                 //平方米
                 _entity.MeterEnable = ParseBoolForString(dt.Rows[0]["MeterEnable"].ToString());
                 _entity.MeterMinPrice = ParseDecimalForString(dt.Rows[0]["MeterMinPrice"].ToString());
                 _entity.MeterMaxPrice = ParseDecimalForString(dt.Rows[0]["MeterMaxPrice"].ToString());
                 //月区间
                 _entity.IMonthEnable = ParseBoolForString(dt.Rows[0]["IMonthEnable"].ToString());
                 _entity.IMonthMinPrice = ParseDecimalForString(dt.Rows[0]["IMonthMinPrice"].ToString());
                 _entity.IMonthMaxPrice = ParseDecimalForString(dt.Rows[0]["IMonthMaxPrice"].ToString());
                 //单个区间价
                 _entity.ISingleEnable = ParseBoolForString(dt.Rows[0]["ISingleEnable"].ToString());
                 _entity.ISingleMinPrice = ParseDecimalForString(dt.Rows[0]["ISingleMinPrice"].ToString());
                 _entity.ISingleMaxPrice = ParseDecimalForString(dt.Rows[0]["ISingleMaxPrice"].ToString());
                 //单次区间价
                 _entity.OnceEnable = ParseBoolForString(dt.Rows[0]["OnceEnable"].ToString());
                 _entity.OnceMinPrice = ParseDecimalForString(dt.Rows[0]["OnceMinPrice"].ToString());
                 _entity.OnceMaxPrice = ParseDecimalForString(dt.Rows[0]["OnceMaxPrice"].ToString());
                 //其他区间价
                 _entity.OtherEnable = ParseBoolForString(dt.Rows[0]["OtherEnable"].ToString());
                 _entity.OtherMinPrice = ParseDecimalForString(dt.Rows[0]["OtherMinPrice"].ToString());
                 _entity.OtherMaxPrice = ParseDecimalForString(dt.Rows[0]["OtherMaxPrice"].ToString());
             }
             else
             {
                 rows = 0;
             }
             return rows;
		 }
	}
}

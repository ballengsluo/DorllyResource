using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resource.Web.Models
{
    public class OrderRM
    {
        
        private string _RMID;
        private string _RMNo;
        private string _RMLOCNo1;
        private string _RMLOCNo1Name;
        private string _RMLOCNo2;
        private string _RMLOCNo2Name;
        private string _RMLOCNo3;
        private string _RMLOCNo3Name;
        private string _RMLOCNo4;
        private string _RMLOCNo4Name;
        private string _RMRentType;
        private decimal _RMBuildSize;
        private decimal _RMRentSize;
        private string _RMAddr;
        private bool _RMISEnable;
        private string _RMStatus;
        private DateTime _RMReservedDate;
        private DateTime _RMEndReservedDate;
        private string _RMCurrentCustNo;
        private string _RMCurrentCustName;
        private DateTime _RMCreateDate;
        private string _RMCreator;
        private bool _HaveAirCondition;
        
        /// <summary>缺省构造函数</summary>
        public OrderRM() { }

        /// <summary>房间编号【主键】</summary>
        public string RMID
        {
            get { return _RMID; }
            set { _RMID = value; }
        }

        /// <summary>
        /// 功能描述：房间号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMNo
        {
            get { return _RMNo; }
            set { _RMNo = value; }
        }

        /// <summary>
        /// 功能描述：园区
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMLOCNo1
        {
            get { return _RMLOCNo1; }
            set { _RMLOCNo1 = value; }
        }

        /// <summary>
        /// 功能描述：园区名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMLOCNo1Name
        {
            get { return _RMLOCNo1Name; }
            set { _RMLOCNo1Name = value; }
        }

        /// <summary>
        /// 功能描述：建设期
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMLOCNo2
        {
            get { return _RMLOCNo2; }
            set { _RMLOCNo2 = value; }
        }

        /// <summary>
        /// 功能描述：建设期名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMLOCNo2Name
        {
            get { return _RMLOCNo2Name; }
            set { _RMLOCNo2Name = value; }
        }

        /// <summary>
        /// 功能描述：楼栋
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMLOCNo3
        {
            get { return _RMLOCNo3; }
            set { _RMLOCNo3 = value; }
        }

        /// <summary>
        /// 功能描述：楼栋名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMLOCNo3Name
        {
            get { return _RMLOCNo3Name; }
            set { _RMLOCNo3Name = value; }
        }

        /// <summary>
        /// 功能描述：楼层
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMLOCNo4
        {
            get { return _RMLOCNo4; }
            set { _RMLOCNo4 = value; }
        }

        /// <summary>
        /// 功能描述：楼层名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMLOCNo4Name
        {
            get { return _RMLOCNo4Name; }
            set { _RMLOCNo4Name = value; }
        }

        /// <summary>
        /// 功能描述：出租类型
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMRentType
        {
            get { return _RMRentType; }
            set { _RMRentType = value; }
        }

        /// <summary>
        /// 功能描述：出租类型描述【非维护字段】
        /// </summary>
        public string RMRentTypeName
        {
            get
            {
                string _RMRentTypeName = "";
                switch (_RMRentType)
                {
                    case "1":
                        _RMRentTypeName = "写字楼";
                        break;
                    case "2":
                        _RMRentTypeName = "住宅";
                        break;
                    case "3":
                        _RMRentTypeName = "仓库";
                        break;
                }
                return _RMRentTypeName;
            }
        }

        /// <summary>
        /// 功能描述：住宅面积
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public decimal RMBuildSize
        {
            get { return _RMBuildSize; }
            set { _RMBuildSize = value; }
        }

        /// <summary>
        /// 功能描述：出租面积
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public decimal RMRentSize
        {
            get { return _RMRentSize; }
            set { _RMRentSize = value; }
        }

        /// <summary>
        /// 功能描述：位置
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string RMAddr
        {
            get { return _RMAddr; }
            set { _RMAddr = value; }
        }

        /// <summary>
        /// 功能描述：是否禁用
        /// </summary>
        public bool RMISEnable
        {
            get { return _RMISEnable; }
            set { _RMISEnable = value; }
        }

        /// <summary>
        /// 功能描述：状态
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMStatus
        {
            get { return _RMStatus; }
            set { _RMStatus = value; }
        }

        /// <summary>
        /// 功能描述：状态描述【非维护字段】
        /// </summary>
        public string RMStatusName
        {
            get
            {
                string _RMStatusName = "";
                switch (_RMStatus)
                {
                    case "free":
                        _RMStatusName = "空闲";
                        break;
                    case "use":
                        _RMStatusName = "占用";
                        break;
                    case "reserve":
                        _RMStatusName = "预留";
                        break;
                }
                return _RMStatusName;
            }
        }

        /// <summary>
        /// 功能描述：预留日期
        /// </summary>
        public DateTime RMReservedDate
        {
            get { return _RMReservedDate; }
            set { _RMReservedDate = value; }
        }

        /// <summary>
        /// 功能描述：预留截止日期
        /// </summary>
        public DateTime RMEndReservedDate
        {
            get { return _RMEndReservedDate; }
            set { _RMEndReservedDate = value; }
        }

        /// <summary>
        /// 功能描述：当前客户编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMCurrentCustNo
        {
            get { return _RMCurrentCustNo; }
            set { _RMCurrentCustNo = value; }
        }

        /// <summary>
        /// 功能描述：当前客户名称【非维护字段】
        /// 长度：80
        /// 不能为空：否
        /// </summary>
        public string RMCurrentCustName
        {
            get { return _RMCurrentCustName; }
            set { _RMCurrentCustName = value; }
        }

        /// <summary>
        /// 功能描述：创建日期
        /// </summary>
        public DateTime RMCreateDate
        {
            get { return _RMCreateDate; }
            set { _RMCreateDate = value; }
        }

        /// <summary>
        /// 功能描述：创建人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMCreator
        {
            get { return _RMCreator; }
            set { _RMCreator = value; }
        }

        /// <summary>
        /// 功能描述：有无空调
        /// </summary>
        public bool HaveAirCondition
        {
            get { return _HaveAirCondition; }
            set { _HaveAirCondition = value; }
        }
    }
}
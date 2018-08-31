using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resource.Web.Models
{
    public class OrderCB
    {
        
        private string _WPNo;
        private string _WPType;
        private string _WPTypeName;
        private int _WPSeat;
        private decimal _WPSeatPrice;
        private string _WPLOCNo1;
        private string _WPLOCNo1Name;
        private string _WPLOCNo2;
        private string _WPLOCNo2Name;
        private string _WPLOCNo3;
        private string _WPLOCNo3Name;
        private string _WPLOCNo4;
        private string _WPLOCNo4Name;
        private string _WPRMID;
        private string _WPProject;
        private string _WPAddr;
        private string _WPStatus;
        private bool _WPISEnable;
        private DateTime _WPCreateDate;
        private string _WPCreator;
        
        /// <summary>缺省构造函数</summary>
        public OrderCB() { }

        /// <summary>工位编号【主键】</summary>
        public string WPNo
        {
            get { return _WPNo; }
            set { _WPNo = value; }
        }

        /// <summary>
        /// 功能描述：工位类型
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string WPType
        {
            get { return _WPType; }
            set { _WPType = value; }
        }

        /// <summary>
        /// 功能描述：工位类型名称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string WPTypeName
        {
            get { return _WPTypeName; }
            set { _WPTypeName = value; }
        }

        /// <summary>
        /// 功能描述：座位数
        /// </summary>
        public int WPSeat
        {
            get { return _WPSeat; }
            set { _WPSeat = value; }
        }

        /// <summary>
        /// 功能描述：每工位单价
        /// </summary>
        public decimal WPSeatPrice
        {
            get { return _WPSeatPrice; }
            set { _WPSeatPrice = value; }
        }

        /// <summary>
        /// 功能描述：园区编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string WPLOCNo1
        {
            get { return _WPLOCNo1; }
            set { _WPLOCNo1 = value; }
        }

        /// <summary>
        /// 功能描述：园区名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string WPLOCNo1Name
        {
            get { return _WPLOCNo1Name; }
            set { _WPLOCNo1Name = value; }
        }

        /// <summary>
        /// 功能描述：建设期
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string WPLOCNo2
        {
            get { return _WPLOCNo2; }
            set { _WPLOCNo2 = value; }
        }

        /// <summary>
        /// 功能描述：建设期名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string WPLOCNo2Name
        {
            get { return _WPLOCNo2Name; }
            set { _WPLOCNo2Name = value; }
        }

        /// <summary>
        /// 功能描述：楼栋
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string WPLOCNo3
        {
            get { return _WPLOCNo3; }
            set { _WPLOCNo3 = value; }
        }

        /// <summary>
        /// 功能描述：楼栋名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string WPLOCNo3Name
        {
            get { return _WPLOCNo3Name; }
            set { _WPLOCNo3Name = value; }
        }

        /// <summary>
        /// 功能描述：楼层
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string WPLOCNo4
        {
            get { return _WPLOCNo4; }
            set { _WPLOCNo4 = value; }
        }

        /// <summary>
        /// 功能描述：楼层名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string WPLOCNo4Name
        {
            get { return _WPLOCNo4Name; }
            set { _WPLOCNo4Name = value; }
        }

        /// <summary>
        /// 功能描述：房屋编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string WPRMID
        {
            get { return _WPRMID; }
            set { _WPRMID = value; }
        }

        /// <summary>
        /// 功能描述：所属项目
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string WPProject
        {
            get { return _WPProject; }
            set { _WPProject = value; }
        }

        /// <summary>
        /// 功能描述：位置
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string WPAddr
        {
            get { return _WPAddr; }
            set { _WPAddr = value; }
        }

        /// <summary>
        /// 功能描述：状态
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string WPStatus
        {
            get { return _WPStatus; }
            set { _WPStatus = value; }
        }

        /// <summary>
        /// 功能描述：状态描述【非维护字段】
        /// </summary>
        public string WPStatusName
        {
            get
            {
                string _WPStatusName = "";
                switch (_WPStatus)
                {
                    case "free":
                        _WPStatusName = "空闲";
                        break;
                    case "use":
                        _WPStatusName = "租用";
                        break;
                    case "reserve":
                        _WPStatusName = "预留";
                        break;
                }
                return _WPStatusName;
            }
        }

        /// <summary>
        /// 功能描述：是否禁用
        /// </summary>
        public bool WPISEnable
        {
            get { return _WPISEnable; }
            set { _WPISEnable = value; }
        }

        /// <summary>
        /// 功能描述：创建日期
        /// </summary>
        public DateTime WPCreateDate
        {
            get { return _WPCreateDate; }
            set { _WPCreateDate = value; }
        }

        /// <summary>
        /// 功能描述：创建人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string WPCreator
        {
            get { return _WPCreator; }
            set { _WPCreator = value; }
        }
    }
}
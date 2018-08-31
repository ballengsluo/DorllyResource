using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resource.Web.Models
{
    public class OrderAD
    {
        
        private string _BBNo;
        private string _BBName;
        private string _BBSPNo;
        private string _BBSPName;
        private string _BBLOCNo;
        private string _BBLOCName;
        private string _BBAddr;
        private string _BBSize;
        private string _BBType;
        private string _BBTypeName;
        private decimal _BBINPriceDay;
        private decimal _BBOUTPriceDay;
        private decimal _BBINPriceMonth;
        private decimal _BBOUTPriceMonth;
        private decimal _BBINPriceQuarter;
        private decimal _BBOUTPriceQuarter;
        private decimal _BBINPriceYear;
        private decimal _BBOUTPriceYear;
        private decimal _BBDeposit;
        private string _BBImage;
        private bool _BBISEnable;
        private string _BBCurrentCustNo;
        private string _BBCurrentCustName;
        private string _BBStatus;
        private DateTime _BBCreateDate;
        private string _BBCreator;
                
        
        /// <summary>缺省构造函数</summary>
        public OrderAD() { }

        /// <summary>广告位编号【主键】</summary>
        public string BBNo
        {
            get { return _BBNo; }
            set { _BBNo = value; }
        }

        /// <summary>
        /// 功能描述：广告位名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string BBName
        {
            get { return _BBName; }
            set { _BBName = value; }
        }

        /// <summary>
        /// 功能描述：服务商编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string BBSPNo
        {
            get { return _BBSPNo; }
            set { _BBSPNo = value; }
        }

        /// <summary>
        /// 功能描述：服务商名称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string BBSPName
        {
            get { return _BBSPName; }
            set { _BBSPName = value; }
        }

        /// <summary>
        /// 功能描述：所属园区
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string BBLOCNo
        {
            get { return _BBLOCNo; }
            set { _BBLOCNo = value; }
        }

        /// <summary>
        /// 功能描述：所属园区名称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string BBLOCName
        {
            get { return _BBLOCName; }
            set { _BBLOCName = value; }
        }

        /// <summary>
        /// 功能描述：位置
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string BBAddr
        {
            get { return _BBAddr; }
            set { _BBAddr = value; }
        }

        /// <summary>
        /// 功能描述：尺寸
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string BBSize
        {
            get { return _BBSize; }
            set { _BBSize = value; }
        }

        /// <summary>
        /// 功能描述：广告位类型
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string BBType
        {
            get { return _BBType; }
            set { _BBType = value; }
        }

        /// <summary>
        /// 功能描述：广告位类型名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string BBTypeName
        {
            get { return _BBTypeName; }
            set { _BBTypeName = value; }
        }

        /// <summary>
        /// 功能描述：对内价格/天
        /// </summary>
        public decimal BBINPriceDay
        {
            get { return _BBINPriceDay; }
            set { _BBINPriceDay = value; }
        }

        /// <summary>
        /// 功能描述：对外价格/天
        /// </summary>
        public decimal BBOUTPriceDay
        {
            get { return _BBOUTPriceDay; }
            set { _BBOUTPriceDay = value; }
        }

        /// <summary>
        /// 功能描述：对内价格/月
        /// </summary>
        public decimal BBINPriceMonth
        {
            get { return _BBINPriceMonth; }
            set { _BBINPriceMonth = value; }
        }

        /// <summary>
        /// 功能描述：对外价格/月
        /// </summary>
        public decimal BBOUTPriceMonth
        {
            get { return _BBOUTPriceMonth; }
            set { _BBOUTPriceMonth = value; }
        }

        /// <summary>
        /// 功能描述：对内价格/季度
        /// </summary>
        public decimal BBINPriceQuarter
        {
            get { return _BBINPriceQuarter; }
            set { _BBINPriceQuarter = value; }
        }

        /// <summary>
        /// 功能描述：对外价格/季度
        /// </summary>
        public decimal BBOUTPriceQuarter
        {
            get { return _BBOUTPriceQuarter; }
            set { _BBOUTPriceQuarter = value; }
        }

        /// <summary>
        /// 功能描述：对内价格/年
        /// </summary>
        public decimal BBINPriceYear
        {
            get { return _BBINPriceYear; }
            set { _BBINPriceYear = value; }
        }

        /// <summary>
        /// 功能描述：对外价格/年
        /// </summary>
        public decimal BBOUTPriceYear
        {
            get { return _BBOUTPriceYear; }
            set { _BBOUTPriceYear = value; }
        }

        /// <summary>
        /// 功能描述：押金
        /// </summary>
        public decimal BBDeposit
        {
            get { return _BBDeposit; }
            set { _BBDeposit = value; }
        }

        /// <summary>
        /// 功能描述：图片
        /// 长度：200
        /// 不能为空：否
        /// </summary>
        public string BBImage
        {
            get { return _BBImage; }
            set { _BBImage = value; }
        }

        /// <summary>
        /// 功能描述：状态
        /// </summary>
        public bool BBISEnable
        {
            get { return _BBISEnable; }
            set { _BBISEnable = value; }
        }
        
        /// <summary>
        /// 功能描述：状态
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string BBStatus
        {
            get { return _BBStatus; }
            set { _BBStatus = value; }
        }

        /// <summary>
        /// 功能描述：状态描述【非维护字段】
        /// </summary>
        public string BBStatusName
        {
            get
            {
                string _BBStatusName = "";
                switch (_BBStatus)
                {
                    case "free":
                        _BBStatusName = "空闲";
                        break;
                    case "use":
                        _BBStatusName = "租用";
                        break;
                }
                return _BBStatusName;
            }
        }

        /// <summary>
        /// 功能描述：当前客户编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string BBCurrentCustNo
        {
            get { return _BBCurrentCustNo; }
            set { _BBCurrentCustNo = value; }
        }

        /// <summary>
        /// 功能描述：当前客户名称【非维护字段】
        /// 长度：80
        /// 不能为空：否
        /// </summary>
        public string BBCurrentCustName
        {
            get { return _BBCurrentCustName; }
            set { _BBCurrentCustName = value; }
        }

        /// <summary>
        /// 功能描述：创建日期
        /// </summary>
        public DateTime BBCreateDate
        {
            get { return _BBCreateDate; }
            set { _BBCreateDate = value; }
        }

        /// <summary>
        /// 功能描述：创建人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string BBCreator
        {
            get { return _BBCreator; }
            set { _BBCreator = value; }
        }
    }
}
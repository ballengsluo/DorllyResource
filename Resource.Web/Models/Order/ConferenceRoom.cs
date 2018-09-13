using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resource.Web.Models
{
    public class ConferenceRoom
    {
        
        private string _CRNo;
        private string _CRName;
        private string _ParkNo;
        private string _CRCapacity;
        private decimal _CRBuildSize;
        private decimal _CRINPriceHour;
        private decimal _CRINPriceHalfDay;
        private decimal _CRINPriceDay;
        private decimal _CRINPriceDelay;
        private decimal _CROUTPriceHour;
        private decimal _CROUTPriceHalfDay;
        private decimal _CROUTPriceDay;
        private decimal _CROUTPriceDelay;
        private decimal _CRDeposit;
        private string _CRAddr;
        private bool _CRIsDay;
        private bool _CRIsHalfDay;
        private bool _CRIsHour;
        private bool _CRISEnable;
        private DateTime _CRCreateDate;
        private string _CRCreator;
        private DateTime _CRUpdateDate;
        private string _CRUpdateUser;
        private string _CRPic;
        private string _Content;
        
        
        /// <summary>缺省构造函数</summary>
        public ConferenceRoom() { }

        /// <summary>会议室编号【主键】</summary>
        public string CRNo
        {
            get { return _CRNo; }
            set { _CRNo = value; }
        }

        /// <summary>
        /// 功能描述：会议室名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string CRName
        {
            get { return _CRName; }
            set { _CRName = value; }
        }

        /// <summary>
        /// 功能描述：园区编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ParkNo
        {
            get { return _ParkNo; }
            set { _ParkNo = value; }
        }

        /// <summary>
        /// 功能描述：容纳人数
        /// </summary>
        public string CRCapacity
        {
            get { return _CRCapacity; }
            set { _CRCapacity = value; }
        }

        /// <summary>
        /// 功能描述：面积
        /// </summary>
        public decimal CRBuildSize
        {
            get { return _CRBuildSize; }
            set { _CRBuildSize = value; }
        }

        /// <summary>
        /// 功能描述：对内价格/小时
        /// </summary>
        public decimal CRINPriceHour
        {
            get { return _CRINPriceHour; }
            set { _CRINPriceHour = value; }
        }

        /// <summary>
        /// 功能描述：对内价格/半天
        /// </summary>
        public decimal CRINPriceHalfDay
        {
            get { return _CRINPriceHalfDay; }
            set { _CRINPriceHalfDay = value; }
        }
        
        /// <summary>
        /// 功能描述：对内价格/全天
        /// </summary>
        public decimal CRINPriceDay
        {
            get { return _CRINPriceDay; }
            set { _CRINPriceDay = value; }
        }
        
        /// <summary>
        /// 功能描述：对内延时价格/小时
        /// </summary>
        public decimal CRINPriceDelay
        {
            get { return _CRINPriceDelay; }
            set { _CRINPriceDelay = value; }
        }

        /// <summary>
        /// 功能描述：对外价格/小时
        /// </summary>
        public decimal CROUTPriceHour
        {
            get { return _CROUTPriceHour; }
            set { _CROUTPriceHour = value; }
        }

        /// <summary>
        /// 功能描述：对外价格/半天
        /// </summary>
        public decimal CROUTPriceHalfDay
        {
            get { return _CROUTPriceHalfDay; }
            set { _CROUTPriceHalfDay = value; }
        }

        /// <summary>
        /// 功能描述：对外价格/全天
        /// </summary>
        public decimal CROUTPriceDay
        {
            get { return _CROUTPriceDay; }
            set { _CROUTPriceDay = value; }
        }

        /// <summary>
        /// 功能描述：对外延时价格/小时
        /// </summary>
        public decimal CROUTPriceDelay
        {
            get { return _CROUTPriceDelay; }
            set { _CROUTPriceDelay = value; }
        }

        /// <summary>
        /// 功能描述：押金
        /// </summary>
        public decimal CRDeposit
        {
            get { return _CRDeposit; }
            set { _CRDeposit = value; }
        }

        /// <summary>
        /// 功能描述：位置
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string CRAddr
        {
            get { return _CRAddr; }
            set { _CRAddr = value; }
        }

        /// <summary>
        /// 功能描述：是否全天租
        /// </summary>
        public bool CRIsDay
        {
            get { return _CRIsDay; }
            set { _CRIsDay = value; }
        }

        /// <summary>
        /// 功能描述：是否半天租
        /// </summary>
        public bool CRIsHalfDay
        {
            get { return _CRIsHalfDay; }
            set { _CRIsHalfDay = value; }
        }

        /// <summary>
        /// 功能描述：是否小时租
        /// </summary>
        public bool CRIsHour
        {
            get { return _CRIsHour; }
            set { _CRIsHour = value; }
        }

        /// <summary>
        /// 功能描述：状态
        /// </summary>
        public bool CRISEnable
        {
            get { return _CRISEnable; }
            set { _CRISEnable = value; }
        }

        /// <summary>
        /// 功能描述：创建日期
        /// </summary>
        public DateTime CRCreateDate
        {
            get { return _CRCreateDate; }
            set { _CRCreateDate = value; }
        }

        /// <summary>
        /// 功能描述：创建人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CRCreator
        {
            get { return _CRCreator; }
            set { _CRCreator = value; }
        }

        /// <summary>
        /// 功能描述：最后更新日期
        /// </summary>
        public DateTime CRUpdateDate
        {
            get { return _CRUpdateDate; }
            set { _CRUpdateDate = value; }
        }

        /// <summary>
        /// 功能描述：最后更新人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CRUpdateUser
        {
            get { return _CRUpdateUser; }
            set { _CRUpdateUser = value; }
        }

        public string CRPic
        {
            get { return _CRPic; }
            set { _CRPic = value; }
        }

        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
    /// <summary>
    /// 资源信息
    /// </summary>
    public class Entity_T_Resource
    {
        private string _name;
        private string _resourceKindID;
        private Decimal _area;
        private Decimal _rentArea;
        private Decimal _startArea;
        private string _rangeArea;
        private int _number;
        private int _rentNum;
        private int _startNum;
        private string _rangeNum;
        private string _size;
        private string _rentSize;
        private string _startSize;
        private string _rangeSize;
        private string _content;
        private string _location;


        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 资源种类ID
        /// 1.办公室   2.工位   3.会议室   4.广告  
        /// </summary>
        public string ResourceKindID
        {
            get { return _resourceKindID; }
            set { _resourceKindID = value; }
        }

        #region 房间 会议室
        //面积    房间，   会议室只对应面积
        public Decimal Area
        {
            get { return _area; }
            set { _area = value; }
        }

        //出租面积
        public Decimal RentArea
        {
            get { return _rentArea; }
            set { _rentArea = value; }
        }

        //起租面积
        public Decimal StartArea
        {
            get { return _startArea; }
            set { _startArea = value; }
        }

        //可租面积
        public string RangeArea
        {
            get { return _rangeArea; }
            set { _rangeArea = value; }
        }
        #endregion

        #region 工位
        //个数
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }
        //出租个数
        public int RentNum
        {
            get { return _rentNum; }
            set { _rentNum = value; }
        }
        //起租个数
        public int StartNum
        {
            get { return _startNum; }
            set { _startNum = value; }
        }
        //可租个数
        public string RangeNum
        {
            get { return _rangeNum; }
            set { _rangeNum = value; }
        }
        #endregion

        #region 广告位
        //规格
        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }
        //出租大小
        public string RentSize
        {
            get { return _rentSize; }
            set { _rentSize = value; }
        }
        //起租大小
        public string StartSize
        {
            get { return _startSize; }
            set { _startSize = value; }
        }
        //可租大小
        public string RangeSize
        {
            get { return _rangeSize; }
            set { _rangeSize = value; }
        }
        #endregion

        //详细信息
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        //位置
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }
    }
}

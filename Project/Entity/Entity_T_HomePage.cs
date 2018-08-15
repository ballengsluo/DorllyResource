using System;

namespace Project.Entity
{
    /// <summary>
    /// 首页
    /// </summary>
	 public class Entity_T_HomePage
	 {
		 private Int32 _iD;
		 private string _cityID;
		 private Int32 _position;
		 private string _title;
		 private string _subTitle;
		 private Int32 _orderNum;
		 private string _imgUrl;
		 private string _linkUrl;
		 private Int32 _status;

		 /// <summary>
		 /// 缺省构造函数
		 /// </summary>
		 public Entity_T_HomePage(){}

		 public Int32 ID
		 {
			 get { return _iD; }
			 set { _iD = value; }
		 }
		 public string CityID
		 {
			 get { return _cityID; }
			 set { _cityID = value; }
		 }
		 public Int32 Position
		 {
			 get { return _position; }
			 set { _position = value; }
		 }
		 public string Title
		 {
			 get { return _title; }
			 set { _title = value; }
		 }
		 public string SubTitle
		 {
			 get { return _subTitle; }
			 set { _subTitle = value; }
		 }
		 public Int32 OrderNum
		 {
			 get { return _orderNum; }
			 set { _orderNum = value; }
		 }
		 public string ImgUrl
		 {
			 get { return _imgUrl; }
			 set { _imgUrl = value; }
		 }
		 public string LinkUrl
		 {
			 get { return _linkUrl; }
			 set { _linkUrl = value; }
		 }
		 public Int32 Status
		 {
			 get { return _status; }
			 set { _status = value; }
		 }
	 }
}

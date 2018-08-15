using System;

namespace Project.Entity
{
    /// <summary>
    /// 资源发布列表
    /// </summary>
	 public class Entity_T_ResourcePublic
	 {
		 private string _resourceID;
         private string _resourceName;
         private decimal _orderPrice;
         private string _parentID;
         private string _parentName;
         private string _resourceKindID;
         private string _parkID;
         private string _cityName;
         private string _buildingID;
         private string _buildingName;
         private string _floorName;

         private string _coverImg;

		 /// <summary>
		 /// 缺省构造函数
		 /// </summary>
		 public Entity_T_ResourcePublic(){}

         /// <summary>
         /// 资源ID
         /// </summary>
		 public string ResourceID
		 {
			 get { return _resourceID; }
			 set { _resourceID = value; }
		 }
         /// <summary>
         /// 资源名称
         /// </summary>
         public string ResourceName
         {
             get { return _resourceName; }
             set { _resourceName = value; }
         }
         /// <summary>
         /// 价格排序，取所有价格最低价
         /// </summary>
         public Decimal OrderPrice
         {
             get { return _orderPrice; }
             set { _orderPrice = value; }
         }
         /// <summary>
         /// 父节点ID
         /// </summary>
         public string ParentID
         {
             get { return _parentID; }
             set { _parentID = value; }
         }
         /// <summary>
         /// 父节点名称
         /// </summary>
         public string ParentName
         {
             get { return _parentName; }
             set { _parentName = value; }
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
         /// <summary>
         /// 园区编号
         /// </summary>
         public string ParkID
         {
             get { return _parkID; }
             set { _parkID = value; }
         }
         /// <summary>
         /// 城市名
         /// </summary>
         public string CityName
         {
             get { return _cityName; }
             set { _cityName = value; }
         }
         /// <summary>
         /// 建筑ID
         /// </summary>
         public string BuildingID
         {
             get { return _buildingID; }
             set { _buildingID = value; }
         }
         /// <summary>
         /// 建筑名
         /// </summary>
         public string BuildingName
         {
             get { return _buildingName; }
             set { _buildingName = value; }
         }
         /// <summary>
         /// 楼层名
         /// </summary>
         public string FloorName
         {
             get { return _floorName; }
             set { _floorName = value; }
         }
         /// <summary>
         /// 封面图片
         /// </summary>
         public string CoverImg
         {
             get { return _coverImg; }
             set { _coverImg = value; }
         }
	 }
}

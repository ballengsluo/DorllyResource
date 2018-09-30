using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using Newtonsoft.Json;
using Resource.Model;
using Resource.Web.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Resource.Web.WebApi
{
    /// <summary>
    /// ResourceService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ResourceService : System.Web.Services.WebService
    {
        #region 房屋基本信息操作

        [WebMethod]
        public string AddOrUpdateRoom(string rmJsonObject)
        {
            Result result = new Result();
            Room room = null;
            bool add = false;
            try
            {
                room = JsonConvert.DeserializeObject<Room>(rmJsonObject);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Result.Exception(msg: "json转换对象异常，请查阅异常信息并检查字段！",
                    exmsg: ex.StackTrace));
            }
            try
            {
                DbContext dc = DbContextFactory.Create();
                var resource = dc.Set<T_Resource>().Where(a => a.ID == room.RMID).FirstOrDefault();
                if (resource == null)
                {
                    result.Msg = "目标数据库无此数据，随后进行添加操作；";
                    resource = new T_Resource();
                    add = true;
                }
                else
                {
                    result.Msg = "目标数据库存在此数据，随后进行更新操作；";
                }
                resource.ID = room.RMID;
                resource.Name = room.RMNo;
                resource.Loc1 = room.RMLOCNo1;
                resource.Loc2 = room.RMLOCNo2;
                resource.Loc3 = room.RMLOCNo3;
                resource.Loc4 = room.RMLOCNo4;
                resource.ResourceKindID = 1;
                resource.ResourceTypeID = room.RMRentType;
                resource.Area = room.RMBuildSize;
                resource.RentArea = room.RMRentSize;
                resource.Enable = !room.RMISEnable;
                resource.CreateUser = room.RMCreator;
                resource.CreateTime = room.RMCreateDate;
                resource.Location = room.RMAddr;
                if (add)
                {
                    dc.Set<T_Resource>().Add(resource);
                }
                else
                {
                    dc.Set<T_Resource>().AddOrUpdate(resource);
                }
                dc.SaveChanges();
                result.Msg += "操作成功！";
                result.Flag = 1;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Result.Exception(msg: "添加或更新异常，请查阅异常信息！", exmsg: ex.StackTrace));
            }
            return JsonConvert.SerializeObject(result);
        }

        #endregion

        #region 工位基本信息操作
        [WebMethod]
        public string AddOrUpdateWorkPlace(string wpJsonObject)
        {
            Result result = new Result();
            WorkPlace workplace = null;
            bool add = false;
            try
            {
                workplace = JsonConvert.DeserializeObject<WorkPlace>(wpJsonObject);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Result.Exception(msg: "json转换对象异常，请查阅异常信息并检查字段！",
                    exmsg: ex.StackTrace));
            }
            try
            {
                DbContext dc = DbContextFactory.Create();
                var resource = dc.Set<T_Resource>().Where(a => a.ID == workplace.WPNo).FirstOrDefault();
                if (resource == null)
                {
                    result.Msg = "目标数据库无此数据，随后进行添加操作；";
                    resource = new T_Resource();
                    add = true;
                }
                else
                {
                    result.Msg = "目标数据库存在此数据，随后进行更新操作；";
                }
                resource.ID = workplace.WPNo;
                resource.Loc1 = workplace.WPLOCNo1;
                resource.Loc2 = workplace.WPLOCNo2;
                resource.Loc3 = workplace.WPLOCNo3;
                resource.Loc4 = workplace.WPLOCNo4;
                resource.Loc5 = workplace.WPRMID;
                resource.ResourceKindID = 2;
                resource.ResourceTypeID = workplace.WPType;
                resource.Number = workplace.WPSeat;
                resource.Price = workplace.WPSeatPrice;
                resource.Enable =! workplace.WPISEnable;
                resource.Location = workplace.WPAddr;
                resource.CreateUser = workplace.WPCreator;
                resource.CreateTime = workplace.WPCreateDate;
                if (add)
                {
                    dc.Set<T_Resource>().Add(resource);
                }
                else
                {
                    dc.Set<T_Resource>().AddOrUpdate(resource);
                }
                dc.SaveChanges();
                result.Msg += "操作成功！";
                result.Flag = 1;
                //if (dc.SaveChanges() > 0)
                //{
                //    result.Msg += "操作成功！";
                //    result.Flag = 1;
                //}
                //else
                //{
                //    result.Msg += "操作失败！可能存在非空、数据类型、约束等条件不满足！";
                //    result.Flag = 2;
                //}
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Result.Exception(msg: "添加或更新异常，请查阅异常信息！", exmsg: ex.StackTrace));
            }
            return JsonConvert.SerializeObject(result);
        }
        #endregion

        #region 会议室基本信息操作
        [WebMethod]
        public string AddOrUpdateConferenceRoom(string crJsonObject)
        {
            Result result = new Result();
            ConferenceRoom conferenceRoom = null;
            bool resourceAdd = false;
            bool priceAdd = false;
            try
            {
                conferenceRoom = JsonConvert.DeserializeObject<ConferenceRoom>(crJsonObject);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Result.Exception(msg: "json转换对象异常，请查阅异常信息并检查字段！",
                    exmsg: ex.StackTrace));
            }
            try
            {
                DbContext dc = DbContextFactory.Create();
                var resource = dc.Set<T_Resource>().Where(a => a.ID == conferenceRoom.CRNo).FirstOrDefault();
                var price = dc.Set<T_ResourcePrice>().Where(a => a.ResourceID == conferenceRoom.CRNo).FirstOrDefault();
                if (price == null) { price = new T_ResourcePrice(); priceAdd = true; }
                if (resource == null)
                {
                    result.Msg = "目标数据库无此数据，随后进行添加操作；";
                    resource = new T_Resource();
                    resourceAdd = true;
                }
                else
                {
                    result.Msg = "目标数据库存在此数据，随后进行更新操作；";
                }
                resource.ID = conferenceRoom.CRNo;
                resource.Name = conferenceRoom.CRName;
                resource.Number = conferenceRoom.CRCapacity;
                resource.RangeNum = conferenceRoom.CRCapacity.ToString();
                resource.Area = conferenceRoom.CRBuildSize;
                resource.Loc1 = conferenceRoom.ParkNo;
                resource.ResourceKindID = 3;
                resource.Location = conferenceRoom.CRAddr;
                resource.Deposit = conferenceRoom.CRDeposit;
                resource.Enable = conferenceRoom.CRISEnable;
                resource.CreateUser = conferenceRoom.CRCreator;
                resource.CreateTime = conferenceRoom.CRCreateDate;
                resource.UpdateTime = conferenceRoom.CRUpdateDate;
                resource.UpdateUser = conferenceRoom.CRUpdateUser;

                price.ResourceID = conferenceRoom.CRNo;
                price.HourEnable = conferenceRoom.CRIsHour;
                price.HourInPrice = conferenceRoom.CRINPriceHour;
                price.HourOutPrice = conferenceRoom.CROUTPriceHour;
                price.HDayEnable = conferenceRoom.CRIsHalfDay;
                price.HDayInPrice = conferenceRoom.CRINPriceHalfDay;
                price.HDayOutPrice = conferenceRoom.CROUTPriceHalfDay;
                price.DayEnable = conferenceRoom.CRIsDay;
                price.DayInPrice = conferenceRoom.CRINPriceDay;
                price.DayOutPrice = conferenceRoom.CROUTPriceDay;
                price.DelayEnable = true;
                price.DelayInPrice = conferenceRoom.CRINPriceDelay;
                price.DelayOutPrice = conferenceRoom.CROUTPriceDelay;
                if (resourceAdd) dc.Set<T_Resource>().Add(resource);
                else dc.Set<T_Resource>().AddOrUpdate(resource);
                if (priceAdd) dc.Set<T_ResourcePrice>().Add(price);
                else dc.Set<T_ResourcePrice>().AddOrUpdate(price);
                dc.SaveChanges();
                result.Msg += "操作成功！";
                result.Flag = 1;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Result.Exception(msg: "添加或更新异常，请查阅异常信息！", exmsg: ex.StackTrace));
            }
            return JsonConvert.SerializeObject(result);
        }
        #endregion

        #region 广告位基本信息操作
        [WebMethod]
        public string AddOrUpdateBillboard(string bbJsonObject)
        {
            Result result = new Result();
            Billboard billboard = null;
            bool resourceAdd = false;
            bool priceAdd = false;
            try
            {
                billboard = JsonConvert.DeserializeObject<Billboard>(bbJsonObject);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Result.Exception(msg: "json转换对象异常，请查阅异常信息并检查字段！",
                    exmsg: ex.StackTrace));
            }
            try
            {
                DbContext dc = DbContextFactory.Create();
                var resource = dc.Set<T_Resource>().Where(a => a.ID == billboard.BBNo).FirstOrDefault();
                var price = dc.Set<T_ResourcePrice>().Where(a => a.ResourceID == billboard.BBNo).FirstOrDefault();
                if (price == null) { price = new T_ResourcePrice(); priceAdd = true; }
                if (resource == null)
                {
                    result.Msg = "目标数据库无此数据，随后进行添加操作；";
                    resource = new T_Resource();
                    resourceAdd = true;
                }
                else
                {
                    result.Msg = "目标数据库存在此数据，随后进行更新操作；";
                }
                resource.ID = billboard.BBNo;
                resource.Name = billboard.BBName;
                resource.Loc1 = billboard.BBLOCNo;
                resource.ResourceKindID = 4;
                resource.Size = billboard.BBSize;
                resource.ResourceTypeID = billboard.BBType;
                resource.Deposit = billboard.BBDeposit;
                resource.Enable = !billboard.BBISEnable;
                resource.CreateUser = billboard.BBCreator;
                resource.CreateTime = billboard.BBCreateDate;
                resource.Location = billboard.BBAddr;

                price.ResourceID = billboard.BBNo;
                price.DayInPrice = billboard.BBINPriceDay;
                price.DayOutPrice = billboard.BBOUTPriceDay;
                price.MonthInPrice = billboard.BBINPriceMonth;
                price.MonthOutPrice = billboard.BBOUTPriceMonth;
                price.QuaterInPrcie = billboard.BBINPriceQuarter;
                price.QuaterOutPrice = billboard.BBOUTPriceQuarter;
                price.YearInPrice = billboard.BBINPriceYear;
                price.YearOutPrice = billboard.BBOUTPriceYear;

                if (resourceAdd) dc.Set<T_Resource>().Add(resource);
                else dc.Set<T_Resource>().AddOrUpdate(resource);
                if (priceAdd) dc.Set<T_ResourcePrice>().Add(price);
                else dc.Set<T_ResourcePrice>().AddOrUpdate(price);
                dc.SaveChanges();
                result.Msg += "操作成功！";
                result.Flag = 1;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Result.Exception(msg: "添加或更新异常，请查阅异常信息！", exmsg: ex.StackTrace));
            }
            return JsonConvert.SerializeObject(result);
        }
        #endregion


        #region 共有操作

        [WebMethod]
        public string DeleteResource(string resourceId)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                var resource = dc.Set<T_Resource>().Where(a => a.ID == resourceId).FirstOrDefault();
                if (resource == null)
                {
                    return JsonConvert.SerializeObject(Result.Fail(msg: "数据库无此数据，删除失败！"));
                }
                var price = dc.Set<T_ResourcePrice>().Where(a => a.ResourceID == resourceId).FirstOrDefault();
                if (price != null) { dc.Set<T_ResourcePrice>().Remove(price); }
                dc.Set<T_Resource>().Remove(resource);
                dc.SaveChanges();
                return JsonConvert.SerializeObject(Result.Success(msg: "删除成功"));
                //if (dc.SaveChanges() > 0)
                //{
                //    return JsonConvert.SerializeObject(Result.Success(msg: "删除成功"));
                //}
                //else
                //{
                //    return JsonConvert.SerializeObject(Result.Fail(msg: "删除失败，有坑！"));
                //}
            }
            catch (Exception ex)
            {

                return JsonConvert.SerializeObject(Result.Exception(msg: "删除异常，没啥好说的,比如说有图片信息这些！",
                    exmsg: ex.StackTrace));
            }

        }

        /// <summary>
        /// 资源租赁
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        [WebMethod]
        public string LeaseIn(string jsonList)
        {
            DbContext dc = DbContextFactory.Create();
            List<T_ResourceStatus> rsList = new List<T_ResourceStatus>();
            try
            {
                rsList = JsonConvert.DeserializeObject<List<T_ResourceStatus>>(jsonList);
                if (rsList.Count() <= 0) return JsonConvert.SerializeObject(Result.Fail(msg: "对象数组个数为0！"));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Result.Exception(msg: "json转换对象异常，请查阅异常信息并检查字段！",
                       exmsg: ex.StackTrace));
            }
            try
            {
                var status = dc.Set<T_ResourceStatus>();
                foreach (var item in rsList)
                {
                    //存在此数据，删除
                    var obj = status.Where(a => a.BusinessID == item.BusinessID && a.ResourceID == item.ResourceID).FirstOrDefault();
                    if (obj != null) status.Remove(obj);
                    //合同是租赁合同
                    if (item.BusinessType == 1)
                    {
                        //删除物业合同
                        var property = status.Where(a => a.ResourceID == item.ResourceID && a.BusinessType == 2).ToList();
                        foreach (var pro in property)
                        {
                            status.Remove(pro);
                        }
                    }
                    //合同是物业合同,存在租赁合同，跳过
                    if (item.BusinessType == 2 &&
                        status.Where(a => a.ResourceID == item.ResourceID && a.BusinessType == 1).Count() > 0) continue;
                    status.Add(item);
                }
                dc.SaveChanges();
                return JsonConvert.SerializeObject(Result.Success());
                //if (dc.SaveChanges() > 0) return JsonConvert.SerializeObject(Result.Success());
                //return JsonConvert.SerializeObject(Result.Fail());
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Result.Exception(exmsg: ex.StackTrace));
            }


        }
        /// <summary>
        /// 资源退租
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        [WebMethod]
        public string LeaseOut(string jsonList)
        {
            DbContext dc = DbContextFactory.Create();
            List<T_ResourceStatus> rsList = new List<T_ResourceStatus>();
            try
            {
                rsList = JsonConvert.DeserializeObject<List<T_ResourceStatus>>(jsonList);
                if (rsList.Count() <= 0) return JsonConvert.SerializeObject(Result.Fail(msg: "对象数组个数为0！"));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Result.Exception(msg: "json转换对象异常，请查阅异常信息并检查字段！",
                       exmsg: ex.StackTrace));
            }
            try
            {
                var status = dc.Set<T_ResourceStatus>();
                foreach (var item in rsList)
                {
                    var obj = status.Where(a => a.BusinessID == item.BusinessID && a.ResourceID == item.ResourceID).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.RentEndTime = item.RentEndTime;
                        status.AddOrUpdate(obj);
                    }

                }
                dc.SaveChanges();
                return JsonConvert.SerializeObject(Result.Success());
                //JsonConvert.SerializeObject();
                //if (dc.SaveChanges() > 0) return JsonConvert.SerializeObject(Result.Success());
                //return JsonConvert.SerializeObject(Result.Fail());
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        /// <summary>
        /// 资源租赁删除
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        [WebMethod]
        public string LeaseDel(string jsonList)
        {
            DbContext dc = DbContextFactory.Create();
            List<T_ResourceStatus> rsList = new List<T_ResourceStatus>();
            try
            {
                rsList = JsonConvert.DeserializeObject<List<T_ResourceStatus>>(jsonList);
                if (rsList.Count() <= 0) return JsonConvert.SerializeObject(Result.Fail(msg: "对象数组个数为0！"));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Result.Exception(msg: "json转换对象异常，请查阅异常信息并检查字段！",
                       exmsg: ex.StackTrace));
            }
            try
            {
                var status = dc.Set<T_ResourceStatus>();
                foreach (var item in rsList)
                {
                    var obj = status.Where(a => a.BusinessID == item.BusinessID && a.ResourceID == item.ResourceID).FirstOrDefault();
                    if (obj != null) status.Remove(obj);
                }
                dc.SaveChanges();
                return JsonConvert.SerializeObject(Result.Success());
                //if (dc.SaveChanges() > 0) return JsonConvert.SerializeObject(Result.Success());
                //return JsonConvert.SerializeObject(Result.Fail());
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Result.Exception(exmsg: ex.StackTrace));
            }
        }

        #endregion



    }
}

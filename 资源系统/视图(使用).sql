use DorllyResource;
/*
** 楼层查询视图
*/
Create View V_Floor AS
SELECT F.*,
C.Name+'/'+R.Name+'/'+P.Name+'/'+S.Name+'/'+B.NAME AS FullPosition,
B.NAME AS BuildingName,
B.StageID,S.Name as StageName,
S.ParkID,P.Name as ParkName,
P.RegionID,R.Name as RegionName,
R.CityID,C.Name AS CityName
FROM T_Floor F
LEFT JOIN T_BUILDING B ON B.ID = F.BUILDINGID
LEFT JOIN T_STAGE S ON S.ID = B.STAGEID
LEFT JOIN T_PARK P ON P.ID = S.PARKID
LEFT JOIN T_REGION R ON R.ID = P.REGIONID
LEFT JOIN T_CITY C ON C.ID = R.CITYID;
/*
** 建筑查询视图
*/
Create View V_Building AS
SELECT B.*,
C.Name+'/'+R.Name+'/'+P.Name+'/'+S.Name AS FullPosition,
S.Name as StageName,S.ParkID,
P.Name as ParkName,P.RegionID,
R.Name as RegionName,R.CityID,
C.Name AS CityName
FROM T_BUILDING B
LEFT JOIN T_STAGE S ON S.ID = B.STAGEID
LEFT JOIN T_PARK P ON P.ID = S.PARKID
LEFT JOIN T_REGION R ON R.ID = P.REGIONID
LEFT JOIN T_CITY C ON C.ID = R.CITYID;
/*
** 建设期查询视图
*/
Create View V_Stage AS
SELECT S.*,
C.Name+'/'+R.Name+'/'+P.Name AS FullPosition,
P.Name as ParkName,P.RegionID,
R.Name as RegionName,R.CityID,
C.Name AS CityName
FROM T_STAGE S
LEFT JOIN T_PARK P ON P.ID = S.PARKID
LEFT JOIN T_REGION R ON R.ID = P.REGIONID
LEFT JOIN T_CITY C ON C.ID = R.CITYID;
/*
** 园区查询视图
*/
Create View V_Park AS
SELECT P.*,
C.Name+'/'+R.Name AS FullPosition,
R.Name as RegionName,R.CityID,
C.Name AS CityName
FROM T_PARK P
LEFT JOIN T_REGION R ON R.ID = P.REGIONID
LEFT JOIN T_CITY C ON C.ID = R.CITYID;

ALTER VIEW V_RM AS 
SELECT A.*,T.Name  AS ResourceTypeName,
P.Name+'/'+S.Name+'/'+B.Name+'/'+F.Name AS Position,
F.Name AS FloorName,F.BuildingID,
B.Name AS BuildingName,B.StageID,
S.Name AS StageName,P.Name AS ParkName,
G.Name AS GroupName
FROM T_Resource A 
LEFT JOIN T_Floor F ON F.ID=A.ParentID
LEFT JOIN T_Building B ON B.ID=F.BuildingID
LEFT JOIN T_STAGE S ON S.ID=B.StageID
LEFT JOIN T_PARK P ON P.ID=A.ParkID
LEFT JOIN T_ResourceGroup G ON G.ID=A.GroupID
LEFT JOIN T_ResourceType T ON T.ID=A.ResourceTypeID
WHERE A.ResourceKindID=1;

ALTER VIEW V_CB AS
SELECT A.*,T.Name  AS ResourceTypeName,
P.Name+'/'+S.Name+'/'+B.Name+'/'+F.Name+'/'+R.Name AS Position,
F.ID AS FloorID,F.Name AS FloorName,
F.BuildingID,B.Name AS BuildingName,
B.StageID,S.Name AS StageName,
P.Name AS ParkName,
G.Name AS GroupName
FROM T_Resource A 
LEFT JOIN T_Resource R ON R.ID=A.ParentID
LEFT JOIN T_Floor F ON F.ID=R.ParentID
LEFT JOIN T_Building B ON B.ID=F.BuildingID
LEFT JOIN T_STAGE S ON S.ID=B.StageID
LEFT JOIN T_PARK P ON P.ID=A.ParkID
LEFT JOIN T_ResourceGroup G ON G.ID=A.GroupID
LEFT JOIN T_ResourceType T ON T.ID=A.ResourceTypeID
WHERE A.ResourceKindID=2;

ALTER VIEW V_MR AS
SELECT A.*,T.Name AS ResourceTypeName,P.Name AS ParkName,
G.Name AS GroupName
FROM T_Resource A 
LEFT JOIN T_PARK P ON P.ID=A.ParkID
LEFT JOIN T_ResourceGroup G ON G.ID=A.GroupID
LEFT JOIN T_ResourceType T ON T.ID=A.ResourceTypeID
WHERE A.ResourceKindID=3;


ALTER VIEW V_AD AS
SELECT A.*,T.Name AS ResourceTypeName,P.Name AS ParkName,
G.Name AS GroupName
FROM T_Resource A 
LEFT JOIN T_PARK P ON P.ID=A.ParkID
LEFT JOIN T_ResourceGroup G ON G.ID=A.GroupID
LEFT JOIN T_ResourceType T ON T.ID=A.ResourceTypeID
WHERE A.ResourceKindID=4;

ALTER VIEW V_Public AS
SELECT L.*,
A.Name AS ResourceName,
A.ParkID,P.Name AS ParkName,
A.GroupID,G.Name AS GroupName,
A.ResourceKindID, K.Name AS ResourceKindName
FROM T_ResourcePublic L 
LEFT JOIN T_Resource A ON A.ID=L.ResourceID
LEFT JOIN T_PARK P ON P.ID=A.ParkID
LEFT JOIN T_ResourceGroup G ON G.ID=A.GroupID
LEFT JOIN T_ResourceKind K ON K.ID=A.ResourceKindID;

alter VIEW V_Releasing AS
SELECT A.*,
P.Name AS ParkName,
G.Name AS GroupName,
K.Name AS ResourceKindName
FROM T_Resource A 
LEFT JOIN T_Park P ON P.ID=A.ParkID
LEFT JOIN T_ResourceGroup G ON G.ID=A.GroupID
LEFT JOIN T_ResourceKind K ON K.ID=A.ResourceKindID
WHERE A.ID NOT IN(SELECT ResourceID FROM T_ResourcePublic WHERE Status <>6)
And A.Status<>1 And A.Enable=1;

create view V_Order as
select a.*,
r.Name,r.ParkID,p.Name AS ParkName, r.ParentID,r.GroupID,
r.ResourceKindID,k.Name As ResourceKindName,
r.ResourceTypeID,t.Name As ResourceTypeName,r.UnitPrice,r.OrderPrice,r.Deposit,
r.RentBeginTime,r.RentEndTime,r.Area,r.RentArea,r.StartArea,r.RangeArea,
r.Number,r.RentNum,r.StartNum,r.RangeNum,r.Size,r.RentSize,r.StartSize,r.RangeSize,
r.Content,r.Remark,r.Location
from t_order a 
left join T_Resource r on a.ResourceID = r.ID
left join T_Park p on r.ParkID=p.ID
left join T_ResourceKind k on r.ResourceKindID=k.ID
left join T_ResourceType t on r.ResourceTypeID=t.ID;

alter view V_SRM as
SELECT 
A.ID,
A.Name,
A.ParkID,P.Name AS ParkName,
S.ID AS StageID,s.Name AS StageName,
b.ID AS BuildingID,b.Name AS BuildingName,
A.ParentID as FloorID,F.Name AS FloorName,
A.GroupID,G.Name AS GroupName,
A.CustID,M.CustShortName AS CustName,M.CustContactMobile AS CustPhone,
A.ResourceKindID,K.Name  AS ResourceKindName,
A.ResourceTypeID,t.Name AS ResourceTypeName,
A.Status,A.Enable,A.RentBeginTime,
A.RentEndTime,A.Area,A.RentArea
FROM T_Resource A 
LEFT JOIN T_Floor F ON F.ID=A.ParentID
LEFT JOIN T_Building B ON B.ID=F.BuildingID
LEFT JOIN T_STAGE S ON S.ID=B.StageID
LEFT JOIN T_PARK P ON P.ID=A.ParkID
LEFT JOIN T_ResourceGroup G ON G.ID=A.GroupID
LEFT JOIN T_ResourceType T ON T.ID=A.ResourceTypeID
LEFT JOIN T_ResourceKind K ON k.ID=A.ResourceKindID
LEFT JOIN Mstr_Customer M ON A.CustID = M.CustNo
WHERE A.ResourceKindID=1 and a.Enable=1;

alter view V_SCB as
SELECT 
A.ID,
A.ParkID,P.Name AS ParkName,
S.ID AS StageID,S.Name AS StageName,
B.ID AS BuildingID,B.Name AS BuildingName,
F.ID as FloorID,F.Name AS FloorName,
Z.ID AS RoomID,Z.Name AS RoomName,
A.GroupID,G.Name AS GroupName,
A.CustID,M.CustShortName AS CustName,M.CustContactMobile AS CustPhone,
A.ResourceKindID,K.Name  AS ResourceKindName,
A.ResourceTypeID,t.Name AS ResourceTypeName,
A.Status,A.Enable,A.Number,A.RentNum,A.RangeArea,A.RentBeginTime,A.RentEndTime
FROM T_Resource A
LEFT JOIN T_Resource Z ON Z.ID=A.ParentID
LEFT JOIN T_Floor F ON F.ID=Z.ParentID
LEFT JOIN T_Building B ON B.ID=F.BuildingID
LEFT JOIN T_STAGE S ON S.ID=B.StageID
LEFT JOIN T_PARK P ON P.ID=A.ParkID
LEFT JOIN T_ResourceGroup G ON G.ID=A.GroupID
LEFT JOIN T_ResourceType T ON T.ID=A.ResourceTypeID
LEFT JOIN T_ResourceKind K ON k.ID=A.ResourceKindID
LEFT JOIN Mstr_Customer M ON A.CustID = M.CustNo
WHERE A.ResourceKindID=2 and a.Enable=1;

alter view V_SAD as
SELECT 
A.ID,A.Name,
A.ParkID,P.Name AS ParkName,
A.GroupID,G.Name AS GroupName,
A.CustID,M.CustShortName AS CustName,M.CustContactMobile AS CustPhone,
A.ResourceKindID,K.Name  AS ResourceKindName,
A.ResourceTypeID,t.Name AS ResourceTypeName,
A.Status,A.Enable,A.Size,A.RentSize,A.RangeSize,A.Location,
A.RentBeginTime,A.RentEndTime
FROM T_Resource A
LEFT JOIN T_PARK P ON P.ID=A.ParkID
LEFT JOIN T_ResourceGroup G ON G.ID=A.GroupID
LEFT JOIN T_ResourceType T ON T.ID=A.ResourceTypeID
LEFT JOIN T_ResourceKind K ON k.ID=A.ResourceKindID
LEFT JOIN Mstr_Customer M ON A.CustID = M.CustNo
WHERE A.ResourceKindID=4 and a.Enable=1;

alter view V_Resource as
select r.*,
p.Name as ParkName,k.Name as ResourceKindName,t.Name as ResourceTypeName,
g.Name as GroupName,c.CustName
from T_Resource r 
left join T_Park p on r.ParkID=p.ID
left join T_ResourceKind k on r.ResourceKindID=k.ID
left join T_ResourceType t on r.ResourceTypeID=t.ID
left join T_ResourceGroup g on r.GroupID=g.ID
left join Mstr_Customer c on r.CustID=c.CustNo

select * from V_Resource;


declare @col varchar(1000)
set @col=''
select @col=@col+','+CHAR(10)+'r.'+name from syscolumns 
where id=object_id('T_Resource')order by colid;
set @col=stuff(@col,1,1,'')
print @col;


select @col=@col+','+CHAR(10)+'r.'+name from syscolumns 
where id= ('T_Resource')order by colid;

select * from T_Resource r 
left join T_Floor f on f.ID=r.ParentID
left join T_Building b on b.ID=f.BuildingID
left join T_Stage s on s.ID=b.StageID
left join T_Park p on p.ID= r.ParentID
where r.ResourceKindID=1;

select b.Name,SUM(r.RentArea) as Area,COUNT(*) as Number from T_Resource r 
left join T_Floor f on f.ID=r.ParentID
left join T_Building b on b.ID=f.BuildingID
left join T_Stage s on s.ID=b.StageID
left join T_Park p on p.ID= r.ParentID
where r.ResourceKindID=1 and r.Status<>1 group by b.ID,b.Name order by b.ID;

select COUNT(*),
cast((select COUNT(*) from T_Resource where ResourceKindID=3 and Status =1)
/cast(COUNT(*) as decimal(15,4)) as numeric(18,2)) from T_Resource r 
where r.ResourceKindID=3 and r.Status<>1 

group by r.ResourceKindID;

select CAST(1/6.00 as numeric(12,4))


update T_Resource set Status=2 where ResourceKindID =3













































/*
** 房屋查询视图
*/
Create VIEW V_RMView
AS 
SELECT A.*,
G.Name as GroupName,
RT.Name AS RentTypeName,
Case A.RoomStatus 
when '1' then '已租' 
when '2' then '空闲' 
when '3' then '预订' 
when '4' then '预留' end as RSText,
P.Name+'/'+S.Name+'/'+B.Name+'/'+F.Name AS FullPosition,
F.Name AS FloorName,
B.Code as BuildingCode,B.Name as BuildingName,
S.Code as StageCode,S.Name as StageName,
P.Name as ParkName,
R.Code as RegionCode,R.Name as RegionName,
C.Code as CityCode,C.Name as CityName
FROM T_Room A
LEFT JOIN T_RGROUP G ON G.CODE=A.GROUPCODE
LEFT JOIN T_RENTTYPE RT ON RT.ID=A.RENTTYPEID
LEFT JOIN T_Floor F ON F.CODE=A.FLOORCODE
LEFT JOIN T_Building B ON B.CODE=F.BUILDINGCODE
LEFT JOIN T_STAGE S ON S.CODE=B.STAGECODE
LEFT JOIN T_PARK P ON P.CODE=S.PARKCODE
LEFT JOIN T_REGION R ON R.CODE=P.REGIONCODE
LEFT JOIN T_CITY C ON C.CODE=R.CITYCODE;
/*
** 工位查询视图
*/
Create VIEW V_CBView
AS 
SELECT A.*,
G.Name as GroupName,
Case A.CubicleStatus 
when '1' then '已租' 
when '2' then '空闲' 
when '3' then '预订' 
when '4' then '预留' end as RSText,
P.Name+'/'+S.Name+'/'+B.Name+'/'+F.Name as FullPosition,
F.Code as FloorCode,F.Name as FloorName,
B.Code as BuildingCode,B.Name as BuildingName,
S.Code as StageCode,S.Name as StageName,
P.Name as ParkName,
R.Code as RegionCode,R.Name as RegionName,
C.Code as CityCode,C.Name as CityName
FROM T_Cubicle A
LEFT JOIN T_Room TR ON TR.RoomCode=A.RoomCode
LEFT JOIN T_RGROUP G ON G.CODE=A.GROUPCODE
LEFT JOIN T_Floor F ON F.CODE=TR.FLOORCODE
LEFT JOIN T_Building B ON B.CODE=F.BUILDINGCODE
LEFT JOIN T_STAGE S ON S.CODE=B.STAGECODE
LEFT JOIN T_PARK P ON P.CODE=S.PARKCODE
LEFT JOIN T_REGION R ON R.CODE=P.REGIONCODE
LEFT JOIN T_CITY C ON C.CODE=R.CITYCODE;
/*
** 会议室查询视图
*/
Create view V_MRView as
select m.*,p.Name as ParkName,
r.code as RegionCode,r.name as RegionName,
c.code as CityCode,c.name as CityName
from t_meetingroom m
LEFT JOIN T_PARK p ON p.CODE=m.parkcode 
LEFT JOIN T_REGION r ON r.CODE=p.REGIONCODE
LEFT JOIN T_CITY c ON c.CODE=r.CITYCODE;
/*
** 广告位查询视图
*/
Create View V_ADView as
SELECT A.*,
Case A.ADStatus
 when '1' then '已租' 
 when '2' then '空闲' 
 when '3' then '预订' 
 when '4' then '预留' end as RSText,
P.Name AS ParkName,T.Name AS ADTypeName,G.Name AS GroupName
FROM T_AD A
LEFT JOIN T_Park P ON A.ParkCode = P.Code
LEFT JOIN  T_ADType T ON A.ADTypeCode=T.Code
LEFT JOIN T_RGroup G ON A.GroupCode = G.Code
/*
** 房屋发布查询视图
*/
create view V_RMEPView as
select * from v_rmview where RoomCode not in
(select distinct(ResourceCode) from T_Public where PublicStatus in(1,2,3,4,5) and rtypeid=1)
--declare @col varchar(1000)
--set @col=''
--select @col=@col+',r.'+name from syscolumns 
--where id=object_id('V_RoomView') and name<>'updatetime' and name<>'updateuser' order by colid;
--set @col=stuff(@col,1,1,'')
--select @col from T_Public;

create view V_RMAPView as
select p.*,
r.RoomCode,r.RoomName,r.ParkCode,r.FloorCode,r.GroupCode,r.CustCode,r.RoomAddr,r.RoomEnable,r.RoomStatus,
r.RentTypeID,r.RoomArea,r.RentArea,r.RoomContent,r.RentRangeArea,r.RentBeginArea,r.MeterEnable,r.MeterPrice,
r.SingleEnable,r.SinglePrice,r.MonthEnable,r.MonthPrice,r.OnceEnable,r.OncePrice,r.OtherEnable,r.OtherPrice,
r.GroupName,r.RentTypeName,r.RSText,r.FullPosition,r.FloorName,r.BuildingCode,r.BuildingName,r.StageCode,
r.StageName,r.ParkName,r.RegionCode,r.RegionName,r.CityCode,r.CityName
from T_Public p left join v_rmview r on p.ResourceCode=r.RoomCode where rtypeid=1;
--declare @col varchar(1000)
--set @col=''
--select @col=@col+',r.'+name from syscolumns 
--where id=object_id('V_RoomView') and name<>'updatetime' and name<>'updateuser' order by colid;
--set @col=stuff(@col,1,1,'')
--select @col from T_Public;
/*
** 工位发布查询视图
*/
create view V_CBEPView as
select * from v_cbview where cubiclecode not in
	(select distinct(ResourceCode) from T_Public where PublicStatus in(1,2,3,4,5) and rtypeid=2);
--declare @col varchar(1000)
--set @col=''
--select @col=@col+',r.'+name from syscolumns 
--where id=object_id('V_RoomView') and name<>'updatetime' and name<>'updateuser' order by colid;
--set @col=stuff(@col,1,1,'')
--select @col from T_Public;

create view V_CBAPView as
select p.*,
r.CubicleCode,r.CubicleName,r.ParkCode,r.RoomCode,r.GroupCode,r.CustCode,
r.CubicleAddr,r.CubicleEnable,r.CubicleStatus,r.CubicleSeat,r.CubicleUnitPrice,
r.CubicleContent,r.RentRangeSeat,r.RentBeginSeat,r.SingleEnable,r.SinglePrice,
r.OnceEnable,r.OncePrice,r.OtherEnable,r.OtherPrice,r.GroupName,r.RSText,
r.FullPosition,r.FloorCode,r.FloorName,r.BuildingCode,r.BuildingName,r.StageCode,
r.StageName,r.ParkName,r.RegionCode,r.RegionName,r.CityCode,r.CityName
from T_Public p left join v_cbview r on p.ResourceCode=r.cubiclecode where rtypeid=2;
--declare @col varchar(1000)
--set @col=''
--select @col=@col+',r.'+name from syscolumns 
--where id=object_id('V_RoomView') and name<>'updatetime' and name<>'updateuser' order by colid;
--set @col=stuff(@col,1,1,'')
--select @col from T_Public;

/*
** 会议室发布查询视图
*/
create view V_MREPView as
select * from v_mrview where mrcode not in
	(select distinct(ResourceCode) from T_Public where PublicStatus in(1,2,3,4,5) and rtypeid=3);
--declare @col varchar(1000)
--set @col=''
--select @col=@col+',r.'+name from syscolumns 
--where id=object_id('V_RoomView') and name<>'updatetime' and name<>'updateuser' order by colid;
--set @col=stuff(@col,1,1,'')
--select @col from T_Public;

create view V_MRAPView as
select p.*,
r.MRCode,r.MRName,r.ParkCode,r.GroupCode,r.CustCode,r.MRAddr,r.MREnable,r.MRStatus,
r.MRSeat,r.MRArea,r.MRDeposit,r.MRContent,r.DayEnable,r.InDayPrice,r.OutDayPrice,
r.HalfDayEnable,r.InHalfDayPrice,r.OutHalfDayPrice,r.HourEnable,r.InHourPrice,
r.OutHourPrice,r.InDelayPrice,r.OutDelayPrice,r.ParkName,r.RegionCode,r.RegionName,
r.CityCode,r.CityName
from T_Public p left join v_mrview r on p.ResourceCode=r.mrcode where p.rtypeid=3;
--declare @col varchar(1000)
--set @col=''
--select @col=@col+',r.'+name from syscolumns 
--where id=object_id('V_RoomView') and name<>'updatetime' and name<>'updateuser' order by colid;
--set @col=stuff(@col,1,1,'')
--select @col from T_Public;

/*
** 会议室发布查询视图
*/
create view V_ADEPView as
select * from v_adview where adcode not in
	(select distinct(ResourceCode) from T_Public where PublicStatus in(1,2,3,4,5) and rtypeid=4);
--declare @col varchar(1000)
--set @col=''
--select @col=@col+',r.'+name from syscolumns 
--where id=object_id('V_RoomView') and name<>'updatetime' and name<>'updateuser' order by colid;
--set @col=stuff(@col,1,1,'')
--select @col from T_Public;

create view V_ADAPView as
select p.*,
r.ADCode,r.ADName,r.ADTypeCode,r.ParkCode,r.GroupCode,r.CustCode,
r.ADAddr,r.ADEnable,r.ADStatus,r.ADSize,r.ADDeposit,r.ADContent,
r.DayEnable,r.InDayPrice,r.OutDayPrice,r.HalfDayEnable,r.InHalfDayPrice,
r.OutHalfDayPrice,r.WeekEnable,r.InWeekPrice,r.OutWeekPrice,r.MonthEnable,
r.InMonthPrice,r.OutMonthPrice,r.QuarterEnable,r.InQuarterPrice,r.OutQuarterPrice,
r.SingleEnable,r.InSinglePrice,r.OutSinglePrice,r.YearEnable,r.InYearPrice,
r.OutYearPrice,r.RSText,r.ParkName,r.ADTypeName,r.GroupName
from T_Public p left join v_adview r on p.ResourceCode=r.adcode where p.rtypeid=4;
--declare @col varchar(1000)
--set @col=''
--select @col=@col+',r.'+name from syscolumns 
--where id=object_id('V_RoomView') and name<>'updatetime' and name<>'updateuser' order by colid;
--set @col=stuff(@col,1,1,'')
--select @col from T_Public;

create view V_PublicView as
select p.*,
t.Name as RTypeName,
r.*,g.Name as GroupName,k.Name as ParkName from T_Public p left join (
select RoomCode as Code,RoomName as Name,ParkCode,GroupCode from T_Room union
select CubicleCode,CubicleName,ParkCode,GroupCode from T_Cubicle union
select MRCode,MRName,ParkCode,GroupCode from T_MeetingRoom union
select ADCode,ADName,ParkCode,GroupCode from T_AD ) r on p.ResourceCode=r.Code
left join T_Park k on r.ParkCode = k.Code
left join T_RGroup g on r.GroupCode=g.Code
left join T_RType t on p.RTypeID=t.ID;
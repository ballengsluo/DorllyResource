/*
 * 城市表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_City')
	DROP TABLE T_City;
CREATE TABLE T_City(
	ID NVARCHAR(50) PRIMARY KEY,			
	Name NVARCHAR(50),	
	IsDefault BIT DEFAULT(0),
	Enable BIT DEFAULT(1)
);
INSERT INTO T_City VALUES('GDSZ','深圳',1,'1');
INSERT INTO T_City VALUES('GZGY','贵阳',default,'1');
INSERT INTO T_City VALUES('SXXA','西安',default,'1');
INSERT INTO T_City VALUES('QC','重庆',default,'1');

/*
 * 行政区域表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Region')
	DROP TABLE T_Region;
CREATE TABLE T_Region(
	ID NVARCHAR(50) PRIMARY KEY,	
	Name NVARCHAR(50),	
	CityID NVARCHAR(30),
	Enable BIT DEFAULT(1)
);
INSERT INTO T_Region VALUES('001','福田区','GDSZ','1');
INSERT INTO T_Region VALUES('002','龙岗区','GDSZ','1');
INSERT INTO T_Region VALUES('011','高新区','GZGY','1');
INSERT INTO T_Region VALUES('021','高新区','SXXA','1');
INSERT INTO T_Region VALUES('031','南岸区','QC','1');
/*
 * 园区表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Park')
	DROP TABLE T_Park;
CREATE TABLE T_Park(
	ID NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(50),
	FullName NVARCHAR(50),	
	RegionID NVARCHAR(30),	
	Addr NVARCHAR(200),
	GisX NVARCHAR(30),
	GisY NVARCHAR(30),
	ContactsName NVARCHAR(50),
	ContactsPhone NVARCHAR(11),
	ContactsMobile Nvarchar(11),
	ContactsEmail Nvarchar(50),
	ContactsAddr Nvarchar(200),
	ReMark Nvarchar(200),
	Enable BIT DEFAULT(1)
);
select * from DorllyButler.dbo.Mstr_Park;
insert into T_Park(ID,Name,FullName,Addr,GisX,GisY,ContactsName,ContactsPhone,RegionID)
select ParkNo,ShortName,ParkName,Addr,GPS_X,GPS_Y,Contact,ContactTel,
case ShortName 
when '福田园区' then '001'
when '贵阳园区' then '011'
when '西安园区' then '021'
when '龙岗园区' then '002'
when '重庆园区' then '031'
end as ReigonID
from DorllyButler.dbo.Mstr_Park

--INSERT INTO T_Park(ID,Name,RegionID,Addr,GisX,GisY,Enable) VALUES('FTYQ','福田园区','001','深圳市福田区上梅林梅华路105号科技楼一楼服务中心','114.062318','22.56899','1');
--INSERT INTO T_Park(ID,Name,RegionID,Addr,GisX,GisY,Enable) VALUES('GYYQ','贵阳园区','011','贵州省贵阳市高新区长岭路31号','106.658586','26.620337','1');
--INSERT INTO T_Park(ID,Name,RegionID,Addr,GisX,GisY,Enable) VALUES('XAYQ','西安园区','021','陕西省西安市高新区云水一路925号','108.846185','34.222564','1');
--INSERT INTO T_Park(ID,Name,RegionID,Addr,GisX,GisY,Enable) VALUES('LGYQ','龙岗园区','002','广东省深圳市龙岗区佳兴路一号','22.7926492889','114.3045935677','1');
--INSERT INTO T_Park(ID,Name ,RegionID,Addr,GisX,GisY,Enable) VALUES('CQYQ','重庆园区','031','重庆市南岸区南坪街道南城大道199号','106.568984','29.527807','1');
--SELECT * FROM T_Park;
/*
 * 建设期表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Stage')
	DROP TABLE T_Stage;
CREATE TABLE T_Stage(	
	ID NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(50),
	ParkID NVARCHAR(30),
	Enable BIT DEFAULT(1)
);
INSERT INTO T_Stage VALUES('1','一期','01','1');
--delete from T_Stage
/*
 * 建筑表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Building')
	DROP TABLE T_Building;
CREATE TABLE T_Building(	
	ID NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(50),
	StageID NVARCHAR(30),
	Enable BIT DEFAULT(1)
);
INSERT INTO T_Building(ID,Name,StageID) SELECT LOCNo,LOCName,'1' FROM DorllyOrder.DBO.Mstr_Location WHERE LOCLevel=3;

/*
 * 楼层表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Floor')
	DROP TABLE T_Floor;
CREATE TABLE T_Floor(	
	ID NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(50),
	BuildingID NVARCHAR(30),
	Enable BIT DEFAULT(1)
);
INSERT INTO T_Floor(ID,Name,BuildingID) SELECT LOCNo,LOCName,ParentLOCNo FROM DorllyOrder.DBO.Mstr_Location WHERE LOCLevel=4;

SELECT * FROM T_City;
SELECT * FROM T_Region;
SELECT * FROM T_Park;
SELECT * FROM T_Stage;
SELECT * FROM T_Building;
SELECT * FROM T_Floor;


IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Cust')
	DROP TABLE T_Cust;
CREATE TABLE T_Cust(	
	ID INT IDENTITY(1,1) PRIMARY KEY,
	CustNo NVARCHAR(50) UNIQUE,
	CustName NVARCHAR(200),
	CustShortName NVARCHAR(50),
	CustType NVARCHAR(30),
	CustMail NVARCHAR(320),
	CustTel NVARCHAR(30),
	CustAddr NVARCHAR(300),	
	Remark NVARCHAR(3000)
);

insert into T_Cust select a.CustNo,a.CustName,a.CustShortName,a.CustType,'','','','' from DorllyOrder.dbo.Mstr_Customer a;
select * from T_Cust;

with tab as(
	select * from Mstr_Location where LOCNo='FTYQ-1-05-001'
	union all
	select b.* from tab a, Mstr_Location b where a.ParentLOCNo=b.LOCNo
	)
select * from tab;

select * from Mstr_Room;
select * from Op_ContractRMRentalDetail;
select * from Op_Contract;

select a.RMID from (
select d.RowPointer,d.RefRP,d.RMID,c.ContractCustNo,c.ContractStartDate,c.ContractEndDate 
from Op_ContractRMRentalDetail d
left join Op_Contract c on d.RefRP=c.RowPointer
where c.ContractType='01' and c.ContractStartDate<GETDATE() and c.ContractEndDate>GETDATE() and c.ContractStatus=2
) a group by a.RMID having(COUNT(*)>1);

select d.RowPointer,d.RefRP,d.RMID,c.ContractCustNo,c.ContractStartDate,c.ContractEndDate 
from Op_ContractRMRentalDetail d
left join Op_Contract c on d.RefRP=c.RowPointer
where c.ContractType='01' and c.ContractStatus=2 and c.ContractStartDate<GETDATE() and c.ContractEndDate>GETDATE();
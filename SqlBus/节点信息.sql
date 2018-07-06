/*
 * 城市表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_City')
	DROP TABLE T_City;
CREATE TABLE T_City(
	ID INT IDENTITY(1,1) PRIMARY KEY,	
	CityCode NVARCHAR(30) NOT NULL CONSTRAINT U_CityCode UNIQUE,			
	CityName NVARCHAR(50),
	IsDefault BIT DEFAULT(0)
)
INSERT INTO T_City VALUES('GDSZ','深圳',1);
INSERT INTO T_City VALUES('GZGY','贵阳',default);
INSERT INTO T_City VALUES('SXXA','西安',default);
INSERT INTO T_City VALUES('QC','重庆',default);
--SELECT * FROM T_City;
/*
 * 行政区域表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Region')
	DROP TABLE T_Region;
CREATE TABLE T_Region(
	ID INT IDENTITY(1,1) PRIMARY KEY,	
	RegionCode NVARCHAR(30) NOT NULL CONSTRAINT U_RegionCode UNIQUE,
	RegionName NVARCHAR(50),
	CityCode NVARCHAR(30) 
)
INSERT INTO T_Region VALUES('001','福田区','GDSZ');
INSERT INTO T_Region VALUES('002','龙岗区','GDSZ');
INSERT INTO T_Region VALUES('011','高新区','GZGY');
INSERT INTO T_Region VALUES('021','高新区','SXXA');
INSERT INTO T_Region VALUES('031','南岸区','QCNA');
--SELECT * FROM T_Region;
/*
 * 园区表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Park')
	DROP TABLE T_Park;
CREATE TABLE T_Park(
	ID INT IDENTITY(1,1) PRIMARY KEY,	
	ParkCode NVARCHAR(30) NOT NULL CONSTRAINT U_ParkCode UNIQUE,
	RegionCode NVARCHAR(30),
	ParkName NVARCHAR(50),
	ParkShortName NVARCHAR(50),	
	Addr NVARCHAR(200),
	ContactName NVARCHAR(50),
	ContactPhone NVARCHAR(11),
	ContactMobile Nvarchar(11),
	ContactEmail Nvarchar(50),
	ContactAddr Nvarchar(200),
	ContactMark Nvarchar(200),
	GisX NVARCHAR(30),
	GisY NVARCHAR(30)
)
INSERT INTO T_Park(ParkCode,ParkShortName,RegionCode,Addr,GisX,GisY) VALUES('01','福田园区','001','深圳市福田区上梅林梅华路105号科技楼一楼服务中心','114.062318','22.56899');
INSERT INTO T_Park(ParkCode,ParkShortName,RegionCode,Addr,GisX,GisY) VALUES('02','贵阳园区','011','贵州省贵阳市高新区长岭路31号','106.658586','26.620337');
INSERT INTO T_Park(ParkCode,ParkShortName,RegionCode,Addr,GisX,GisY) VALUES('03','西安园区','021','陕西省西安市高新区云水一路925号','108.846185','34.222564');
INSERT INTO T_Park(ParkCode,ParkShortName,RegionCode,Addr,GisX,GisY) VALUES('04','龙岗园区','002','广东省深圳市龙岗区佳兴路一号','22.7926492889','114.3045935677');
INSERT INTO T_Park(ParkCode,ParkShortName ,RegionCode,Addr,GisX,GisY) VALUES('05','重庆园区','031','重庆市南岸区南坪街道南城大道199号','106.568984','29.527807');
--SELECT * FROM T_Park;
/*
 * 建设期表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Stage')
	DROP TABLE T_Stage;
CREATE TABLE T_Stage(
	ID INT IDENTITY(1,1) PRIMARY KEY,	
	StageCode NVARCHAR(10) NOT NULL CONSTRAINT U_StageCode UNIQUE,
	StageName NVARCHAR(50),
	ParkCode NVARCHAR(30)
)
INSERT INTO T_Stage VALUES('1','一期','01');
/*
 * 建筑表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Building')
	DROP TABLE T_Building;
CREATE TABLE T_Building(
	ID INT IDENTITY(1,1) PRIMARY KEY,	
	BuildingCode NVARCHAR(30) NOT NULL CONSTRAINT U_BuildingCode UNIQUE,
	BuildingName NVARCHAR(50),
	StageCode NVARCHAR(30),
	GisX NVARCHAR(30),
	GisY NVARCHAR(30)
)
INSERT INTO T_Building(BuildingCode,BuildingName,StageCode,GisX,GisY)
SELECT LOCNo,LOCName,'1',0,0 FROM DorllyOrder.DBO.Mstr_Location WHERE LOCLevel=3;

/*
 * 楼层表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Floor')
	DROP TABLE T_Floor;
CREATE TABLE T_Floor(
	ID INT IDENTITY(1,1) PRIMARY KEY,	
	FloorCode NVARCHAR(30) NOT NULL CONSTRAINT U_FloorCode UNIQUE,
	FloorName NVARCHAR(50),
	BuildingCode NVARCHAR(30),
)
INSERT INTO T_Floor(FloorCode,FloorName,BuildingCode)
SELECT LOCNo,LOCName,ParentLOCNo FROM DorllyOrder.DBO.Mstr_Location WHERE LOCLevel=4;

SELECT * FROM T_City;
SELECT * FROM T_Region;
SELECT * FROM T_Park;
SELECT * FROM T_Stage;
SELECT * FROM T_Building;
SELECT * FROM T_Floor;

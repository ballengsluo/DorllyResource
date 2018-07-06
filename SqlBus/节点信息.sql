/*
 * ���б�
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_City')
	DROP TABLE T_City;
CREATE TABLE T_City(
	ID INT IDENTITY(1,1) PRIMARY KEY,	
	CityCode NVARCHAR(30) NOT NULL CONSTRAINT U_CityCode UNIQUE,			
	CityName NVARCHAR(50),
	IsDefault BIT DEFAULT(0)
)
INSERT INTO T_City VALUES('GDSZ','����',1);
INSERT INTO T_City VALUES('GZGY','����',default);
INSERT INTO T_City VALUES('SXXA','����',default);
INSERT INTO T_City VALUES('QC','����',default);
--SELECT * FROM T_City;
/*
 * ���������
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Region')
	DROP TABLE T_Region;
CREATE TABLE T_Region(
	ID INT IDENTITY(1,1) PRIMARY KEY,	
	RegionCode NVARCHAR(30) NOT NULL CONSTRAINT U_RegionCode UNIQUE,
	RegionName NVARCHAR(50),
	CityCode NVARCHAR(30) 
)
INSERT INTO T_Region VALUES('001','������','GDSZ');
INSERT INTO T_Region VALUES('002','������','GDSZ');
INSERT INTO T_Region VALUES('011','������','GZGY');
INSERT INTO T_Region VALUES('021','������','SXXA');
INSERT INTO T_Region VALUES('031','�ϰ���','QCNA');
--SELECT * FROM T_Region;
/*
 * ԰����
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
INSERT INTO T_Park(ParkCode,ParkShortName,RegionCode,Addr,GisX,GisY) VALUES('01','����԰��','001','�����и�������÷��÷��·105�ſƼ�¥һ¥��������','114.062318','22.56899');
INSERT INTO T_Park(ParkCode,ParkShortName,RegionCode,Addr,GisX,GisY) VALUES('02','����԰��','011','����ʡ�����и���������·31��','106.658586','26.620337');
INSERT INTO T_Park(ParkCode,ParkShortName,RegionCode,Addr,GisX,GisY) VALUES('03','����԰��','021','����ʡ�����и�������ˮһ·925��','108.846185','34.222564');
INSERT INTO T_Park(ParkCode,ParkShortName,RegionCode,Addr,GisX,GisY) VALUES('04','����԰��','002','�㶫ʡ����������������·һ��','22.7926492889','114.3045935677');
INSERT INTO T_Park(ParkCode,ParkShortName ,RegionCode,Addr,GisX,GisY) VALUES('05','����԰��','031','�������ϰ�����ƺ�ֵ��ϳǴ��199��','106.568984','29.527807');
--SELECT * FROM T_Park;
/*
 * �����ڱ�
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Stage')
	DROP TABLE T_Stage;
CREATE TABLE T_Stage(
	ID INT IDENTITY(1,1) PRIMARY KEY,	
	StageCode NVARCHAR(10) NOT NULL CONSTRAINT U_StageCode UNIQUE,
	StageName NVARCHAR(50),
	ParkCode NVARCHAR(30)
)
INSERT INTO T_Stage VALUES('1','һ��','01');
/*
 * ������
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
 * ¥���
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

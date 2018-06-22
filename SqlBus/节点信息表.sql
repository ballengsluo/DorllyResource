/*
 * ���б�
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_City')
	DROP TABLE T_City;
CREATE TABLE T_City(
	ID INT IDENTITY(1,1) PRIMARY KEY,	
	Code NVARCHAR(30) NOT NULL CONSTRAINT UQ_CITYCODE UNIQUE,			
	Name NVARCHAR(50)
)
INSERT INTO T_City VALUES('GDSZ','����');
INSERT INTO T_City VALUES('GZGY','����');
INSERT INTO T_City VALUES('SXXA','����');
INSERT INTO T_City VALUES('QC','����');

/*
 * ���������
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Region')
	DROP TABLE T_Region;
CREATE TABLE T_Region(
	ID INT IDENTITY(1,1) PRIMARY KEY,	
	Code NVARCHAR(30) NOT NULL CONSTRAINT UQ_REGIONCODE UNIQUE,
	Name NVARCHAR(50),
	CityCode NVARCHAR(30) 
)
INSERT INTO T_Region VALUES('001','������','GDSZ');
INSERT INTO T_Region VALUES('002','������','GDSZ');
INSERT INTO T_Region VALUES('011','������','GZGY');
INSERT INTO T_Region VALUES('021','������','SXXA');
INSERT INTO T_Region VALUES('031','�ϰ���','QC');

/*
 * ԰����
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Park')
	DROP TABLE T_Park;
CREATE TABLE T_Park(
	ID INT IDENTITY(1,1) PRIMARY KEY,	
	Code NVARCHAR(30) NOT NULL CONSTRAINT UQ_PARKCODE UNIQUE,
	Name NVARCHAR(50),
	RegionCode NVARCHAR(30),
	Addr NVARCHAR(200),	
	GisX NVARCHAR(30),
	GisY NVARCHAR(30)
)
INSERT INTO T_Park VALUES('01','����԰��','001','�����и�������÷��÷��·105�ſƼ�¥һ¥��������','114.062318','22.56899');
INSERT INTO T_Park VALUES('02','����԰��','011','����ʡ�����и���������·31��','106.658586','26.620337');
INSERT INTO T_Park VALUES('03','����԰��','021','����ʡ�����и�������ˮһ·925��','108.846185','34.222564');
INSERT INTO T_Park VALUES('04','����԰��','002','�㶫ʡ����������������·һ��','22.7926492889','114.3045935677');
INSERT INTO T_Park VALUES('05','����԰��','031','�������ϰ�����ƺ�ֵ��ϳǴ��199��','106.568984','29.527807');

/*
 * �����ڱ�
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Stage')
	DROP TABLE T_Stage;
CREATE TABLE T_Stage(
	ID INT IDENTITY(1,1) PRIMARY KEY,	
	Code NVARCHAR(10) NOT NULL CONSTRAINT UQ_STAGECODE UNIQUE,				--�����ڱ���	
	Name NVARCHAR(50),														--����������
	ParkCode NVARCHAR(30),													--԰������
)
INSERT INTO T_Stage VALUES('1','һ��','01');
/*
 * ������
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Building')
	DROP TABLE T_Building;
CREATE TABLE T_Building(
	ID INT IDENTITY(1,1) PRIMARY KEY,	
	Code NVARCHAR(30) NOT NULL CONSTRAINT UQ_BUILDCODE UNIQUE,				--��������	
	Name NVARCHAR(50),														--��������
	StageCode NVARCHAR(30),													--�����ڱ���
	GisX NVARCHAR(30),
	GisY NVARCHAR(30)
)
INSERT INTO T_Building(Code,Name,StageCode,GisX,GisY)
SELECT LOCNo,LOCName,'1',0,0 FROM DorllyOrder.DBO.Mstr_Location WHERE LOCLevel=3;

/*
 * ¥���
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Floor')
	DROP TABLE T_Floor;
CREATE TABLE T_Floor(
	ID INT IDENTITY(1,1) PRIMARY KEY,	
	Code NVARCHAR(30) NOT NULL CONSTRAINT UQ_FLOORCODE UNIQUE,				--¥�����	
	Name NVARCHAR(50),														--¥������
	BuildingCode NVARCHAR(30),												--��������
)
INSERT INTO T_Floor(Code,Name,BuildingCode)
SELECT LOCNo,LOCName,ParentLOCNo FROM DorllyOrder.DBO.Mstr_Location WHERE LOCLevel=4;

SELECT * FROM T_City;
SELECT * FROM T_Region;
SELECT * FROM T_Park;
SELECT * FROM T_Stage;
SELECT * FROM T_Building;
SELECT * FROM T_Floor;

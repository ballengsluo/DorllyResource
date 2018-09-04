/*
 * ���б�
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_City')
	DROP TABLE T_City;
CREATE TABLE T_City(
	ID NVARCHAR(50) PRIMARY KEY,			
	Name NVARCHAR(50),	
	IsDefault BIT DEFAULT(0),
	Enable BIT DEFAULT(1)
);
INSERT INTO T_City VALUES('GDSZ','����',1,'1');
INSERT INTO T_City VALUES('GZGY','����',default,'1');
INSERT INTO T_City VALUES('SXXA','����',default,'1');
INSERT INTO T_City VALUES('QC','����',default,'1');

/*
 * ���������
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Region')
	DROP TABLE T_Region;
CREATE TABLE T_Region(
	ID NVARCHAR(50) PRIMARY KEY,	
	Name NVARCHAR(50),	
	CityID NVARCHAR(30),
	Enable BIT DEFAULT(1)
);
INSERT INTO T_Region VALUES('001','������','GDSZ','1');
INSERT INTO T_Region VALUES('002','������','GDSZ','1');
INSERT INTO T_Region VALUES('011','������','GZGY','1');
INSERT INTO T_Region VALUES('021','������','SXXA','1');
INSERT INTO T_Region VALUES('031','�ϰ���','QC','1');
/*
 * ԰����
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
when '����԰��' then '001'
when '����԰��' then '011'
when '����԰��' then '021'
when '����԰��' then '002'
when '����԰��' then '031'
end as ReigonID
from DorllyButler.dbo.Mstr_Park

--INSERT INTO T_Park(ID,Name,RegionID,Addr,GisX,GisY,Enable) VALUES('FTYQ','����԰��','001','�����и�������÷��÷��·105�ſƼ�¥һ¥��������','114.062318','22.56899','1');
--INSERT INTO T_Park(ID,Name,RegionID,Addr,GisX,GisY,Enable) VALUES('GYYQ','����԰��','011','����ʡ�����и���������·31��','106.658586','26.620337','1');
--INSERT INTO T_Park(ID,Name,RegionID,Addr,GisX,GisY,Enable) VALUES('XAYQ','����԰��','021','����ʡ�����и�������ˮһ·925��','108.846185','34.222564','1');
--INSERT INTO T_Park(ID,Name,RegionID,Addr,GisX,GisY,Enable) VALUES('LGYQ','����԰��','002','�㶫ʡ����������������·һ��','22.7926492889','114.3045935677','1');
--INSERT INTO T_Park(ID,Name ,RegionID,Addr,GisX,GisY,Enable) VALUES('CQYQ','����԰��','031','�������ϰ�����ƺ�ֵ��ϳǴ��199��','106.568984','29.527807','1');
--SELECT * FROM T_Park;
/*
 * �����ڱ�
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Stage')
	DROP TABLE T_Stage;
CREATE TABLE T_Stage(	
	ID NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(50),
	ParkID NVARCHAR(30),
	Enable BIT DEFAULT(1)
);
INSERT INTO T_Stage VALUES('1','һ��','01','1');
--delete from T_Stage
/*
 * ������
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
 * ¥���
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
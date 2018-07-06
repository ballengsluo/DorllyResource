
/*
 * ��Դ���
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_RType')
	DROP TABLE T_RType;
CREATE TABLE T_RType(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	ResourceTypeName NVARCHAR(30) NOT NULL
)
INSERT INTO T_RType VALUES('����');
INSERT INTO T_RType VALUES('��λ');
INSERT INTO T_RType VALUES('������');
INSERT INTO T_RType VALUES('���λ');
SELECT * FROM T_RType;
/*
 * ��������
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_RentType')
	DROP TABLE T_RentType;
CREATE TABLE T_RentType(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	RentTypeName NVARCHAR(30) NOT NULL
)
INSERT INTO T_RentType VALUES('д��¥');
INSERT INTO T_RentType VALUES('סլ');
INSERT INTO T_RentType VALUES('�ֿ�');
SELECT * FROM T_RentType;

 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ADType')
	DROP TABLE T_ADType;
CREATE TABLE T_ADType(
	ADTypeCode NVARCHAR(30) PRIMARY KEY,
	ADTypeName NVARCHAR(30) NOT NULL
)
INSERT INTO T_ADType VALUES('DL-GG-001','���ڹ��');
INSERT INTO T_ADType VALUES('DL-GG-002','������');
INSERT INTO T_ADType VALUES('DL-GG-003','�������');
SELECT * FROM T_ADType;
/*
 * ��Դ����
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_RGroup')
	DROP TABLE T_RGroup;
CREATE TABLE T_RGroup(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	GroupCode NVARCHAR(30) Constraint U_GroupCode UNIQUE,
	GroupName NVARCHAR(30),
	ResourceTypeID INT,
	ParkCode NVARCHAR(30),
	Status BIT,
	UpdateTime DATETIME DEFAULT(GETDATE()),
	UpdateUser NVARCHAR(30)	
)
INSERT INTO T_RGroup VALUES('FTYQ-HHD','�𻨵�',1,'FTYQ',1,default,'ADMIN');
select * from T_RGroup;
/*
 * ��Դ�۸�
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_RPrice')
	DROP TABLE T_RPrice;
CREATE TABLE T_RPrice(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	PriceName NVARCHAR(30),
	ResourceCode NVARCHAR(30) NOT NULL,	
	MinPrice DECIMAL(15,4),
	MaxPrice DECIMAL(15,4),
	InPrice DECIMAL(15,4),
	OutPrice DECIMAL(15,4),
	InDelayPrice DECIMAL(15,4),
	OutDelayPrice DECIMAL(15,4),
	UnitName NVARCHAR(10),
	UpdateTime DATETIME DEFAULT(GETDATE()),
	UpdateUser NVARCHAR(30)
)
/*
 * ������Դ
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Room')
	DROP TABLE T_Room;
CREATE TABLE T_Room(
	RoomCode NVARCHAR(30) PRIMARY KEY,
	RoomName NVARCHAR(30),
	ParkCode NVARCHAR(30),
	FloorCode NVARCHAR(30),
	GroupCode NVARCHAR(30),
	CustCode NVARCHAR(30),
	RoomAddr NVARCHAR(300),
	RoomEnable BIT,
	RoomStatus INT,--�⣬�У�������
	RentTypeID INT,
	RoomArea DECIMAL(15,4),	
	RentArea DECIMAL(15,4),
	RoomContent NVARCHAR(MAX),
	RentRangeArea NVARCHAR(20),
	RentBeginArea NVARCHAR(20),
	UpdateTime DATETIME DEFAULT(GETDATE()),
	UpdateUser NVARCHAR(30)	
)
/*
 * ��λ��Դ
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Cubicle')
	DROP TABLE T_Cubicle;
CREATE TABLE T_Cubicle(
	CubicleCode NVARCHAR(30) PRIMARY KEY,
	CubicleName NVARCHAR(30),
	ParkCode NVARCHAR(30),
	RoomCode NVARCHAR(30),
	GroupCode NVARCHAR(30),
	CustCode NVARCHAR(30),
	CubicleAddr NVARCHAR(300),
	CubicleEnable BIT,
	CubicleStatus INT,--�⣬�У�������
	CubicleSeat INT,
	CubicleUnitPrice DECIMAL(15,4),
	CubicleContent NVARCHAR(MAX),
	RentRangeSeat NVARCHAR(20),
	RentBeginSeat NVARCHAR(20),
	UpdateTime DATETIME DEFAULT(GETDATE()),
	UpdateUser NVARCHAR(30)	
)
/*
 * ��������Դ
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_MeetingRoom')
	DROP TABLE T_MeetingRoom;
CREATE TABLE T_MeetingRoom(
	MRCode NVARCHAR(30) PRIMARY KEY,
	MRName NVARCHAR(30),
	ParkCode NVARCHAR(30),
	GroupCode NVARCHAR(30),
	CustCode NVARCHAR(30),
	MRAddr NVARCHAR(300),
	MREnable BIT,
	MRStatus INT,--�⣬�У�������
	MRSeat NVARCHAR(10),
	MRArea DECIMAL(15,4),
	MRDeposit DECIMAL(15,4),
	MRContent NVARCHAR(MAX),
	UpdateTime DATETIME DEFAULT(GETDATE()),
	UpdateUser NVARCHAR(30)	
)
/*
 * ���λ��Դ
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_AD')
	DROP TABLE T_AD;
CREATE TABLE T_AD(
	ADCode NVARCHAR(30) PRIMARY KEY,
	ADName NVARCHAR(30),
	ADTypeCode NVARCHAR(30),
	ParkCode NVARCHAR(30),
	GroupCode NVARCHAR(30),
	CustCode NVARCHAR(30),
	ADAddr NVARCHAR(300),
	ADEnable BIT,
	MRStatus INT,--�⣬�У�������
	ADSize NVARCHAR(30),
	ADDeposit DECIMAL(15,4),
	MRContent NVARCHAR(MAX),
	UpdateTime DATETIME DEFAULT(GETDATE()),
	UpdateUser NVARCHAR(30)	
)

/*
 * ��Դ������
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Public')
	DROP TABLE T_Public;
CREATE TABLE T_Public(
	PublicCode NVARCHAR(50) PRIMARY KEY,
	ResourceCode NVARCHAR(30),
	BeginTime DATETIME,
	EndTime DATETIME,
	PublicChannel INT,--1,���ң�2.������
	PublicLevel INT,
	PublicStatus INT
)

/*
 * ��ԴͼƬ
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_RImage')
	DROP TABLE T_RImage;
CREATE TABLE T_RImage(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	ResourceCode NVARCHAR(30) NOT NULL,
	ImgURL NVARCHAR(300),
	IsCover BIT,
	UpdateTime DATETIME DEFAULT(GETDATE()),
	UpdateUser NVARCHAR(30)
)
/*
 * λ������
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_PagePosition')
	DROP TABLE T_PagePosition;
CREATE TABLE T_PagePosition(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	PPType INT NOT NULL,--1,��ҳ��2���ײ�
	PPName NVARCHAR(50)
)
insert into T_PagePosition values(1,'����ֲ�');
insert into T_PagePosition values(1,'Bannerͼ');
insert into T_PagePosition values(1,'�˵�');
insert into T_PagePosition values(2,'ҳ��ײ�');
insert into T_PagePosition values(2,'��ϵ����');
insert into T_PagePosition values(2,'���ڶ���');

/*
 * ��ҳ
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_HomePage')
	DROP TABLE T_HomePage;
CREATE TABLE T_HomePage(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	CityCode NVARCHAR(30),
	PPID INT NOT NULL,
	Title NVARCHAR(200),
	SubTitle NVARCHAR(200),
	OrderNum INT,
	ImgURL NVARCHAR(300),
	LinkURL NVARCHAR(300),
	Status BIT,
	UpdateTime DATETIME DEFAULT(GETDATE()),
	UpdateUser NVARCHAR(30)
)
/*
 * �ײ�
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_PageFoot')
	DROP TABLE T_PageFoot;
CREATE TABLE T_PageFoot(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	CityCode NVARCHAR(30),
	PPID INT NOT NULL,
	CSEmail NVARCHAR(100),
	CSPhone NVARCHAR(20),
	QQServcie NVARCHAR(18),
	QRCode1 NVARCHAR(200), 
	QRCode2 NVARCHAR(200),
	BusinessPhone NVARCHAR(20),
	ContactPhone NVARCHAR(20),
	ContactAddr NVARCHAR(200),
	Content NVARCHAR(MAX),
	UpdateTime DATETIME DEFAULT(GETDATE()),
	UpdateUser NVARCHAR(30)
)

 
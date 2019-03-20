IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Order')
	DROP TABLE T_Order;
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourcePublic')
	DROP TABLE T_ResourcePublic;
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourceImg')
	DROP TABLE T_ResourceImg;
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourcePrice')
	DROP TABLE T_ResourcePrice;
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Resource')
	DROP TABLE T_Resource;
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourceGroup')
	DROP TABLE T_ResourceGroup;
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourceType')
	DROP TABLE T_ResourceType;
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourceKind')
	DROP TABLE T_ResourceKind;



/*
 * ��Դ���
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourceKind')
	DROP TABLE T_ResourceKind;
CREATE TABLE T_ResourceKind(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(30) NOT NULL
);

/*
 * ��Դ����
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourceType')
	DROP TABLE T_ResourceType;
CREATE TABLE T_ResourceType(
	ID NVARCHAR(30) PRIMARY KEY,
	Name NVARCHAR(30) NOT NULL,
	ResourceKindID INT CONSTRAINT FK_RT_RK REFERENCES T_ResourceKind(ID)	
);


/*
 * ��Դ���
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourceGroup')
	DROP TABLE T_ResourceGroup;
CREATE TABLE T_ResourceGroup(
	ID NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(30),
	ResourceKindID INT NOT NULL CONSTRAINT FK_Group_RKind REFERENCES T_ResourceKind(ID),
	ParkID NVARCHAR(30),
	Enable BIT,
	CreateUser NVARCHAR(50),
	CreateTime DATETIME DEFAULT(GETDATE()),	
	UpdateUser NVARCHAR(50),
	UpdateTime DATETIME DEFAULT(GETDATE())
)
SELECT * FROM T_ResourceGroup;
/*
 * ��Դ
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Resource')
	DROP TABLE T_Resource;
CREATE TABLE T_Resource(
	ID NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(30),
	ParkID NVARCHAR(30),
	ParentID NVARCHAR(30),
	GroupID NVARCHAR(30),
	CustID NVARCHAR(30),
	ResourceKindID INT NOT NULL CONSTRAINT FK_Resource_RKID REFERENCES T_ResourceKind(ID),
	ResourceTypeID NVARCHAR(30),
	UnitPrice DECIMAL(15,4),
	OrderPrice DECIMAL(15,4),
	Deposit DECIMAL(15,4),
	Status INT DEFAULT(2),--1 ���⣬2 ���У�3 Ԥ����4 Ԥ��
	Enable BIT DEFAULT(1),
	--����ʱ��
	RentBeginTime DATETIME,
	RentEndTime DATETIME,
	--���
	Area DECIMAL(15,4),
	RentArea DECIMAL(15,4),
	StartArea DECIMAL(15,4),	
	RangeArea NVARCHAR(50),
	--����
	Number INT,
	RentNum INT,
	StartNum INT,	
	RangeNum NVARCHAR(50),
	--���
	Size NVARCHAR(90),
	RentSize NVARCHAR(90),
	StartSize NVARCHAR(90),	
	RangeSize NVARCHAR(90),
	--����
	Content NVARCHAR(MAX),
	Remark NVARCHAR(500),
	Location NVARCHAR(500),
	CreateUser NVARCHAR(50),
	CreateTime DATETIME DEFAULT(GETDATE()),	
	UpdateUser NVARCHAR(50),
	UpdateTime DATETIME DEFAULT(GETDATE())
);

/*
 * ��ԴͼƬ
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourceImg')
	DROP TABLE T_ResourceImg;
CREATE TABLE T_ResourceImg(
	ID NVARCHAR(50) PRIMARY KEY,
	ResourceID NVARCHAR(50)NOT NULL CONSTRAINT FK_Img_RID REFERENCES T_Resource(ID),
	ImgUrl NVARCHAR(300),
	IsCover BIT
)

/*
 * ��Դ״̬
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourceStatus')
	DROP TABLE T_ResourceStatus;
CREATE TABLE T_ResourceStatus(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	SysID INT,--������Դϵͳ��ţ�1������ϵͳ��2�����ڹܼң�3����Դϵͳ
	RID NVARCHAR(50),--��Դ���
	BSID NVARCHAR(100),--ҵ����
	BSType INT,--ҵ������:1���ͻ�ռ�ã�2���ڲ�ռ��
	BegTime DATETIME,--ʹ�ÿ�ʼʱ��
	EndTime DATETIME,--ʹ�ý���ʱ��
	EndType INT,--�������ͣ�1.����ͣ�ã�2.ΥԼͣ�ã�3.δ��ȡ��
	RealEndTime DATETIME,--ʵ�ʽ���ʱ��
	Company NVARCHAR(200),--��˾����
	ContactName NVARCHAR(100),--��ϵ��
	ContactTel NVARCHAR(20),--��ϵ�˵绰
	UseDept NVARCHAR(100),--ʹ�ò���
	UseName NVARCHAR(30),--ʹ����
	Remark NVARCHAR(500),--��ע
	Enable BIT Default(1),--����״̬��0�����٣�1��ʹ��	
	UpUser NVARCHAR(50),--�����û�
	UpTime DATETIME DEFAULT(GETDATE())--����ʱ��
)

/*
 * ��Դ�۸�
 */
-- IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourcePrice')
--	DROP TABLE T_ResourcePrice;
--CREATE TABLE T_ResourcePrice(
--	ID INT IDENTITY(1,1) PRIMARY KEY,
--	ResourceID NVARCHAR(50) NOT NULL CONSTRAINT FK_Price_RID REFERENCES T_Resource(ID),
--	Code NVARCHAR(20),
--	Name NVARCHAR(30),
--	Model INT,--1 ���ۣ�2 ����ۣ�3 ��Ա�ۣ�4 ΥԼ��
--	Enable BIT DEFAULT(0),
--	Normal DECIMAL(15,4),
--	NormalMin DECIMAL(15,4),
--	NormalMax DECIMAL(15,4),
--	NormalBreak DECIMAL(15,4),
--	VIP DECIMAL(15,4),
--	VIPMin DECIMAL(15,4),
--	VIPMax DECIMAL(15,4),
--	VIPBreak DECIMAL(15,4)
--)
/*
 * ��Դ�۸�
 */
 
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourcePrice')
	DROP TABLE T_ResourcePrice;
CREATE TABLE T_ResourcePrice(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	ResourceID NVARCHAR(50) NOT NULL CONSTRAINT FK_Price_RID REFERENCES T_Resource(ID),
	YearEnable BIT DEFAULT(0),
	YearInPrice DECIMAL(15,4),
	YearOutPrice DECIMAL(15,4),
	QuaterEnable BIT DEFAULT(0),
	QuaterInPrcie DECIMAL(15,4),
	QuaterOutPrice DECIMAL(15,4),
	MonthEnable BIT DEFAULT(0),
	MonthInPrice DECIMAL(15,4),
	MonthOutPrice DECIMAL(15,4),
	WeekEnable BIT DEFAULT(0),
	WeekInPrice DECIMAL(15,4),
	WeekOutPrice DECIMAL(15,4),
	DayEnable BIT DEFAULT(0),
	DayInPrice DECIMAL(15,4),
	DayOutPrice DECIMAL(15,4),
	HDayEnable BIT DEFAULT(0),
	HDayInPrice DECIMAL(15,4),
	HDayOutPrice DECIMAL(15,4),
	HourEnable BIT DEFAULT(0),
	HourInPrice DECIMAL(15,4),
	HourOutPrice DECIMAL(15,4),
	SingleEnable BIT DEFAULT(0),
	SingleInPrice DECIMAL(15,4),
	SingleOutPrice DECIMAL(15,4),
	DelayInPrice DECIMAL(15,4),
	DelayOutPrice DECIMAL(15,4),
	MeterEnable BIT DEFAULT(0),
	MeterMinPrice DECIMAL(15,4),
	MeterMaxPrice DECIMAL(15,4),
	IMonthEnable BIT DEFAULT(0),
	IMonthMinPrice DECIMAL(15,4),
	IMonthMaxPrice DECIMAL(15,4),
	ISingleEnable BIT DEFAULT(0),
	ISingleMinPrice DECIMAL(15,4),
	ISingleMaxPrice DECIMAL(15,4),
	OnceEnable BIT DEFAULT(0),
	OnceMinPrice DECIMAL(15,4),
	OnceMaxPrice DECIMAL(15,4),
	OtherEnable BIT DEFAULT(0),
	OtherMinPrice DECIMAL(15,4),
	OtherMaxPrice DECIMAL(15,4)		
)

/*
 * ��Դ����
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourcePublic')
	DROP TABLE T_ResourcePublic;
CREATE TABLE T_ResourcePublic(
	ID NVARCHAR(50) PRIMARY KEY DEFAULT(NEWID()),
	ResourceID NVARCHAR(50) NOT NULL CONSTRAINT FK_Public_RID REFERENCES T_Resource(ID),
	Level INT,
	BeginTime DATETIME,
	EndTime DATETIME,
	AZWEnable BIT DEFAULT(0),--������
	AZGJEnable BIT DEFAULT(0),--���ڹܼ�
	ZYPTEnable BIT DEFAULT(0),--��Դƽ̨
	Status INT,--1,����ˣ�2���ͨ����3��˲�ͨ����4�ϼܣ�5�¼�, 6 ����
	CreateUser NVARCHAR(50),
	CreateTime DATETIME DEFAULT(GETDATE()),	
	UpdateUser NVARCHAR(50),
	UpdateTime DATETIME DEFAULT(GETDATE())
)

/*
 * ������
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Order')
	DROP TABLE T_Order;
CREATE TABLE T_Order(
	ID NVARCHAR(50) PRIMARY KEY DEFAULT(NEWID()),
	ResourceID NVARCHAR(50) NOT NULL CONSTRAINT FK_Order_RID REFERENCES T_Resource(ID),
	CustID NVARCHAR(50),
	City NVARCHAR(30),
	CustName NVARCHAR(50),
	CustPhone NVARCHAR(15),
	CustAddr NVARCHAR(100),
	CreateTime DateTime,
	BeginTime DATETIME,
	EndTime DateTime,
	Status INT,
	AuthUser NVARCHAR(30)
);
select *from T_Order;
--��Դ�������
INSERT INTO T_ResourceKind VALUES('����');
INSERT INTO T_ResourceKind VALUES('��λ');
INSERT INTO T_ResourceKind VALUES('������');
INSERT INTO T_ResourceKind VALUES('���λ');
SELECT * FROM T_ResourceKind;

--��Դ��������
--INSERT INTO T_ResourceType VALUES('none.rm','��������',1);
--INSERT INTO T_ResourceType VALUES('none.','��������',2);
--INSERT INTO T_ResourceType VALUES(3,'��������',3);
--INSERT INTO T_ResourceType VALUES(4,'��������',4);

INSERT INTO T_ResourceType VALUES(1,'д��¥',1);
INSERT INTO T_ResourceType VALUES(2,'סլ',1);
INSERT INTO T_ResourceType VALUES(3,'�ֿ�',1);
insert into T_ResourceType select WPTypeNo,WPTypeName,2 from DorllyOrder.dbo.Mstr_WorkPlaceType;
insert into T_ResourceType select BBTypeNo,BBTypeName,4 from DorllyOrder.dbo.Mstr_BillboardType;
SELECT * FROM T_ResourceType;

--��Դ����
exec Pro_InsertResourceFromOrder;
SELECT * FROM T_Resource;
delete from T_Resource
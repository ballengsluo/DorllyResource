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
 * 资源类别
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourceKind')
	DROP TABLE T_ResourceKind;
CREATE TABLE T_ResourceKind(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(30) NOT NULL
);

/*
 * 资源类型
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourceType')
	DROP TABLE T_ResourceType;
CREATE TABLE T_ResourceType(
	ID NVARCHAR(30) PRIMARY KEY,
	Name NVARCHAR(30) NOT NULL,
	ResourceKindID INT CONSTRAINT FK_RT_RK REFERENCES T_ResourceKind(ID)	
);


/*
 * 资源组别
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
 * 资源
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
	Status INT DEFAULT(2),--1 已租，2 空闲，3 预定，4 预留
	Enable BIT DEFAULT(1),
	--租赁时间
	RentBeginTime DATETIME,
	RentEndTime DATETIME,
	--面积
	Area DECIMAL(15,4),
	RentArea DECIMAL(15,4),
	StartArea DECIMAL(15,4),	
	RangeArea NVARCHAR(50),
	--数量
	Number INT,
	RentNum INT,
	StartNum INT,	
	RangeNum NVARCHAR(50),
	--规格
	Size NVARCHAR(90),
	RentSize NVARCHAR(90),
	StartSize NVARCHAR(90),	
	RangeSize NVARCHAR(90),
	--其他
	Content NVARCHAR(MAX),
	Remark NVARCHAR(500),
	Location NVARCHAR(500),
	CreateUser NVARCHAR(50),
	CreateTime DATETIME DEFAULT(GETDATE()),	
	UpdateUser NVARCHAR(50),
	UpdateTime DATETIME DEFAULT(GETDATE())
);

/*
 * 资源图片
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
 * 资源状态
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourceStatus')
	DROP TABLE T_ResourceStatus;
CREATE TABLE T_ResourceStatus(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	SysID INT,--数据来源系统编号：1，订单系统，2，艾众管家，3，资源系统
	RID NVARCHAR(50),--资源编号
	BSID NVARCHAR(100),--业务编号
	BSType INT,--业务类型:1，客户占用，2，内部占用
	BegTime DATETIME,--使用开始时间
	EndTime DATETIME,--使用结束时间
	EndType INT,--结束类型：1.到期停用，2.违约停用，3.未用取消
	RealEndTime DATETIME,--实际结束时间
	Company NVARCHAR(200),--公司名称
	ContactName NVARCHAR(100),--联系人
	ContactTel NVARCHAR(20),--联系人电话
	UseDept NVARCHAR(100),--使用部门
	UseName NVARCHAR(30),--使用人
	Remark NVARCHAR(500),--备注
	Enable BIT Default(1),--数据状态：0，销毁，1，使用	
	UpUser NVARCHAR(50),--操作用户
	UpTime DATETIME DEFAULT(GETDATE())--操作时间
)

/*
 * 资源价格
 */
-- IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourcePrice')
--	DROP TABLE T_ResourcePrice;
--CREATE TABLE T_ResourcePrice(
--	ID INT IDENTITY(1,1) PRIMARY KEY,
--	ResourceID NVARCHAR(50) NOT NULL CONSTRAINT FK_Price_RID REFERENCES T_Resource(ID),
--	Code NVARCHAR(20),
--	Name NVARCHAR(30),
--	Model INT,--1 单价；2 区间价；3 会员价；4 违约价
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
 * 资源价格
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
 * 资源发布
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_ResourcePublic')
	DROP TABLE T_ResourcePublic;
CREATE TABLE T_ResourcePublic(
	ID NVARCHAR(50) PRIMARY KEY DEFAULT(NEWID()),
	ResourceID NVARCHAR(50) NOT NULL CONSTRAINT FK_Public_RID REFERENCES T_Resource(ID),
	Level INT,
	BeginTime DATETIME,
	EndTime DATETIME,
	AZWEnable BIT DEFAULT(0),--艾众网
	AZGJEnable BIT DEFAULT(0),--艾众管家
	ZYPTEnable BIT DEFAULT(0),--资源平台
	Status INT,--1,待审核，2审核通过，3审核不通过，4上架，5下架, 6 作废
	CreateUser NVARCHAR(50),
	CreateTime DATETIME DEFAULT(GETDATE()),	
	UpdateUser NVARCHAR(50),
	UpdateTime DATETIME DEFAULT(GETDATE())
)

/*
 * 订单表
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
--资源类别数据
INSERT INTO T_ResourceKind VALUES('房屋');
INSERT INTO T_ResourceKind VALUES('工位');
INSERT INTO T_ResourceKind VALUES('会议室');
INSERT INTO T_ResourceKind VALUES('广告位');
SELECT * FROM T_ResourceKind;

--资源类型数据
--INSERT INTO T_ResourceType VALUES('none.rm','暂无数据',1);
--INSERT INTO T_ResourceType VALUES('none.','暂无数据',2);
--INSERT INTO T_ResourceType VALUES(3,'暂无数据',3);
--INSERT INTO T_ResourceType VALUES(4,'暂无数据',4);

INSERT INTO T_ResourceType VALUES(1,'写字楼',1);
INSERT INTO T_ResourceType VALUES(2,'住宅',1);
INSERT INTO T_ResourceType VALUES(3,'仓库',1);
insert into T_ResourceType select WPTypeNo,WPTypeName,2 from DorllyOrder.dbo.Mstr_WorkPlaceType;
insert into T_ResourceType select BBTypeNo,BBTypeName,4 from DorllyOrder.dbo.Mstr_BillboardType;
SELECT * FROM T_ResourceType;

--资源数据
exec Pro_InsertResourceFromOrder;
SELECT * FROM T_Resource;
delete from T_Resource
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Resource')
	DROP TABLE T_Resource;
CREATE TABLE T_Resource(
	ID NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(30),
	--位置
	Loc1 NVARCHAR(30),--y
	Loc2 NVARCHAR(30),--j
	Loc3 NVARCHAR(30),--l
	Loc4 NVARCHAR(30),--c
	Loc5 NVARCHAR(30),--f
	GroupID NVARCHAR(30),
	RSKindID INT,
	RSTypeID NVARCHAR(30),
	--价格
	Price DECIMAL(15,2),
	MaxPrice DECIMAL(15,2),
	MinPrice DECIMAL(15,2),
	DepPrice DECIMAL(15,2),
	--面积
	Area DECIMAL(15,2),
	RentArea DECIMAL(15,2),
	StartArea DECIMAL(15,2),	
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
	Status INT DEFAULT(2),--1 占用，2 空闲，3 预留
	Enable BIT DEFAULT(1),
	Content NVARCHAR(MAX),
	Remark NVARCHAR(500),
	Addr NVARCHAR(500),
	CrUser NVARCHAR(50),
	CrTime DATETIME DEFAULT(GETDATE()),	
	UpUser NVARCHAR(50),
	UpTime DATETIME DEFAULT(GETDATE())
);

/*
 * 资源状态
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_RS_Status')
	DROP TABLE T_RS_Status;
CREATE TABLE T_RS_Status(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	RSID NVARCHAR(50),--资源外键
	BSID NVARCHAR(100),--业务编号
	BegTime DATETIME,--使用开始时间
	EndTime DATETIME,--使用结束时间
	Enterprise NVARCHAR(300),--企业
	Customer NVARCHAR(300),--客户
	CustTel NVARCHAR(300),--客户电话
	UseType NVARCHAR(300),--使用类型:1，业务占用，2，内部预留，3，外部预留
	DesType NVARCHAR(300),--销毁类型:1，正常使用，2，正常销毁，3，主动销毁
	CrUser NVARCHAR(300),--添加人
	CrTime DATETIME DEFAULT(GETDATE())--添加时间
)


/*
 * 首页
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_HomePage')
	DROP TABLE T_HomePage;
CREATE TABLE T_HomePage(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	CityID NVARCHAR(50) NOT NULL,
	Position INT NOT NULL,--1 banner;2 广告;3 菜单
	Title NVARCHAR(200) NOT NULL,
	SubTitle NVARCHAR(200) NOT NULL,
	OrderNum INT,
	ImgUrl NVARCHAR(300),
	LinkUrl NVARCHAR(300),
	Status INT DEFAULT(1),--1,待审核，2审核通过，3审核不通过，4上架，5下架, 6 作废
	CreateUser NVARCHAR(50),
	CreateTime DATETIME DEFAULT(GETDATE()),	
	UpdateUser NVARCHAR(50),
	UpdateTime DATETIME DEFAULT(GETDATE())
)


/*
 * 底部
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_PageFoot')
	DROP TABLE T_PageFoot;
CREATE TABLE T_PageFoot(
	Position INT NOT NULL PRIMARY KEY,--1 首页底部;2 关于我们;3 关于多丽
	CityID NVARCHAR(30),	
	CSEmail NVARCHAR(100),--客服邮箱
	CSPhone NVARCHAR(20),--客服热线
	CSQQ NVARCHAR(18),--客服QQ
	CompanyEmail NVARCHAR(100),--邮箱
	BusinessPhone NVARCHAR(20),--招商热线
	ContactPhone NVARCHAR(20),--联系电话
	ContactAddr NVARCHAR(200),--联系地址
	Content NVARCHAR(MAX),--自由编辑的内容
	QRCode1 NVARCHAR(200),--二维码1
	QRCode2 NVARCHAR(200),--二维码2 
	CreateUser NVARCHAR(50),
	CreateTime DATETIME DEFAULT(GETDATE()),	
	UpdateUser NVARCHAR(50),
	UpdateTime DATETIME DEFAULT(GETDATE())
);
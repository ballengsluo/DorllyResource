/*
 * 资源图片表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_RImage')
	DROP TABLE T_RImage;
CREATE TABLE T_RImage(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	ResourceCode NVARCHAR(30) NOT NULL,
	ImgURL NVARCHAR(300),
	IsCover BIT
)
/*
 * 首页
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_HomeImage')
	DROP TABLE T_HomeImage;
CREATE TABLE T_HomeImage(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	CityCode NVARCHAR(30),
	HPositionID INT NOT NULL,
	Title NVARCHAR(200),
	SubTitle NVARCHAR(200),
	OrderNum INT,
	ImgURL NVARCHAR(300),
	Status BIT,
	UpdateTime DATETIME DEFAULT(GETDATE()),
	UpdateUser NVARCHAR(30)
)
SELECT * FROM T_City;
insert into T_HomeImage values('GDSZ',1,'AA','BB',1,'',1,default,'admin');
insert into T_HomeImage values('GDSZ',1,'AA','BB',1,'../Content/image/content/201806266366560075635205807307234.jpg',0,default,'admin');
insert into T_HomeImage values('GZGY',2,'CC','DD',1,'',1,default,'admin');
insert into T_HomeImage values('GDSZ',3,'FF','EEE',3,'',1,default,'admin');
insert into T_HomeImage values('GZGY',2,'DSFD','DGFSD',4,'',1,default,'admin');
insert into T_HomeImage values('dd',2,'DSFD','DGFSD',4,'',1,default,'admin');

select * from t_rimage;

/*
 * 首页底部
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_HomeFoot')
	DROP TABLE T_HomeFoot;
CREATE TABLE T_HomeFoot(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	CityCode NVARCHAR(30),
	HPositionID INT NOT NULL,
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

 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_HomePosition')
	DROP TABLE T_HomePosition;
CREATE TABLE T_HomePosition(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	PositionType INT NOT NULL,
	PositionName NVARCHAR(50)
)
insert into T_HomePosition values(1,'广告轮播');
insert into T_HomePosition values(1,'Banner图');
insert into T_HomePosition values(1,'菜单');

insert into T_HomePosition values(2,'网页底部');
insert into T_HomePosition values(2,'关于我们');
insert into T_HomePosition values(2,'关于多丽');

select * from T_HomePosition; 
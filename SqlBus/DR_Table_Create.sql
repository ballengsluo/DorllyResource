USE DORLLYRES
---------------------------- 权限部分 START --------------------------
/*
 * 角色表
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Role')
	DROP TABLE T_Role;
CREATE TABLE T_Role(
	RoleID INT IDENTITY(20001,1) PRIMARY KEY,	--角色编号
	RoleName NVARCHAR(50) NOT NULL,				--角色名称
	RoleDesc NVARCHAR(100)						--角色描述
)
/*
 * 菜单表
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Menu')
	DROP TABLE T_Menu;
CREATE TABLE T_Menu(
	MenuID INT IDENTITY(30001,1) PRIMARY KEY,	--菜单编号
	MenuPID INT,								--父级编号
	MenuName NVARCHAR(50),						--菜单名称
	MenuPath NVARCHAR(200),						--菜单路径
	Level INT,									--菜单层级
	OrderNum INT,								--排序序号
	ClassName NVARCHAR(20),						--样式名称
	FuncCode NVARCHAR(50),						--功能编号
	FuncName NVARCHAR(200)						--功能名称
)

/*
 * 权限表
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Permission')
	DROP TABLE T_Permission;
CREATE TABLE T_Permission(
	PerID INT IDENTITY(1001,1) PRIMARY KEY,	--权限编号
	RoleID INT NOT NULL ,					--角色编号
	MenuID INT NOT NULL,				    --菜单编号
	FuncCode INT							--功能编号
)
ALTER TABLE T_Permission ADD CONSTRAINT FK_ROLE_PER FOREIGN KEY(RoleID) REFERENCES T_ROLE(RoleID)
ALTER TABLE T_Permission ADD CONSTRAINT FK_Menu_PER FOREIGN KEY(MenuID) REFERENCES T_Menu(MenuID)
/*
 * 用户表
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_User')
	DROP TABLE T_User;
CREATE TABLE T_User(
	UserID INT IDENTITY(40001,1) PRIMARY KEY,	--用户编号
	UserName NVARCHAR(50) NOT NULL,				--登录名称
	Password NVARCHAR(100) NOT NULL,			--登录密码
	NickName NVARCHAR(50),						--用户昵称
	RoleID INT NOT NULL,						--角色编号
	Phone NVARCHAR(11),							--手机号码
	Email NVARCHAR(100),						--邮箱地址
	Addr NVARCHAR(200),							--联系地址
	ImgURL NVARCHAR(100),						--头像链接
	CreateDate DATETIME,						--创建时间
	Status BIT									--用户状态
)
ALTER TABLE T_User ADD CONSTRAINT FK_ROLE_User FOREIGN KEY(RoleID) REFERENCES T_ROLE(RoleID)
---------------------------- 权限部分 END ----------------------------

---------------------------- 基础资料部分 START ----------------------

/*
 * 资源表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Resource')
	DROP TABLE T_Resource;
CREATE TABLE T_Resource(
	ID NVARCHAR(30) PRIMARY KEY,			--资源编码	
	Name NVARCHAR(30),						--资源名称
	ParkID NVARCHAR(10),					--园区编码:外键
	FloorID NVARCHAR(30),					--楼层编码：外键
	ShortAddr NVARCHAR(30),					--位置简称：产品介绍页地址
	FullAddr NVARCHAR(30),					--详细位置：产品详细页地址
	SProviderID NVARCHAR(30),				--服务商编码：外键
	RTypeID INT,							--资源类别编码：外键
	RGroupID INT,							--资源分组编码：外键
	OriginalArea DECIMAL(12,4),				--初始面积(建筑面积)
	RentArea DECIMAL(12,4),					--可租面积
	LimitRentArea DECIMAL(12,4),			--起租面积
	RentOutArea DECIMAL(12,4),				--已租面积
	OriginalNum INT,						--初始个数
	RentNum INT,							--可租个数
	LimitRentNum INT,						--起租个数
	RentOutNum INT,							--已租个数
	PersonNum INT,							--容纳人数
	Size NVARCHAR(30),						--尺寸大小（长*宽/长*宽*高）
	Deposit DECIMAL(15,4),					--资源押金
	Position NVARCHAR(200),					--具体位置描述
	Introduce NVARCHAR(MAX),
	Status BIT,								--资源状态
	UpdateTime DATETIME DEFAULT(GETDATE()),	--修改时间
	UpdateUser NVARCHAR(30),				--修改用户
	Rmark NVARCHAR(100)						--备注
	--Feature NVARCHAR(30),					--资源特色
	--Type INT,								--资源类型
	--Detail NVARCHAR(MAX),					--详情资料
	--Matching NVARCHAR(MAX),				--配套资料	
)
ALTER TABLE T_Resource ADD CONSTRAINT FK_TR_RType FOREIGN KEY(RTypeID) REFERENCES T_RType(ID)
--房屋
INSERT INTO T_Resource(ID,Name,ParkID,FloorID,SProviderID,RTypeID,OriginalArea,RentArea)
SELECT RMID,RMNo,'01',rmlocno4,'FWC-003' AS SProviderID,1,RMBuildSize,RMRentSize FROM DorllyOrder.dbo.Mstr_Room;
--工位
INSERT INTO T_Resource(ID,Name,ParkID,SProviderID,RTypeID,RentNum,ShortAddr,FullAddr)
SELECT WPNo,SUBSTRING(WPAddr,CHARINDEX('三',WPAddr),LEN(WPAddr)),'01','FWC-001',2,1,
WPProject,WPAddr FROM DorllyOrder.DBO.Mstr_WorkPlace;
--会议室
INSERT INTO T_Resource(ID,Name,ParkID,SProviderID,RTypeID,PersonNum,RentArea) VALUES('00002','博会中心(含LED屏)','01','FWC-001',3,'300','367.2000');
INSERT INTO T_Resource(ID,Name,ParkID,SProviderID,RTypeID,PersonNum,RentArea,FullAddr) VALUES('00004','火花岛咖啡厅','01','FWC-001',3,'60','90.0000','福田国际电子商务产业园3栋1楼火花岛智慧双创社区');
INSERT INTO T_Resource(ID,Name,ParkID,SProviderID,RTypeID,PersonNum,RentArea,FullAddr) VALUES('00005','火花岛洽谈室','01','FWC-001',3,'6','8.0000','福田国际电子商务产业园3栋1楼火花岛智慧双创社区');
INSERT INTO T_Resource(ID,Name,ParkID,SProviderID,RTypeID,PersonNum,RentArea,ShortAddr) VALUES('00006','火花岛大会议室','01','FWC-001',3,'15','30.0000','火花岛');
select * from T_Resource;




/*
 * 资源图片表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_RImage')
	DROP TABLE T_RImage;
CREATE TABLE T_RImage(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	ResourceID NVARCHAR(30) NOT NULL,
	ImgURL NVARCHAR(300),
	IsCover BIT
)

---------------------------- 基础资料部分 END ------------------------

---------------------------- 业务资料部分 START ----------------------

/*
 * 资源状态表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='ResourceStatus')
	DROP TABLE ResourceStatus;
CREATE TABLE ResourceStatus(
	ResourceID NVARCHAR(30),	--资源编号
	CustID NVARCHAR(30),		--客户编号
	ChannelID INT,				--渠道编号
	BeginTime DATETIME,			--开始时间
	EndTime DATETIME,			--结束时间
	LockStatus BIT,				--锁定状态：0/锁定；1/释放
	Status INT					--资源状态：1/预留；2/在租；3/	
)

/*
 * 资源发布表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='ResourcePublic')
	DROP TABLE ResourcePublic;
CREATE TABLE ResourcePublic(
	ID INT IDENTITY(10001,1) PRIMARY KEY,	--编号
	Code NVARCHAR(50),
	ResourceID NVARCHAR(30),				--资源编号
	BeginTime DATETIME,						--发布开始时间
	EndTime DATETIME,						--发布结束时间
	ChannelID INT,							--渠道编号
	PublicLevel INT,						--发布等级：数字越低等级越高
	Status INT								--发布状态：0/注销；1/使用
)

/*
 * 订单表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='ResourceOrder')
	DROP TABLE ResourceOrder;
CREATE TABLE ResourceOrder(
	OrderID NVARCHAR(30),		--订单编号
	ResourceID NVARCHAR(30),	--资源编号
	TotalPrice DECIMAL(15,2),	--总价
	BeginTime DATETIME,			--租赁开始时间
	EndTime DATETIME,			--租赁结束时间
	CustName NVARCHAR(80),		--租赁姓名
	Phone NVARCHAR(11),			--联系方式
	Email NVARCHAR(80),			--电子邮箱
	Addr NVARCHAR(200),			--通讯地址
	Remark NVARCHAR(200),		--备注
	Status INT					--订单状态：1.审核；2.成功；3.取消
)
---------------------------- 业务资料部分 END ----------------------
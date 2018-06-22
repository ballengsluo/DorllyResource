USE DORLLYRES
---------------------------- Ȩ�޲��� START --------------------------
/*
 * ��ɫ��
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Role')
	DROP TABLE T_Role;
CREATE TABLE T_Role(
	RoleID INT IDENTITY(20001,1) PRIMARY KEY,	--��ɫ���
	RoleName NVARCHAR(50) NOT NULL,				--��ɫ����
	RoleDesc NVARCHAR(100)						--��ɫ����
)
/*
 * �˵���
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Menu')
	DROP TABLE T_Menu;
CREATE TABLE T_Menu(
	MenuID INT IDENTITY(30001,1) PRIMARY KEY,	--�˵����
	MenuPID INT,								--�������
	MenuName NVARCHAR(50),						--�˵�����
	MenuPath NVARCHAR(200),						--�˵�·��
	Level INT,									--�˵��㼶
	OrderNum INT,								--�������
	ClassName NVARCHAR(20),						--��ʽ����
	FuncCode NVARCHAR(50),						--���ܱ��
	FuncName NVARCHAR(200)						--��������
)

/*
 * Ȩ�ޱ�
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Permission')
	DROP TABLE T_Permission;
CREATE TABLE T_Permission(
	PerID INT IDENTITY(1001,1) PRIMARY KEY,	--Ȩ�ޱ��
	RoleID INT NOT NULL ,					--��ɫ���
	MenuID INT NOT NULL,				    --�˵����
	FuncCode INT							--���ܱ��
)
ALTER TABLE T_Permission ADD CONSTRAINT FK_ROLE_PER FOREIGN KEY(RoleID) REFERENCES T_ROLE(RoleID)
ALTER TABLE T_Permission ADD CONSTRAINT FK_Menu_PER FOREIGN KEY(MenuID) REFERENCES T_Menu(MenuID)
/*
 * �û���
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_User')
	DROP TABLE T_User;
CREATE TABLE T_User(
	UserID INT IDENTITY(40001,1) PRIMARY KEY,	--�û����
	UserName NVARCHAR(50) NOT NULL,				--��¼����
	Password NVARCHAR(100) NOT NULL,			--��¼����
	NickName NVARCHAR(50),						--�û��ǳ�
	RoleID INT NOT NULL,						--��ɫ���
	Phone NVARCHAR(11),							--�ֻ�����
	Email NVARCHAR(100),						--�����ַ
	Addr NVARCHAR(200),							--��ϵ��ַ
	ImgURL NVARCHAR(100),						--ͷ������
	CreateDate DATETIME,						--����ʱ��
	Status BIT									--�û�״̬
)
ALTER TABLE T_User ADD CONSTRAINT FK_ROLE_User FOREIGN KEY(RoleID) REFERENCES T_ROLE(RoleID)
---------------------------- Ȩ�޲��� END ----------------------------

---------------------------- �������ϲ��� START ----------------------

/*
 * ��Դ��
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Resource')
	DROP TABLE T_Resource;
CREATE TABLE T_Resource(
	ID NVARCHAR(30) PRIMARY KEY,			--��Դ����	
	Name NVARCHAR(30),						--��Դ����
	ParkID NVARCHAR(10),					--԰������:���
	FloorID NVARCHAR(30),					--¥����룺���
	ShortAddr NVARCHAR(30),					--λ�ü�ƣ���Ʒ����ҳ��ַ
	FullAddr NVARCHAR(30),					--��ϸλ�ã���Ʒ��ϸҳ��ַ
	SProviderID NVARCHAR(30),				--�����̱��룺���
	RTypeID INT,							--��Դ�����룺���
	RGroupID INT,							--��Դ������룺���
	OriginalArea DECIMAL(12,4),				--��ʼ���(�������)
	RentArea DECIMAL(12,4),					--�������
	LimitRentArea DECIMAL(12,4),			--�������
	RentOutArea DECIMAL(12,4),				--�������
	OriginalNum INT,						--��ʼ����
	RentNum INT,							--�������
	LimitRentNum INT,						--�������
	RentOutNum INT,							--�������
	PersonNum INT,							--��������
	Size NVARCHAR(30),						--�ߴ��С����*��/��*��*�ߣ�
	Deposit DECIMAL(15,4),					--��ԴѺ��
	Position NVARCHAR(200),					--����λ������
	Introduce NVARCHAR(MAX),
	Status BIT,								--��Դ״̬
	UpdateTime DATETIME DEFAULT(GETDATE()),	--�޸�ʱ��
	UpdateUser NVARCHAR(30),				--�޸��û�
	Rmark NVARCHAR(100)						--��ע
	--Feature NVARCHAR(30),					--��Դ��ɫ
	--Type INT,								--��Դ����
	--Detail NVARCHAR(MAX),					--��������
	--Matching NVARCHAR(MAX),				--��������	
)
ALTER TABLE T_Resource ADD CONSTRAINT FK_TR_RType FOREIGN KEY(RTypeID) REFERENCES T_RType(ID)
--����
INSERT INTO T_Resource(ID,Name,ParkID,FloorID,SProviderID,RTypeID,OriginalArea,RentArea)
SELECT RMID,RMNo,'01',rmlocno4,'FWC-003' AS SProviderID,1,RMBuildSize,RMRentSize FROM DorllyOrder.dbo.Mstr_Room;
--��λ
INSERT INTO T_Resource(ID,Name,ParkID,SProviderID,RTypeID,RentNum,ShortAddr,FullAddr)
SELECT WPNo,SUBSTRING(WPAddr,CHARINDEX('��',WPAddr),LEN(WPAddr)),'01','FWC-001',2,1,
WPProject,WPAddr FROM DorllyOrder.DBO.Mstr_WorkPlace;
--������
INSERT INTO T_Resource(ID,Name,ParkID,SProviderID,RTypeID,PersonNum,RentArea) VALUES('00002','��������(��LED��)','01','FWC-001',3,'300','367.2000');
INSERT INTO T_Resource(ID,Name,ParkID,SProviderID,RTypeID,PersonNum,RentArea,FullAddr) VALUES('00004','�𻨵�������','01','FWC-001',3,'60','90.0000','������ʵ��������ҵ԰3��1¥�𻨵��ǻ�˫������');
INSERT INTO T_Resource(ID,Name,ParkID,SProviderID,RTypeID,PersonNum,RentArea,FullAddr) VALUES('00005','�𻨵�Ǣ̸��','01','FWC-001',3,'6','8.0000','������ʵ��������ҵ԰3��1¥�𻨵��ǻ�˫������');
INSERT INTO T_Resource(ID,Name,ParkID,SProviderID,RTypeID,PersonNum,RentArea,ShortAddr) VALUES('00006','�𻨵��������','01','FWC-001',3,'15','30.0000','�𻨵�');
select * from T_Resource;




/*
 * ��ԴͼƬ��
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_RImage')
	DROP TABLE T_RImage;
CREATE TABLE T_RImage(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	ResourceID NVARCHAR(30) NOT NULL,
	ImgURL NVARCHAR(300),
	IsCover BIT
)

---------------------------- �������ϲ��� END ------------------------

---------------------------- ҵ�����ϲ��� START ----------------------

/*
 * ��Դ״̬��
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='ResourceStatus')
	DROP TABLE ResourceStatus;
CREATE TABLE ResourceStatus(
	ResourceID NVARCHAR(30),	--��Դ���
	CustID NVARCHAR(30),		--�ͻ����
	ChannelID INT,				--�������
	BeginTime DATETIME,			--��ʼʱ��
	EndTime DATETIME,			--����ʱ��
	LockStatus BIT,				--����״̬��0/������1/�ͷ�
	Status INT					--��Դ״̬��1/Ԥ����2/���⣻3/	
)

/*
 * ��Դ������
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='ResourcePublic')
	DROP TABLE ResourcePublic;
CREATE TABLE ResourcePublic(
	ID INT IDENTITY(10001,1) PRIMARY KEY,	--���
	Code NVARCHAR(50),
	ResourceID NVARCHAR(30),				--��Դ���
	BeginTime DATETIME,						--������ʼʱ��
	EndTime DATETIME,						--��������ʱ��
	ChannelID INT,							--�������
	PublicLevel INT,						--�����ȼ�������Խ�͵ȼ�Խ��
	Status INT								--����״̬��0/ע����1/ʹ��
)

/*
 * ������
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='ResourceOrder')
	DROP TABLE ResourceOrder;
CREATE TABLE ResourceOrder(
	OrderID NVARCHAR(30),		--�������
	ResourceID NVARCHAR(30),	--��Դ���
	TotalPrice DECIMAL(15,2),	--�ܼ�
	BeginTime DATETIME,			--���޿�ʼʱ��
	EndTime DATETIME,			--���޽���ʱ��
	CustName NVARCHAR(80),		--��������
	Phone NVARCHAR(11),			--��ϵ��ʽ
	Email NVARCHAR(80),			--��������
	Addr NVARCHAR(200),			--ͨѶ��ַ
	Remark NVARCHAR(200),		--��ע
	Status INT					--����״̬��1.��ˣ�2.�ɹ���3.ȡ��
)
---------------------------- ҵ�����ϲ��� END ----------------------
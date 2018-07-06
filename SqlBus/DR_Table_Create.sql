USE DORLLYRES
---------------------------- Ȩ�޲��� START --------------------------
/*
 * ��ɫ��
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Role')
	DROP TABLE T_Role;
CREATE TABLE T_Role(
	ID INT IDENTITY(20001,1) PRIMARY KEY,		--��ɫ���
	RoleName NVARCHAR(50) NOT NULL,				--��ɫ����
	RoleDesc NVARCHAR(100)						--��ɫ����
)
/*
 * �˵���
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Menu')
	DROP TABLE T_Menu;
CREATE TABLE T_Menu(
	ID INT IDENTITY(30001,1) PRIMARY KEY,	--�˵����
	PID INT,								--�������
	MenuName NVARCHAR(50),						--�˵�����
	MenuPath NVARCHAR(200),						--�˵�·��
	MenuLevel INT,									--�˵��㼶
	OrderNum INT,								--�������
	ClassName NVARCHAR(20),						--��ʽ����
	FuncCode NVARCHAR(500),						--���ܱ��
	FuncName NVARCHAR(500)						--��������
)

/*
 * Ȩ�ޱ�
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Permission')
	DROP TABLE T_Permission;
CREATE TABLE T_Permission(
	ID INT IDENTITY(1001,1) PRIMARY KEY,	--Ȩ�ޱ��
	RoleID INT NOT NULL ,					--��ɫ���
	MenuID INT NOT NULL,				    --�˵����
	FuncCode NVARCHAR(500)					--���ܱ��
)

/*
 * �û���
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_User')
	DROP TABLE T_User;
CREATE TABLE T_User(
	ID INT IDENTITY(40001,1) PRIMARY KEY,	--�û����
	UserName NVARCHAR(50) NOT NULL,				--��¼����
	Password NVARCHAR(100) NOT NULL,			--��¼����
	NickName NVARCHAR(50),						--�û��ǳ�
	RoleID INT NOT NULL CONSTRAINT FK_ROLE_User REFERENCES T_ROLE(ID),--��ɫ���
	Phone NVARCHAR(11),							--�ֻ�����
	Email NVARCHAR(100),						--�����ַ
	Addr NVARCHAR(200),							--��ϵ��ַ
	ImgURL NVARCHAR(100),						--ͷ������
	CreateDate DATETIME,						--����ʱ��
	Status BIT									--�û�״̬
)
---------------------------- Ȩ�޲��� END ----------------------------

---------------------------- �������ϲ��� START ----------------------

/*
 * ��Դ��
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Resource')
	DROP TABLE T_Resource;
CREATE TABLE T_Resource(
	ResrouceCode NVARCHAR(30) PRIMARY KEY,			--��Դ����	
	ResourceName NVARCHAR(30),						--��Դ����
	ParkCode NVARCHAR(10),					--԰������:���
	FloorCode NVARCHAR(30),					--¥����룺���
	ShortAddr NVARCHAR(30),					--λ�ü�ƣ���Ʒ����ҳ��ַ
	FullAddr NVARCHAR(30),					--��ϸλ�ã���Ʒ��ϸҳ��ַ
	SProviderCode NVARCHAR(30),				--�����̱��룺���
	RTypeID INT CONSTRAINT FK_Resrouce_RType REFERENCES T_RType(ID),
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
	Content NVARCHAR(MAX),
	Status BIT,								--��Դ״̬
	UpdateTime DATETIME DEFAULT(GETDATE()),	--�޸�ʱ��
	UpdateUser NVARCHAR(30),				--�޸��û�
	Rmark NVARCHAR(100)						--��ע
	--Feature NVARCHAR(30),					--��Դ��ɫ
	--Type INT,								--��Դ����
	--Detail NVARCHAR(MAX),					--��������
	--Matching NVARCHAR(MAX),				--��������	
)
--����
INSERT INTO T_Resource(ResrouceCode,ResourceName,ParkCode,FloorCode,SProviderCode,RTypeID,OriginalArea,RentArea)
SELECT RMID,RMNo,'01',rmlocno4,'FWC-003' AS SProviderCode,1,RMBuildSize,RMRentSize FROM DorllyOrder.dbo.Mstr_Room;
--��λ
INSERT INTO T_Resource(ResrouceCode,ResourceName,ParkCode,SProviderCode,RTypeID,RentNum,ShortAddr,FullAddr)
SELECT WPNo,SUBSTRING(WPAddr,CHARINDEX('��',WPAddr),LEN(WPAddr)),'01','FWC-001',2,1,
WPProject,WPAddr FROM DorllyOrder.DBO.Mstr_WorkPlace;
--������
INSERT INTO T_Resource(ResrouceCode,ResourceName,ParkCode,SProviderCode,RTypeID,PersonNum,RentArea) VALUES('00002','��������(��LED��)','01','FWC-001',3,'300','367.2000');
INSERT INTO T_Resource(ResrouceCode,ResourceName,ParkCode,SProviderCode,RTypeID,PersonNum,RentArea,FullAddr) VALUES('00004','�𻨵�������','01','FWC-001',3,'60','90.0000','������ʵ��������ҵ԰3��1¥�𻨵��ǻ�˫������');
INSERT INTO T_Resource(ResrouceCode,ResourceName,ParkCode,SProviderCode,RTypeID,PersonNum,RentArea,FullAddr) VALUES('00005','�𻨵�Ǣ̸��','01','FWC-001',3,'6','8.0000','������ʵ��������ҵ԰3��1¥�𻨵��ǻ�˫������');
INSERT INTO T_Resource(ResrouceCode,ResourceName,ParkCode,SProviderCode,RTypeID,PersonNum,RentArea,ShortAddr) VALUES('00006','�𻨵��������','01','FWC-001',3,'15','30.0000','�𻨵�');
select * from T_Resource;






---------------------------- �������ϲ��� END ------------------------

---------------------------- ҵ�����ϲ��� START ----------------------

/*
 * ��Դ״̬��
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_RStatus')
	DROP TABLE T_RStatus;
CREATE TABLE T_RStatus(
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
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Public')
	DROP TABLE T_Public;
CREATE TABLE T_Public(
	PublicCode NVARCHAR(50) PRIMARY KEY,
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
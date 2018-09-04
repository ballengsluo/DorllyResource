
/*
 * ��ҳ
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_HomePage')
	DROP TABLE T_HomePage;
CREATE TABLE T_HomePage(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	CityID NVARCHAR(50) NOT NULL,
	Position INT NOT NULL,--1 banner;2 ���;3 �˵�
	Title NVARCHAR(200) NOT NULL,
	SubTitle NVARCHAR(200) NOT NULL,
	OrderNum INT,
	ImgUrl NVARCHAR(300),
	LinkUrl NVARCHAR(300),
	Status INT DEFAULT(1),--1,����ˣ�2���ͨ����3��˲�ͨ����4�ϼܣ�5�¼�, 6 ����
	CreateUser NVARCHAR(50),
	CreateTime DATETIME DEFAULT(GETDATE()),	
	UpdateUser NVARCHAR(50),
	UpdateTime DATETIME DEFAULT(GETDATE())
)


/*
 * �ײ�
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_PageFoot')
	DROP TABLE T_PageFoot;
CREATE TABLE T_PageFoot(
	Position INT NOT NULL PRIMARY KEY,--1 ��ҳ�ײ�;2 ��������;3 ���ڶ���
	CityID NVARCHAR(30),	
	CSEmail NVARCHAR(100),--�ͷ�����
	CSPhone NVARCHAR(20),--�ͷ�����
	CSQQ NVARCHAR(18),--�ͷ�QQ
	CompanyEmail NVARCHAR(100),--����
	BusinessPhone NVARCHAR(20),--��������
	ContactPhone NVARCHAR(20),--��ϵ�绰
	ContactAddr NVARCHAR(200),--��ϵ��ַ
	Content NVARCHAR(MAX),--���ɱ༭������
	QRCode1 NVARCHAR(200),--��ά��1
	QRCode2 NVARCHAR(200),--��ά��2 
	CreateUser NVARCHAR(50),
	CreateTime DATETIME DEFAULT(GETDATE()),	
	UpdateUser NVARCHAR(50),
	UpdateTime DATETIME DEFAULT(GETDATE())
);
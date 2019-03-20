IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Resource')
	DROP TABLE T_Resource;
CREATE TABLE T_Resource(
	ID NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(30),
	--λ��
	Loc1 NVARCHAR(30),--y
	Loc2 NVARCHAR(30),--j
	Loc3 NVARCHAR(30),--l
	Loc4 NVARCHAR(30),--c
	Loc5 NVARCHAR(30),--f
	GroupID NVARCHAR(30),
	RSKindID INT,
	RSTypeID NVARCHAR(30),
	--�۸�
	Price DECIMAL(15,2),
	MaxPrice DECIMAL(15,2),
	MinPrice DECIMAL(15,2),
	DepPrice DECIMAL(15,2),
	--���
	Area DECIMAL(15,2),
	RentArea DECIMAL(15,2),
	StartArea DECIMAL(15,2),	
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
	Status INT DEFAULT(2),--1 ռ�ã�2 ���У�3 Ԥ��
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
 * ��Դ״̬
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_RS_Status')
	DROP TABLE T_RS_Status;
CREATE TABLE T_RS_Status(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	RSID NVARCHAR(50),--��Դ���
	BSID NVARCHAR(100),--ҵ����
	BegTime DATETIME,--ʹ�ÿ�ʼʱ��
	EndTime DATETIME,--ʹ�ý���ʱ��
	Enterprise NVARCHAR(300),--��ҵ
	Customer NVARCHAR(300),--�ͻ�
	CustTel NVARCHAR(300),--�ͻ��绰
	UseType NVARCHAR(300),--ʹ������:1��ҵ��ռ�ã�2���ڲ�Ԥ����3���ⲿԤ��
	DesType NVARCHAR(300),--��������:1������ʹ�ã�2���������٣�3����������
	CrUser NVARCHAR(300),--�����
	CrTime DATETIME DEFAULT(GETDATE())--���ʱ��
)


/*
 * ��Դ�۸��
 * ģʽһ������������
 * ģʽ�������������
 * ģʽ�������������
 * ģʽ�ģ����������
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_RPrice')
	DROP TABLE T_RPrice;
CREATE TABLE T_RPrice(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	ResourceCode NVARCHAR(30) NOT NULL,
	Name NVARCHAR(30),
	Model INT NOT NULL,
	OverPrice DECIMAL(15,4),
	Price DECIMAL(15,4),	
	MinPrice DECIMAL(15,4),
	MaxPrice DECIMAL(15,4),
	DelayPrice DECIMAL(15,4),
	InPrice DECIMAL(15,4),
	InMinPrice DECIMAL(15,4),
	InMaxPrice DECIMAL(15,4),
	InDelayPrice DECIMAL(15,4),
	OutPrice DECIMAL(15,4),
	OutMinPrice DECIMAL(15,4),
	OutMaxPrice DECIMAL(15,4),
	OutDelayPrice DECIMAL(15,4),
	UnitCode NVARCHAR(10) CONSTRAINT FK_RPrice_Unit FOREIGN KEY REFERENCES T_Unit(UnitCode),
	UpdateTime DATETIME DEFAULT(GETDATE()),
	UpdateUser NVARCHAR(30)
)
--������
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00002','ȫ��Ԥ��','6000.0000','7000.0000','Day',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00004','ȫ��Ԥ��','2000.0000','3000.0000','Day',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00005','ȫ��Ԥ��','200.0000','300.0000','Day',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00006','ȫ��Ԥ��','400.0000','600.0000','Day',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00002','����Ԥ��','3000.0000','4000.0000','HalfDay',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00004','����Ԥ��','1000.0000','1500.0000','HalfDay',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00005','����Ԥ��','100.0000','150.0000','HalfDay',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00006','����Ԥ��','200.0000','300.0000','HalfDay',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00002','СʱԤ��','500.0000','500.0000','Hour',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00004','СʱԤ��','330.0000','500.0000','Hour',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00005','СʱԤ��','30.0000','50.0000','Hour',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00006','СʱԤ��','50.0000','100.0000','Hour',3)
select * from T_RPrice;

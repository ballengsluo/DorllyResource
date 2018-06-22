/*
 * ��Դ��׼�Ƽ۱�
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_StandardPrice')
	DROP TABLE T_StandardPrice;
CREATE TABLE T_StandardPrice(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(30),
	RTypeID INT,
	Price DECIMAL(15,4),						
	MinPrice DECIMAL(15,4),
	MaxPrice DECIMAL(15,4),
	InPrice DECIMAL(15,4),
	InMinPrice DECIMAL(15,4),
	InMaXPrice DECIMAL(15,4),
	OutPice DECIMAL(15,4),
	OutMinPrice DECIMAL(15,4),
	OutMaxPrice DECIMAL(15,4),
	UnitCode NVARCHAR(10),
	UpdateTime DATETIME DEFAULT(GETDATE()),
	UpdateUser NVARCHAR(30)
)
INSERT INTO T_StandardPrice VALUES('��/�O','1','','','admin');
INSERT INTO T_StandardPrice VALUES('��/��','1','','','admin');
INSERT INTO T_StandardPrice VALUES('��/��','1','','','admin');

INSERT INTO T_StandardPrice VALUES('��/��/��','2','','','admin');
INSERT INTO T_StandardPrice VALUES('��/��','2','','','admin');

INSERT INTO T_StandardPrice VALUES('ȫ��Ԥ��','3','','','admin');
INSERT INTO T_StandardPrice VALUES('����Լ��','3','','','admin');
INSERT INTO T_StandardPrice VALUES('СʱԤ��','3','','','admin');

INSERT INTO T_StandardPrice VALUES('����Ԥ��','4','','','admin');
INSERT INTO T_StandardPrice VALUES('����Ԥ��','4','','','admin');
INSERT INTO T_StandardPrice VALUES('����Ԥ��','4','','','admin');
INSERT INTO T_StandardPrice VALUES('����Ԥ��','4','','','admin');
INSERT INTO T_StandardPrice VALUES('ȫ��Ԥ��','4','','','admin');
INSERT INTO T_StandardPrice VALUES('����Ԥ��','4','','','admin');
/*
 * 资源基准计价表
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
INSERT INTO T_StandardPrice VALUES('按/㎡','1','','','admin');
INSERT INTO T_StandardPrice VALUES('按/个','1','','','admin');
INSERT INTO T_StandardPrice VALUES('按/月','1','','','admin');

INSERT INTO T_StandardPrice VALUES('按/个/月','2','','','admin');
INSERT INTO T_StandardPrice VALUES('按/月','2','','','admin');

INSERT INTO T_StandardPrice VALUES('全天预定','3','','','admin');
INSERT INTO T_StandardPrice VALUES('半天约定','3','','','admin');
INSERT INTO T_StandardPrice VALUES('小时预定','3','','','admin');

INSERT INTO T_StandardPrice VALUES('按年预定','4','','','admin');
INSERT INTO T_StandardPrice VALUES('季度预定','4','','','admin');
INSERT INTO T_StandardPrice VALUES('按月预定','4','','','admin');
INSERT INTO T_StandardPrice VALUES('按周预定','4','','','admin');
INSERT INTO T_StandardPrice VALUES('全天预定','4','','','admin');
INSERT INTO T_StandardPrice VALUES('半天预定','4','','','admin');
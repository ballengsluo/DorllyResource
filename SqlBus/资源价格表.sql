
/*
 * 资源价格表
 * 模式一：正常基本价
 * 模式二：正常区间价
 * 模式三：内外基本价
 * 模式四：内外区间价
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
--会议室
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00002','全天预定','6000.0000','7000.0000','Day',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00004','全天预定','2000.0000','3000.0000','Day',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00005','全天预定','200.0000','300.0000','Day',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00006','全天预定','400.0000','600.0000','Day',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00002','半天预定','3000.0000','4000.0000','HalfDay',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00004','半天预定','1000.0000','1500.0000','HalfDay',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00005','半天预定','100.0000','150.0000','HalfDay',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00006','半天预定','200.0000','300.0000','HalfDay',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00002','小时预定','500.0000','500.0000','Hour',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00004','小时预定','330.0000','500.0000','Hour',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00005','小时预定','30.0000','50.0000','Hour',3)
INSERT INTO T_RPrice(ResourceCode,Name,InPrice,OutPrice,UnitCode,Model) VALUES('00006','小时预定','50.0000','100.0000','Hour',3)
select * from T_RPrice;

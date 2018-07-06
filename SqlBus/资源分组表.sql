/*
 * ��Դ�����
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_RGroup')
	DROP TABLE T_RGroup;
CREATE TABLE T_RGroup(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	GroupCode NVARCHAR(30) UNIQUE,
	GroupName NVARCHAR(30),
	RTypeID INT,
	ParkCode NVARCHAR(30),
	Status BIT,
	UpdateTime DATETIME DEFAULT(GETDATE()),
	UpdateUser NVARCHAR(30)	
)
ALTER TABLE T_RGroup ADD CONSTRAINT FK_RG_RType FOREIGN KEY(RTypeID) REFERENCES T_RType(ID)
INSERT INTO T_RGroup VALUES('FZ-WE','���Է���',1,'FTYQ',1,default,'admin');
INSERT INTO T_RGroup VALUES('FZ-WEda','���Է���',2,'FTYQ',1,default,'admin');
select * from T_RGroup;
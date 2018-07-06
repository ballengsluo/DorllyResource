
/*
 * 资源发布表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Public')
	DROP TABLE T_Public;
CREATE TABLE T_Public(
	PublicCode NVARCHAR(30) PRIMARY KEY,
	ResourceCode NVARCHAR(30),
	BeginTime DATETIME,
	EndTime DATETIME,
	ChannelID INT,
	PublicLevel INT,
	PublicEnable BIT,
	UpdateTime DATETIME DEFAULT(GETDATE()),
	UpdateUser NVARCHAR(30)	
)
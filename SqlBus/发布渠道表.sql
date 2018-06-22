/*
 * 发布渠道表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_RChannel')
	DROP TABLE T_RChannel;
CREATE TABLE T_RChannel(
	ID INT IDENTITY(1,1) PRIMARY KEY,		--渠道编号
	Name NVARCHAR(50),						--渠道名称
	URL NVARCHAR(300),						--渠道接口
	Status BIT,								--渠道状态
	UpdateTime DATETIME,					--修改时间
	UpdateUser NVARCHAR(30)					--修改用户
)
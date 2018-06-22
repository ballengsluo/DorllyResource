/*
 * 合作伙伴表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Partner')
	DROP TABLE T_Partner;
CREATE TABLE T_Partner(
	ID NVARCHAR(30),						--伙伴编号
	Name NVARCHAR(80),						--伙伴名称
	ShortName NVARCHAR(20),					--伙伴简称
	TEL NVARCHAR(20),						--联系电话
	HomeURL NVARCHAR(100),					--官网URL
	LogoURL NVARCHAR(100),					--logo图片URL
	Status Bit,								--伙伴状态
	UpdateTime DATETIME DEFAULT(GETDATE()),	--修改时间
	UpdateUser NVARCHAR(30)					--修改用户	
)
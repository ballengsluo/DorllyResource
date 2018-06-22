/*
 * 服务商表
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_SProvider')
	DROP TABLE T_SProvider;
CREATE TABLE T_SProvider(
	ID NVARCHAR(30) PRIMARY KEY,				--服务商编号
	Name NVARCHAR(80),							--服务商名称
	ShortName Nvarchar(20),						--服务商简称
	Tel Nvarchar(20),							--联系方式
	Status Bit,									--服务商状态
	UpdateTime DATETIME DEFAULT(GETDATE()),		--修改时间
	UpdateUser NVARCHAR(30)						--修改用户	
)
INSERT INTO T_SProvider
Select a.SPNo as id,a.SPName as name,a.SPShortName as shortname,a.SPTel as tel, 
1 as status,getdate() as updatetime,'admin' as updateuser
from DorllyOrder.dbo.Mstr_ServiceProvider a;
SELECT * FROM T_SProvider;
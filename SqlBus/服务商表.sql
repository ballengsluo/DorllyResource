/*
 * �����̱�
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_SProvider')
	DROP TABLE T_SProvider;
CREATE TABLE T_SProvider(
	ID NVARCHAR(30) PRIMARY KEY,				--�����̱��
	Name NVARCHAR(80),							--����������
	ShortName Nvarchar(20),						--�����̼��
	Tel Nvarchar(20),							--��ϵ��ʽ
	Status Bit,									--������״̬
	UpdateTime DATETIME DEFAULT(GETDATE()),		--�޸�ʱ��
	UpdateUser NVARCHAR(30)						--�޸��û�	
)
INSERT INTO T_SProvider
Select a.SPNo as id,a.SPName as name,a.SPShortName as shortname,a.SPTel as tel, 
1 as status,getdate() as updatetime,'admin' as updateuser
from DorllyOrder.dbo.Mstr_ServiceProvider a;
SELECT * FROM T_SProvider;
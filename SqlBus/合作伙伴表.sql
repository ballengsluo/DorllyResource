/*
 * ��������
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Partner')
	DROP TABLE T_Partner;
CREATE TABLE T_Partner(
	ID NVARCHAR(30),						--�����
	Name NVARCHAR(80),						--�������
	ShortName NVARCHAR(20),					--�����
	TEL NVARCHAR(20),						--��ϵ�绰
	HomeURL NVARCHAR(100),					--����URL
	LogoURL NVARCHAR(100),					--logoͼƬURL
	Status Bit,								--���״̬
	UpdateTime DATETIME DEFAULT(GETDATE()),	--�޸�ʱ��
	UpdateUser NVARCHAR(30)					--�޸��û�	
)
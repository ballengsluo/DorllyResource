/*
 * ����������
 */
 IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_RChannel')
	DROP TABLE T_RChannel;
CREATE TABLE T_RChannel(
	ID INT IDENTITY(1,1) PRIMARY KEY,		--�������
	Name NVARCHAR(50),						--��������
	URL NVARCHAR(300),						--�����ӿ�
	Status BIT,								--����״̬
	UpdateTime DATETIME,					--�޸�ʱ��
	UpdateUser NVARCHAR(30)					--�޸��û�
)
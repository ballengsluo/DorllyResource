USE DorllyRes;
--��ɫ������
INSERT INTO T_ROLE VALUES('��������Ա');
INSERT INTO T_ROLE VALUES('��ͨ�û�');
SELECT * FROM T_ROLE;
--�û�������
INSERT INTO T_User(UserName,Password,Phone,Email,Addr,CreateDate,RoleID,Status) VALUES('Test����1','123456','18126771077','my@qq.com','�㶫ʡ',GetDate(),'',1);
SELECT * FROM T_USER;


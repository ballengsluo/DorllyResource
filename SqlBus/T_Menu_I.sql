USE DORLLYRES;
--�˵�������
INSERT INTO T_MENU(MenuName,MenuLevel,OrderNum,ClassName) VALUES('��Դ����',1,1,'fa fa-paper-plane-o');
INSERT INTO T_MENU(MenuName,MenuLevel,OrderNum,ClassName) VALUES('��Դ״̬',1,2,'fa fa-television');
INSERT INTO T_MENU(MenuName,MenuLevel,OrderNum,ClassName) VALUES('��������',1,3,'fa fa-book');
INSERT INTO T_MENU(MenuName,MenuLevel,OrderNum,ClassName) VALUES('�������',1,4,'fa fa-line-chart');
INSERT INTO T_MENU(MenuName,MenuLevel,OrderNum,ClassName) VALUES('ϵͳ����',1,5,'fa fa-cog');
--
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30001,'�����б�','',2,1,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30001,'������Դ','/Public/Index',2,2,'','','');
--
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30002,'״̬�б�','',2,1,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30002,'��������','',2,2,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30002,'��λ����','',2,3,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30002,'����������','',2,4,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30002,'���λ����','',2,5,'','','');
--
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'��ҳ����','',2,1,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'�������','',2,2,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'��ҵ����','',2,3,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'��Դ���','/Group/Index',2,4,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'��������','',2,5,'','','');
--
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30004,'��Դͳ�Ʊ���','',2,1,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30004,'����ͳ�Ʊ���','',2,2,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30004,'��λͳ�Ʊ���','',2,3,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30004,'������ͳ�Ʊ���','',2,4,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30004,'���λͳ�Ʊ���','',2,5,'','','');
-- 
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30005,'��ɫ����','/Role/Index',2,1,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30005,'Ȩ�޹���','',2,2,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30005,'�û�����','User/Index',2,3,'','','');



SELECT *FROM T_MENU;

INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30014,'����','/City/Index',3,1,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30014,'��������','/Region/Index',3,2,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30014,'԰��','/Park/Index',3,3,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30014,'������','/Stage/Index',3,4,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30014,'����','/Building/Index',3,5,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30014,'¥��','/Floor/Index',3,6,'','','');
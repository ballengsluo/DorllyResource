--�û�
insert into T_Role values('admin','��������Ա',1,'admin',default,'admin',default);
insert into T_Role values('default','Ĭ���û�',1,'admin',default,'admin',default);
--SELECT * FROM T_Role;
INSERT INTO T_User VALUES('admin','RbILKxfH0h4=','Fuck you','all','13168790599','admin@qq.com','�㶫����',DEFAULT,1,'admin',default,'admin',default);
INSERT INTO T_User VALUES('default','RbILKxfH0h4=','Fuck you','','13168790599','default@qq.com','�㶫����',DEFAULT,1,'admin',default,'admin',default);
--select * from T_User;
insert into T_UserRole values('admin',1);
insert into T_UserRole values('default',2);
--�˵�


INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class) VALUES('order','ԤԼ�������',1,5,'fa fa-book');

--����
INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class,Path) VALUES('pub','��Դ��������',1,3,'fa fa-paper-plane-o','/public/index');
insert into T_MenuFunc values('pub','add','����');
insert into T_MenuFunc values('pub','edit','�༭');
insert into T_MenuFunc values('pub','del','ɾ��');
insert into T_MenuFunc values('pub','auth','���ͨ��/��˲�ͨ��');
insert into T_MenuFunc values('pub','pub','�ϼ�/�¼�');
insert into T_MenuFunc values('pub','off','����');
insert into T_MenuFunc values('pub','check','�鿴');

select * from T_MenuFunc where Code = 'check';
update T_MenuFunc set Name='�鿴' where Code = 'check'

--��ҳ
INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class) VALUES('page','ǰ�˲����趨',1,17,'fa fa-window-restore');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('style','page','��ҳ���ֹ���',2,1,'/homepage/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('bottom','page','�ײ����ֹ���',2,3,'/homepagefoot/index');
insert into T_MenuFunc values('style','add','����');
insert into T_MenuFunc values('style','edit','�༭');
insert into T_MenuFunc values('style','del','ɾ��');
insert into T_MenuFunc values('style','auth','���ͨ��/��˲�ͨ��');
insert into T_MenuFunc values('style','pub','�ϼ�/�¼�');
insert into T_MenuFunc values('style','off','����');
insert into T_MenuFunc values('style','check','�鿴');

INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class) VALUES('resource','��Դ��Ϣ����',1,1,'fa fa-map-o');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('rm','resource','�칫�ռ�����',2,1,'/r_rm/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('cb','resource','��λ����',2,3,'/r_cb/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('mr','resource','����������',2,5,'/r_mr/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('ad','resource','���λ����',2,7,'/r_ad/index');
--����
insert into T_MenuFunc values('rm','add','����');
insert into T_MenuFunc values('rm','edit','�༭');
insert into T_MenuFunc values('rm','del','ɾ��');
insert into T_MenuFunc values('rm','switch','����/ͣ��');
--��λ
insert into T_MenuFunc values('cb','add','����');
insert into T_MenuFunc values('cb','edit','�༭');
insert into T_MenuFunc values('cb','del','ɾ��');
insert into T_MenuFunc values('cb','switch','����/ͣ��');
--������
insert into T_MenuFunc values('mr','add','����');
insert into T_MenuFunc values('mr','edit','�༭');
insert into T_MenuFunc values('mr','del','ɾ��');
insert into T_MenuFunc values('mr','switch','����/ͣ��');
--���λ
insert into T_MenuFunc values('ad','add','����');
insert into T_MenuFunc values('ad','edit','�༭');
insert into T_MenuFunc values('ad','del','ɾ��');
insert into T_MenuFunc values('ad','switch','����/ͣ��');


INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class) VALUES('node','���������趨����',1,11,'fa fa-wrench');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('group','node','��Դ��������',2,1,'/a_group/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('city','node','�����趨',2,2,'/a_city/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('region','node','�������趨',2,3,'/a_region/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('park','node','԰���趨',2,5,'/a_park/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('stage','node','�������趨',2,7,'/a_stage/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('building','node','����¥���趨',2,9,'/a_building/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('floor','node','¥���趨',2,11,'/a_floor/index');
--����
insert into T_MenuFunc values('city','add','����');
insert into T_MenuFunc values('city','edit','�༭');
insert into T_MenuFunc values('city','del','ɾ��');
insert into T_MenuFunc values('city','switch','����/ͣ��');
--������
insert into T_MenuFunc values('region','add','����');
insert into T_MenuFunc values('region','edit','�༭');
insert into T_MenuFunc values('region','del','ɾ��');
insert into T_MenuFunc values('region','switch','����/ͣ��');
--԰��
insert into T_MenuFunc values('park','add','����');
insert into T_MenuFunc values('park','edit','�༭');
insert into T_MenuFunc values('park','del','ɾ��');
insert into T_MenuFunc values('park','switch','����/ͣ��');
--������
insert into T_MenuFunc values('stage','add','����');
insert into T_MenuFunc values('stage','edit','�༭');
insert into T_MenuFunc values('stage','del','ɾ��');
insert into T_MenuFunc values('stage','switch','����/ͣ��');
--����
insert into T_MenuFunc values('building','add','����');
insert into T_MenuFunc values('building','edit','�༭');
insert into T_MenuFunc values('building','del','ɾ��');
insert into T_MenuFunc values('building','switch','����/ͣ��');
--¥��
insert into T_MenuFunc values('floor','add','����');
insert into T_MenuFunc values('floor','edit','�༭');
insert into T_MenuFunc values('floor','del','ɾ��');
insert into T_MenuFunc values('floor','switch','����/ͣ��');
--����
insert into T_MenuFunc values('group','add','����');
insert into T_MenuFunc values('group','edit','�༭');
insert into T_MenuFunc values('group','del','ɾ��');
insert into T_MenuFunc values('group','switch','����/ͣ��');


INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class) VALUES('status','��Դ״̬����',1,7,'fa fa-bars');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('statuslist','status','��Դ״̬�б�',2,1,'/s_resource/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('srm','status','�칫�ռ�����',2,3,'/s_rm/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('scb','status','��λ����',2,5,'/s_cb/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('smr','status','����������',2,7,'/s_mr/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('sad','status','���λ����',2,9,'/s_ad/index');
insert into T_MenuFunc values('statuslist','reserve','Ԥ��/ȡ��Ԥ��');
insert into T_MenuFunc values('statuslist','muse','�ڲ�ռ��');
insert into T_MenuFunc values('statuslist','cuse','�ͻ�ռ��');
insert into T_MenuFunc values('statuslist','free','����');
delete from T_MenuFunc where MenuID ='statuslist'
select * from T_MenuFunc where MenuID ='statuslist'
delete T_RoleFunc where FuncID in(59,72,73)
--insert into T_MenuFunc values('statuslist','del','ɾ��');
--insert into T_MenuFunc values('statuslist','switch','����/ͣ��');


INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class) VALUES('table','�������',1,9,'fa fa-pie-chart');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('tablelist','table','��Դͳ���б�',2,1,'/StatisticsList/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('trm','table','�칫�ռ�ͳ�Ʊ���',2,3,'/StatisticsList_RM/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('tcb','table','��λͳ�Ʊ���',2,5,'/StatisticsList_WP/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('tmr','table','������ͳ�Ʊ���',2,7,'/StatisticsList_CR/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('tad','table','���λͳ�Ʊ���',2,9,'/StatisticsList_BB/index');

INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class) VALUES('sys','ϵͳ����',1,13,'fa fa-cogs');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('user','sys','�û�����',2,1,'/user/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('role','sys','��ɫȨ�޹���',2,2,'/role/index');
INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class,Path) VALUES('reset','��������',1,18,'fa fa fa-key','/user/singlereset');
--�û�
insert into T_MenuFunc values('user','add','����');
insert into T_MenuFunc values('user','edit','�༭');
insert into T_MenuFunc values('user','del','ɾ��');
insert into T_MenuFunc values('user','role','�󶨽�ɫ');
insert into T_MenuFunc values('user','switch','����/ͣ��');
insert into T_MenuFunc values('user','reset','��������');
--��ɫ
insert into T_MenuFunc values('role','add','����');
insert into T_MenuFunc values('role','edit','�༭');
insert into T_MenuFunc values('role','del','ɾ��');
insert into T_MenuFunc values('role','func','�༭Ȩ��');
insert into T_MenuFunc values('role','switch','����/ͣ��');


delete from T_RoleMenu;
delete from  T_RoleFunc;
INSERT INTO T_RoleMenu SELECT 1,A.ID FROM T_Menu A;
INSERT INTO T_RoleFunc SELECT 1,A.ID FROM T_MenuFunc A;


SELECT * FROM T_User;
SELECT * FROM T_UserRole;

SELECT * FROM T_Menu;
SELECT * FROM T_MenuFunc;

SELECT * FROM T_Role;
SELECT * FROM T_RoleMenu;
SELECT * FROM T_RoleFunc;
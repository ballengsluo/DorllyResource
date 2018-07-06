USE DORLLYRES;
--菜单表数据
INSERT INTO T_MENU(MenuName,MenuLevel,OrderNum,ClassName) VALUES('资源发布',1,1,'fa fa-paper-plane-o');
INSERT INTO T_MENU(MenuName,MenuLevel,OrderNum,ClassName) VALUES('资源状态',1,2,'fa fa-television');
INSERT INTO T_MENU(MenuName,MenuLevel,OrderNum,ClassName) VALUES('基础资料',1,3,'fa fa-book');
INSERT INTO T_MENU(MenuName,MenuLevel,OrderNum,ClassName) VALUES('报表管理',1,4,'fa fa-line-chart');
INSERT INTO T_MENU(MenuName,MenuLevel,OrderNum,ClassName) VALUES('系统管理',1,5,'fa fa-cog');
--
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30001,'发布列表','',2,1,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30001,'发布资源','/Public/Index',2,2,'','','');
--
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30002,'状态列表','',2,1,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30002,'房屋销控','',2,2,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30002,'工位销控','',2,3,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30002,'会议室销控','',2,4,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30002,'广告位销控','',2,5,'','','');
--
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'首页管理','',2,1,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'区域管理','',2,2,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'企业管理','',2,3,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'资源类别','/Group/Index',2,4,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'渠道管理','',2,5,'','','');
--
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30004,'资源统计报表','',2,1,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30004,'房屋统计报表','',2,2,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30004,'工位统计报表','',2,3,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30004,'会议室统计报表','',2,4,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30004,'广告位统计报表','',2,5,'','','');
-- 
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30005,'角色管理','/Role/Index',2,1,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30005,'权限管理','',2,2,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30005,'用户管理','User/Index',2,3,'','','');



SELECT *FROM T_MENU;

INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30014,'城市','/City/Index',3,1,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30014,'行政区域','/Region/Index',3,2,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30014,'园区','/Park/Index',3,3,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30014,'建设期','/Stage/Index',3,4,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30014,'建筑','/Building/Index',3,5,'','','');
INSERT INTO T_MENU(PID,MenuName,MenuPath,MenuLevel,OrderNum,ClassName,FuncCode,FuncName) VALUES(30014,'楼层','/Floor/Index',3,6,'','','');
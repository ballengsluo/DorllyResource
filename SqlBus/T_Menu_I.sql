USE DORLLYRES;
--菜单表数据
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(NULL,'资源发布','',1,1,'fa fa-paper-plane-o',NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(NULL,'资源状态','',1,2,'fa fa-television',NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(NULL,'基础资料','',1,3,'fa fa-book',NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(NULL,'报表管理','',1,4,'fa fa-line-chart',NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(NULL,'系统管理','',1,5,'fa fa-cog',NULL,NULL);
--
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30001,'发布列表','',2,1,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30001,'发布资源','',2,2,NULL,NULL,NULL);
--
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30002,'状态列表','',2,1,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30002,'房屋销控','',2,2,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30002,'工位销控','',2,3,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30002,'会议室销控','',2,4,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30002,'广告位销控','',2,5,NULL,NULL,NULL);
--
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'首页轮播','',2,1,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'区域管理','',2,2,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'园区管理','',2,3,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'企业管理','',2,4,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'资源类别','',2,5,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'渠道管理','',2,6,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30003,'计价管理','',2,7,NULL,NULL,NULL);
--
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30004,'资源统计报表','',2,1,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30004,'房屋统计报表','',2,2,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30004,'工位统计报表','',2,3,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30004,'会议室统计报表','',2,4,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30004,'广告位统计报表','',2,5,NULL,NULL,NULL);
-- 
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30005,'角色管理','',2,1,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30005,'权限管理','',2,2,NULL,NULL,NULL);
INSERT INTO T_MENU(MenuPID,MenuName,MenuPath,Level,OrderNum,ClassName,FuncCode,FuncName) VALUES(30005,'用户管理','',2,3,NULL,NULL,NULL);
SELECT *FROM T_MENU;
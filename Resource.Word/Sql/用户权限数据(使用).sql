--用户
insert into T_Role values('admin','超级管理员',1,'admin',default,'admin',default);
insert into T_Role values('default','默认用户',1,'admin',default,'admin',default);
--SELECT * FROM T_Role;
INSERT INTO T_User VALUES('admin','RbILKxfH0h4=','Fuck you','all','13168790599','admin@qq.com','广东深圳',DEFAULT,1,'admin',default,'admin',default);
INSERT INTO T_User VALUES('default','RbILKxfH0h4=','Fuck you','','13168790599','default@qq.com','广东深圳',DEFAULT,1,'admin',default,'admin',default);
--select * from T_User;
insert into T_UserRole values('admin',1);
insert into T_UserRole values('default',2);
--菜单


INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class) VALUES('order','预约申请管理',1,5,'fa fa-book');

--发布
INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class,Path) VALUES('pub','资源发布管理',1,3,'fa fa-paper-plane-o','/public/index');
insert into T_MenuFunc values('pub','add','新增');
insert into T_MenuFunc values('pub','edit','编辑');
insert into T_MenuFunc values('pub','del','删除');
insert into T_MenuFunc values('pub','auth','审核通过/审核不通过');
insert into T_MenuFunc values('pub','pub','上架/下架');
insert into T_MenuFunc values('pub','off','作废');
insert into T_MenuFunc values('pub','check','查看');

select * from T_MenuFunc where Code = 'check';
update T_MenuFunc set Name='查看' where Code = 'check'

--首页
INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class) VALUES('page','前端布局设定',1,17,'fa fa-window-restore');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('style','page','首页布局管理',2,1,'/homepage/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('bottom','page','底部布局管理',2,3,'/homepagefoot/index');
insert into T_MenuFunc values('style','add','新增');
insert into T_MenuFunc values('style','edit','编辑');
insert into T_MenuFunc values('style','del','删除');
insert into T_MenuFunc values('style','auth','审核通过/审核不通过');
insert into T_MenuFunc values('style','pub','上架/下架');
insert into T_MenuFunc values('style','off','作废');
insert into T_MenuFunc values('style','check','查看');

INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class) VALUES('resource','资源信息管理',1,1,'fa fa-map-o');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('rm','resource','办公空间资料',2,1,'/r_rm/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('cb','resource','工位资料',2,3,'/r_cb/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('mr','resource','会议室资料',2,5,'/r_mr/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('ad','resource','广告位资料',2,7,'/r_ad/index');
--房屋
insert into T_MenuFunc values('rm','add','新增');
insert into T_MenuFunc values('rm','edit','编辑');
insert into T_MenuFunc values('rm','del','删除');
insert into T_MenuFunc values('rm','switch','启用/停用');
--工位
insert into T_MenuFunc values('cb','add','新增');
insert into T_MenuFunc values('cb','edit','编辑');
insert into T_MenuFunc values('cb','del','删除');
insert into T_MenuFunc values('cb','switch','启用/停用');
--会议室
insert into T_MenuFunc values('mr','add','新增');
insert into T_MenuFunc values('mr','edit','编辑');
insert into T_MenuFunc values('mr','del','删除');
insert into T_MenuFunc values('mr','switch','启用/停用');
--广告位
insert into T_MenuFunc values('ad','add','新增');
insert into T_MenuFunc values('ad','edit','编辑');
insert into T_MenuFunc values('ad','del','删除');
insert into T_MenuFunc values('ad','switch','启用/停用');


INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class) VALUES('node','基础数据设定管理',1,11,'fa fa-wrench');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('group','node','资源分组资料',2,1,'/a_group/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('city','node','城市设定',2,2,'/a_city/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('region','node','行政区设定',2,3,'/a_region/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('park','node','园区设定',2,5,'/a_park/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('stage','node','建设期设定',2,7,'/a_stage/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('building','node','建筑楼栋设定',2,9,'/a_building/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('floor','node','楼层设定',2,11,'/a_floor/index');
--城市
insert into T_MenuFunc values('city','add','新增');
insert into T_MenuFunc values('city','edit','编辑');
insert into T_MenuFunc values('city','del','删除');
insert into T_MenuFunc values('city','switch','启用/停用');
--行政区
insert into T_MenuFunc values('region','add','新增');
insert into T_MenuFunc values('region','edit','编辑');
insert into T_MenuFunc values('region','del','删除');
insert into T_MenuFunc values('region','switch','启用/停用');
--园区
insert into T_MenuFunc values('park','add','新增');
insert into T_MenuFunc values('park','edit','编辑');
insert into T_MenuFunc values('park','del','删除');
insert into T_MenuFunc values('park','switch','启用/停用');
--建设期
insert into T_MenuFunc values('stage','add','新增');
insert into T_MenuFunc values('stage','edit','编辑');
insert into T_MenuFunc values('stage','del','删除');
insert into T_MenuFunc values('stage','switch','启用/停用');
--建筑
insert into T_MenuFunc values('building','add','新增');
insert into T_MenuFunc values('building','edit','编辑');
insert into T_MenuFunc values('building','del','删除');
insert into T_MenuFunc values('building','switch','启用/停用');
--楼层
insert into T_MenuFunc values('floor','add','新增');
insert into T_MenuFunc values('floor','edit','编辑');
insert into T_MenuFunc values('floor','del','删除');
insert into T_MenuFunc values('floor','switch','启用/停用');
--分组
insert into T_MenuFunc values('group','add','新增');
insert into T_MenuFunc values('group','edit','编辑');
insert into T_MenuFunc values('group','del','删除');
insert into T_MenuFunc values('group','switch','启用/停用');


INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class) VALUES('status','资源状态管理',1,7,'fa fa-bars');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('statuslist','status','资源状态列表',2,1,'/s_resource/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('srm','status','办公空间销控',2,3,'/s_rm/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('scb','status','工位销控',2,5,'/s_cb/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('smr','status','会议室销控',2,7,'/s_mr/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('sad','status','广告位销控',2,9,'/s_ad/index');
insert into T_MenuFunc values('statuslist','reserve','预留/取消预留');
insert into T_MenuFunc values('statuslist','muse','内部占用');
insert into T_MenuFunc values('statuslist','cuse','客户占用');
insert into T_MenuFunc values('statuslist','free','空闲');
delete from T_MenuFunc where MenuID ='statuslist'
select * from T_MenuFunc where MenuID ='statuslist'
delete T_RoleFunc where FuncID in(59,72,73)
--insert into T_MenuFunc values('statuslist','del','删除');
--insert into T_MenuFunc values('statuslist','switch','启用/停用');


INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class) VALUES('table','报表管理',1,9,'fa fa-pie-chart');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('tablelist','table','资源统计列表',2,1,'/StatisticsList/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('trm','table','办公空间统计报表',2,3,'/StatisticsList_RM/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('tcb','table','工位统计报表',2,5,'/StatisticsList_WP/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('tmr','table','会议室统计报表',2,7,'/StatisticsList_CR/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('tad','table','广告位统计报表',2,9,'/StatisticsList_BB/index');

INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class) VALUES('sys','系统管理',1,13,'fa fa-cogs');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('user','sys','用户管理',2,1,'/user/index');
INSERT INTO T_MENU(ID,PID,Name,Level,OrderNum,Path) VALUES('role','sys','角色权限管理',2,2,'/role/index');
INSERT INTO T_MENU(ID,Name,Level,OrderNum,Class,Path) VALUES('reset','密码重置',1,18,'fa fa fa-key','/user/singlereset');
--用户
insert into T_MenuFunc values('user','add','新增');
insert into T_MenuFunc values('user','edit','编辑');
insert into T_MenuFunc values('user','del','删除');
insert into T_MenuFunc values('user','role','绑定角色');
insert into T_MenuFunc values('user','switch','启用/停用');
insert into T_MenuFunc values('user','reset','重置密码');
--角色
insert into T_MenuFunc values('role','add','新增');
insert into T_MenuFunc values('role','edit','编辑');
insert into T_MenuFunc values('role','del','删除');
insert into T_MenuFunc values('role','func','编辑权限');
insert into T_MenuFunc values('role','switch','启用/停用');


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
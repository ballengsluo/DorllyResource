USE DorllyRes;
--角色表数据
INSERT INTO T_ROLE VALUES('超级管理员');
INSERT INTO T_ROLE VALUES('普通用户');
SELECT * FROM T_ROLE;
--用户表数据
INSERT INTO T_User(UserName,Password,Phone,Email,Addr,CreateDate,RoleID,Status) VALUES('Test问问1','123456','18126771077','my@qq.com','广东省',GetDate(),'',1);
SELECT * FROM T_USER;


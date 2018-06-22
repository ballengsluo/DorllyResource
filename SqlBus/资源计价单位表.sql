/*
 * 资源计价单位表
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Unit')
	DROP TABLE T_Unit;
CREATE TABLE T_Unit(
	UnitCode NVARCHAR(10) PRIMARY KEY,		--单位编码
	UnitName NVARCHAR(10)					--单位名称
)
INSERT INTO T_Unit VALUES('Year','年');
INSERT INTO T_Unit VALUES('Quarter','季度');
INSERT INTO T_Unit VALUES('Month','月');
INSERT INTO T_Unit VALUES('HalfMonth','半月');
INSERT INTO T_Unit VALUES('Week','周');
INSERT INTO T_Unit VALUES('Day','天');
INSERT INTO T_Unit VALUES('HalfDay','半天');
INSERT INTO T_Unit VALUES('Hour','小时');
INSERT INTO T_Unit VALUES('HalfHour','半小时');
INSERT INTO T_Unit VALUES('Minute','分钟');
INSERT INTO T_Unit VALUES('Single','个');
INSERT INTO T_Unit VALUES('Meter','㎡');
--INSERT INTO T_Unit VALUES('Once','一口价');
SELECT * FROM T_Unit;
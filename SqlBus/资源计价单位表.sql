/*
 * ��Դ�Ƽ۵�λ��
 */
IF EXISTS(SELECT * FROM DBO.SYSOBJECTS WHERE name='T_Unit')
	DROP TABLE T_Unit;
CREATE TABLE T_Unit(
	UnitCode NVARCHAR(10) PRIMARY KEY,		--��λ����
	UnitName NVARCHAR(10)					--��λ����
)
INSERT INTO T_Unit VALUES('Year','��');
INSERT INTO T_Unit VALUES('Quarter','����');
INSERT INTO T_Unit VALUES('Month','��');
INSERT INTO T_Unit VALUES('HalfMonth','����');
INSERT INTO T_Unit VALUES('Week','��');
INSERT INTO T_Unit VALUES('Day','��');
INSERT INTO T_Unit VALUES('HalfDay','����');
INSERT INTO T_Unit VALUES('Hour','Сʱ');
INSERT INTO T_Unit VALUES('HalfHour','��Сʱ');
INSERT INTO T_Unit VALUES('Minute','����');
INSERT INTO T_Unit VALUES('Single','��');
INSERT INTO T_Unit VALUES('Meter','�O');
--INSERT INTO T_Unit VALUES('Once','һ�ڼ�');
SELECT * FROM T_Unit;
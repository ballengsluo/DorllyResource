declare @SqlBackupDataBase as nvarchar(1000)
set @SqlBackupDataBase=N'BACKUP DATABASE DorllyOrder TO DISK = ''E:\XLWork\DB\do-'+
CONVERT(varchar(11),GETDATE(),112)+'-'+REPLACE(CONVERT(varchar(12),GETDATE(),108),':','')+'.bak'''
+'with STATS = 1,compression'
--�����ļ���ʽ��Northwind-20170111132424.bak
exec sp_executesql @SqlBackupDataBase --����ϵͳ�洢���̣�ִ��SQL


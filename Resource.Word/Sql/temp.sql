select 
a.RMLOCNo1,a.RMLOCNo2,a.RMLOCNo3,a.RMLOCNo4, a.RMID,a.RMNo,
a.RMAddr,a.RMBuildSize,a.RMCurrentCustNo,a.RMStatus,c.ContractStatus,a.RMReservedDate,a.RMEndReservedDate
,c.ContractStartDate,c.ContractEndDate
from Mstr_Room a
left join Op_ContractRMRentalDetail b on a.RMID=b.RMID
left join (select *from Op_Contract where ContractEndDate>GETDATE() and OffLeaseStatus=1) c on b.RefRP=c.RowPointer
where c.ContractEndDate>GETDATE() and c.OffLeaseStatus=1;

select Z.RMID from (
select 
a.RMLOCNo1,a.RMLOCNo2,a.RMLOCNo3,a.RMLOCNo4, a.RMID,a.RMNo,
a.RMAddr,a.RMBuildSize,a.RMCurrentCustNo,a.RMStatus,a.RMReservedDate,a.RMEndReservedDate
,c.ContractStartDate,c.ContractEndDate
from Mstr_Room a
left join Op_ContractRMRentalDetail b on a.RMID=b.RMID
left join Op_Contract c on b.RefRP=c.RowPointer
where c.ContractEndDate>GETDATE()
) Z group BY Z.RMID HAVING(COUNT(*)>1)


select * from Op_Contract where ContractStatus=2 and OffLeaseStatus=1 and ContractEndDate>GETDATE()
select RMID from Mstr_Room where RMStatus='use';

select * from Op_Contract a where 

select *from Op_ContractRMRentalDetail b 
left join Op_Contract c on b.RefRP=c.RowPointer
where b.RMID='FTYQ-1-01-011-K/1103' and c.ContractEndDate>GETDATE()
--where a.ContractEndDate>GETDATE() and ContractStatus=2 and OffLeaseStatus=1


select *from Mstr_WorkPlace;
 
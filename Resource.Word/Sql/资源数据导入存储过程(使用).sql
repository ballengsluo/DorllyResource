USE [DorllyResource]
GO
/****** Object:  StoredProcedure [dbo].[Pro_InsertResourceData]    Script Date: 08/11/2018 19:04:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
Create PROCEDURE [dbo].[Pro_InsertResourceData]
AS
BEGIN
select *from DorllyOrder.dbo.Mstr_Room;
--资源数据
--房屋数据
SELECT IDENTITY(INT,1,1) AS OID,A.* INTO #TEMP_RM from DorllyOrder.dbo.Mstr_Room A
DECLARE @ID INT
DECLARE @COUNT INT
DECLARE @GUID NVARCHAR(50)
SET @ID=1
SET @COUNT = ISNULL((SELECT MAX(OID) FROM #TEMP_RM),0)
WHILE(@ID<=@COUNT)
BEGIN
	INSERT INTO T_Resource(ID,Name,ParkID,ParentID,ResourceTypeID,CustID,
	Area,RentArea,Location,ResourceKindID,CreateUser,CreateTime,UpdateUser,UpdateTime)
	 SELECT RMID,RMNo,RMLOCNo1,RMLOCNo4,RMRentType,RMCurrentCustNo,RMBuildSize,RMRentSize,RMAddr,1
	 ,'admin',GETDATE(),'admin',GETDATE() AS ExtraInfoID FROM #TEMP_RM WHERE OID=@ID
	SET @ID=@ID+1
END
--工位数据
SELECT IDENTITY(INT,1,1) AS OID,A.* into #TEMP_CB FROM DorllyOrder.dbo.Mstr_WorkPlace A
SET @ID=1
SET @COUNT = ISNULL((SELECT MAX(OID) FROM #TEMP_CB),0)
WHILE(@ID<=@COUNT)
BEGIN
	SET @GUID=NEWID()
	INSERT INTO T_Resource(ID,Number,UnitPrice,ParkID,ParentID,ResourceTypeID, Location,ResourceKindID,
	CreateUser,CreateTime,UpdateUser,UpdateTime) 
		SELECT WPNo,WPSeat,WPSeatPrice,WPLOCNo1,WPRMID,WPType,WPAddr,2,
		'admin',GETDATE(),'admin',GETDATE() FROM #TEMP_CB WHERE OID=@ID
	SET @ID=@ID+1
END
--会议室数据
Select IDENTITY(INT,1,1) AS OID,A.* into #TEMP_MR from DorllyButler.DBO.Mstr_ConferenceRoom A
SET @ID=1
SET @COUNT = ISNULL((SELECT MAX(OID) FROM #TEMP_MR),0)
WHILE(@ID<=@COUNT)
BEGIN
	SET @GUID=NEWID()
	INSERT INTO T_Resource(ID,Name,Area,RangeNum,Deposit,Location,ParkID,ResourceKindID
	,CreateUser,CreateTime,UpdateUser,UpdateTime) 
		Select CRNo,CRName,CRBuildSize,CRCapacity,CRDeposit,CRAddr,'FTYQ',3,
		'admin',GETDATE(),'admin',GETDATE() FROM #TEMP_MR WHERE OID=@ID
	
	SET @ID=@ID+1
END

--广告位数据
Select IDENTITY(INT,1,1) AS OID,A.* into #TEMP_AD from DorllyOrder.dbo.Mstr_Billboard A
SET @ID=1
SET @COUNT = ISNULL((SELECT MAX(OID) FROM #TEMP_AD),0)
WHILE(@ID<=@COUNT)
BEGIN
	SET @GUID=NEWID()
	INSERT INTO T_Resource(ID,Name,ParkID,ResourceTypeID,Location,ResourceKindID,CreateUser,CreateTime,UpdateUser,UpdateTime) 
		Select BBNo,BBName,BBLOCNo,BBType,BBAddr,4,'admin',GETDATE(),'admin',GETDATE() FROM #TEMP_AD WHERE OID=@ID
	SET @ID=@ID+1
END

END



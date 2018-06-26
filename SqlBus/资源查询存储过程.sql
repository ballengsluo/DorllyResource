-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET NOCOUNT ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ResSearch
	@Model int,	
	@FloorCode NVARCHAR(30),
	@BuildingCode NVARCHAR(30),
	@StageCode NVARCHAR(30),
	@ParkCode NVARCHAR(30),
	@RegionCode NVARCHAR(30),	
	@CityCode NVARCHAR(30)
AS
BEGIN
Declare @Sql Nvarchar(Max)

	IF(@Model=1)
	Begin
		SET @Sql='
			SELECT R.ID AS RID,R.Name AS RNAME,
			F.CODE AS FCODE,F.NAME AS FNAME,
			B.CODE AS BCODE,B.NAME AS BNAME,
			S.CODE AS SCODE,S.NAME AS SNAME,			
			P.CODE AS PCODE,P.NAME AS PNAME,
			G.CODE AS GCODE,G.NAME AS GNAME,
			C.CODE AS CCODE,C.NAME AS CNAME			
			FROM T_Resource R 
			LEFT JOIN T_Floor F ON F.CODE=R.FloorID
			LEFT JOIN T_Building B ON B.CODE=F.BUILDINGCODE 
			LEFT JOIN T_Stage S ON S.CODE = B.STAGECODE
			LEFT JOIN T_Park P ON P.CODE = S.PARKCODE
			LEFT JOIN T_Region G ON G.CODE = P.REGIONCODE
			LEFT JOIN T_City C ON C.CODE = G.CITYCODE
			WHERE R.RTypeID=1
			'
	End
	ELSE
	BEGIN
		SET @Sql='
			SELECT R.ID AS RID,R.Name AS RNAME,T.ID,T.NAME AS TNAME,				
			P.CODE AS PCODE,P.NAME AS PNAME,
			G.CODE AS GCODE,G.NAME AS GNAME,
			C.CODE AS CCODE,C.NAME AS CNAME			
			FROM T_Resource R
			LEFT JOIN T_RType T ON R.RTypeID=T.ID
			LEFT JOIN T_Park P ON P.CODE = R.ParkID
			LEFT JOIN T_Region G ON G.CODE = P.REGIONCODE
			LEFT JOIN T_City C ON C.CODE = G.CITYCODE
			WHERE R.RTypeID='+@Model
	END
	--ELSE IF(@Model=3)
	--BEGIN
	--SET @Sql='
	--		SELECT R.ID AS RID,R.Name AS RNAME,T.ID,T.NAME AS TNAME,				
	--		P.CODE AS PCODE,P.NAME AS PNAME,
	--		G.CODE AS GCODE,G.NAME AS GNAME,
	--		C.CODE AS CCODE,C.NAME AS CNAME			
	--		FROM T_Resource R
	--		LEFT JOIN T_RType T ON R.RTypeID=T.ID
	--		LEFT JOIN T_Park P ON P.CODE = R.ParkID
	--		LEFT JOIN T_Region G ON G.CODE = P.REGIONCODE
	--		LEFT JOIN T_City C ON C.CODE = G.CITYCODE
	--		WHERE R.RTypeID=3
	--		'
	--END
	--ELSE IF(@Model=4)
	--BEGIN
	--SET @Sql='
	--		SELECT R.ID AS RID,R.Name AS RNAME,T.ID,T.NAME AS TNAME,				
	--		P.CODE AS PCODE,P.NAME AS PNAME,
	--		G.CODE AS GCODE,G.NAME AS GNAME,
	--		C.CODE AS CCODE,C.NAME AS CNAME			
	--		FROM T_Resource R
	--		LEFT JOIN T_RType T ON R.RTypeID=T.ID
	--		LEFT JOIN T_Park P ON P.CODE = R.ParkID
	--		LEFT JOIN T_Region G ON G.CODE = P.REGIONCODE
	--		LEFT JOIN T_City C ON C.CODE = G.CITYCODE
	--		WHERE R.RTypeID=4
	--		'
	--END
	IF(@FloorCode!='')
		SET @Sql=@Sql+' ADN F.CODE='''+@FloorCode+''''
	ELSE IF(@BuildingCode!='')
		SET @Sql=@Sql+' ADN B.CODE='''+@BuildingCode+''''
	ELSE IF(@StageCode!='')
		SET @Sql=@Sql+' ADN S.CODE='''+@StageCode+''''
	ELSE IF(@ParkCode!='')
		SET @Sql=@Sql+' ADN P.CODE='''+@ParkCode+''''
	ELSE IF(@RegionCode!='')
		SET @Sql=@Sql+' ADN G.CODE='''+@RegionCode+''''
	ELSE IF(@CityCode!='')
		SET @Sql=@Sql+' ADN C.CODE='''+@CityCode+''''
	EXEC(@Sql)
END
GO

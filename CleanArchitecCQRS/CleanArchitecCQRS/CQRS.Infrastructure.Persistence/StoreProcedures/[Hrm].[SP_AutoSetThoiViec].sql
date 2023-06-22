USE [Esuhai.HRM]
GO
/****** Object:  StoredProcedure [Hrm].[SP_AutoSetThoiViec]    Script Date: 2/19/2021 11:07:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**
STORE : [Hrm].[SP_AutoSetThoiViec]
Function : Db run store nay de set lai trang thai thoi viec cua nhan vien
Param : none
Date create : 2021-02-19
User create : Huy ICT
Date update : 
User update : 
**/

	CREATE PROCEDURE [Hrm].[SP_AutoSetThoiViec]
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		SELECT tv.NhanVienId
        INTO   #tmpTV
        FROM (
              SELECT tv.NhanVienId
              FROM  [Hrm].NhanVien_ThoiViecs tv
              WHERE CONVERT(char(10), tv.NgayThoiViec,126) <= CONVERT(char(10), GETDATE(),126)
			  AND   ISNULL(tv.Deleted,0)                   <> 1
        ) tv

		--UPDATE TrangthaiId from #TMP
		UPDATE [Hrm].[NhanViens] 
        SET    TrangThaiId      =   (SELECT distinct dm.Id
									 FROM   [Esuhai].DanhMucs dm
									 WHERE  dm.PhanLoai     = N'TrangThai'
									 AND    dm.TenVN        = N'Thôi việc')
		      ,LastModified     =   GETDATE()
			  ,LastModifiedBy   =   'SYSTEM' 
        FROM   #tmpTV
        WHERE  [Hrm].[NhanViens].[Id]               =  #tmpTV.NhanVienId
        AND   ISNULL([Hrm].[NhanViens].[Deleted],0) <> 1;

		--Remove #TMP
		DROP TABLE #tmpTV;
	END
	

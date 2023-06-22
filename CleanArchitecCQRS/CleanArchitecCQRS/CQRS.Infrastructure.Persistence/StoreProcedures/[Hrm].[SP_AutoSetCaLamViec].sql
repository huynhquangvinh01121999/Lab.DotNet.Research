USE [Esuhai.HRM]
GO
/****** Object:  StoredProcedure [Hrm].[SP_AutoSetCaLamViec]    Script Date: 2/19/2021 8:47:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**
STORE : [Hrm].[SP_AutoSetCaLamViec]
Function : Moi ngay, Db run store nay de set lai ca lam viec dung cho nhan vien
Param : none
Date create : 2021-02-19
User create : Huy ICT
Date update : 
User update : 
**/

	ALTER PROCEDURE [Hrm].[SP_AutoSetCaLamViec]
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		SELECT clv.NhanVienId
			  ,clv.CaLamViecId
        INTO   #tmpCLV
        FROM (
              SELECT clv.NhanVienId
                    ,clv.CaLamViecId
                    ,ROW_NUMBER() OVER (PARTITION BY clv.NhanvienId 
					                    ORDER BY clv.Batdau ASC) AS ROWNUM
              FROM  [Hrm].NhanVien_CaLamViecs clv
              WHERE CONVERT(char(10), clv.BatDau,126) >= CONVERT(char(10), GETDATE(),126)
			  AND   ISNULL(clv.Deleted,0)             <> 1
        ) clv
        WHERE clv.ROWNUM = 1;

		--UPDATE CaLamViecId from #TMP
		UPDATE [Hrm].[NhanViens] 
        SET    CalamviecId      =   #tmpCLV.CaLamViecId
		      ,LastModified     =   GETDATE()
			  ,LastModifiedBy   =   'SYSTEM' 
        FROM   #tmpCLV
        WHERE  [Hrm].[NhanViens].[Id]                 = #tmpCLV.NhanVienId
		AND    [Hrm].[NhanViens].[TrangThaiId]       != (SELECT dm.Id
												         FROM   [Esuhai].DanhMucs dm
												         WHERE  dm.PhanLoai     = N'TrangThai'
												         AND    dm.TenVN        = N'Thôi việc')
         AND   ISNULL([Hrm].[NhanViens].[Deleted],0) <> 1;

		--Remove #TMP
		DROP TABLE #tmpCLV;
	END
	

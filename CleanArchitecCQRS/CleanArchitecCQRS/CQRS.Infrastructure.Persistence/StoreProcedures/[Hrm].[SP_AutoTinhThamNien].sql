USE [Esuhai.HRM]
GO
/****** Object:  StoredProcedure [Hrm].[SP_AutoTinhThamNien]    Script Date: 1/18/2021 2:19:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**
STORE : [Hrm].[SP_AutoTinhThamNien]
Function : Moi 2 tuan, db se run store nay de tinh so nam tham nien lai cho tung nhan vien
Param : none
Date create : 2021-01-18
User create : Huy ICT
Date update : 
User update : 
**/

	CREATE PROCEDURE [Hrm].[SP_AutoTinhThamNien]
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		SELECT    nv.Id, ROUND(cast((DATEDIFF(DAY, nv.NgayBatDauLamViec, GETDATE()) 
						   - ISNULL(sum(DATEDIFF(DAY, vm.TuNgay, vm.DenNgay) + 1)
							       ,0)
						   - ISNULL(sum(DATEDIFF(DAY, tv.NgayThoiViec, tv.NgayQuayLai) + 1)
							       ,0)) as float)
						   /365
					  ,1) as THAMNIEN
		INTO      #TMP
		FROM      [Hrm].[NhanViens]  nv
		left join [Hrm].[NhanVien_VangMats] vm
		on        nv.Id           =  vm.NhanVienId
		and       vm.TinhThamNien =  0
		left join [Hrm].[NhanVien_ThoiViecs] tv
		on        nv.Id           =  tv.NhanVienId
		and       tv.TinhThamNien =  0
		left join [Esuhai].[DanhMucs]        dm
		on        nv.TrangThaiId  =  dm.Id
		and       dm.PhanLoai     =  N'TrangThai'
		where     nv.TrangThaiId  is null
		or        dm.TenVN        != N'Thôi việc'
		group by  nv.Id,nv.NgayBatDauLamViec;

		--UPDATE ThamNien from #TMP
		UPDATE [Hrm].[NhanViens] 
        SET ThamNien = #TMP.THAMNIEN
        FROM #TMP
        WHERE [Hrm].[NhanViens].[Id] = #TMP.Id;

		--Remove #TMP
		DROP TABLE #TMP;
	END
	

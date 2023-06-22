﻿USE [DevEsuhaiDb]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetViecBenNgoaisHrView]    Script Date: 2/8/2023 2:38:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create procedure [Hrm].[SP_GetViecBenNgoaisHrView]
	@pageNumber int,			
	@pageSize int,				
	@phongId int,				
	@banId int,					
	@trangThai nvarchar(max),	
	@keyword nvarchar(max),		
	@thoiGianBatDau datetime,	
	@thoiGianKetThuc datetime,	
	@TotalItems int output		
as
begin
	
	SET NOCOUNT ON;
	
	declare @dateNow datetime 
	set @dateNow = getdate()

	-- giới hạn tối đa số ngày XD của NXD1
	declare @limitXD1 int
	set @limitXD1 = 7
	
	-- giới hạn tối đa số ngày XD của NXD2
	declare @limitXD2 int
	set @limitXD2 = 11

	select vbn.Id, vbn.Created
		, concat(nv.HoTenDemVN,' ', nv.TenVN) as [HoTen]
		, vbn.ThoiGianBatDau, vbn.ThoiGianKetThuc
		, vbn.LoaiCongTac, vbn.MoTa, vbn.SoGio, vbn.TrangThaiXetDuyet, vbn.DiaDiem, vbn.NguoiGap, vbn.DiemDenId
		, dd.Ten as [DiemDenTen], dd.DiaChi as [DiemDenDiaChi], dd.DonVi as [DiemDenDonVi]
		, dd.SdtLienLac as [DiemDenSdtLienLac], dd.NguoiGap as [DiemDenNguoiGap]
		, concat(nvtt.HoTenDemVN,' ', nvtt.TenVN) as [NhanVienThayTheTen]

		, vbn.NguoiXetDuyetCap1Id, vbn.NXD1_TrangThai as [NXD1_TrangThai1], vbn.NXD1_GhiChu
		, (CASE WHEN DATEDIFF(DAY, vbn.Created, @dateNow) > @limitXD1 THEN cast(1 as bit) else  cast(0 as bit) end) as NXD1_isHetHanDuyet
	
		, vbn.NguoiXetDuyetCap2Id, vbn.NXD2_TrangThai as [NXD2_TrangThai1], vbn.NXD2_GhiChu
		, (CASE WHEN DATEDIFF(DAY, vbn.Created, @dateNow) > @limitXD2 THEN cast(1 as bit) else cast(0 as bit) end) as NXD2_isHetHanDuyet

		, vbn.HRXetDuyetId, vbn.HR_TrangThai, vbn.HR_GhiChu

	into #tmpViecBenNgoais
	from Hrm.ViecBenNgoais vbn
	left join Vbn.DiemDens dd on dd.Id = vbn.DiemDenId
	left join Hrm.NhanViens nv on nv.Id = vbn.NhanVienId
	left join Hrm.NhanViens nvtt on nvtt.Id = vbn.NhanVienThayTheId
	where 
		(nv.PhongId = @phongId OR @phongId = 0) 
	and 
		(nv.BanId = @banId OR @banId = 0)
	and		
		((nv.HoTENDemVN + ' ' +nv.TenVN) like N'%' +@keyword +'%' or nv.MaNhanVien like N'%' +@keyword +'%')
	and 
		(
			cast(vbn.ThoiGianBatDau as date) between @thoiGianBatDau and @thoiGianKetThuc
		or
			cast(vbn.ThoiGianKetThuc as date) between @thoiGianBatDau and @thoiGianKetThuc
		)
	
	declare @queryCondition nvarchar(max) = 
			(CASE
			
				WHEN LOWER(@trangThai) = 'waiting' THEN ' WHERE HR_TrangThai IS NULL and ((NguoiXetDuyetCap1Id is not null and NguoiXetDuyetCap2Id is null and NXD1_TrangThai1 is null) or (NguoiXetDuyetCap2Id is not null and NXD2_TrangThai1 is null))'
			
				when LOWER(@trangThai) = 'waitinghr' then ' WHERE HR_TrangThai is null and ((NguoiXetDuyetCap1Id is not null and NguoiXetDuyetCap2Id is null and NXD1_TrangThai1 is not null) or (NguoiXetDuyetCap2Id is not null and NXD2_TrangThai1 is not null) or (NguoiXetDuyetCap1Id is null and NguoiXetDuyetCap2Id is null))'
			
				WHEN LOWER(@trangThai) = 'hrapproval' THEN ' WHERE LOWER(HR_TrangThai) = ''approved'''
			
				WHEN LOWER(@trangThai) = 'hrrejected' THEN ' WHERE LOWER(HR_TrangThai) = ''rejected'''
			
				WHEN LOWER(@trangThai) IS NULL OR LOWER(@trangThai) = 'all' THEN ' WHERE (HR_TrangThai is null or HR_TrangThai in (''approved'', ''rejected''))'

				ELSE ' WHERE 1 = 0'
			END)

	-- đếm tổng record query
	DECLARE @sql_get_total NVARCHAR(max)
	SET @sql_get_total = 
						'SELECT @TotalItems = COUNT(*) 
						FROM #tmpViecBenNgoais ' + @queryCondition;

	 EXEC sp_executesql 
        @query = @sql_get_total, 
        @params = N'@TotalItems INT OUTPUT', 
        @TotalItems = @TotalItems OUTPUT;

	declare @sqlQuery nvarchar(max)
	SET @sqlQuery = 
		'SELECT *
				, (CASE
						WHEN NXD1_isHetHanDuyet = 1			
								AND [NXD1_TrangThai1] IS NULL
								AND NguoiXetDuyetCap1Id IS NOT NULL
							THEN ''expried''
						WHEN NXD1_isHetHanDuyet != 1				
								AND [NXD1_TrangThai1] IS NULL
								AND [NXD2_TrangThai1] IS NULL
								AND NguoiXetDuyetCap1Id IS NOT NULL
								AND HR_TrangThai IS NULL
							THEN ''waiting''
						ELSE LOWER([NXD1_TrangThai1])
				END) AS [NXD1_TrangThai]
				, (CASE
						WHEN NXD2_isHetHanDuyet = 1	
								AND [NXD2_TrangThai1] IS NULL
								AND NguoiXetDuyetCap2Id IS NOT NULL
							THEN ''expried''
						WHEN NXD2_isHetHanDuyet != 1	
								AND NguoiXetDuyetCap2Id IS NOT NULL
								AND [NXD2_TrangThai1] IS NULL
								AND HR_TrangThai IS NULL
							THEN
								CASE 
									WHEN NguoiXetDuyetCap1Id IS NOT NULL
											AND [NXD1_TrangThai1] IS NULL
											AND NXD1_isHetHanDuyet != 1
										THEN ''waitingc1''
									ELSE ''waiting''
								END
						ELSE LOWER([NXD2_TrangThai1])
				END) AS NXD2_TrangThai
				from #tmpViecBenNgoais'

	DECLARE @offSetValue INT = ((@pageNumber - 1) * @pageSize) 

		-- fetch dữ liệu phân trang theo offset
	DECLARE @excOffSetValue NVARCHAR(MAX) = ' ORDER BY Created, Id OFFSET ' + CAST(@offSetValue AS NVARCHAR) + ' ROWS FETCH NEXT ' + CAST(@pageSize AS NVARCHAR) + ' ROW ONLY'
	
		-- nối chuỗi câu query
	DECLARE @excQuery NVARCHAR(MAX)
	SET @excQuery = @sqlQuery + @queryCondition + @excOffSetValue

		-- thực thi câu query
	EXECUTE(@excQuery)
end
GO



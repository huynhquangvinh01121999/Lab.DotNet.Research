USE [Esuhai.HRM]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetTimesheetsNotHrView]    Script Date: 11/1/2022 10:15:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*==============================================================================================
   Author			Date			Updated					Description
   -----------------------------------------------------------------------------------------
   Vinh Huynh		2022.11.01		2022.12.15				Get danh sách phụ cấp cho nhân sự
============================================================================================= */

CREATE PROCEDURE [Hrm].[SP_GetTimesheetsNotHrView]
	@pageNumber INT,			
	@pageSize INT,				
	@nhanvienId NVARCHAR(MAX),	
	@thoigianbatdau DATETIME,	
	@thoigianketthuc DATETIME,	
	@trangThai NVARCHAR(MAX),	
	@keyword NVARCHAR(MAX),		
	@TotalItems INT OUTPUT		
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @dateNow DATETIME
	SET @dateNow = GETDATE()

	-- giới hạn tối đa số ngày XD của NXD1
	declare @limitXD1 int
	set @limitXD1 = 7
	
	-- giới hạn tối đa số ngày XD của NXD2
	declare @limitXD2 int
	set @limitXD2 = 11

	SELECT ts.Id, ts.Created, ts.NhanVienId
		, concat(nv.HoTenDemVN,' ',nv.TenVN) as [HoTenNhanVienVN], nv.PhongId, nv.BanId

		, ts.NgayLamViec, ts.CaLamViec_BatDau, ts.CaLamViec_BatDauNghi, ts.CaLamViec_KetThucNghi, ts.CaLamViec_KetThuc
		, ts.GioVao, ts.GioRa, ts.DieuChinh_GioVao, ts.DieuChinh_GioRa, ts.DieuChinh_GhiChu

		, ts.NguoiXetDuyetCap1Id, ts.NXD1_TrangThai as [NXD1_TrangThai1], ts.NXD1_GhiChu
		, DATEADD(DAY, @limitXD1, ts.NgayGoiDon) as [NXD1_HanDuyet]
		, (CASE WHEN ts.NguoiXetDuyetCap1Id = @nhanvienId THEN CAST(1 as BIT) else CAST(0 as BIT) end) as [IsXetDuyetCap1]
		, (CASE WHEN DATEDIFF(DAY, ts.NgayGoiDon, @dateNow) > @limitXD1 THEN CAST(1 as BIT) else  CAST(0 as BIT) end) as [NXD1_isHetHanDuyet]
		, concat(tpb1.HoTenDemVN,' ',tpb1.TenVN) as [NXD1_Ten]

		, ts.NguoiXetDuyetCap2Id, ts.NXD2_TrangThai as [NXD2_TrangThai1], ts.NXD2_GhiChu
		, DATEADD(DAY, @limitXD2, ts.NgayGoiDon) as [NXD2_HanDuyet]
		, (CASE WHEN ts.NguoiXetDuyetCap2Id = @nhanvienId THEN CAST(1 as BIT) else CAST(0 as BIT) end) as IsXetDuyetCap2
		, (CASE WHEN DATEDIFF(DAY, ts.NgayGoiDon, @dateNow) > @limitXD2 THEN CAST(1 as BIT) else CAST(0 as BIT) end) as NXD2_isHetHanDuyet
		, concat(tpb2.HoTenDemVN,' ',tpb2.TenVN) as [NXD2_Ten]

		, ts.HRXetDuyetId, ts.HR_TrangThai, ts.HR_GhiChu
		, (CASE WHEN ts.HR_GioVao IS NULL THEN ts.DieuChinh_GioVao else ts.HR_GioVao END) as [HR_GioVao]
		, (CASE WHEN ts.HR_GioRa IS NULL THEN ts.DieuChinh_GioRa else ts.HR_GioRa END) as [HR_GioRa]
		
	INTO #tmpTS
	FROM Hrm.Timesheets ts
	LEFT JOIN Hrm.NhanViens nv on nv.Id = ts.NhanVienId
	left join Hrm.NhanViens tpb1 on tpb1.Id = ts.NguoiXetDuyetCap1Id
	left join Hrm.NhanViens tpb2 on tpb2.Id = ts.NguoiXetDuyetCap2Id
	WHERE
		ts.NgayLamViec BETWEEN @thoiGianBatDau AND @thoiGianKetThuc
	AND 
		(ts.NguoiXetDuyetCap1Id = @nhanvienId OR ts.NguoiXetDuyetCap2Id = @nhanvienId)
	AND 
		(concat(nv.HoTENDemVN,' ',nv.TenVN) LIKE N'%' +@keyword +'%' OR nv.MaNhanVien LIKE N'%' +@keyword +'%')
	AND
		ts.[TrangThai] = N'Submitted'
			
	declare @queryCondition nvarchar(max) = 
		(case
			/* lấy ra những đơn phụ cấp mà NXD1,2 còn hạn duyệt và tất cả đều chưa được HR chưa duyệt*/
			--when LOWER(@trangThai) = 'waiting' then ' WHERE [HR_TrangThai] IS NULL and ((IsXetDuyetCap2 = 1 AND NXD2_isHetHanDuyet != 1) or (IsXetDuyetCap1 = 1 AND NXD1_isHetHanDuyet != 1))'
			
			/*
			 - là c1 - ko có c2
			 - là c2 - ko có c1
			 - là c1 - có c2
			 - là c2 - có c1
			*/
			--when LOWER(@trangThai) = 'waiting' 
			--then ' WHERE [HR_TrangThai] IS NULL 
			--			and ((IsXetDuyetCap1 = 1 and NXD1_TrangThai1 is null and NXD1_isHetHanDuyet != 1 and NguoiXetDuyetCap2Id is null)
			--				or (IsXetDuyetCap2 = 1 and NXD2_TrangThai1 is null and NXD2_isHetHanDuyet != 1 and NguoiXetDuyetCap1Id is null)
			--				or (IsXetDuyetCap1 = 1 and NXD1_TrangThai1 is null and NXD1_isHetHanDuyet != 1 and NguoiXetDuyetCap2Id is not null and NXD2_TrangThai1 is null) 
			--				or (IsXetDuyetCap2 = 1 and NXD2_TrangThai1 is null and NXD2_isHetHanDuyet != 1 and NguoiXetDuyetCap1Id is not null and NXD1_TrangThai1 is not null))'

			when LOWER(@trangThai) = 'waiting' 
			then ' WHERE [HR_TrangThai] IS NULL 
						and ((NguoiXetDuyetCap1Id is not null and NguoiXetDuyetCap2Id is null and NXD1_isHetHanDuyet != 1 and NXD1_TrangThai1 is null)
							or	(NguoiXetDuyetCap1Id is null and NguoiXetDuyetCap2Id is not null and NXD2_isHetHanDuyet != 1 and NXD2_TrangThai1 is null)
							or	(NguoiXetDuyetCap1Id is not null and NguoiXetDuyetCap2Id is not null))'
			
			/*  - có c1 - ko có c2 - c1 hết hạn/đã duyệt
				- có c2 - ko có c1 - c2 hết hạn/đã duyệt
				- có c1 - có c2 - c1 hết hạn/đã duyệt - c2 hết hạn/đã duyệt */
			--when LOWER(@trangThai) = 'waitinghr' then ' WHERE ((NguoiXetDuyetCap1Id is not null and NguoiXetDuyetCap2Id is null and (NXD1_TrangThai1 is not null or NXD1_isHetHanDuyet = 1)) 
			--											  or  (NguoiXetDuyetCap1Id is null and NguoiXetDuyetCap2Id is not null and (NXD2_TrangThai1 is not null or NXD2_isHetHanDuyet = 1))
			--											  or  (NguoiXetDuyetCap1Id is not null and NguoiXetDuyetCap2Id is not null and (NXD1_TrangThai1 is not null or NXD1_isHetHanDuyet = 1) and (NXD2_TrangThai1 is not null or NXD2_isHetHanDuyet = 1)))
			--											and HR_TrangThai is null'

			when LOWER(@trangThai) = 'waitinghr' then ' WHERE HR_TrangThai is null'

			when LOWER(@trangThai) = 'hrapproval' then ' WHERE LOWER(HR_TrangThai) = ''approved'''
			
			when LOWER(@trangThai) = 'hrrejected' then ' WHERE LOWER(HR_TrangThai) = ''rejected'''
			
			WHEN LOWER(@trangThai) IS NULL OR LOWER(@trangThai) = 'all' THEN ' WHERE (HR_TrangThai is null or HR_TrangThai in (''approved'', ''rejected''))'

			ELSE ' WHERE 1 = 0'
		end)

	-- đếm tổng record query
	DECLARE @sql_get_total NVARCHAR(MAX)
	SET @sql_get_total = 
						'SELECT @TotalItems = COUNT(*) 
						FROM #tmpTS'  + @queryCondition;
	 EXEC sp_executesql 
        @query = @sql_get_total, 
        @params = N'@TotalItems INT OUTPUT', 
        @TotalItems = @TotalItems OUTPUT;

	SELECT *
		, (CASE
				WHEN [NXD1_isHetHanDuyet] = 1			
						AND [NXD1_TrangThai1] IS NULL
						AND [NguoiXetDuyetCap1Id] IS NOT NULL
					THEN 'expried'
				WHEN [NXD1_isHetHanDuyet] != 1				
						AND [NXD1_TrangThai1] IS NULL
						AND [NXD2_TrangThai1] IS NULL
						AND [NguoiXetDuyetCap1Id] IS NOT NULL
						AND [HR_TrangThai] IS NULL
						THEN 'waiting'
				ELSE LOWER([NXD1_TrangThai1])
			END) AS [NXD1_TrangThai]
			, (CASE
					WHEN [NXD2_isHetHanDuyet] = 1	
							AND [NXD2_TrangThai1] IS NULL
							AND [NguoiXetDuyetCap2Id] IS NOT NULL
						THEN 'expried'
					WHEN [NXD2_isHetHanDuyet] != 1	
							AND [NguoiXetDuyetCap2Id] IS NOT NULL
							AND [NXD2_TrangThai1] IS NULL
							AND [HR_TrangThai] IS NULL
						THEN
							CASE 
								WHEN [NguoiXetDuyetCap1Id] IS NOT NULL
										AND [NXD1_TrangThai1] IS NULL
										AND [NXD1_isHetHanDuyet] != 1
									THEN 'waitingc1'
								ELSE 'waiting'
							END
					when [NXD1_isHetHanDuyet] != 1 and [NXD2_Ten] is null -- Nam Le | 2022.01.05
						then 'waitingc1'
					ELSE LOWER([NXD2_TrangThai1])
			END) AS [NXD2_TrangThai]
		INTO #TempExcquery
		from #tmpTS

	declare @excQuery nvarchar(max)
	set @excQuery = 
					'select ( select * 
								, ( SELECT np.Id, np.ThoiGianBatDau, np.ThoiGianKetThuc, np.SoNgayDangKy as [SoNgayDangKy], np.TrangThaiNghi, np.TrangThaiDangKy, np.MoTa
									, (case
											when np.ThoiGianBatDau between CaLamViec_BatDau and CaLamViec_BatDauNghi then np.ThoiGianBatDau
											when np.ThoiGianBatDau between CaLamViec_KetThucNghi and CaLamViec_KetThuc then CaLamViec_KetThucNghi
											when np.ThoiGianBatDau < CaLamViec_BatDau then CaLamViec_BatDau
									end) as [TGBD_Display]
									, (case
										when DATEPART(w, NgayLamViec) = 7 then CaLamViec_BatDauNghi
										when np.ThoiGianKetThuc between CaLamViec_BatDau and CaLamViec_BatDauNghi then CaLamViec_BatDauNghi
										when np.ThoiGianKetThuc between CaLamViec_KetThucNghi and CaLamViec_KetThuc then np.ThoiGianKetThuc
										when np.ThoiGianKetThuc > CaLamViec_KetThuc then CaLamViec_KetThuc
									end) as [TGKT_Display]
								FROM Hrm.NghiPheps np
								where np.NhanVienId = #TempExcquery.[NhanVienId]
										AND np.TrangThaiDangKy <> N'''+N'Không nghỉ' +'''
										AND (NgayLamViec >= CONVERT(NVARCHAR(10), np.ThoiGianBatDau, 25)
										AND NgayLamViec <= CONVERT(NVARCHAR(10), np.ThoiGianKetThuc, 25))
										AND (case when DATEPART(w, Ngaylamviec) != 1 Then 1 else 0 end) = 1
								FOR JSON PATH
							) as [NghiPheps]
							, (	select vbn.Id
									, vbn.ThoiGianBatDau, vbn.ThoiGianKetThuc
									, (case
										when NgayLamViec = CONVERT(NVARCHAR(10), vbn.ThoiGianBatDau, 25) then vbn.ThoiGianBatDau
										else CaLamViec_BatDau
									end) as [TGBD_Display]
									, (case
										when NgayLamViec = CONVERT(NVARCHAR(10), vbn.ThoiGianKetThuc, 25) then vbn.ThoiGianKetThuc
										else CaLamViec_KetThuc
									end) as [TGKT_Display]
									, vbn.SoGio, vbn.MoTa, vbn.LoaiCongTac
								from hrm.ViecBenNgoais vbn
								where vbn.NhanVienId = #TempExcquery.[NhanVienId]
									AND (NgayLamViec >= CONVERT(NVARCHAR(10), vbn.ThoiGianBatDau, 25)
									AND NgayLamViec <= CONVERT(NVARCHAR(10), vbn.ThoiGianKetThuc, 25))
									AND vbn.TrangThaiXetDuyet = ''approved''
								FOR JSON PATH
							) as [ViecBenNgoais]
							from #TempExcquery'

		-- công thức tính phân trang
	DECLARE @offSetValue INT = ((@pageNumber - 1) * @pageSize) 

		-- fetch dữ liệu phân trang theo offset
	declare @excOffSetValue nvarchar(max)
	set @excOffSetValue = ' ORDER BY [NgayLamViec], [Id] OFFSET ' + CAST(@offSetValue AS NVARCHAR) + ' ROWS FETCH NEXT ' + CAST(@pageSize AS NVARCHAR) + ' ROWS ONLY FOR JSON PATH) as [Result]'
	
		-- nối chuỗi câu query
	SET @excQuery += @queryCondition + @excOffSetValue

		-- thực thi câu query
	EXECUTE(@excQuery)
END
GO
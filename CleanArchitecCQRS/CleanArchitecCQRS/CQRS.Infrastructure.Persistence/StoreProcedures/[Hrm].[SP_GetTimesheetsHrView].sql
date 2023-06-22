USE [Esuhai.HRM]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetTimesheetsHrView]    Script Date: 10/31/2022 5:20:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*==============================================================================================
   Author			Date			Updated					Description
   -----------------------------------------------------------------------------------------
   Vinh Huynh		2022.11.01		2022-12-01				Get danh sách timesheet cho nhân sự
============================================================================================= */

CREATE PROCEDURE [Hrm].[SP_GetTimesheetsHrView]
	@pageNumber int,			
	@pageSize int,				
	@phongId int,				
	@banId int,					
	@trangThai varchar(50),		
	@keyword nvarchar(max),		
	@thoiGianBatDau datetime,	
	@thoiGianKetThuc datetime,	
	@TotalItems int output		
as
begin

	SET NOCOUNT ON;

	DECLARE @dateNow DATE
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
		, (CASE WHEN DATEDIFF(DAY, ts.NgayGoiDon, @dateNow) > @limitXD1 THEN CAST(1 as BIT) else  CAST(0 as BIT) end) as [NXD1_isHetHanDuyet]

		, ts.NguoiXetDuyetCap2Id, ts.NXD2_TrangThai as [NXD2_TrangThai1], ts.NXD2_GhiChu
		, DATEADD(DAY, @limitXD2, ts.NgayGoiDon) as [NXD2_HanDuyet]
		, (CASE WHEN DATEDIFF(DAY, ts.NgayGoiDon, @dateNow) > @limitXD2 THEN CAST(1 as BIT) else CAST(0 as BIT) end) as NXD2_isHetHanDuyet

		, ts.HRXetDuyetId, ts.HR_TrangThai as [HR_TrangThai1], ts.HR_GhiChu
		, (CASE WHEN ts.HR_GioVao IS NULL THEN ts.DieuChinh_GioVao else ts.HR_GioVao END) as [HR_GioVao]
		, (CASE WHEN ts.HR_GioRa IS NULL THEN ts.DieuChinh_GioRa else ts.HR_GioRa END) as [HR_GioRa]
		
	INTO #tmpTS
	FROM Hrm.Timesheets ts
	LEFT JOIN Hrm.NhanViens nv on nv.Id = ts.NhanVienId
	WHERE 
			(nv.PhongId = @phongId or @phongId = 0) 
		AND 
			(nv.BanId = @banId or @banId = 0)
		AND 
			(ts.NgayLamViec >= @thoiGianBatDau and ts.NgayLamViec <= @thoiGianKetThuc)
		AND 
			(concat(nv.HoTENDemVN,' ',nv.TenVN) LIKE N'%' +@keyword +'%' OR nv.MaNhanVien LIKE N'%' +@keyword +'%')
		AND
			ts.[TrangThai] = N'Submitted'

		-- so sánh trạng thái duyệt
	declare @queryCondition varchar(max) = 
		(case
			/*	Đối với trạng thái 'waiting'
					+ Bắt buộc HR_TrangThai null
					+ Kiểm tra điều kiện người xét duyệt 1,2:
						Có ng cấp 1 và ko có cấp 2 thì kèm theo đk nxd1_trangthai null 
					hoặc
						Có người cấp 2 thì kèm theo đk nxd2_trangthai null */
			when LOWER(@trangThai) = 'waiting' then ' WHERE HR_TrangThai1 is null and ((NguoiXetDuyetCap1Id is not null and NguoiXetDuyetCap2Id is null and NXD1_TrangThai1 is null) or (NguoiXetDuyetCap2Id is not null and NXD2_TrangThai1 is null))'
			
			/*	Đối với trạng thái 'waiting'
					+ Bắt buộc HR_TrangThai null
					+ Kiểm tra điều kiện người xét duyệt 1,2:
						 Có ng cấp 1 và ko có cấp 2 thì kèm theo đk nxd1_trangthai null 
					hoặc Có người cấp 2 thì kèm theo đk nxd2_trangthai null 
					hoặc Không có người cấp 1 + 2 */
			when LOWER(@trangThai) = 'waitinghr' then ' WHERE HR_TrangThai1 is null and ((NguoiXetDuyetCap1Id is not null and NguoiXetDuyetCap2Id is null and NXD1_TrangThai1 is not null) or (NguoiXetDuyetCap2Id is not null and NXD2_TrangThai1 is not null) or (NguoiXetDuyetCap1Id is null and NguoiXetDuyetCap2Id is null))'
			
			when LOWER(@trangThai) = 'hrapproval' then ' WHERE LOWER(HR_TrangThai1) = ''approved'''
			
			when LOWER(@trangThai) = 'hrrejected' then ' WHERE LOWER(HR_TrangThai1) = ''rejected'''
			
			when LOWER(@trangThai) is null or LOWER(@trangThai) = 'all' then ' WHERE (HR_TrangThai1 is null or HR_TrangThai1 in (''approved'', ''rejected''))'
			
			else ' WHERE 1 = 0'
		 end)

	-- đếm tổng record query
	declare @sql_get_total nvarchar(max)
	SET @sql_get_total = 'SELECT @TotalItems = COUNT(*) 
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
						AND [HR_TrangThai1] IS NULL
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
							AND [HR_TrangThai1] IS NULL
						THEN
							CASE 
								WHEN [NguoiXetDuyetCap1Id] IS NOT NULL
										AND [NXD1_TrangThai1] IS NULL
										AND [NXD1_isHetHanDuyet] != 1
									THEN 'waitingc1'
								ELSE 'waiting'
							END
					ELSE LOWER([NXD2_TrangThai1])
			END) AS [NXD2_TrangThai]
			, (case
					when [HR_TrangThai1] is null 
							and [NguoiXetDuyetCap1Id] is null 
							and [NguoiXetDuyetCap2Id] is not null
						then
							case
								when [NXD2_TrangThai1] is null and [NXD2_isHetHanDuyet] = 1 then 'waiting'
								when [NXD2_TrangThai1] is null and [NXD2_isHetHanDuyet] != 1 then 'waitingc2'
								when [NXD2_TrangThai1] is not null then 'waiting' 
							end
			
					when [HR_TrangThai1] is null
							and [NguoiXetDuyetCap1Id] is not null
							and [NguoiXetDuyetCap2Id] is null
						then
							case
								when [NXD1_TrangThai1] is null and [NXD1_isHetHanDuyet] != 1 then 'waitingc1'
								when [NXD1_TrangThai1] is null and [NXD1_isHetHanDuyet] = 1 then 'waiting' 
								when [NXD1_TrangThai1] is not null then 'waiting'
							end

					when [HR_TrangThai1] is null
							and [NguoiXetDuyetCap1Id] is not null
							and [NguoiXetDuyetCap2Id] is not null
						then
							case
								when [NXD1_TrangThai1] is null and [NXD2_TrangThai1] is null and [NXD1_isHetHanDuyet] != 1 then 'waitingc1'
						
								when (NXD1_TrangThai1 is not null or (NXD1_TrangThai1 is null and [NXD1_isHetHanDuyet] = 1))
										and [NXD2_TrangThai1] is null
										and [NXD2_isHetHanDuyet] != 1 then 'waitingc2'

								when (NXD1_TrangThai1 is not null or (NXD1_TrangThai1 is null and [NXD1_isHetHanDuyet] = 1))
										and (NXD2_TrangThai1 is not null or (NXD2_TrangThai1 is null and [NXD2_isHetHanDuyet] = 1)) then 'waiting'
							end

					when [HR_TrangThai1] is null
							and [NguoiXetDuyetCap1Id] is null
							and [NguoiXetDuyetCap2Id] is null then 'waiting'
					else HR_TrangThai1
			end) as [HR_TrangThai]
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
	declare @offSetValue int = ((@pageNumber - 1) * @pageSize)

		-- fetch dữ liệu phân trang theo offset
	declare @excOffSetValue nvarchar(max) = ' ORDER BY NgayLamViec, Id OFFSET ' + CAST(@offSetValue as nvarchar) +' ROWS FETCH NEXT ' +CAST(@pageSize as nvarchar) +' ROWS ONLY FOR JSON PATH) as Result'
	
		-- nối chuỗi câu query
	SET @excQuery += @queryCondition + @excOffSetValue

		-- thực thi câu query
	execute(@excQuery)
end
GO
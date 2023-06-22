USE [Esuhai.HRM]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetTangCasHrView]    Script Date: 10/19/2022 3:06:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*==============================================================================================
   Author			Date			Updated					Description
   -----------------------------------------------------------------------------------------
   Vinh Huynh		2022.10.19		2022.11.18				Get danh sách tăng ca cho nhân sự
============================================================================================= */

CREATE PROCEDURE [Hrm].[SP_GetTangCasHrView]
	@pageNumber int,			-- giá trị số trang hiện tại: không được phép truyền NULL, chỉ được truyền giá trị số nguyên dương (từ 0 trở lên)
	@pageSize int,				-- giá trị số lượng record trong trang: không được phép truyền NULL, chỉ được truyền giá trị số nguyên dương (từ 0 trở lên)
	@phongId int,				-- giá trị mã phòng: không được phép truyền NULL, chỉ được truyền giá trị số nguyên dương (từ 0 trở lên)
	@banId int,					-- giá trị mã ban: không được phép truyền NULL, chỉ được truyền giá trị số nguyên dương (từ 0 trở lên)
	@trangThai varchar(50),		-- giá trị loại trạng thái xét duyệt: được phép truyền null, chỉ nhận giá trị chuỗi
	@keyword nvarchar(max),		-- từ khóa tìm kiếm
	@thoiGianBatDau datetime,	-- thời gian bắt đầu
	@thoiGianKetThuc datetime,	-- thời gian kết thúc
	@TotalItems int output		-- tổng số item
as
begin

	SET NOCOUNT ON;

	DECLARE @dateNow DATETIME
	SET @dateNow = GETDATE()

	select tc.Id, tc.NhanVienId, tc.Created
	, CASE
		WHEN DATENAME(dw, tc.ThoiGianBatDau) = 'Monday' Then N'Thứ 2'
		WHEN DATENAME(dw, tc.ThoiGianBatDau) = 'Tuesday' Then N'Thứ 3'
		WHEN DATENAME(dw, tc.ThoiGianBatDau) = 'Wednesday' Then N'Thứ 4'
		WHEN DATENAME(dw, tc.ThoiGianBatDau) = 'Thursday' Then N'Thứ 5'
		WHEN DATENAME(dw, tc.ThoiGianBatDau) = 'Friday' Then N'Thứ 6'
		WHEN DATENAME(dw, tc.ThoiGianBatDau) = 'Saturday' Then N'Thứ 7'					
		ELSE N'Chủ Nhật'
	END as Thu
	, tc.ThoiGianBatDau, tc.ThoiGianKetThuc, tc.MoTa
	, tc.NgayTangCa, tc.TrangThai
	, tc.SoGioDangKy, tc.SoGioNgayLe, tc.SoGioCuoiTuan, tc.SoGioNgayThuong
	, tc.HRXetDuyetId, tc.HR_TrangThai, tc.HR_GhiChu

	, thdl.NhanVienId as [TongHopDuLieuNhanVienId], thdl.CaLamViec_BatDau, thdl.CaLamViec_BatDauNghi, thdl.CaLamViec_KetThucNghi, thdl.CaLamViec_KetThuc
	, thdl.Timesheet_GioVao, thdl.Timesheet_GioRa, thdl.DiTre, thdl.VeSom, thdl.NgayLamViec

	, CAST(Hrm.ufn_CaculateOTHours(tc.NhanVienId, tc.ThoiGianBatDau, tc.ThoiGianKetThuc) as REAL) AS [SoGioDoiChieu]
	, (CASE 
			WHEN tc.HR_TrangThai IS NULL THEN CAST(Hrm.ufn_CaculateOTHours(tc.NhanVienId, tc.ThoiGianBatDau, tc.ThoiGianKetThuc) as REAL) 
			ELSE tc.SoGioDuocDuyet 
	 END) as [SoGioDuocDuyet]

	, (nv.HoTenDemVN + ' ' +nv.TenVN) as HoTenNhanVienVN, nv.PhongId, nv.BanId

	, dateadd(day, 7, tc.Created) as NXD1_HanDuyet
	, dateadd(day, 11, tc.Created) as NXD2_HanDuyet
	, tc.NguoiXetDuyetCap1Id, tc.NXD1_TrangThai as [NXD1_TrangThai1], tc.NXD1_GhiChu
	, tc.NguoiXetDuyetCap2Id, tc.NXD2_TrangThai as [NXD2_TrangThai1], tc.NXD2_GhiChu
	, (CASE WHEN DATEDIFF(DAY, tc.Created, @dateNow) > 7 THEN CAST(1 as BIT) else  CAST(0 as BIT) end) as NXD1_isHetHanDuyet
	, (CASE WHEN DATEDIFF(DAY, tc.Created, @dateNow) > 11 THEN CAST(1 as BIT) else CAST(0 as BIT) end) as NXD2_isHetHanDuyet
	INTO #tmpTC
	from Hrm.TangCas tc 
		inner join Hrm.NhanViens nv on nv.Id = tc.NhanVienId
		inner join Hrm.TongHopDuLieus thdl on thdl.NhanVienId = tc.NhanVienId and cast(thdl.NgayLamViec as nvarchar(10)) = cast(tc.NgayTangCa as nvarchar(10))
	where 
		(nv.PhongId = @phongId or @phongId = 0) 
	and 
		(nv.BanId = @banId or @banId = 0)
	and 
		tc.NgayTangCa between @thoiGianBatDau and @thoiGianKetThuc
	and 
		((nv.HoTENDemVN + ' ' +nv.TenVN) LIKE N'%' +@keyword +'%' OR nv.MaNhanVien LIKE N'%' +@keyword +'%')

		-- so sánh trạng thái duyệt
	declare @queryCondition varchar(max) = 
		(case
			WHEN LOWER(@trangThai) = 'waiting' THEN ' WHERE HR_TrangThai IS NULL and ((NguoiXetDuyetCap1Id is not null and NguoiXetDuyetCap2Id is null and NXD1_TrangThai1 is null) or (NguoiXetDuyetCap2Id is not null and NXD2_TrangThai1 is null))'
			
			when LOWER(@trangThai) = 'waitinghr' then ' WHERE HR_TrangThai is null and ((NguoiXetDuyetCap1Id is not null and NguoiXetDuyetCap2Id is null and NXD1_TrangThai1 is not null) or (NguoiXetDuyetCap2Id is not null and NXD2_TrangThai1 is not null) or (NguoiXetDuyetCap1Id is null and NguoiXetDuyetCap2Id is null))'
			
			-- trường hợp trạng thái truyền vào = 'hrapproval' thì sẽ lấy ra những phụ cấp đã được HR đồng ý
			when LOWER(@trangThai) = 'hrapproval' then ' WHERE LOWER(HR_TrangThai) = ''approved'''
			
			-- trường hợp trạng thái truyền vào = 'hrrejected' thì sẽ lấy ra những phụ cấp đã được HR từ chối
			when LOWER(@trangThai) = 'hrrejected' then ' WHERE LOWER(HR_TrangThai) = ''rejected'''
			
			-- trường hợp trạng thái truyền vào = '' thì sẽ lấy ra toàn bộ phụ cấp không tuân theo trạng thái nào
			WHEN LOWER(@trangThai) IS NULL OR LOWER(@trangThai) = 'all' THEN ' WHERE (HR_TrangThai is null or HR_TrangThai in (''approved'', ''rejected''))'

			ELSE ' WHERE 1 = 0'
		END)

	-- đếm tổng record query
	declare @sql_get_total nvarchar(max)
	SET @sql_get_total = 
						'SELECT @TotalItems = COUNT(*) 
						FROM #tmpTC'  + @queryCondition;
	 EXEC sp_executesql 
        @query = @sql_get_total, 
        @params = N'@TotalItems INT OUTPUT', 
        @TotalItems = @TotalItems OUTPUT;

	declare @sqlQuery nvarchar(max)
	SET @sqlQuery = 
		'SELECT(
			SELECT *
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
				, ( SELECT np.NhanVienId, np.ThoiGianBatDau, np.ThoiGianKetThuc, np.SoNgayDangKy as [SoNgayDangKy], np.TrangThaiNghi, np.TrangThaiDangKy, np.MoTa
							, (case
									when np.ThoiGianBatDau between CaLamViec_BatDau and CaLamViec_BatDauNghi then np.ThoiGianBatDau
									when np.ThoiGianBatDau between CaLamViec_KetThucNghi and CaLamViec_KetThuc then CaLamViec_KetThucNghi
									when np.ThoiGianBatDau < CaLamViec_BatDau then CaLamViec_BatDau
								end) as [TGBD_Display]
								, (case
									when np.ThoiGianKetThuc between CaLamViec_BatDau and CaLamViec_BatDauNghi then CaLamViec_BatDauNghi
									when np.ThoiGianKetThuc between CaLamViec_KetThucNghi and CaLamViec_KetThuc then np.ThoiGianKetThuc
									when np.ThoiGianKetThuc > CaLamViec_KetThuc then CaLamViec_KetThuc
								end) as [TGKT_Display]
						FROM Hrm.NghiPheps np
						where np.NhanVienId = TongHopDuLieuNhanVienId
								AND np.TrangThaiDangKy <> N'''+N'Không nghỉ' +'''
								AND (NgayLamViec >= CONVERT(NVARCHAR(10), np.ThoiGianBatDau, 25)
								AND NgayLamViec <= CONVERT(NVARCHAR(10), np.ThoiGianKetThuc, 25))
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
						where vbn.NhanVienId = TongHopDuLieuNhanVienId
							 AND (NgayLamViec >= CONVERT(NVARCHAR(10), vbn.ThoiGianBatDau, 25)
							 AND NgayLamViec <= CONVERT(NVARCHAR(10), vbn.ThoiGianKetThuc, 25))
							 AND vbn.TrangThaiXetDuyet = N''approved''
						 FOR JSON PATH
					) as [ViecBenNgoais]
				from #tmpTC'

		-- công thức tính phân trang
	declare @offSetValue int = ((@pageNumber - 1) * @pageSize) 

		-- fetch dữ liệu phân trang theo offset
	declare @excOffSetValue nvarchar(max) = ' ORDER BY Created, Id OFFSET ' + CAST(@offSetValue as nvarchar) +' ROWS FETCH NEXT ' +CAST(@pageSize as nvarchar) +' ROWS ONLY FOR JSON PATH) as Result'
	
		-- nối chuỗi câu query
	declare @excQuery nvarchar(max)
	SET @excQuery = @sqlQuery + @queryCondition + @excOffSetValue

		-- thực thi câu query
	execute(@excQuery)
end
GO



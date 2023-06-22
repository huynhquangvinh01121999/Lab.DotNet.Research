USE [Esuhai.HRM]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetPhuCapsHrView]    Script Date: 9/29/2022 8:56:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*==============================================================================================
   Author			Date			Updated					Description
   -----------------------------------------------------------------------------------------
   Vinh Huynh		2022.09.24		2022.11.18				Get danh sách phụ cấp cho nhân sự
============================================================================================= */


CREATE PROCEDURE [Hrm].[SP_GetPhuCapsHrView]
	@pageNumber INT,			-- giá trị số trang hiện tại: không được phép truyền NULL, chỉ được truyền giá trị số nguyên dương (từ 0 trở lên)
	@pageSize INT,				-- giá trị số lượng record trong trang: không được phép truyền NULL, chỉ được truyền giá trị số nguyên dương (từ 0 trở lên)
	@phongId INT,				-- giá trị mã phòng: không được phép truyền NULL, chỉ được truyền giá trị số nguyên dương (từ 0 trở lên)
	@banId INT,					-- giá trị mã ban: không được phép truyền NULL, chỉ được truyền giá trị số nguyên dương (từ 0 trở lên)
	@trangThai NVARCHAR(50),	-- giá trị loại trạng thái xét duyệt: được phép truyền null, chỉ nhận giá trị chuỗi
	@keyword NVARCHAR(MAX),		-- từ khóa tìm kiếm
	@thoiGianBatDau DATETIME,	-- thời gian bắt đầu
	@thoiGianKetThuc DATETIME,	-- thời gian kết thúc
	@TotalItems INT OUTPUT		-- tổng số item
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @dateNow DATETIME
	SET @dateNow = GETDATE()

	SELECT pc.Id, pc.Created
	, (CASE WHEN pc.HR_TrangThai IS NULL THEN pc.ThoiGianBatDau ELSE pc.XD_ThoiGianBatDau END) AS XD_ThoiGianBatDau
	, (CASE WHEN pc.HR_TrangThai IS NULL THEN pc.ThoiGianKetThuc ELSE pc.XD_ThoiGianKetThuc END) AS XD_ThoiGianKetThuc
	, (CASE WHEN pc.HR_TrangThai IS NULL THEN pc.SoLanPhuCap ELSE pc.XD_SoLanPhuCap END) AS XD_SoLanPhuCap
	, (CASE WHEN pc.HR_TrangThai IS NULL THEN pc.SoBuoiSang ELSE pc.XD_SoBuoiSang END) AS XD_SoBuoiSang
	, (CASE WHEN pc.HR_TrangThai IS NULL THEN pc.SoBuoiTrua ELSE pc.XD_SoBuoiTrua END) AS XD_SoBuoiTrua
	, (CASE WHEN pc.HR_TrangThai IS NULL THEN pc.SoBuoiChieu ELSE pc.XD_SoBuoiChieu END) AS XD_SoBuoiChieu
	, (CASE WHEN pc.HR_TrangThai IS NULL THEN pc.SoQuaDem ELSE pc.XD_SoQuaDem END) AS XD_SoQuaDem
	, concat(nv.HoTENDemVN,' ',nv.TenVN) AS HoTenNhanVienVN, nv.PhongId, nv.BanId, nv.Id as [NhanVienId]
	, pc.ThoiGianBatDau, pc.ThoiGianKetThuc, pc.TrangThai, pc.MoTa
	, pc.SoLanPhuCap, pc.SoBuoiSang, pc.SoBuoiTrua, pc.SoBuoiChieu, pc.SoQuaDem
	, pc.SoLanCuoiTuan, pc.SoLanNgayLe, pc.SoLanNgayThuong
	, lpc.Ten AS LoaiPhuCapTen
	, pc.HRXetDuyetId, pc.HR_TrangThai, pc.HR_GhiChu
	, pc.NguoiXetDuyetCap1Id, pc.NXD1_TrangThai as [NXD1_TrangThai1], pc.NXD1_GhiChu
	, pc.NguoiXetDuyetCap2Id, pc.NXD2_TrangThai as [NXD2_TrangThai1], pc.NXD2_GhiChu
	, DATEADD(DAY, 7, pc.Created) AS NXD1_HanDuyet
	, DATEADD(DAY, 11, pc.Created) AS NXD2_HanDuyet
	, (CASE WHEN DATEDIFF(DAY, pc.Created, @dateNow) > 7 THEN CAST(1 as BIT) else  CAST(0 as BIT) end) as NXD1_isHetHanDuyet
	, (CASE WHEN DATEDIFF(DAY, pc.Created, @dateNow) > 11 THEN CAST(1 as BIT) else CAST(0 as BIT) end) as NXD2_isHetHanDuyet
	, ts.NhanVienId as [TimesheetNhanVienId], ts.NgayLamViec, ts.CaLamViec_BatDau, ts.CaLamViec_BatDauNghi, ts.CaLamViec_KetThucNghi, ts.CaLamViec_KetThuc
	INTO #tmpPC 
	FROM Hrm.PhuCaps pc 
		LEFT JOIN Hrm.Timesheets ts ON ts.NhanVienId = pc.NhanVienId AND ts.NgayLamViec = CONVERT(NVARCHAR(10), pc.ThoiGianBatDau, 25)
		INNER JOIN Hrm.NhanViens nv ON nv.Id = pc.NhanVienId
		INNER JOIN Hrm.LoaiPhuCaps lpc ON lpc.Id = pc.LoaiPhuCapId 
	WHERE 
		(nv.PhongId = @phongId OR @phongId = 0) 
	AND 
		(nv.BanId = @banId OR @banId = 0)
	AND 
			((pc.ThoiGianBatDau BETWEEN @thoiGianBatDau AND @thoiGianKetThuc) 
		OR	
			(pc.ThoiGianKetThuc BETWEEN @thoiGianBatDau AND @thoiGianKetThuc))
	AND 
		((nv.HoTENDemVN + ' ' +nv.TenVN) LIKE N'%' +@keyword +'%' OR nv.MaNhanVien LIKE N'%' +@keyword +'%')

		-- so sánh trạng thái duyệt
	DECLARE @queryCondition NVARCHAR(MAX) = 
		(CASE
			
			WHEN LOWER(@trangThai) = 'waiting' THEN ' WHERE HR_TrangThai IS NULL and ((NguoiXetDuyetCap1Id is not null and NguoiXetDuyetCap2Id is null and NXD1_TrangThai1 is null) or (NguoiXetDuyetCap2Id is not null and NXD2_TrangThai1 is null))'
			
			when LOWER(@trangThai) = 'waitinghr' then ' WHERE HR_TrangThai is null and ((NguoiXetDuyetCap1Id is not null and NguoiXetDuyetCap2Id is null and NXD1_TrangThai1 is not null) or (NguoiXetDuyetCap2Id is not null and NXD2_TrangThai1 is not null) or (NguoiXetDuyetCap1Id is null and NguoiXetDuyetCap2Id is null))'
			
			-- trường hợp trạng thái truyền vào = 'hrapproval' thì sẽ lấy ra những phụ cấp đã được HR đồng ý
			WHEN LOWER(@trangThai) = 'hrapproval' THEN ' WHERE LOWER(HR_TrangThai) = ''approved'''
			
			-- trường hợp trạng thái truyền vào = 'hrrejected' thì sẽ lấy ra những phụ cấp đã được HR từ chối
			WHEN LOWER(@trangThai) = 'hrrejected' THEN ' WHERE LOWER(HR_TrangThai) = ''rejected'''
			
			-- trường hợp trạng thái truyền vào = '' thì sẽ lấy ra toàn bộ phụ cấp không tuân theo trạng thái nào
			WHEN LOWER(@trangThai) IS NULL OR LOWER(@trangThai) = 'all' THEN ' WHERE (HR_TrangThai is null or HR_TrangThai in (''approved'', ''rejected''))'

			ELSE ' WHERE 1 = 0'
		END)

	-- đếm tổng record query
	DECLARE @sql_get_total NVARCHAR(max)
	SET @sql_get_total = 
						'SELECT @TotalItems = COUNT(*) 
						FROM #tmpPC ' + @queryCondition;

	 EXEC sp_executesql 
        @query = @sql_get_total, 
        @params = N'@TotalItems INT OUTPUT', 
        @TotalItems = @TotalItems OUTPUT;

	DECLARE @sqlQuery NVARCHAR(MAX) 
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
				, ( SELECT np.Id, np.ThoiGianBatDau, np.ThoiGianKetThuc, np.SoNgayDangKy as [SoNgayDangKy], np.TrangThaiNghi, np.TrangThaiDangKy, np.MoTa
					FROM Hrm.NghiPheps np
					where np.NhanVienId = pc.NhanVienId and np.TrangThaiDangKy <> N'''+N'Không nghỉ' +'''
							and (cast(np.ThoiGianBatDau as date) between cast(pc.ThoiGianBatDau as date) and cast(pc.ThoiGianKetThuc as date) or
								cast(np.ThoiGianKetThuc as date) between cast(pc.ThoiGianBatDau as date) and cast(pc.ThoiGianKetThuc as date)) 
					FOR JSON PATH
				) as [NghiPheps]
				, (	select vbn.Id, vbn.ThoiGianBatDau, vbn.ThoiGianKetThuc, vbn.SoGio, vbn.MoTa, vbn.LoaiCongTac
					from hrm.ViecBenNgoais vbn
					where vbn.NhanVienId = pc.NhanVienId and vbn.TrangThaiXetDuyet = ''approved''
							and (cast(vbn.ThoiGianBatDau as date) between cast(pc.ThoiGianBatDau as date) and cast(pc.ThoiGianKetThuc as date) or  
								cast(vbn.ThoiGianKetThuc as date) between cast(pc.ThoiGianBatDau as date) and cast(pc.ThoiGianKetThuc as date)) 
					FOR JSON PATH
				) as [ViecBenNgoais]
				from #tmpPC pc'

		-- công thức tính phân trang
	DECLARE @offSetValue INT = ((@pageNumber - 1) * @pageSize) 

		-- fetch dữ liệu phân trang theo offset
	DECLARE @excOffSetValue NVARCHAR(MAX) = ' ORDER BY Created, Id OFFSET ' +CAST(@offSetValue AS NVARCHAR) +' ROWS FETCH NEXT ' +CAST(@pageSize AS NVARCHAR) +' ROWS ONLY FOR JSON PATH) as Result'
	
		-- nối chuỗi câu query
	DECLARE @excQuery NVARCHAR(MAX)
	SET @excQuery = @sqlQuery + @queryCondition + @excOffSetValue

		-- thực thi câu query
	EXECUTE(@excQuery)
END
GO
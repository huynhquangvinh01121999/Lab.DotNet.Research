USE [Esuhai.HRM]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetTangCasNotHrView]    Script Date: 10/19/2022 2:33:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*==============================================================================================
   Author			Date			Updated					Description
   -----------------------------------------------------------------------------------------
   Vinh Huynh		2022.10.19		2022.12.15				Get danh sách tăng ca cho trưởng phòng ban
============================================================================================= */

CREATE PROCEDURE [Hrm].[SP_GetTangCasNotHrView]
	@pageNumber int,			-- giá trị số trang hiện tại: không được phép truyền NULL, chỉ được truyền giá trị số nguyên dương (từ 0 trở lên)
	@pageSize int,				-- giá trị số lượng record trong trang: không được phép truyền NULL, chỉ được truyền giá trị số nguyên dương (từ 0 trở lên)
	@nhanvienId nvarchar(max),	-- mã nhân viên
	@thoigianbatdau datetime,	-- thời gian bắt đầu
	@thoigianketthuc datetime,	-- thời gian kết thúc
	@trangThai nvarchar(50),	-- trạng thái của đơn đăng ký tăng ca
	@keyword nvarchar(max),		-- từ khóa tìm kiếm
	@TotalItems int output		-- tổng số item
as
begin

	SET NOCOUNT ON;
	
	DECLARE @dateNow datetime
	SET @dateNow = GETDATE()

	-- giới hạn tối đa số ngày XD của NXD1
	declare @limitXD1 int
	set @limitXD1 = 7
	
	-- giới hạn tối đa số ngày XD của NXD2
	declare @limitXD2 int
	set @limitXD2 = 11

	SELECT tc.Id, tc.NhanVienId, tc.Created
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
	, thdl.Timesheet_GioVao, thdl.Timesheet_GioRa, thdl.DiTre, thdl.VeSom
	, concat(nv.HoTenDemVN,' ',nv.TenVN) as HoTenNhanVienVN, nv.PhongId, nv.BanId
	, tc.NgayTangCa, tc.TrangThai, tc.SoGioDangKy, tc.SoGioNgayLe, tc.SoGioCuoiTuan, tc.SoGioNgayThuong

	, CAST(Hrm.ufn_CaculateOTHours(tc.NhanVienId, tc.ThoiGianBatDau, tc.ThoiGianKetThuc) as REAL) AS [SoGioDoiChieu]
	, (CASE 
			WHEN tc.HR_TrangThai IS NULL THEN CAST(Hrm.ufn_CaculateOTHours(tc.NhanVienId, tc.ThoiGianBatDau, tc.ThoiGianKetThuc) as REAL) 
			ELSE tc.SoGioDuocDuyet 
	 END) as [SoGioDuocDuyet]

	, tc.HRXetDuyetId, tc.HR_TrangThai, tc.HR_GhiChu

	, tc.NguoiXetDuyetCap1Id, tc.NXD1_TrangThai as [NXD1_TrangThai1], tc.NXD1_GhiChu
	, dateadd(day, @limitXD1, tc.Created) as [NXD1_HanDuyet]
	, (case when tc.NguoiXetDuyetCap1Id = @nhanvienId then cast(1 as bit) else cast(0 as bit) end) as [IsXetDuyetCap1]
	, (case when DATEDIFF(DAY, tc.Created, @dateNow) > @limitXD1 then cast(1 as bit) else  cast(0 as bit) end) as [NXD1_isHetHanDuyet]
	, concat(tpb1.HoTenDemVN,' ',tpb1.TenVN) as [NXD1_Ten]

	, tc.NguoiXetDuyetCap2Id, tc.NXD2_TrangThai as [NXD2_TrangThai1], tc.NXD2_GhiChu
	, dateadd(day, @limitXD2, tc.Created) as [NXD2_HanDuyet]
	, (case when tc.NguoiXetDuyetCap2Id = @nhanvienId then cast(1 as bit) else cast(0 as bit) end) as [IsXetDuyetCap2]
	, (case when DATEDIFF(DAY, tc.Created, @dateNow) > @limitXD2 then cast(1 as bit) else cast(0 as bit) end) as [NXD2_isHetHanDuyet]
	, concat(tpb2.HoTenDemVN,' ',tpb2.TenVN) as [NXD2_Ten]

	, thdl.NhanVienId as [TongHopDuLieuNhanVienId], thdl.NgayLamViec, thdl.CaLamViec_BatDau, thdl.CaLamViec_BatDauNghi, thdl.CaLamViec_KetThucNghi, thdl.CaLamViec_KetThuc
	INTO #tmpTC
	from Hrm.TangCas tc 
		inner join Hrm.NhanViens nv on nv.Id = tc.NhanVienId
		inner join Hrm.TongHopDuLieus thdl on thdl.NhanVienId = tc.NhanVienId and cast(thdl.NgayLamViec as date) = cast(tc.NgayTangCa as date)
		left join Hrm.NhanViens tpb1 on tpb1.Id = tc.NguoiXetDuyetCap1Id
		left join Hrm.NhanViens tpb2 on tpb2.Id = tc.NguoiXetDuyetCap2Id
	where
		tc.NgayTangCa between @thoiGianBatDau and @thoiGianKetThuc
	and 
		(tc.NguoiXetDuyetCap1Id = @nhanvienId or tc.NguoiXetDuyetCap2Id = @nhanvienId)
	and 
		(concat(nv.HoTENDemVN,' ',nv.TenVN) LIKE N'%' +@keyword +'%' OR nv.MaNhanVien LIKE N'%' +@keyword +'%')
	 
		-- so sánh trạng thái duyệt
	declare @queryCondition nvarchar(max) = 
		(case
			/* trường hợp trạng thái truyền vào = 'waiting' thì sẽ lấy ra những phụ cấp mà NXD1, NXD2 chưa duyệt hoặc đã duyệt rồi 
				nhưng có thể duyệt lại và tất cả những phụ cấp này phải chưa được HR duyệt*/
			--when LOWER(@trangThai) = 'waiting' then ' WHERE [HR_TrangThai] IS NULL and ((IsXetDuyetCap2 = 1 AND NXD2_isHetHanDuyet != 1) or (IsXetDuyetCap1 = 1 AND NXD1_isHetHanDuyet != 1))'

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
	declare @sql_get_total nvarchar(max)
	SET @sql_get_total = 
						'SELECT @TotalItems = COUNT(*) 
						FROM #tmpTC'  + @queryCondition;
	 EXEC sp_executesql 
        @query = @sql_get_total, 
        @params = N'@TotalItems INT OUTPUT', 
        @TotalItems = @TotalItems OUTPUT;

	SELECT #tmpTC.*
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
					ELSE LOWER([NXD2_TrangThai1])
			END) AS [NXD2_TrangThai]
		INTO #TempExcquery
		from #tmpTC

	declare @excQuery nvarchar(max)
	set @excQuery = 'select ( select * 
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
								where np.NhanVienId = [TongHopDuLieuNhanVienId]
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
								where vbn.NhanVienId = [TongHopDuLieuNhanVienId]
									AND (NgayLamViec >= CONVERT(NVARCHAR(10), vbn.ThoiGianBatDau, 25)
									AND NgayLamViec <= CONVERT(NVARCHAR(10), vbn.ThoiGianKetThuc, 25))
									AND vbn.TrangThaiXetDuyet = ''approved''
								FOR JSON PATH
							) as [ViecBenNgoais]
							from #TempExcquery'

		-- công thức tính phân trang
	declare @offSetValue int = ((@pageNumber - 1) * @pageSize) 

		-- fetch dữ liệu phân trang theo offset
	declare @excOffSetValue nvarchar(max)
	set @excOffSetValue = ' ORDER BY [Created], [Id] OFFSET ' + CAST(@offSetValue AS NVARCHAR) + ' ROWS FETCH NEXT ' + CAST(@pageSize AS NVARCHAR) + ' ROWS ONLY FOR JSON PATH) as [Result]'
	
		-- nối chuỗi câu query
	SET @excQuery += @queryCondition + @excOffSetValue

		-- thực thi câu query
	execute(@excQuery)
end
GO
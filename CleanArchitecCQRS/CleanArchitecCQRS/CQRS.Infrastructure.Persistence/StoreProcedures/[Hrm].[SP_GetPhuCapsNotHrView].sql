USE [Esuhai.HRM]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetPhuCapsNotHrView]    Script Date: 10/4/2022 11:50:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*==============================================================================================
   Author			Date			Updated					Description
   -----------------------------------------------------------------------------------------
   Vinh Huynh		2022.10.04		2022.12.15				Get danh sách phụ cấp cho trưởng phòng ban
============================================================================================= */

CREATE PROCEDURE [Hrm].[SP_GetPhuCapsNotHrView]
	@pageNumber int,			-- giá trị số trang hiện tại: không được phép truyền NULL, chỉ được truyền giá trị số nguyên dương (từ 0 trở lên)
	@pageSize int,				-- giá trị số lượng record trong trang: không được phép truyền NULL, chỉ được truyền giá trị số nguyên dương (từ 0 trở lên)
	@nhanvienId nvarchar(max),	-- mã nhân viên
	@thoigianbatdau datetime,	-- thời gian bắt đầu
	@thoigianketthuc datetime,	-- thời gian kết thúc
	@trangThai nvarchar(50),	-- trạng thái của đơn phụ cấp
	@keyword nvarchar(max),		-- từ khóa tìm kiếm
	@TotalItems int output		-- tổng số item
as
begin

	SET NOCOUNT ON;

	DECLARE @dateNow DATETIME
	SET @dateNow = cast(GETDATE() as date)

	-- giới hạn tối đa số ngày XD của NXD1
	declare @limitXD1 int
	set @limitXD1 = 7
	
	-- giới hạn tối đa số ngày XD của NXD2
	declare @limitXD2 int
	set @limitXD2 = 11

	SELECT pc.Id, pc.Created, lpc.Ten as [LoaiPhuCapTen]
		, concat(nv.HoTenDemVN,' ',nv.TenVN) as [HoTenNhanVienVN], nv.PhongId, nv.BanId, nv.Id as [NhanVienId]
		
		, pc.ThoiGianBatDau, pc.ThoiGianKetThuc, pc.TrangThai, pc.MoTa
		, pc.SoLanPhuCap, pc.SoBuoiSang, pc.SoBuoiTrua, pc.SoBuoiChieu, pc.SoQuaDem
		, pc.SoLanCuoiTuan, pc.SoLanNgayLe, pc.SoLanNgayThuong

		, pc.XD_ThoiGianBatDau, pc.XD_ThoiGianKetThuc, pc.XD_SoLanPhuCap
		, pc.XD_SoBuoiSang, pc.XD_SoBuoiTrua, pc.XD_SoBuoiChieu, pc.XD_SoQuaDem

		, pc.HRXetDuyetId, pc.HR_TrangThai, pc.HR_GhiChu

		, pc.NguoiXetDuyetCap1Id, pc.NXD1_TrangThai as [NXD1_TrangThai1], pc.NXD1_GhiChu
		, dateadd(day, @limitXD1, cast(pc.Created as date)) as [NXD1_HanDuyet]
		, (case when pc.NguoiXetDuyetCap1Id = @nhanvienId then cast(1 as bit) else cast(0 as bit) end) as [IsXetDuyetCap1]
		, (CASE WHEN DATEDIFF(DAY, cast(pc.Created as date), @dateNow) > @limitXD1 THEN CAST(1 as BIT) else  CAST(0 as BIT) end) as [NXD1_isHetHanDuyet]
		, concat(tpb1.HoTenDemVN,' ',tpb1.TenVN) as [NXD1_Ten]

		, pc.NguoiXetDuyetCap2Id, pc.NXD2_TrangThai as [NXD2_TrangThai1], pc.NXD2_GhiChu
		, dateadd(day, @limitXD2, cast(pc.Created as date)) as [NXD2_HanDuyet]
		, (case when pc.NguoiXetDuyetCap2Id = @nhanvienId then cast(1 as bit) else cast(0 as bit) end) as [IsXetDuyetCap2]
		, (CASE WHEN DATEDIFF(DAY, cast(pc.Created as date), @dateNow) > @limitXD2 THEN CAST(1 as BIT) else CAST(0 as BIT) end) as [NXD2_isHetHanDuyet]
		, concat(tpb2.HoTenDemVN,' ',tpb2.TenVN) as [NXD2_Ten]

	INTO #tmpPC
	FROM Hrm.PhuCaps pc 
		inner join Hrm.NhanViens nv on nv.Id = pc.NhanVienId 
		inner join Hrm.LoaiPhuCaps lpc on lpc.Id = pc.LoaiPhuCapId
		left join Hrm.NhanViens tpb1 on tpb1.Id = pc.NguoiXetDuyetCap1Id
		left join Hrm.NhanViens tpb2 on tpb2.Id = pc.NguoiXetDuyetCap2Id
	WHERE
		(
			(cast(pc.ThoiGianBatDau as date) between cast(@thoigianbatdau as date) and cast(@thoigianketthuc as date)) 
		or
			(cast(pc.ThoiGianKetThuc as date) between cast(@thoigianbatdau as date) and cast(@thoigianketthuc as date))
		) 
	and 
		(pc.NguoiXetDuyetCap1Id = @nhanvienId or pc.NguoiXetDuyetCap2Id = @nhanvienId)
	and 
		(concat(nv.HoTENDemVN,' ',nv.TenVN) LIKE N'%' +@keyword +'%' OR nv.MaNhanVien LIKE N'%' +@keyword +'%')
	 
		-- so sánh trạng thái duyệt
	declare @queryCondition nvarchar(max) = 
		(case
			/* lấy ra những đơn phụ cấp mà NXD1,2 còn hạn duyệt và tất cả đều chưa được HR chưa duyệt*/
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
	DECLARE @sql_get_total nvarchar(max)
	SET @sql_get_total = 
						'SELECT @TotalItems = COUNT(*) 
						from #tmpPC '  + @queryCondition;
	 EXEC sp_executesql 
        @query = @sql_get_total, 
        @params = N'@TotalItems INT OUTPUT', 
        @TotalItems = @TotalItems OUTPUT;

	SELECT #tmpPC.*
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
		from #tmpPC

	declare @excQuery nvarchar(max)
	set @excQuery = 'select ( select * 
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
							from #TempExcquery pc'

	-- công thức tính phân trang
	declare @offSetValue int = ((@pageNumber - 1) * @pageSize) 

	-- fetch dữ liệu phân trang theo offset
	declare @excOffSetValue nvarchar(max)
	set @excOffSetValue = ' ORDER BY [Created], [Id] OFFSET ' + CAST(@offSetValue AS NVARCHAR) + ' ROWS FETCH NEXT ' + CAST(@pageSize AS NVARCHAR) + ' ROWS ONLY FOR JSON PATH) as [Result]'
	
	-- nối chuỗi câu query
	set @excQuery += @queryCondition + @excOffSetValue

	-- thực thi câu query
	execute(@excQuery)
end
GO
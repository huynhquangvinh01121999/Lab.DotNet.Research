USE [Esuhai.HRMDev]
GO

/****** Object:  StoredProcedure [Hrm].[SP_QuanLyNgayCong]    Script Date: 12/2/2022 3:22:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*==============================================================================================
   Author			Date			Updated					Description
   -----------------------------------------------------------------------------------------
   Vinh Huynh		2022.12.02		2022.12.12				Stored get tổng thông tin liên quan đến ngày công của nhân viên (checkin-out, việc bên ngoài, nghỉ phép,...)
============================================================================================= */

CREATE procedure [Hrm].[SP_QuanLyNgayCong]
		@pageNumber int,
		@pageSize int,
		@thang datetime,
		@phongId int,
		@keyword nvarchar(max),
		@orderBy nvarchar(max),
		@TotalItems INT OUTPUT
as
begin

	SET NOCOUNT ON;

	declare @TongNgayCong float 
	set @TongNgayCong = (select TongNgayCong from Hrm.CauHinhNgayCongs where Nam = year(@thang) and Thang = month(@thang))

	-- get tong so gio viec ben ngoai
	select NhanVienId, sum(SoGio) as [SoGioViecBenNgoai]
	into #tmpVbn
	from Hrm.ViecBenNgoais 
	where   year(@thang) between year(ThoiGianBatDau) and year(ThoiGianKetThuc)
		and month(@thang) between month(ThoiGianBatDau) and month(ThoiGianKetThuc)
	group by NhanVienId

	select THDL.Id, thdl.NhanVienId, nv.MaNhanVien, nv.HoTenDemVN as [HoTenDem], nv.TenVN as [Ten], pb.TenVN as [TenPhong]
		, thdl.NgayLamViec, thdl.isNgayLe, isCuoiTuan, NgayCong, NghiPhep1Id, NghiPhep2Id, DiTre, VeSom
		, (case
			when thdl.isNgayLe = 1 and isCuoiTuan <> 1 then NgayCong
			else 0
		end) as [nc_nghile]

		, (case
			when thdl.isNgayLe <> 1 and (thdl.NghiPhep1Id is not null and thdl.NghiPhep2Id is null) then NgayCong - 0.5
			when thdl.isNgayLe <> 1 and (thdl.NghiPhep1Id is null and thdl.NghiPhep2Id is not null) then NgayCong - 0.5
			when thdl.isNgayLe <> 1 and thdl.NghiPhep1Id is null and thdl.NghiPhep2Id is null then thdl.NgayCong
			else 0
		end) as [nc_ngaythuong]

		, (case
			when thdl.isNgayLe <> 1
				and thdl.NghiPhep1Id is not null
				and thdl.NghiPhep2Id is null
				and thdl.NghiPhep1TrangThaiNghi = N'Hợp lệ' 
			then 0.5
			when thdl.isNgayLe <> 1
				and thdl.NghiPhep2Id is not null
				and thdl.NghiPhep1Id is null
				and thdl.NghiPhep2TrangThaiNghi = N'Hợp lệ' 
			then 0.5
			when thdl.isNgayLe <> 1
				and thdl.NghiPhep1Id is not null
				and thdl.NghiPhep2Id is not null
				and thdl.NghiPhep1TrangThaiNghi = N'Hợp lệ'
				and thdl.NghiPhep2TrangThaiNghi = N'Hợp lệ'
			then 1
			when thdl.isNgayLe <> 1
				and thdl.NghiPhep1Id is not null
				and thdl.NghiPhep2Id is not null
				and thdl.NghiPhep1TrangThaiNghi = N'Hợp lệ'
				and thdl.NghiPhep2TrangThaiNghi = N'Không hợp lệ'
			then 0.5
			when thdl.isNgayLe <> 1
				and thdl.NghiPhep1Id is not null
				and thdl.NghiPhep2Id is not null
				and thdl.NghiPhep1TrangThaiNghi = N'Không Hợp lệ'
				and thdl.NghiPhep2TrangThaiNghi = N'Hợp lệ'
			then 0.5
			else 0
			end) as [nc_nghiphep_hl]

			, (case
			when thdl.isNgayLe <> 1
				and thdl.NghiPhep1Id is not null
				and thdl.NghiPhep2Id is null
				and thdl.NghiPhep1TrangThaiNghi = N'Không hợp lệ' 
			then 0.5
			when thdl.isNgayLe <> 1
				and thdl.NghiPhep2Id is not null
				and thdl.NghiPhep1Id is null
				and thdl.NghiPhep2TrangThaiNghi = N'Không hợp lệ' 
			then 0.5
			when thdl.isNgayLe <> 1
				and thdl.NghiPhep1Id is not null
				and thdl.NghiPhep2Id is not null
				and thdl.NghiPhep1TrangThaiNghi = N'Không hợp lệ'
				and thdl.NghiPhep2TrangThaiNghi = N'Không hợp lệ'
			then 1
			when thdl.isNgayLe <> 1
				and thdl.NghiPhep1Id is not null
				and thdl.NghiPhep2Id is not null
				and thdl.NghiPhep1TrangThaiNghi = N'Hợp lệ'
				and thdl.NghiPhep2TrangThaiNghi = N'Không hợp lệ'
			then 0.5
			when thdl.isNgayLe <> 1
				and thdl.NghiPhep1Id is not null
				and thdl.NghiPhep2Id is not null
				and thdl.NghiPhep1TrangThaiNghi = N'Không Hợp lệ'
				and thdl.NghiPhep2TrangThaiNghi = N'Hợp lệ'
			then 0.5
			else 0
			end) as [nc_nghiphep_khl]
	into #temp
	from hrm.TongHopDuLieus thdl
		inner join hrm.NhanViens nv on thdl.NhanVienId = nv.Id
		left join hrm.PhongBans pb on nv.PhongId = pb.Id
	where YEAR(thdl.NgayLamViec) = year(@thang) 
		and MONTH(thdl.NgayLamViec) = month(@thang)
		and (nv.PhongId = @phongId or @phongId = 0)
		and
			(concat(nv.[HoTENDemVN],' ',nv.[TenVN]) like N'%' + @keyword + '%' 
				or  nv.[MaNhanVien] like N'%' + @keyword + '%' )
	order by thdl.NgayLamViec

	/**/
	select te.NhanVienId, te.MaNhanVien, te.HoTenDem, Ten, TenPhong
		, SUM(nc_nghile) as [SoNgayNghiLe]
		, SUM(nc_ngaythuong) as [SoNgayCong]
		, SUM(nc_nghiphep_hl) as [SoNgayNghiPhepHopLe]
		, SUM(nc_nghiphep_khl) as [SoNgayNghiPhepKhongHopLe]
		, SUM(nc_nghiphep_hl) + SUM(nc_nghiphep_khl) as [SoNgayPhep]
		, round(case when vbn.SoGioViecBenNgoai is null then 0 else vbn.SoGioViecBenNgoai end, 1) as [SoGioViecBenNgoai]
		, sum(DiTre) as [SoPhutDiTre]
		, sum(VeSom) as [SoPhutVeSom]
	into #temp1
	from #temp te
	left join #tmpVbn vbn on vbn.NhanVienId = te.NhanVienId
	group by te.NhanVienId, te.MaNhanVien, te.HoTenDem, Ten, TenPhong, vbn.SoGioViecBenNgoai
	order by te.MaNhanVien

	/*
	* @Description: Tinh tong record co trong query
	*/
	DECLARE @sql_get_total NVARCHAR(max)
	SET @sql_get_total = 'SELECT @TotalItems = COUNT(*) FROM #temp1';

	 EXEC sp_executesql 
        @query = @sql_get_total, 
        @params = N'@TotalItems INT OUTPUT', 
        @TotalItems = @TotalItems OUTPUT;


	/**/
	declare @offSetValue int = ((@pageNumber - 1) * @pageSize) 
	declare @execQuery nvarchar(max)
	set @execQuery = 
	'select NhanVienId, MaNhanVien, HoTenDem, Ten, TenPhong
		, cast((case when SoPhutDiTre is null then 0 else SoPhutDiTre end) as float) as [SoPhutDiTre]
		, cast((case when SoPhutVeSom is null then 0 else SoPhutVeSom end) as float) as [SoPhutVeSom]
		, SoNgayNghiLe, SoNgayCong, cast(SoNgayNghiPhepHopLe as float) as [SoNgayNghiPhepHopLe]
		, cast(SoNgayNghiPhepKhongHopLe as float) as [SoNgayNghiPhepKhongHopLe]
		, cast(SoNgayPhep as float) as [SoNgayPhep], SoGioViecBenNgoai
		, (SoNgayCong + SoNgayNghiLe + SoNgayPhep) as [SoNgayCongThucTe]
		, (' + cast(@TongNgayCong as nvarchar(max)) +' - (SoNgayCong + SoNgayNghiLe + SoNgayPhep)) as [SoNgayCongThieu]
	from #temp1
	order by ' + ISNULL(NULLIF(@orderBy,''), 'TenPhong, Ten') 
		+ ' OFFSET ' + cast(@offSetValue as nvarchar) + ' ROWS FETCH NEXT ' + cast(@pageSize as nvarchar) + ' ROWS ONLY
		
	drop table #tmpVbn
	drop table #temp
	drop table #temp1'

	execute(@execQuery)
end

GO



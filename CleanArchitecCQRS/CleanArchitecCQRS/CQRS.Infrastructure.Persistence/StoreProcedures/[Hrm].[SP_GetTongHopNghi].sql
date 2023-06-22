USE [Esuhai.HRMDev]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetTongHopNghi]    Script Date: 1/5/2023 10:14:45 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [Hrm].[SP_GetTongHopNghi]
	@thang int,
	@nam int
as
begin
	declare @thoiGian datetime 
	set @thoiGian = cast(concat(@nam,'-',@thang,'-01') as datetime)

	CREATE TABLE #NgayLamViec(
		[Id] INT,
		[ThoiGian] varchar(10)
	)

	DECLARE @ngayCuoiThang INT
	SET @ngayCuoiThang = DAY(EOMONTH(@thoiGian))

	DECLARE @cnt INT = 1;
	while @cnt <= @ngayCuoiThang
	begin
		INSERT into #NgayLamViec(Id, ThoiGian) 
		VALUES (@cnt, convert(varchar(10), DATEADD(DAY, @cnt - 1, @thoiGian), 23))
		set @cnt = @cnt + 1
	end

	-----------------

	declare @tongNgayCong float
	set @tongNgayCong = (select TongNgayCong from Hrm.CauHinhNgayCongs where Nam = year(@thoiGian) and Thang = month(@thoiGian))

	select concat(nv.[HoTenDemVN],' ', nv.[TenVN]) as [HoTen]
		, nv.[MaNhanVien], nv.Id as [NhanVienId]
		, (case when thdl.NgayCong is null then 0 else thdl.NgayCong end) as [NgayCong]
		, nlv.ThoiGian as [ThoiGianLamViec]
		, day(nlv.[ThoiGian]) as [Ngay]
		, (CASE
				WHEN DATENAME(dw, nlv.ThoiGian) = 'Monday' Then N'T2'
				WHEN DATENAME(dw, nlv.ThoiGian) = 'Tuesday' Then N'T3'
				WHEN DATENAME(dw, nlv.ThoiGian) = 'Wednesday' Then N'T4'
				WHEN DATENAME(dw, nlv.ThoiGian) = 'Thursday' Then N'T5'
				WHEN DATENAME(dw, nlv.ThoiGian) = 'Friday' Then N'T6'
				WHEN DATENAME(dw, nlv.ThoiGian) = 'Saturday' Then N'T7'					
				ELSE N'CN'
		END) as [Thu]
		, (case 
				--when thdl.[isNgayLe] <> 1 
				--	and thdl.[NghiPhep1Id] is not null 
				--	and thdl.[NghiPhep2Id] is not null 
				--	and thdl.NgayCong = 1 
				--then 1

				--when thdl.[isNgayLe] <> 1 
				--	and thdl.[NghiPhep1Id] is not null 
				--	and thdl.[NghiPhep2Id] is not null 
				--	and thdl.NgayCong = 0.5 
				--then 0.5

				--when thdl.[isNgayLe] <> 1 
				--	and thdl.[NghiPhep1Id] is not null 
				--	and thdl.[NghiPhep2Id] is null
				--then 0.5

				--when thdl.[isNgayLe] <> 1 
				--	and thdl.[NghiPhep1Id] is null 
				--	and thdl.[NghiPhep2Id] is not null 
				--then 0.5

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
		end) as [SoNgayNghiPhep]
		, (case 
				when isNgayLe = 1 and isCuoiTuan <> 1 then NgayCong 
				else 0 
		end) as [SoNgayNghiPhepCheDo]
		, ( case
				when thdl.[isNgayLe] = 1 
					and thdl.[isCuoiTuan] <> 1 
					and thdl.[NghiPhep1Id] is null 
					and thdl.[NghiPhep2Id] is null
					and thdl.NgayCong = 1
				then 'L'

				when thdl.[isNgayLe] = 1 
					and thdl.[isCuoiTuan] <> 1 
					and thdl.[NghiPhep1Id] is null 
					and thdl.[NghiPhep2Id] is null
					and thdl.NgayCong = 0.5
				then 'L/2'

				when thdl.[isNgayLe] <> 1 
					and thdl.[isCuoiTuan] <> 1 
					and thdl.[NghiPhep1Id] is null 
					and thdl.[NghiPhep2Id] is null 
					and thdl.NgayCong = 1 
				then 'X'

				when thdl.[isNgayLe] <> 1 
					and thdl.[isCuoiTuan] <> 1 
					and thdl.[NghiPhep1Id] is null 
					and thdl.[NghiPhep2Id] is null 
					and thdl.NgayCong = 0.5 
				then 'X/2'

				when thdl.[isNgayLe] <> 1 
					and thdl.[NghiPhep1Id] is not null 
					and thdl.[NghiPhep2Id] is not null 
					and thdl.NgayCong = 1 
				then 'P'

				when thdl.[isNgayLe] <> 1 
					and thdl.[isCuoiTuan] <> 1 
					and thdl.[NghiPhep1Id] is not null 
					and thdl.[NghiPhep2Id] is not null 
					and thdl.NgayCong = 0.5 
				then 'P/2'	-- cho xac nhan lai vs HR

				when thdl.[isNgayLe] <> 1 
					and thdl.[isCuoiTuan] <> 1
					and thdl.[NghiPhep1Id] is not null 
					and thdl.[NghiPhep2Id] is null
				then 'P/2' -- cho xac nhan lai vs HR

				when thdl.[isNgayLe] <> 1 
					and thdl.[isCuoiTuan] <> 1
					and thdl.[NghiPhep1Id] is null 
					and thdl.[NghiPhep2Id] is not null 
				then 'P/2' -- cho xac nhan lai vs HR

				when DATENAME(dw, nlv.ThoiGian) = 'Sunday' then 'CN'
				else 'NO'
		end) as [StrNgayLamViec]
	into #tmpThdlNv
	from Hrm.NhanViens nv
	left join Esuhai.DanhMucs dm on dm.[Id] = nv.[TrangThaiId]
	left join #NgayLamViec nlv on 1 = 1
	left join Hrm.TongHopDuLieus thdl on thdl.NgayLamViec = nlv.ThoiGian and thdl.NhanVienId = nv.Id
	where dm.[TenVN] <> N'Thôi việc'

	SELECT 
		top(1)
		STUFF((
				SELECT ',' + cast(nv1.[Ngay] as varchar(2))
				FROM #tmpThdlNv nv1 where nv1.[NhanVienId] = nv.[NhanVienId]
				order by nv1.HoTen, nv1.ThoiGianLamViec
				FOR XML PATH('')
				), 1, 1, '') 
				+ '|'
				+ STUFF((
				SELECT ',' + nv2.[Thu]
				FROM #tmpThdlNv nv2 where nv2.[NhanVienId] = nv.[NhanVienId]
				order by nv2.HoTen, nv2.ThoiGianLamViec
				FOR XML PATH('')
				), 1, 1, '') as [StrNgayLamViec]
		, STUFF((
				SELECT '|' + cast(nv1.[Ngay] as varchar(2))
				FROM #tmpThdlNv nv1 where nv1.[NhanVienId] = nv.[NhanVienId] and DATEPART(w, cast(concat(@nam,'-',@thang,'-',nv1.[Ngay]) as datetime)) = 1
				order by nv1.HoTen, nv1.ThoiGianLamViec
				FOR XML PATH('')
				), 1, 1, '') as [HoTen]
	into #tmpRow1
	FROM #tmpThdlNv nv
	group by HoTen, MaNhanVien, NhanVienId

	SELECT HoTen, MaNhanVien, NhanVienId
		,STUFF((
				SELECT ',' + nv1.[StrNgayLamViec]
				FROM #tmpThdlNv nv1 where nv1.[NhanVienId] = nv.[NhanVienId]
				order by nv1.HoTen, nv1.ThoiGianLamViec
				FOR XML PATH('')
				), 1, 1, '') as [StrNgayLamViec]
	into #tmpNlv
	FROM #tmpThdlNv nv
	group by HoTen, MaNhanVien, NhanVienId

	select thdl.HoTen, thdl.MaNhanVien, thdl.NhanVienId
		, sum(thdl.NgayCong) as [NgayCong]
		, sum(cast(thdl.SoNgayNghiPhep as float)) as [NghiPhep]
		, sum(cast(thdl.SoNgayNghiPhepCheDo as float)) as [NghiPhepCheDo]
		, (sum(cast(thdl.SoNgayNghiPhep as float)) + sum(cast(thdl.SoNgayNghiPhepCheDo as float))) as [TongNghi]
		, (round(isnull((select sum(vbn.SoGio) from Hrm.ViecBenNgoais vbn
			where (year(@thoiGian) between year(vbn.ThoiGianBatDau) and year(vbn.ThoiGianKetThuc)) and 
				  (month(@thoiGian) between month(vbn.ThoiGianBatDau) and month(vbn.ThoiGianKetThuc)) and
				  LoaiCongTac = N'Cá nhân' and
				  vbn.NhanVienId = thdl.NhanVienId),0),0)
		) as [ViecRieng]
		, (case when @tongNgayCong is null then 0 else @tongNgayCong end) as [TongNgayCong]
		, t2.StrNgayLamViec
	into #tmpTongHop
	from #tmpThdlNv thdl
	left join #tmpNlv t2 on t2.NhanVienId = thdl.NhanVienId
	group by thdl.HoTen, thdl.MaNhanVien, thdl.NhanVienId, t2.StrNgayLamViec

	select concat('|',[HoTen],'|') as [HoTen], null as [MaNhanVien]
		, null as [NgayCongThucTe], null as [NghiPhep], null as [NghiPhepCheDo]
		, null as [TongNghi]
		, (case when @tongNgayCong is null then 0 else @tongNgayCong end) as [TongNgayCong]
		, null as [ViecRieng], StrNgayLamViec from #tmpRow1
	union
	select [HoTen], [MaNhanVien]
		, ([NgayCong] - [NghiPhep] - [NghiPhepCheDo]) as [NgayCongThucTe]
		, [NghiPhep], [NghiPhepCheDo]
		, ([NghiPhep] + [NghiPhepCheDo]) as [TongNghi]
		, [TongNgayCong], [ViecRieng] * 60 as [ViecRieng], [StrNgayLamViec] from #tmpTongHop 

	drop table #NgayLamViec
	drop table #tmpThdlNv
	drop table #tmpRow1
	drop table #tmpNlv
	drop table #tmpTongHop
end
GO
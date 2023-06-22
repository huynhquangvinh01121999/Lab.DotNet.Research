USE [Esuhai.HRMDev]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetTimesheetsPhongBanV3C1C2]    Script Date: 11/29/2022 2:21:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [Hrm].[SP_GetTimesheetsPhongBanV3C1C2]
	@pageNumber int,
	@pageSize int,
	@thoiGian datetime,
	@nhanvienId nvarchar(max),
	@keyword nvarchar(max),
	@TotalItems int output
as
begin
	
	declare @thoihan int
	set @thoihan = 5
	
	create table #TongHop(
		NhanVienId uniqueidentifier not null,
		HoTen nvarchar(100) not null,
		NgayLamViec datetime not null,
		PhanLoai nvarchar(100) null,
		ThoiGianBatDau datetime null,
		ThoiGianKetThuc datetime null,
		TrangThai nvarchar(100) null
	)

	CREATE TABLE #DayWork(
		[Id] INT,
		[Workday] varchar(10)
	)

	DECLARE @lastDoM INT
	SET @lastDoM = DAY(EOMONTH(@thoiGian))

	DECLARE @cnt INT = 1;
	while @cnt <= @lastDoM
	begin
		INSERT into #DayWork(Id, Workday) 
		VALUES (@cnt, convert(varchar(10), DATEADD(DAY, @cnt - 1, @thoiGian), 23))
		set @cnt = @cnt + 1
	end

	---------------------

	select nv.[Id] as [NhanVienId], nv.[MaNhanVien], pb.TenVN as [PhongBanTen]
		, concat(nv.[HoTENDemVN],' ',nv.[TenVN]) as [HoTen], nv.[TenVN]
	into #tnv
	from Hrm.NhanViens nv
	left join Hrm.PhongBans pb on pb.Id = nv.PhongId
	left join Esuhai.DanhMucs dm on dm.[Id] = nv.[TrangThaiId]
	where 
				(nv.[XetDuyetCap1] = @nhanvienId or nv.[XetDuyetCap2] = @nhanvienId)
			and
				(
					concat(nv.[HoTENDemVN],' ',nv.[TenVN]) like N'%' +@keyword +'%' 
				or 
					nv.[MaNhanVien] like N'%' +@keyword +'%'
				)
			and 
				dm.[TenVN] <> N'Thôi việc'
	
	/*
	* @Description: return total record
	* @Return: int
	*/
	DECLARE @sql_get_total NVARCHAR(MAX)
	SET @sql_get_total = '
						SELECT @TotalItems = COUNT(*) 
						FROM #tnv';
	 EXEC sp_executesql 
        @query = @sql_get_total, 
        @params = N'@TotalItems INT OUTPUT', 
        @TotalItems = @TotalItems OUTPUT;

	/*
	* @Description: Paging with pageSize & pageNumber
	*/
	SELECT * INTO #tnv2
	FROM #tnv
	ORDER BY [TenVN] OFFSET ((@pageNumber - 1) * @pageSize) ROWS FETCH NEXT @pageSize ROWS ONLY
	
	/*
	* @Description: Select danh sách tất cả các ngày làm việc trong tháng của nhân viên và đưa vào table tạm để sử dụng cho các query insert bên dưới
	*/
	select #tnv2.*, info.[Workday]
		, thdl.[Timesheet_GioVao], thdl.[Timesheet_GioRa], thdl.[NgayLamViec], thdl.[isCuoiTuan]
		, thdl.[CaLamViec_BatDau], thdl.[CaLamViec_BatDauNghi], thdl.[CaLamViec_KetThucNghi], thdl.[CaLamViec_KetThuc]
	into #tmpTH
	from #tnv2
	left join #DayWork info on 1 = 1
	left join Hrm.TongHopDuLieus thdl on thdl.[NhanVienId] = #tnv2.[NhanVienId] and thdl.[NgayLamViec]  = info.[Workday]

	/*
	* @Description: Insert data CheckIn-Out các ngày trong tháng của nhân viên vào table #TongHop
	*/
	insert into #TongHop(NhanVienId, HoTen, NgayLamViec, PhanLoai, ThoiGianBatDau, ThoiGianKetThuc, TrangThai) 
	select [NhanVienId], [HoTen]
		, (case 
				when [NgayLamViec] is null then [Workday]
				else [NgayLamViec]
			end
		) as [NgayLamViec]
		, N'CIO' as [PhanLoai]
		, [Timesheet_GioVao] as [ThoiGianBatDau], [Timesheet_GioRa] as [ThoiGianKetThuc]
		, (case
				when (datediff(MINUTE, #tmpTH.[CaLamViec_BatDau], #tmpTH.[Timesheet_GioVao]) >= @thoihan)
					or #tmpTH.[Timesheet_GioVao] is null then N'Đi trễ'

				when #tmpTH.[Timesheet_GioRa] is null
					or (datediff(MINUTE, #tmpTH.[Timesheet_GioRa], #tmpTH.[CaLamViec_KetThuc]) >= @thoihan) then N'Về sớm'
		end) as [TrangThai]
	from #tmpTH
	where 
		( [Timesheet_GioVao] is not null and [Timesheet_GioRa] is null ) or
		( [Timesheet_GioVao] is null and [Timesheet_GioRa] is not null ) or
		( [Timesheet_GioVao] is not null and [Timesheet_GioRa] is not null )
	order by [NhanVienId], [Workday]
	
	/*
	* @Description: Insert data Nghỉ phép có trong các ngày trong tháng của nhân viên vào table #TongHop
	*/
	insert into #TongHop(NhanVienId, HoTen, NgayLamViec, PhanLoai, ThoiGianBatDau, ThoiGianKetThuc, TrangThai) 
	select #tmpTH.[NhanVienId], #tmpTH.[HoTen]
		, (case 
				when #tmpTH.[NgayLamViec] is null then #tmpTH.[Workday]
				else #tmpTH.[NgayLamViec]
			end
		) as [NgayLamViec]
		, N'NPH' as [PhanLoai]
		, (case
				when np.[ThoiGianBatDau] between #tmpTH.[CaLamViec_BatDau] and #tmpTH.[CaLamViec_BatDauNghi] then np.[ThoiGianBatDau]
				when np.[ThoiGianBatDau] between #tmpTH.[CaLamViec_KetThucNghi] and #tmpTH.[CaLamViec_KetThuc] then #tmpTH.[CaLamViec_KetThucNghi]
				when np.[ThoiGianBatDau] < #tmpTH.[CaLamViec_BatDau] then #tmpTH.[CaLamViec_BatDau]
		end) as [ThoiGianBatDau]
		, (case
				when DATEPART(w, #tmpTH.[NgayLamViec]) = 7 then CaLamViec_BatDauNghi
				when np.[ThoiGianKetThuc] between #tmpTH.[CaLamViec_BatDau] and #tmpTH.[CaLamViec_BatDauNghi] then #tmpTH.[CaLamViec_BatDauNghi]
				when np.[ThoiGianKetThuc] between #tmpTH.[CaLamViec_KetThucNghi] and #tmpTH.[CaLamViec_KetThuc] then np.[ThoiGianKetThuc]
				when np.[ThoiGianKetThuc] > #tmpTH.[CaLamViec_KetThuc] then #tmpTH.[CaLamViec_KetThuc]
		end) as [ThoiGianKetThuc]
		, (case
				when np.TrangThaiDangKy = N'Nghỉ đột xuất' then N'Không hợp lệ' else np.TrangThaiNghi
		end) as [TrangThai]
	from #tmpTH
	left join Hrm.NghiPheps np on 
			(#tmpTH.[NgayLamViec] between cast(np.[ThoiGianBatDau] as date) and cast(np.[ThoiGianKetThuc] as date))
		and
			np.[NhanVienId] = #tmpTH.[NhanVienId]
	where 
			(
				(np.[ThoiGianBatDau] is not null and np.[ThoiGianKetThuc] is null) or
				(np.[ThoiGianBatDau] is null and np.[ThoiGianKetThuc] is not null) or
				(np.[ThoiGianBatDau] is not null and np.[ThoiGianKetThuc] is not null)
			)
		and #tmpTH.[isCuoiTuan] <> 1
	order by #tmpTH.[NhanVienId], #tmpTH.[Workday]
	
	/*
	* @Description: Insert data Việc bên ngoài có trong các ngày trong tháng của nhân viên vào table #TongHop
	*/
	insert into #TongHop(NhanVienId, HoTen, NgayLamViec, PhanLoai, ThoiGianBatDau, ThoiGianKetThuc) 
	select #tmpTH.[NhanVienId], #tmpTH.[HoTen]
		, (case 
				when #tmpTH.[NgayLamViec] is null then #tmpTH.[Workday]
				else #tmpTH.[NgayLamViec]
			end
		) as [NgayLamViec]
		, N'VBN' as [PhanLoai]
		, (case
				when #tmpTH.[NgayLamViec] = CONVERT(NVARCHAR(10), vbn.[ThoiGianBatDau], 25) then vbn.[ThoiGianBatDau]
				else #tmpTH.[CaLamViec_BatDau]
		end) as [ThoiGianBatDau]
		, (case
				when #tmpTH.[NgayLamViec] = CONVERT(NVARCHAR(10), vbn.[ThoiGianKetThuc], 25) then vbn.[ThoiGianKetThuc]
				else #tmpTH.[CaLamViec_KetThuc]
		end) as [ThoiGianKetThuc]
	from #tmpTH
	left join Hrm.ViecBenNgoais vbn on
			(#tmpTH.[NgayLamViec] between cast(vbn.[ThoiGianBatDau] as date) and cast(vbn.[ThoiGianKetThuc] as date))
		and
			vbn.[NhanVienId] = #tmpTH.[NhanVienId]
	where 
		(vbn.[ThoiGianBatDau] is not null and vbn.[ThoiGianKetThuc] is null) or
		(vbn.[ThoiGianBatDau] is null and vbn.[ThoiGianKetThuc] is not null) or
		(vbn.[ThoiGianBatDau] is not null and vbn.[ThoiGianKetThuc] is not null)
	order by #tmpTH.[NhanVienId], #tmpTH.[Workday]

	/*
	* @Description: Format result from execute query to json text
	* @Return: json text
	*/
	select(
		select #tnv2.*
			, thdl.[NgayCong] as [SoNgayCong]
			, (case 
					when thdl.[NgayLamViec] is null then info.Workday
					else thdl.[NgayLamViec]
			end) as [NgayLamViec]
			,( CASE
					WHEN DATENAME(dw, info.Workday) = 'Monday' Then N'T.2'
					WHEN DATENAME(dw, info.Workday) = 'Tuesday' Then N'T.3'
					WHEN DATENAME(dw, info.Workday) = 'Wednesday' Then N'T.4'
					WHEN DATENAME(dw, info.Workday) = 'Thursday' Then N'T.5'
					WHEN DATENAME(dw, info.Workday) = 'Friday' Then N'T.6'
					WHEN DATENAME(dw, info.Workday) = 'Saturday' Then N'T.7'					
				ELSE N'CN'
			END) as [Thu]
			,(
				select th.[PhanLoai], th.[ThoiGianBatDau], th.[ThoiGianKetThuc], th.[TrangThai]
				from #TongHop th
				where thdl.[NgayLamViec] = th.[NgayLamViec] and #tnv2.[NhanVienId] = th.[NhanVienId]
				for json auto
			) as [List]
		from #tnv2
		left join #DayWork info on 1 = 1
		left join Hrm.TongHopDuLieus thdl on thdl.[NhanVienId] = #tnv2.[NhanVienId] and thdl.[NgayLamViec]  = info.[Workday]
		order by #tnv2.[TenVN], #tnv2.[NhanVienId], info.[Workday]
		for json auto
	) as [Result]

	/*
	* @Description remove tables temp
	*/
	drop table #tnv
	drop table #tnv2
	drop table #tmpTH
	drop table #TongHop
	drop table #DayWork
end
GO



USE [Esuhai.HRM]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetTimesheetsPhongBanHr]    Script Date: 11/7/2022 10:23:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*==============================================================================================
   Author			Date			Updated					Description
   -----------------------------------------------------------------------------------------
   Vinh Huynh		2022.11.07		2022.11.10				Get danh sách timesheet cho HR
============================================================================================= */

CREATE procedure [Hrm].[SP_GetTimesheetsPhongBanHr]
	@pageNumber int,
	@pageSize int,
	@thoiGian datetime,
	@phongId int,
	@banId int,
	@keyword nvarchar(max),
	@TotalItems int output
as
begin

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

	select [Id]
		, ([HoTenDemVN] + ' ' + [TenVN]) as [HoTen]
	into #tnv
	from Hrm.NhanViens
	where 
				([PhongId] = @phongId or @phongId = 0) 
			and 
				([BanId] = @banId or @banId = 0)
			and
				(
					([HoTENDemVN] + ' ' + [TenVN]) like N'%' +@keyword +'%' 
				or 
					 [MaNhanVien] like N'%' +@keyword +'%'
				)

	-- đếm tổng record query
	DECLARE @sql_get_total NVARCHAR(MAX)
	SET @sql_get_total = '
						SELECT @TotalItems = COUNT(*) 
						FROM #tnv';
	 EXEC sp_executesql 
        @query = @sql_get_total, 
        @params = N'@TotalItems INT OUTPUT', 
        @TotalItems = @TotalItems OUTPUT;

	SELECT * INTO #tnv2 
	FROM #tnv
	ORDER BY Id OFFSET ((@pageNumber - 1) * @pageSize) ROWS FETCH NEXT @pageSize ROWS ONLY

	select (
		select #tnv2.*
		, info.[Id] as 'Stt'
		, (case 
				when ts.[NgayLamViec] is null then info.[Workday]
				else ts.[NgayLamViec]
			end
		) as [NgayLamViec]
		, CASE
			WHEN DATENAME(dw, info.[Workday]) = 'Monday' Then N'T.2'
			WHEN DATENAME(dw, info.[Workday]) = 'Tuesday' Then N'T.3'
			WHEN DATENAME(dw, info.[Workday]) = 'Wednesday' Then N'T.4'
			WHEN DATENAME(dw, info.[Workday]) = 'Thursday' Then N'T.5'
			WHEN DATENAME(dw, info.[Workday]) = 'Friday' Then N'T.6'
			WHEN DATENAME(dw, info.[Workday]) = 'Saturday' Then N'T.7'					
			ELSE N'CN'
		END as [Thu]
		, convert(varchar(5), ts.[GioVao], 24) as [GioVao]
		, convert(varchar(5), ts.[GioRa], 24) as [GioRa]
		, cast(thdl.[NgayCong] as varchar) as [NgayCong]
		, ( SELECT np.[ThoiGianBatDau], np.[ThoiGianKetThuc], np.[SoNgayDangKy], np.[TrangThaiNghi]
			FROM Hrm.NghiPheps np
			WHERE np.[NhanVienId] = #tnv2.[Id]
				AND np.[TrangThaiNghi] in (N''+N'Nghỉ' +'', N''+N'Nghỉ đột xuất' +'')
				AND (ts.[NgayLamViec] >= CONVERT(NVARCHAR(10), np.[ThoiGianBatDau], 25) AND 
					ts.[NgayLamViec] <= CONVERT(NVARCHAR(10), np.[ThoiGianKetThuc], 25))
			FOR JSON PATH
		) as [NghiPheps]
		FROM #tnv2
		left join #DayWork info on 1 = 1
		left join Hrm.Timesheets ts on ts.[NhanVienId] = #tnv2.[Id] and ts.[NgayLamViec] = info.[Workday]
		left join Hrm.TongHopDuLieus thdl on thdl.[NhanVienId] = ts.[NhanVienId] and thdl.[NgayLamViec] = ts.[NgayLamViec]
		FOR JSON AUTO
	) as [Result]

end
GO



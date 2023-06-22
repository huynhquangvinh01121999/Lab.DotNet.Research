USE [Esuhai.HRM]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetTimesheetsPhongBanV2Hr]    Script Date: 11/11/2022 2:32:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*==============================================================================================
   Author			Date			Updated					Description
   -----------------------------------------------------------------------------------------
   Vinh Huynh		2022.11.11		2022.11.21				Get danh sách timesheet phòng ban cho HR version 2
============================================================================================= */

CREATE procedure [Hrm].[SP_GetTimesheetsPhongBanV2Hr]
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

	select nv.[Id] as [NhanVienId]
		, concat(nv.[HoTENDemVN],' ',nv.[TenVN]) as [HoTen]
	into #tnv
	from Hrm.NhanViens nv
	left join Esuhai.DanhMucs dm on dm.[Id] = nv.[TrangThaiId]
	where 
				(nv.[PhongId] = @phongId or @phongId = 0) 
			and 
				(nv.[BanId] = @banId or @banId = 0)
			and
				(
					concat(nv.[HoTENDemVN],' ',nv.[TenVN]) like N'%' +@keyword +'%' 
				or 
					 nv.[MaNhanVien] like N'%' +@keyword +'%'
				)
			and
				dm.[TenVN] != N'Thôi việc'	-- ko get ~ nv thôi việc

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
	ORDER BY NhanVienId OFFSET ((@pageNumber - 1) * @pageSize) ROWS FETCH NEXT @pageSize ROWS ONLY

	select (
		select #tnv2.*
		, info.[Id] as [Stt]
		, cast(ts.Id as varchar(100)) as [Id]
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

		, cast(thdl.[NgayCong] as varchar) as [NgayCong]
		
		-- ============= START CA 1
		, ( select 
					(case
						when thdl.NghiPhepId is not null and NghiPhepThoiGianBatDau between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi then NghiPhepThoiGianBatDau
						when thdl.NghiPhepId is not null and NghiPhepThoiGianBatDau < thdl.CaLamViec_BatDau then NghiPhepThoiGianBatDau
						when thdl.NghiPhepId is not null and NghiPhepThoiGianBatDau > thdl.CaLamViec_BatDauNghi and thdl.Timesheet_GioVao between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi then thdl.Timesheet_GioVao
						when thdl.NghiPhepId is not null and NghiPhepThoiGianBatDau > thdl.CaLamViec_BatDauNghi and thdl.Timesheet_GioVao < thdl.CaLamViec_BatDau then thdl.Timesheet_GioVao
						
						when thdl.NghiPhepId is null and thdl.Timesheet_GioVao between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi then thdl.Timesheet_GioVao
						when thdl.NghiPhepId is null and thdl.Timesheet_GioVao < thdl.CaLamViec_BatDau then thdl.Timesheet_GioVao

						when thdl.Timesheet_GioRa is null and (thdl.Timesheet_GioVao between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi or thdl.Timesheet_GioVao < thdl.CaLamViec_BatDau) then thdl.Timesheet_GioVao
					end) as [TGBD_Display]
					,(case
						when thdl.NghiPhepId is not null and NghiPhepThoiGianKetThuc between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi then NghiPhepThoiGianKetThuc
						when thdl.NghiPhepId is not null and NghiPhepThoiGianKetThuc < thdl.CaLamViec_KetThucNghi then NghiPhepThoiGianKetThuc
						when thdl.NghiPhepId is not null and NghiPhepThoiGianBatDau < thdl.CaLamViec_BatDauNghi and NghiPhepThoiGianKetThuc > thdl.CaLamViec_KetThucNghi then thdl.CaLamViec_BatDauNghi
						when thdl.NghiPhepId is not null and NghiPhepThoiGianBatDau >= thdl.CaLamViec_KetThucNghi then thdl.Timesheet_GioRa

						when thdl.NghiPhepId is null and thdl.Timesheet_GioRa between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi then thdl.Timesheet_GioRa
						when thdl.NghiPhepId is null and thdl.Timesheet_GioRa between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc then thdl.CaLamViec_BatDauNghi
						when thdl.NghiPhepId is null and thdl.CaLamViec_KetThuc = thdl.CaLamViec_BatDauNghi and thdl.Timesheet_GioRa > thdl.CaLamViec_BatDauNghi then thdl.Timesheet_GioRa
						when thdl.NghiPhepId is null and thdl.Timesheet_GioRa > thdl.CaLamViec_KetThuc then thdl.CaLamViec_BatDauNghi
						when thdl.NghiPhepId is null and thdl.Timesheet_GioRa < thdl.CaLamViec_KetThucNghi then thdl.Timesheet_GioRa

						when thdl.Timesheet_GioRa is null and (thdl.Timesheet_GioVao between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi or thdl.Timesheet_GioVao < thdl.CaLamViec_BatDau) then thdl.CaLamViec_BatDauNghi
					end) as [TGKT_Display]
					,(case
						when 
								thdl.NghiPhepId is not null 
							and 
								(
									(NghiPhepThoiGianBatDau between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi)
								or
									NghiPhepThoiGianBatDau < thdl.CaLamViec_BatDau
								)
							then cast(1 as bit) else cast(0 as bit)
					end) as [isNghiPhep]
			FOR JSON PATH
		) as [Ca1]
		-- ============= END CA SÁNG

		-- ============= START CA CHIỀU
		, ( select 
					(case
						when NghiPhepId is not null 
								and NghiPhepThoiGianBatDau between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi 
								and NghiPhepThoiGianKetThuc between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc then thdl.CaLamViec_KetThucNghi

						when NghiPhepId is not null
								and NghiPhepThoiGianBatDau between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi 
								and NghiPhepThoiGianKetThuc > thdl.CaLamViec_KetThuc then thdl.CaLamViec_KetThucNghi

						when NghiPhepId is not null
								and NghiPhepThoiGianBatDau < thdl.CaLamViec_BatDau
								and NghiPhepThoiGianKetThuc between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc then thdl.CaLamViec_KetThucNghi

						when NghiPhepId is not null
								and NghiPhepThoiGianBatDau < thdl.CaLamViec_BatDau
								and NghiPhepThoiGianKetThuc > thdl.CaLamViec_KetThuc then thdl.CaLamViec_KetThucNghi

						when NghiPhepId is not null
								and NghiPhepThoiGianBatDau between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc
								and NghiPhepThoiGianKetThuc between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc then NghiPhepThoiGianBatDau

						when NghiPhepId is not null
								and NghiPhepThoiGianBatDau between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc
								and NghiPhepThoiGianKetThuc > thdl.CaLamViec_KetThuc then NghiPhepThoiGianBatDau

						when NghiPhepId is not null
								and ((NghiPhepThoiGianBatDau between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi) or (NghiPhepThoiGianBatDau < thdl.CaLamViec_BatDau))
								and NghiPhepThoiGianKetThuc between thdl.CaLamViec_BatDau and thdl.CaLamViec_KetThucNghi then thdl.Timesheet_GioVao

						when NghiPhepId is null
								and ((thdl.Timesheet_GioVao between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi) or (thdl.Timesheet_GioVao < thdl.CaLamViec_BatDau))
								and ((thdl.Timesheet_GioRa between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc) or (thdl.Timesheet_GioRa > thdl.CaLamViec_KetThuc)) then thdl.CaLamViec_KetThucNghi

						when NghiPhepId is null
								and thdl.Timesheet_GioVao between thdl.CaLamViec_BatDauNghi and thdl.CaLamViec_KetThuc
								and thdl.Timesheet_GioRa >= thdl.CaLamViec_KetThucNghi then thdl.Timesheet_GioVao

						when thdl.Timesheet_GioVao is null and (thdl.Timesheet_GioRa between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc or thdl.Timesheet_GioRa > thdl.CaLamViec_KetThuc) then thdl.CaLamViec_KetThucNghi
						when thdl.Timesheet_GioRa is null and (thdl.Timesheet_GioVao between thdl.CaLamViec_BatDauNghi and thdl.CaLamViec_KetThuc) then thdl.Timesheet_GioVao
					end) as [TGBD_Display]
					,(case
						when NghiPhepId is not null
							and ((NghiPhepThoiGianBatDau between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi) or NghiPhepThoiGianBatDau < thdl.CaLamViec_BatDau)
							and ((NghiPhepThoiGianKetThuc between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc) or NghiPhepThoiGianKetThuc > thdl.CaLamViec_KetThuc) then thdl.CaLamViec_KetThuc
						
						when NghiPhepId is not null
							and ((NghiPhepThoiGianBatDau between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi) or NghiPhepThoiGianBatDau < thdl.CaLamViec_BatDau)
							and NghiPhepThoiGianKetThuc <= thdl.CaLamViec_KetThucNghi then thdl.Timesheet_GioRa
						
						when NghiPhepId is not null
							and NghiPhepThoiGianBatDau >= thdl.CaLamViec_BatDauNghi
							and ((NghiPhepThoiGianKetThuc between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc) or NghiPhepThoiGianKetThuc > thdl.CaLamViec_KetThuc) then thdl.CaLamViec_KetThuc
					
						when NghiPhepId is null
							and ((thdl.Timesheet_GioVao between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi) or thdl.Timesheet_GioVao < thdl.CaLamViec_BatDau)
							and ((thdl.Timesheet_GioRa between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc) or thdl.Timesheet_GioRa > thdl.CaLamViec_KetThuc) 
							and thdl.CaLamViec_BatDauNghi < thdl.CaLamViec_KetThuc then thdl.Timesheet_GioRa
						
						when NghiPhepId is null
							and thdl.Timesheet_GioVao >= thdl.CaLamViec_BatDauNghi
							and ((thdl.Timesheet_GioRa between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc) or thdl.Timesheet_GioRa > thdl.CaLamViec_KetThuc) then thdl.Timesheet_GioRa

						when thdl.Timesheet_GioVao is null and (thdl.Timesheet_GioRa between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc or thdl.Timesheet_GioRa > thdl.CaLamViec_KetThuc) then thdl.Timesheet_GioRa
					end) as [TGKT_Display]
					,(case
						when 
								thdl.NghiPhepId is not null
							and 
								(
									(NghiPhepThoiGianBatDau between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi)
								or
									NghiPhepThoiGianBatDau < thdl.CaLamViec_BatDau
								or
									NghiPhepThoiGianBatDau between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc
								)
							and
								(
									(NghiPhepThoiGianKetThuc between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc)
								or
									NghiPhepThoiGianKetThuc > thdl.CaLamViec_KetThuc
								)
							then cast(1 as bit) else cast(0 as bit)
					end) as [isNghiPhep]
			FOR JSON PATH
		) as [Ca2]
		-- ============= END CA CHIỀU

		FROM #tnv2
		left join #DayWork info on 1 = 1
		left join Hrm.Timesheets ts on ts.[NhanVienId] = #tnv2.[NhanVienId] and ts.[NgayLamViec] = info.[Workday]
		left join Hrm.TongHopDuLieus thdl on thdl.[NhanVienId] = ts.[NhanVienId] and thdl.[NgayLamViec] = ts.[NgayLamViec]
		order by #tnv2.[NhanVienId], info.[Workday] 
		FOR JSON AUTO
	) as [Result]

end
GO



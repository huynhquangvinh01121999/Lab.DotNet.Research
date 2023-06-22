USE [Esuhai.HRM]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetTimesheetsNvNotHrView]    Script Date: 11/2/2022 5:16:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [Hrm].[SP_GetTimesheetsPhongBan]
	@pageNumber int,
	@pageSize int,
	@thoiGian datetime,
	@TotalItems int output
as
begin

	CREATE TABLE #DayWork(
		Id INT,
		Workday varchar(10)
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

	-- đếm tổng record query
	DECLARE @sql_get_total NVARCHAR(MAX)
	SET @sql_get_total = 
						'SELECT @TotalItems = COUNT(*) FROM Hrm.NhanViens';
	 EXEC sp_executesql 
        @query = @sql_get_total, 
        @params = N'@TotalItems INT OUTPUT', 
        @TotalItems = @TotalItems OUTPUT;

	select (
		select nv.Id
		, (nv.HoTenDemVN + ' ' + nv.TenVN) as HoTen
		, info.Id as 'Stt'
		, (
			case 
				when ts.NgayLamViec is null then info.Workday
				else ts.NgayLamViec
			end
		) as NgayLamViec
		, convert(varchar(5), ts.GioVao, 24) as GioVao
		, convert(varchar(5), ts.GioRa, 24) as GioRa
		FROM Hrm.NhanViens nv
		left join #DayWork info on 1 = 1
		left join Hrm.Timesheets ts on ts.NhanVienId = nv.Id and ts.NgayLamViec = info.Workday
		ORDER BY Id OFFSET ((@pageNumber - 1) * @pageSize * @lastDoM) ROWS FETCH NEXT (@pageSize * @lastDoM) ROWS ONLY
		FOR JSON AUTO
	) as Result

end
GO



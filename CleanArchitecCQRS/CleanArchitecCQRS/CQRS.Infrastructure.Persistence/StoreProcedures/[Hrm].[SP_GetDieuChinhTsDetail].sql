USE [Esuhai.HRMDev]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetDieuChinhTsDetail]    Script Date: 11/18/2022 3:02:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*==============================================================================================
   Author			Date			Updated					Description
   -----------------------------------------------------------------------------------------
   Nam Le			2022.11.18		2022.12.08				Get thông tin timesheet theo id
============================================================================================= */

CREATE PROCEDURE [Hrm].[SP_GetDieuChinhTsDetail]
		@id uniqueidentifier
AS
BEGIN
	
	SET NOCOUNT ON;

	declare @dnow datetime
	set @dnow = GETDATE()

	select te.Id, te.NgayGoiDon, te.DieuChinh_GhiChu
		, te.NguoiXetDuyetCap1Id, te.NXD1_TrangThai, te.NXD1_GhiChu, DATEADD(day, 7, te.NgayGoiDon) as [NXD1_HanDuyet]
		, (case
			when te.NguoiXetDuyetCap1Id is null then N'no'
			when te.NguoiXetDuyetCap1Id IS NOT NULL and te.NXD1_TrangThai IS NULL AND DATEDIFF(day, te.ngaygoidon, @dnow) > 7 then N'error'
			when te.NguoiXetDuyetCap1Id IS NOT NULL and te.NXD1_TrangThai IS NULL and DATEDIFF(day, te.ngaygoidon, @dnow) < 8 and te.NXD2_TrangThai IS NULL and te.HR_TrangThai IS NULL then N'process'
			when te.NXD1_TrangThai = N'Approved' then N'finish'
			when te.NXD1_TrangThai = N'Rejected' then N'error' end) as [NXD1_Display]
		, concat(nv1.HoTenDemVN,' ', nv1.TenVN) as [NXD1_Ten]
		
		, te.NguoiXetDuyetCap2Id, te.NXD2_TrangThai, te.NXD2_GhiChu, DATEADD(day, 11, te.NgayGoiDon) as [NXD2_HanDuyet]
		, (case
			when te.NguoiXetDuyetCap2Id is null then N'no'
			when te.NguoiXetDuyetCap2Id IS NOT NULL and te.NXD2_TrangThai IS NULL AND DATEDIFF(day, te.ngaygoidon, @dnow) > 11 then N'error'
			when te.NguoiXetDuyetCap2Id IS NOT NULL and te.NXD2_TrangThai IS NULL and DATEDIFF(day, te.ngaygoidon,@dnow) < 12 and te.HR_TrangThai IS NULL then
				case
					when te.NguoiXetDuyetCap1Id is not null and te.NXD1_TrangThai is null and DATEDIFF(day, te.ngaygoidon, @dnow) < 8 then 'wait'
				else 'process' end
			when te.NXD2_TrangThai = N'Approved' then N'finish'
			when te.NXD2_TrangThai = N'Rejected' then N'error' end) as [NXD2_Display]
		, concat(nv2.HoTenDemVN,' ', nv2.TenVN) as [NXD2_Ten]

		, te.HRXetDuyetId, te.HR_TrangThai, te.HR_GhiChu, te.HR_GioVao, te.HR_GioRa
		, (case
			when te.NguoiXetDuyetCap2Id IS NOT NULL and te.NXD2_TrangThai IS not NULL then N'process'
			when te.NguoiXetDuyetCap2Id IS NULL and te.NguoiXetDuyetCap1Id IS NULL then N'process'
			when te.NguoiXetDuyetCap2Id IS NULL and te.NguoiXetDuyetCap1Id IS NOT NULL and te.NXD1_TrangThai IS NOT NULL then N'process'
			when te.HR_TrangThai = N'Approved' then N'finish'
			when te.HR_TrangThai = N'Rejected' then N'error'
			else N'wait' end) as [HR_Display]
		, (case
			when te.HR_TrangThai is null and te.NXD2_TrangThai is null and te.NXD1_TrangThai is null then cast(0 as bit)
			else cast(1 as bit)
		end) as [isDisabled]

	into #temp
	from hrm.Timesheets te
	left join Hrm.NhanViens nv1 on nv1.Id = te.NguoiXetDuyetCap1Id
	left join Hrm.NhanViens nv2 on nv2.Id = te.NguoiXetDuyetCap2Id
	where te.Id = @id

	select *
		, (case
			when NXD1_Display = N'process' then 1
			when NXD2_Display = N'process' then 2
			when HR_Display = N'process' then 3
			else 1
			end) as [CurrentStep]
	from #temp
END

GO



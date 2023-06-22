USE [DevEsuhaiDb]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetNghiPhepDetail]    Script Date: 2/1/2023 2:00:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [Hrm].[SP_GetNghiPhepDetail]
	@id uniqueidentifier,
	@nhanVienId uniqueidentifier
as
begin
	
	SET NOCOUNT ON;

	declare @dnow datetime 
	set @dnow = GETDATE()

	select concat(nv.HoTenDemVN,' ', nv.TenVN) as [HoTen]
		, np.ThoiGianBatDau, np.ThoiGianKetThuc, np.SoNgayDangKy, np.MoTa
		, np.TrangThaiDangKy, np.TrangThaiNghi, np.CongViecThayThe
		, concat(nvtt.HoTenDemVN,' ', nvtt.TenVN) as [NhanVienThayTheTen]

		, concat(nv1.HoTenDemVN,' ', nv1.TenVN) as [NXD1_Ten]
		, np.NXD1_TrangThai, np.NXD1_GhiChu, DATEADD(day, 7, np.Created) as [NXD1_HanDuyet]
		, (case
			when np.NguoiXetDuyetCap1Id is null then N'no'
			when np.NguoiXetDuyetCap1Id IS NOT NULL and np.NXD1_TrangThai IS NULL AND DATEDIFF(day, np.Created, @dnow) > 7 then N'error'
			when np.NguoiXetDuyetCap1Id IS NOT NULL and np.NXD1_TrangThai IS NULL and DATEDIFF(day, np.Created, @dnow) < 8 and np.NXD2_TrangThai IS NULL and np.HR_TrangThai IS NULL then N'process'
			when np.NXD1_TrangThai = N'approved' then N'finish'
			when np.NXD1_TrangThai = N'rejected' then N'error' end) as [NXD1_Display]
		
		, concat(nv2.HoTenDemVN,' ', nv2.TenVN) as [NXD2_Ten]
		, np.NXD2_TrangThai, np.NXD2_GhiChu, DATEADD(day, 11, np.Created) as [NXD2_HanDuyet]
		, (case
			when np.NguoiXetDuyetCap2Id is null then N'no'
			when np.NguoiXetDuyetCap2Id IS NOT NULL and np.NXD2_TrangThai IS NULL AND DATEDIFF(day, np.Created, @dnow) > 11 then N'error'
			when np.NguoiXetDuyetCap2Id IS NOT NULL and np.NXD2_TrangThai IS NULL and DATEDIFF(day, np.Created, @dnow) < 12 and np.HR_TrangThai IS NULL then
				case
					when np.NguoiXetDuyetCap1Id is not null and np.NXD1_TrangThai is null and DATEDIFF(day, np.Created, @dnow) < 8 then 'wait'
				else 'process' end
			when np.NXD2_TrangThai = N'approved' then N'finish'
			when np.NXD2_TrangThai = N'rejected' then N'error' end) as [NXD2_Display]
			
		, concat(nv3.HoTenDemVN,' ', nv3.TenVN) as [HR_Ten]
		, np.HR_TrangThai, np.HR_GhiChu
		, (case
			when np.HR_TrangThai = N'approved' then N'finish'
			when np.HR_TrangThai = N'rejected' then N'error'
			when np.NguoiXetDuyetCap2Id IS NOT NULL and np.NXD2_TrangThai IS not NULL then N'process'
			when np.NguoiXetDuyetCap2Id IS NULL and np.NguoiXetDuyetCap1Id IS NULL then N'process'
			when np.NguoiXetDuyetCap2Id IS NULL and np.NguoiXetDuyetCap1Id IS NOT NULL and np.NXD1_TrangThai IS NOT NULL then N'process'
			else N'wait' end) as [HR_Display]

		, (case
			when np.HR_TrangThai is null and np.NXD2_TrangThai is null and np.NXD1_TrangThai is null then cast(0 as bit)
			else cast(1 as bit)
		end) as [isDisabled]
		
		, (case when @nhanVienId = np.NhanVienId then cast(1 as bit) else cast(0 as bit) end) as [isOwner]
		, (case when @nhanVienId = np.NguoiXetDuyetCap1Id then cast(1 as bit) else cast(0 as bit) end) as [isNXD1]
		, (case when @nhanVienId = np.NguoiXetDuyetCap2Id then cast(1 as bit) else cast(0 as bit) end) as [isNXD2]
		, (case when @nhanVienId = np.HRXetDuyetId then cast(1 as bit) else cast(0 as bit) end) as [isHR]

	into #temp
	from hrm.NghiPheps np
	left join Hrm.NhanViens nv on nv.Id = np.NhanVienId
	left join Hrm.NhanViens nvtt on nvtt.Id = np.NhanVienThayTheId
	left join Hrm.NhanViens nv1 on nv1.Id = np.NguoiXetDuyetCap1Id
	left join Hrm.NhanViens nv2 on nv2.Id = np.NguoiXetDuyetCap2Id
	left join Hrm.NhanViens nv3 on nv3.Id = np.HRXetDuyetId
	where np.Id = @id

	select *
		, (case
			when NXD1_Display = N'process' then 1
			when NXD2_Display = N'process' then 2
			when HR_Display = N'process' then 3
			else 1
			end) as [CurrentStep]
	from #temp
end
GO



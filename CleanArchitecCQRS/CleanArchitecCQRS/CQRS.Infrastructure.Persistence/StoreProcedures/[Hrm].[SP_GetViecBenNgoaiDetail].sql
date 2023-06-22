USE [DevEsuhaiDb]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetViecBenNgoaiDetail]    Script Date: 2/7/2023 2:46:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [Hrm].[SP_GetViecBenNgoaiDetail]
	@id uniqueidentifier,
	@nhanVienId uniqueidentifier
as
begin
	
	SET NOCOUNT ON;

	declare @dnow datetime 
	set @dnow = GETDATE()

	select vbn.Id
		, concat(nv.HoTenDemVN,' ', nv.TenVN) as [HoTen]
		, vbn.ThoiGianBatDau, vbn.ThoiGianKetThuc
		, vbn.LoaiCongTac, vbn.MoTa, vbn.SoGio, vbn.TrangThaiXetDuyet, vbn.DiaDiem, vbn.NguoiGap, vbn.DiemDenId
		, dd.Ten as [DiemDenTen], dd.DiaChi as [DiemDenDiaChi], dd.DonVi as [DiemDenDonVi]
		, dd.SdtLienLac as [DiemDenSdtLienLac], dd.NguoiGap as [DiemDenNguoiGap]
		, concat(nvtt.HoTenDemVN,' ', nvtt.TenVN) as [NhanVienThayTheTen]
		
		, concat(nv1.HoTenDemVN,' ', nv1.TenVN) as [NXD1_Ten]
		, vbn.NXD1_TrangThai, vbn.NXD1_GhiChu, DATEADD(day, 7, vbn.Created) as [NXD1_HanDuyet]
		, (case
			when vbn.NguoiXetDuyetCap1Id is null then N'no'
			when vbn.NguoiXetDuyetCap1Id IS NOT NULL and vbn.NXD1_TrangThai IS NULL AND DATEDIFF(day, vbn.Created, @dnow) > 7 then N'error'
			when vbn.NguoiXetDuyetCap1Id IS NOT NULL and vbn.NXD1_TrangThai IS NULL and DATEDIFF(day, vbn.Created, @dnow) < 8 and vbn.NXD2_TrangThai IS NULL and vbn.HR_TrangThai IS NULL then N'process'
			when vbn.NXD1_TrangThai = N'approved' then N'finish'
			when vbn.NXD1_TrangThai = N'rejected' then N'error' end) as [NXD1_Display]
		
		, concat(nv2.HoTenDemVN,' ', nv2.TenVN) as [NXD2_Ten]
		, vbn.NXD2_TrangThai, vbn.NXD2_GhiChu, DATEADD(day, 11, vbn.Created) as [NXD2_HanDuyet]
		, (case
			when vbn.NguoiXetDuyetCap2Id is null then N'no'
			when vbn.NguoiXetDuyetCap2Id IS NOT NULL and vbn.NXD2_TrangThai IS NULL AND DATEDIFF(day, vbn.Created, @dnow) > 11 then N'error'
			when vbn.NguoiXetDuyetCap2Id IS NOT NULL and vbn.NXD2_TrangThai IS NULL and DATEDIFF(day, vbn.Created, @dnow) < 12 and vbn.HR_TrangThai IS NULL then
				case
					when vbn.NguoiXetDuyetCap1Id is not null and vbn.NXD1_TrangThai is null and DATEDIFF(day, vbn.Created, @dnow) < 8 then 'wait'
				else 'process' end
			when vbn.NXD2_TrangThai = N'approved' then N'finish'
			when vbn.NXD2_TrangThai = N'rejected' then N'error' end) as [NXD2_Display]
			
		, concat(nv3.HoTenDemVN,' ', nv3.TenVN) as [HR_Ten]
		, vbn.HR_TrangThai, vbn.HR_GhiChu
		, (case
			when vbn.HR_TrangThai = N'approved' then N'finish'
			when vbn.HR_TrangThai = N'rejected' then N'error'
			when vbn.NguoiXetDuyetCap2Id IS NOT NULL and vbn.NXD2_TrangThai IS not NULL then N'process'
			when vbn.NguoiXetDuyetCap2Id IS NULL and vbn.NguoiXetDuyetCap1Id IS NULL then N'process'
			when vbn.NguoiXetDuyetCap2Id IS NULL and vbn.NguoiXetDuyetCap1Id IS NOT NULL and vbn.NXD1_TrangThai IS NOT NULL then N'process'
			else N'wait' end) as [HR_Display]

		, (case
			when vbn.HR_TrangThai is null and vbn.NXD2_TrangThai is null and vbn.NXD1_TrangThai is null then cast(0 as bit)
			else cast(1 as bit)
		end) as [isDisabled]
		
		, (case when @nhanVienId = vbn.NhanVienId then cast(1 as bit) else cast(0 as bit) end) as [isOwner]
		, (case when @nhanVienId = vbn.NguoiXetDuyetCap1Id then cast(1 as bit) else cast(0 as bit) end) as [isNXD1]
		, (case when @nhanVienId = vbn.NguoiXetDuyetCap2Id then cast(1 as bit) else cast(0 as bit) end) as [isNXD2]
		, (case when @nhanVienId = vbn.HRXetDuyetId then cast(1 as bit) else cast(0 as bit) end) as [isHR]

	into #temp
	from Hrm.ViecBenNgoais vbn
	left join Vbn.DiemDens dd on dd.Id = vbn.DiemDenId
	left join Hrm.NhanViens nv on nv.Id = vbn.NhanVienId
	left join Hrm.NhanViens nvtt on nvtt.Id = vbn.[NhanVienThayTheId]
	left join Hrm.NhanViens nv1 on nv1.Id = vbn.NguoiXetDuyetCap1Id
	left join Hrm.NhanViens nv2 on nv2.Id = vbn.NguoiXetDuyetCap2Id
	left join Hrm.NhanViens nv3 on nv3.Id = vbn.HRXetDuyetId
	where vbn.Id = @id

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



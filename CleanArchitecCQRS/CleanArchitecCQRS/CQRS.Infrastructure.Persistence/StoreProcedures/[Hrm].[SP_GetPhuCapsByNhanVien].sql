USE [Esuhai.HRMDev]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetPhuCapsByNhanVien]    Script Date: 11/23/2022 1:27:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*==============================================================================================
   Author			Date			Updated					Description
   -----------------------------------------------------------------------------------------
   Vinh Huynh		2022.11.23								Get thông tin ds phụ cấp theo tháng của nhân viên
============================================================================================= */

CREATE procedure [Hrm].[SP_GetPhuCapsByNhanVien]
	@thang datetime,
	@nhanVienId uniqueidentifier
as
begin
	declare @dnow date
	set @dnow = getdate()

	-- giới hạn tối đa số ngày XD của NXD1
	declare @limitXD1 int
	set @limitXD1 = 7
	
	-- giới hạn tối đa số ngày XD của NXD2
	declare @limitXD2 int
	set @limitXD2 = 11

	select pc.Id, pc.Created, pc.NhanVienId, lpc.Ten as [PhanLoai], lpc.TenJP as [PhanLoaiJP], pc.MoTa, pc.TrangThai
		, pc.SoLanPhuCap, pc.SoBuoiSang, pc.SoBuoiTrua, pc.SoBuoiChieu, pc.SoQuaDem
		, pc.ThoiGianBatDau, pc.ThoiGianKetThuc, pc.XD_ThoiGianBatDau, pc.XD_ThoiGianKetThuc
		, pc.XD_SoLanPhuCap, pc.XD_SoBuoiSang, pc.XD_SoBuoiTrua, pc.XD_SoBuoiChieu, pc.XD_SoQuaDem
		, pc.SoLanNgayThuong, pc.SoLanNgayLe, pc.SoLanCuoiTuan
		, pc.LoaiPhuCapId
		, pc.NguoiXetDuyetCap1Id, pc.NXD1_GhiChu, DATEADD(day, @limitXD1, pc.Created) as [NXD1_HanDuyet]
		, (case
					when datediff(day,pc.Created, @dnow) > @limitXD1		
							AND pc.NXD1_TrangThai IS NULL
							AND NguoiXetDuyetCap1Id IS NOT NULL
						then 'expried'
					when datediff(day,pc.Created, @dnow) <= @limitXD1				
							AND NXD1_TrangThai IS NULL
							AND NXD2_TrangThai IS NULL
							AND NguoiXetDuyetCap1Id IS NOT NULL
							AND HR_TrangThai IS NULL
						then 'waiting'
				else lower(NXD1_TrangThai) end) as [NXD1_TrangThai]

		, pc.NguoiXetDuyetCap2Id, pc.NXD2_GhiChu, DATEADD(day, @limitXD2, pc.Created) as [NXD2_HanDuyet]
		, (case
				when datediff(day,pc.Created, @dnow) > @limitXD2
						AND NXD2_TrangThai IS NULL
						AND NguoiXetDuyetCap2Id IS NOT NULL
					then 'expried'
				when datediff(day,pc.Created, @dnow) <= @limitXD2	
						AND NguoiXetDuyetCap2Id IS NOT NULL
						AND NXD2_TrangThai IS NULL
						AND HR_TrangThai IS NULL
					then
						case 
							when NguoiXetDuyetCap1Id IS NOT NULL
									AND NXD1_TrangThai IS NULL
									AND datediff(day,pc.Created, @dnow) <= @limitXD1
								then 'waitingc1'
							else 'waiting'
						end
				ELSE LOWER(NXD2_TrangThai) END) AS [NXD2_TrangThai]

		, pc.HRXetDuyetId, pc.HR_GhiChu
		, (
				case
					-- ns chưa duyệt - ko có c1 - có c2
					when pc.HR_TrangThai is null 
							and pc.NguoiXetDuyetCap1Id is null 
							and pc.NguoiXetDuyetCap2Id is not null
						then
							case
								when pc.NXD2_TrangThai is null and datediff(day,pc.Created, @dnow) > @limitXD2 then 'waiting'	-- c2 chưa duyệt - c2 hết hạn
								when pc.NXD2_TrangThai is null and datediff(day,pc.Created, @dnow) <= @limitXD2 then 'waitingc2' -- c2 chưa duyệt - c2 còn hạn
								when pc.NXD2_TrangThai is not null then 'waiting' -- c2 đã duyệt
							end
			
					-- ns chưa duyệt - có c1 - ko có c2 -  
					when pc.HR_TrangThai is null
							and pc.NguoiXetDuyetCap1Id is not null
							and pc.NguoiXetDuyetCap2Id is null
						then
							case
								when pc.NXD1_TrangThai is null and datediff(day,pc.Created, @dnow) <= @limitXD1 then 'waitingc1' -- c1 chưa duyệt - c1 còn hạn
								when pc.NXD1_TrangThai is null and datediff(day,pc.Created, @dnow) > @limitXD1 then 'waiting' -- c1 chưa duyệt - c1 hết hạn
								when pc.NXD1_TrangThai is not null then 'waiting' -- c1 đã duyệt
							end

					-- ns chưa duyệt - có c1, c2
					when pc.HR_TrangThai is null
							and pc.NguoiXetDuyetCap1Id is not null
							and pc.NguoiXetDuyetCap2Id is not null
						then
							case
								when pc.NXD1_TrangThai is null and pc.NXD2_TrangThai is null and datediff(day,pc.Created, @dnow) <= @limitXD1 then 'waitingc1' -- c1,c2 chưa duyệt + c1 còn hạn
						
								-- c1 đã duyệt hoặc (chưa duyệt + hết hạn) - (c2 chưa duyệt & còn hạn)
								when (pc.NXD1_TrangThai is not null or (pc.NXD1_TrangThai is null and datediff(day,pc.Created, @dnow) > @limitXD1))
										and pc.NXD2_TrangThai is null
										and datediff(day,pc.Created, @dnow) <= @limitXD2 then 'waitingc2'

								-- c1 đã duyệt hoặc (chưa duyệt + hết hạn) - c2 đã duyệt hoặc (c2 chưa duyệt & hết hạn)
								when (pc.NXD1_TrangThai is not null or (pc.NXD1_TrangThai is null and datediff(day,pc.Created, @dnow) > @limitXD1))
										and (pc.NXD2_TrangThai is not null or (pc.NXD2_TrangThai is null and datediff(day,pc.Created, @dnow) > @limitXD2)) then 'waiting'
							end

					-- ns chưa duyệt - ko có c1,c2
					when pc.HR_TrangThai is null
							and pc.NguoiXetDuyetCap1Id is null
							and pc.NguoiXetDuyetCap2Id is null then 'waiting'
					else HR_TrangThai
			end) as [HR_TrangThai]

		, (		
				case
					when pc.HR_TrangThai is null and pc.NXD2_TrangThai is null and pc.NXD1_TrangThai is null then cast(0 as bit)
					else cast(1 as bit)
				end) as [isDisabled]

	from Hrm.PhuCaps pc
	left join Hrm.LoaiPhuCaps lpc on lpc.Id = pc.LoaiPhuCapId
	where 
			pc.NhanVienId = @nhanVienId
		and
			year(@thang) between year(pc.ThoiGianBatDau) and year(pc.ThoiGianKetThuc)
		and
			month(@thang) between month(pc.ThoiGianBatDau) and month(pc.ThoiGianKetThuc)
	order by Created desc
end
GO



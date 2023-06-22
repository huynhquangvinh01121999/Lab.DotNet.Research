USE [Esuhai.HRMDev]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetTangCasByNhanVien]    Script Date: 11/21/2022 4:28:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*==============================================================================================
   Author			Date			Updated					Description
   -----------------------------------------------------------------------------------------
   Vinh Huynh		2022.11.21		2022.11.22				Get thông tin ds tăng ca theo ngày của nhân viên
============================================================================================= */

CREATE procedure [Hrm].[SP_GetTangCasByNhanVien]
	@ngayLamViec datetime,
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

	select tc.Id, tc.Created, tc.NhanVienId, tc.NgayTangCa
		, tc.ThoiGianBatDau, tc.ThoiGianKetThuc, tc.SoGioDangKy, tc.SoGioNgayLe, tc.SoGioCuoiTuan, tc.SoGioNgayThuong, tc.SoGioDuocDuyet, tc.MoTa, tc.TrangThai

		, tc.NguoiXetDuyetCap1Id, tc.NXD1_GhiChu, DATEADD(day, @limitXD1, tc.Created) as [NXD1_HanDuyet]
		, (case
				when datediff(day,tc.Created, @dnow) > @limitXD1		
						AND tc.NXD1_TrangThai IS NULL
						AND NguoiXetDuyetCap1Id IS NOT NULL
					then 'expried'
				when datediff(day,tc.Created, @dnow) <= @limitXD1				
						AND NXD1_TrangThai IS NULL
						AND NXD2_TrangThai IS NULL
						AND NguoiXetDuyetCap1Id IS NOT NULL
						AND HR_TrangThai IS NULL
					then 'waiting'
			else lower(NXD1_TrangThai) end) as [NXD1_TrangThai]

		, tc.NguoiXetDuyetCap2Id, tc.NXD2_GhiChu, DATEADD(day, @limitXD2, tc.Created) as [NXD2_HanDuyet]
		, (case
			when datediff(day,tc.Created, @dnow) > @limitXD2
					AND NXD2_TrangThai IS NULL
					AND NguoiXetDuyetCap2Id IS NOT NULL
				then 'expried'
			when datediff(day,tc.Created, @dnow) <= @limitXD2	
					AND NguoiXetDuyetCap2Id IS NOT NULL
					AND NXD2_TrangThai IS NULL
					AND HR_TrangThai IS NULL
				then
					case 
						when NguoiXetDuyetCap1Id IS NOT NULL
								AND NXD1_TrangThai IS NULL
								AND datediff(day,tc.Created, @dnow) <= @limitXD1
							then 'waitingc1'
						else 'waiting'
					end
			ELSE LOWER(NXD2_TrangThai) END) AS [NXD2_TrangThai]

		, tc.HRXetDuyetId, tc.HR_GhiChu
		, (
			case
				-- ns chưa duyệt - ko có c1 - có c2
				when tc.HR_TrangThai is null 
						and tc.NguoiXetDuyetCap1Id is null 
						and tc.NguoiXetDuyetCap2Id is not null
					then
						case
							when tc.NXD2_TrangThai is null and datediff(day,tc.Created, @dnow) > @limitXD2 then 'waiting'	-- c2 chưa duyệt - c2 hết hạn
							when tc.NXD2_TrangThai is null and datediff(day,tc.Created, @dnow) <= @limitXD2 then 'waitingc2' -- c2 chưa duyệt - c2 còn hạn
							when tc.NXD2_TrangThai is not null then 'waiting' -- c2 đã duyệt
						end
			
				-- ns chưa duyệt - có c1 - ko có c2 -  
				when tc.HR_TrangThai is null
						and tc.NguoiXetDuyetCap1Id is not null
						and tc.NguoiXetDuyetCap2Id is null
					then
						case
							when tc.NXD1_TrangThai is null and datediff(day,tc.Created, @dnow) <= @limitXD1 then 'waitingc1' -- c1 chưa duyệt - c1 còn hạn
							when tc.NXD1_TrangThai is null and datediff(day,tc.Created, @dnow) > @limitXD1 then 'waiting' -- c1 chưa duyệt - c1 hết hạn
							when tc.NXD1_TrangThai is not null then 'waiting' -- c1 đã duyệt
						end

				-- ns chưa duyệt - có c1, c2
				when tc.HR_TrangThai is null
						and tc.NguoiXetDuyetCap1Id is not null
						and tc.NguoiXetDuyetCap2Id is not null
					then
						case
							when tc.NXD1_TrangThai is null and tc.NXD2_TrangThai is null and datediff(day,tc.Created, @dnow) <= @limitXD1 then 'waitingc1' -- c1,c2 chưa duyệt + c1 còn hạn
						
							-- c1 đã duyệt hoặc (chưa duyệt + hết hạn) - (c2 chưa duyệt & còn hạn)
							when (tc.NXD1_TrangThai is not null or (tc.NXD1_TrangThai is null and datediff(day,tc.Created, @dnow) > @limitXD1))
									and tc.NXD2_TrangThai is null
									and datediff(day,tc.Created, @dnow) <= @limitXD2 then 'waitingc2'

							-- c1 đã duyệt hoặc (chưa duyệt + hết hạn) - c2 đã duyệt hoặc (c2 chưa duyệt & hết hạn)
							when (tc.NXD1_TrangThai is not null or (tc.NXD1_TrangThai is null and datediff(day,tc.Created, @dnow) > @limitXD1))
									and (tc.NXD2_TrangThai is not null or (tc.NXD2_TrangThai is null and datediff(day,tc.Created, @dnow) > @limitXD2)) then 'waiting'
						end

				-- ns chưa duyệt - ko có c1,c2
				when tc.HR_TrangThai is null
						and tc.NguoiXetDuyetCap1Id is null
						and tc.NguoiXetDuyetCap2Id is null then 'waiting'
				else HR_TrangThai
		end) as [HR_TrangThai]

		, (case
				when tc.HR_TrangThai is null and tc.NXD2_TrangThai is null and tc.NXD1_TrangThai is null then cast(0 as bit)
				else cast(1 as bit)
		end) as [isDisabled]

	from Hrm.TangCas tc
	where
		convert(date, tc.NgayTangCa) = convert(date, @ngayLamViec)
	and
		tc.NhanVienId = @nhanVienId
	order by Created desc
end
GO



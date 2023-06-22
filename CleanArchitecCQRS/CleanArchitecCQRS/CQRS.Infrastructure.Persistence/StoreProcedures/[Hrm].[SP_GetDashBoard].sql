USE [Esuhai.HRM]
GO
/****** Object:  StoredProcedure [Hrm].[SP_GetDashBoard]    Script Date: 1/6/2021 4:39:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	/***********
	NAME: SP_GetDashBoard
	FUNCTION: Get count info for Dashboard screen
	DATE CREATE: 2020/01/06
	USER CREATE: ICT HUY
	DATE UPDATE:
	USER UPDATE:

	************/
	CREATE PROCEDURE [Hrm].[SP_GetDashBoard]
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN	

		SET NOCOUNT ON;
		SELECT NhanVien.Tong        AS TongNhanVien          -- Tong Nhan Vien Chinh Thuc Toi Nam Hien Tai
			 , ThaiSan.Tong         AS TongThaiSan			 -- Tong Nhan Vien Dang Nghi Thai San Trong Nam
   			 , ThoiViec.Tong        AS TongThoiViec			 -- Tong Nhan Vien Da Thoi Viec Trong Nam
			 , ThuViec.Tong         AS TongThuViec			 -- Tong Nhan Vien Dang Thu Viec 
			 , DonThoiViec.Tong     AS TongDonThoiViec		 -- Tong Nhan Vien Dang Nop Don Thoi Viec
			 , NghiDaiHan.Tong      AS TongNghiDaiHan        -- Tong Nhan Vien Nghi Lam Tren 1 thang
			 , TapSu.Tong           AS TongTapSu             -- Tong Nhan Vien Dang Tap Su
			 , CongTac.Tong         AS TongCongTac           -- Tong Nhan Vien Partner
		FROM
			-- Get tong nhan vien chinh thuc dang lam viec
			(
				SELECT COUNT(*) as Tong
				FROM   [Hrm].[NhanViens] nv 
				WHERE  YEAR(nv.NgayBatDauLamViec) <= YEAR(GETDATE()) -- Bat dau lam viec tu nam hien tai tro ve truoc
				AND    nv.TrangThaiId             =  24              -- TrangThai: Chinh thuc
				AND    ISNULL(nv.Deleted,0)       <> 1
			) NhanVien,

			-- Get tong nhan vien tap su dang lam viec
			(
				SELECT COUNT(*) as Tong
				FROM   [Hrm].[NhanViens] nv 
				WHERE  YEAR(nv.NgayBatDauLamViec) <= YEAR(GETDATE()) -- Bat dau lam viec tu nam hien tai tro ve truoc
				AND    nv.TrangThaiId             =  0               -- TrangThai: Tap Su(đang thiếu)
				AND    ISNULL(nv.Deleted,0)       <> 1
			) TapSu,

			-- Get tong nhan vien cong(+) tac dang lam viec
			(
				SELECT COUNT(*) as Tong
				FROM   [Hrm].[NhanViens] nv 
				WHERE  YEAR(nv.NgayBatDauLamViec) <= YEAR(GETDATE()) -- Bat dau lam viec tu nam hien tai tro ve truoc
				AND    nv.TrangThaiId             =  0               -- TrangThai: CongTac(đang thiếu)
				AND    ISNULL(nv.Deleted,0)       <> 1
			) CongTac,

			-- Get tong nhan vien dang nghi thai san
			(
				SELECT COUNT(*) as Tong
				FROM   [Hrm].[NhanViens]         nv 
					  ,[Hrm].[NhanVien_ThaiSans] ts
				WHERE  nv.Id = ts.NhanVienId 
				AND    YEAR(nv.NgayBatDauLamViec)  <=  YEAR(GETDATE())  -- Bat dau lam viec tu nam hien tai tro ve truoc
				-- Đang Thiếu Thai sản Id
				AND    GETDATE() BETWEEN ts.TuNgay AND ts.DenNgay       -- Thoi gian bat dau nghi < hien tai < ket thuc nghi
				AND    ISNULL(nv.Deleted,0)        <>  1
				AND    ISNULL(ts.Deleted,0)        <>  1
			) ThaiSan,

			-- Get tong nhan vien da thoi viec trong nam
			(
				SELECT COUNT(*) as Tong
				FROM   [Hrm].[NhanViens]          nv 
					  ,[Hrm].[NhanVien_ThoiViecs] tv
				WHERE  nv.Id = tv.NhanVienId 
				AND    YEAR(nv.NgayBatDauLamViec) <= YEAR(GETDATE())    -- Bat dau lam viec tu nam hien tai tro ve truoc
				-- Đang Thiếu ThoiViec Id
				AND    YEAR(tv.NgayThoiViec)      =  YEAR(GETDATE())    -- Thoi gian thoi viec trong nam
				AND    tv.NgayThoiViec            <  GETDATE()          -- Ngay thoi viec da qua ngay hien tai
				AND    ISNULL(nv.Deleted,0)       <> 1
				AND    ISNULL(tv.Deleted,0)       <> 1
			) ThoiViec,

			-- Get tong nhan vien chinh thuc dang lam viec
			(
				SELECT COUNT(*) as Tong
				FROM   [Hrm].[NhanViens] nv 
				WHERE  YEAR(nv.NgayBatDauLamViec) <= YEAR(GETDATE())    -- Bat dau lam viec tu nam hien tai tro ve truoc
				AND    nv.TrangThaiId             =  23                 -- TrangThai: Thu viec
				AND    ISNULL(nv.Deleted,0)       <> 1
			) ThuViec,

			-- Get tong nhan vien dang nop don thoi viec
			(
				SELECT COUNT(*) as Tong
				FROM   [Hrm].[NhanViens]          nv 
					  ,[Hrm].[NhanVien_ThoiViecs] tv
				WHERE  nv.Id = tv.NhanVienId 
				AND    YEAR(nv.NgayBatDauLamViec) <= YEAR(GETDATE())     -- Bat dau lam viec tu nam hien tai tro ve truoc
				-- Đang Thiếu ThoiViec Id
				AND    YEAR(tv.NgayThoiViec)      =  YEAR(GETDATE())     -- Thoi gian thoi viec trong nam
				AND    tv.NgayThoiViec            >  GETDATE()           -- Ngay thoi viec chua toi ngay hien tai
				AND    ISNULL(nv.Deleted,0)       <> 1
				AND    ISNULL(tv.Deleted,0)       <> 1
			) DonThoiViec,

			-- Get tong nhan vien dang nghi dai hai khong nhan luong
			(
				SELECT COUNT(*) as Tong
				FROM   [Hrm].[NhanViens]          nv 
					  ,[Hrm].[NhanVien_VangMats]  vm
				WHERE  nv.Id = vm.NhanVienId 
				AND    YEAR(nv.NgayBatDauLamViec) <= YEAR(GETDATE())     -- Bat dau lam viec tu nam hien tai tro ve truoc
				AND    DATEDIFF(MONTH,vm.TuNgay,vm.DenNgay) > 0          -- So ngay nghi dai han nhieu hon 1 thang
				AND   (YEAR(vm.TuNgay)            =  YEAR(GETDATE())     -- Thoi gian bat dau nghi trong nam
				OR     YEAR(vm.DenNgay)           =  YEAR(GETDATE()))    -- Thoi gian ket thuc nghi trong nam
				AND    ISNULL(nv.Deleted,0)       <> 1
				AND    ISNULL(vm.Deleted,0)       <> 1
			) NghiDaiHan
	END
	
--Exec [Hrm].[SP_GetDashBoard]

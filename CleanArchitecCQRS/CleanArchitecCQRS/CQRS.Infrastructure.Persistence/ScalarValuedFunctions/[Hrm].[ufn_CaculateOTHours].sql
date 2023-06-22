USE [Esuhai.HRM]
GO

/****** Object:  UserDefinedFunction [Hrm].[ufn_CaculateOTHours]    Script Date: 11/2/2022 10:25:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*==============================================================================================
   Author			Date			Updated					Description
   -----------------------------------------------------------------------------------------
   Vinh Huynh		2022.11.02								Tinh so gio OT
============================================================================================= */

CREATE FUNCTION [Hrm].[ufn_CaculateOTHours]
(
	@nhanVienId nvarchar(max)
	, @thoiGianBatDau DATETIME
	, @thoiGianKetThuc DATETIME
)
RETURNS FLOAT
AS
BEGIN

	DECLARE @ResultVar FLOAT = 0,
			@isNgayLe BIT, 
			@isCuoiTuan BIT,
			@GioVao DATETIME, 
			@GioRa DATETIME,
			@caLamViec_BatDau DATETIME, 
			@caLamViec_KetThuc DATETIME,
			@caLamViec_BatDauNghi DATETIME, 
			@caLamViec_KetThucNghi DATETIME,
			@viecBenNgoaiThoiGianBatDau DATETIME, 
			@viecBenNgoaiThoiGianKetThuc DATETIME, 
			@nghiPhepThoiGianBatDau DATETIME, 
			@nghiPhepThoiGianKetThuc DATETIME

	SELECT @isNgayLe = isNgayLe
			, @isCuoiTuan = isCuoiTuan
			, @GioVao = Timesheet_GioVao
			, @GioRa = Timesheet_GioRa
			, @caLamViec_BatDau = CaLamViec_BatDau
			, @caLamViec_KetThuc = CaLamViec_KetThuc
			, @caLamViec_BatDauNghi = CaLamViec_BatDauNghi
			, @caLamViec_KetThucNghi = CaLamViec_KetThucNghi
			, @viecBenNgoaiThoiGianBatDau = ViecBenNgoaiThoiGianBatDau
			, @viecBenNgoaiThoiGianKetThuc = ViecBenNgoaiThoiGianKetThuc
			, @nghiPhepThoiGianBatDau = NghiPhepThoiGianBatDau
			, @nghiPhepThoiGianKetThuc = NghiPhepThoiGianKetThuc
	FROM Hrm.TongHopDuLieus
	WHERE NhanVienId = @nhanVienId AND NgayLamViec = CAST(@thoiGianBatDau AS DATE)

	--Truong hop OT vao t7, phai set lai end time cua shift vi t7 chi lam nua ngay
	IF(DATEPART(DW, @thoiGianBatDau) = 7 AND @isNgayLe = 0 AND @isCuoiTuan = 0)
		SET @caLamViec_KetThuc = @caLamViec_BatDauNghi

	--Neu co du lieu OT, bat buoc phai co dulieu Timesheet hoac Business Trip de so sanh
	IF((@GioVao IS NULL OR @GioRa IS NULL) AND 
		(@viecBenNgoaiThoiGianBatDau IS NULL 
			OR @viecBenNgoaiThoiGianKetThuc IS NULL))
	BEGIN
		SET @ResultVar = 0
		GOTO RESULT
	END


	--1. Tong hop thoi gian lam viec tu 2 nguon la timesheet va business trip
	--Truong hop di cong tac va co OT
	IF @viecBenNgoaiThoiGianBatDau IS NOT NULL AND @GioVao IS NULL
	BEGIN
		SET @GioVao = @viecBenNgoaiThoiGianBatDau
		SET @GioRa = @viecBenNgoaiThoiGianKetThuc
	END
	--Truong hop vua work tai van phong, vua di cong tac va co OT
	ELSE IF @viecBenNgoaiThoiGianBatDau IS NOT NULL 
				AND @GioVao IS NOT NULL
	BEGIN
		IF(@GioVao > @viecBenNgoaiThoiGianBatDau)
			SET @GioVao = @viecBenNgoaiThoiGianBatDau

		IF(@GioRa < @viecBenNgoaiThoiGianKetThuc)
			SET @GioRa = @viecBenNgoaiThoiGianKetThuc
	END
	
	
	--2. So sanh thoi gian OT voi thoi gian duoc tong hop

	--Neu OT vao holiday hoac la weekend, thi luc nay khong can care working shift
	IF(@isNgayLe = 1 OR @isCuoiTuan = 1)
	BEGIN
		--Truong hop thoi gian OT som hon thoi gian Time in
		IF @thoiGianBatDau < @GioVao
			SET @thoiGianBatDau = @GioVao
			
		--Truong hop thoi gian OT tre hon thoi gian Time out
		IF @thoiGianKetThuc > @GioRa
			SET @thoiGianKetThuc = @GioRa 
	END

	--Truong hop khung thoi gian OT nam trong working shift hoac bao trum working shift
	ELSE IF (@thoiGianBatDau BETWEEN @caLamViec_BatDau AND @caLamViec_KetThuc) 
			AND (@thoiGianKetThuc BETWEEN @caLamViec_BatDau AND @caLamViec_KetThuc) 
			OR
			((@caLamViec_BatDau BETWEEN @thoiGianBatDau AND @thoiGianKetThuc) 
				AND (@caLamViec_KetThuc BETWEEN @thoiGianBatDau AND @thoiGianKetThuc))
	BEGIN
		SET @ResultVar = 0	
		GOTO RESULT
	END
	ELSE 
	BEGIN
		--Truong hop thoi gian OT som hon thoi gian Time in
		IF @thoiGianBatDau < @GioVao
			SET @thoiGianBatDau = @GioVao
			
		--Truong hop thoi gian OT tre hon thoi gian Time out
		IF @thoiGianKetThuc > @GioRa
			SET @thoiGianKetThuc = @GioRa 


		--Truong hop start time cua OT nam trong gio lam viec, tuong ung truong hop co the OT sau gio lam viec
		IF @thoiGianBatDau BETWEEN @caLamViec_BatDau AND @caLamViec_KetThuc
			SET @thoiGianBatDau = @caLamViec_KetThuc

		--Truong hop end time cua OT nam trong gio lam viec, tuong ung truong hop co the OT truoc gio lam viec
		IF @thoiGianKetThuc BETWEEN @caLamViec_BatDau AND @caLamViec_KetThuc
			SET @thoiGianKetThuc = @caLamViec_BatDau
	END	

	SET @ResultVar = ROUND(CAST((DATEDIFF(MINUTE, Hrm.ufn_RoundingDate(@thoiGianBatDau), Hrm.ufn_RoundingDate(@thoiGianKetThuc))) AS FLOAT)/60, 2)
	IF @ResultVar < 0 SET @ResultVar = 0

	--Round down to neareast half
	SET @ResultVar = FLOOR(@ResultVar * 2) / 2 

	RESULT:
	RETURN @ResultVar 
END

GO



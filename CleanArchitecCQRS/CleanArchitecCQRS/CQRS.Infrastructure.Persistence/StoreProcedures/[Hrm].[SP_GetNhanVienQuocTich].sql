USE[Esuhai.HRM]
GO
/****** Object:  StoredProcedure [Hrm].[SP_GetTrinhDoHocVan]    Script Date: 1/6/2021 4:38:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	/*****************
	NAME: SP_GetNhanVienQuocTich
	FUNCTION: Get list quoc tich nhan vien va count so luong
	DATE CREATE: ICT HUY
	USER CREATE: 2020/01/06
	DATE UPDATE:
	USER UPDATE:

	******************/
	CREATE PROCEDURE [Hrm].[SP_GetNhanVienQuocTich]
    WITH EXECUTE AS 'dbo'
	AS
	BEGIN	

		SET NOCOUNT ON;
        SELECT ISNULL(dm.TenVN, N'Khác')        AS TenDanhMuc
	         , count(nv.QuocTichId)             AS SoLuong
		FROM      [Hrm].[NhanViens]             nv
        LEFT JOIN [Esuhai].[DanhMucs]           dm
        ON        nv.QuocTichId              =  dm.Id
		WHERE     YEAR(nv.NgayBatDauLamViec) <= YEAR(GETDATE()) -- Bat dau lam viec tu nam hien tai tro ve truoc
		AND       nv.TrangThaiId             =  24              -- TrangThai: Chinh thuc
		AND       ISNULL(nv.Deleted,0)       <> 1
		AND       ISNULL(dm.Deleted,0)       <> 1
		GROUP BY  dm.TenVN
		        , nv.QuocTichId
	END

			
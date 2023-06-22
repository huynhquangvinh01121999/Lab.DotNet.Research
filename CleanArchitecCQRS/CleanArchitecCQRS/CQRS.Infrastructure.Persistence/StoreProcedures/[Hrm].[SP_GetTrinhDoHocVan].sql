USE [Esuhai.HRM]
GO
/****** Object:  StoredProcedure [Hrm].[SP_GetTrinhDoHocVan]    Script Date: 1/14/2021 8:32:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	/**************
	NAME: SP_GetTrinhDoHocVan
	FUNCTION: Get list Trinh do hoc van cua nhan vien , count so luong trinh do
	DATE CREATE: 2020/01/06
	USER CREATE: ICT HUY
	DATE UPDATE:
	USER UPDATE:

	***************/
	ALTER PROCEDURE [Hrm].[SP_GetTrinhDoHocVan]
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN	

		SET NOCOUNT ON;
		SELECT TrinhDo.TenDanhMuc
		      ,COUNT(NhanVienId) AS SoLuong
		FROM(
			SELECT ISNULL(dm.TenVN,N'Khác') AS TenDanhMuc
				  ,nv.Id                    AS NhanVienId
			FROM  (SELECT nh.Id
						 ,nh.NgayBatDauLamViec 
				   FROM   [Hrm].NhanViens          nh
				   JOIN   [Esuhai].DanhMucs        da
				   ON     nh.TrangThaiId         = da.Id
				   AND    da.TenVN               = N'Chính thức'
				   WHERE  ISNULL(nh.Deleted, 0) != 1 
				  ) nv 
			LEFT JOIN [Hrm].[NhanVien_ThongTinHocVans]  hv
			ON        nv.Id                    = hv.NhanVienId
			AND       hv.IsCaoNhat             = 1
			LEFT JOIN [Esuhai].[DanhMucs]        dm   
			ON        hv.TrinhDoId             = dm.Id
			AND       dm.PhanLoai              = 'TrinhDo'
			WHERE  YEAR(nv.NgayBatDauLamViec) <= YEAR(GETDATE()) -- Bat dau lam viec tu nam hien tai tro ve truoc
			AND    ISNULL(hv.Deleted,0)       <> 1
		) TrinhDo
		GROUP BY TrinhDo.TenDanhMuc
	END

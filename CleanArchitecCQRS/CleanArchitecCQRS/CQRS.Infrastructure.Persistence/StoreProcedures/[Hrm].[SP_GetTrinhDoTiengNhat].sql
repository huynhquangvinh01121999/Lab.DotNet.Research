USE [Esuhai.HRM]
GO
/****** Object:  StoredProcedure [Hrm].[SP_GetTrinhDoTiengNhat]    Script Date: 1/14/2021 8:04:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	/*****************
	NAME: SP_GetTrinhDoTiengNhat
	FUNCTION: Get list trinh do tieng nhat va count so luong
	DATE CREATE: ICT HUY
	USER CREATE: 2021/01/13
	DATE UPDATE:
	USER UPDATE:

	******************/
	ALTER PROCEDURE [Hrm].[SP_GetTrinhDoTiengNhat]
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN	

		SET NOCOUNT ON;
		SELECT ISNULL(BCCC.TenDanhMuc,N'Khác')
		      ,COUNT(Id) AS SoLuong
		FROM(
			SELECT nv.Id
				  ,ISNULL(bc.ChungChi, NULL)                        AS TenDanhMuc
				  ,ROW_NUMBER() OVER (PARTITION BY nv.Id 
									      ORDER BY nv.Id
									              ,bc.ChungChi ASC) AS ROWNUM
			FROM     (SELECT nh.Id
			                ,nh.NgayBatDauLamViec 
					  FROM   [Hrm].NhanViens          nh
					  JOIN   [Esuhai].DanhMucs        da
					  ON     nh.TrangThaiId         = da.Id
					  AND    da.TenVN               = N'Chính thức'
					  WHERE  ISNULL(nh.Deleted, 0) != 1 
					  ) nv 
		    LEFT JOIN [Hrm].[NhanVien_BangCapChungChis] bc
			ON        nv.Id                   =  bc.NhanVienId
			LEFT JOIN [Esuhai].[DanhMucs]        dm
			ON        bc.PhanLoaiId           =  dm.Id
			AND       dm.TenVN                =  N'Chứng chỉ tiếng Nhật'
			WHERE YEAR(nv.NgayBatDauLamViec) <=  YEAR(GETDATE()) -- Bat dau lam viec tu nam hien tai tro ve truoc
			AND    ISNULL(bc.Deleted,0)      <>  1
		) BCCC
		WHERE BCCC.ROWNUM = 1
		GROUP BY BCCC.TenDanhMuc
	END

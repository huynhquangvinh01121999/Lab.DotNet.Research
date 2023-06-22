USE [Esuhai.HRM]
GO
/****** Object:  StoredProcedure [Hrm].[SP_ListNhanVienMonths]    Script Date: 1/14/2021 3:46:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	/*****************
	NAME: SP_ListNhanVienMonths
	FUNCTION: Get list so luong nhan vien theo tung thang trong nam
	DATE CREATE: ICT HUY
	USER CREATE: 2021/01/14
	DATE UPDATE:
	USER UPDATE:

	******************/
	ALTER PROCEDURE [Hrm].[SP_ListNhanVienMonths]
	(
		@YEAR INT
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN	

		SET NOCOUNT ON;
		--Get List nhan vien bat dau lam viec theo thang/nam truyen vao
		SELECT h.Thang     AS Thang
		     , COUNT(h.Id) AS SoLuong
		INTO #tmp_BatDauLamViec
		FROM(
			SELECT nv.Id
			     , MONTH(nv.NgayBatDauLamViec) AS Thang
			FROM   [Hrm].[NhanViens]             nv
			JOIN   [Esuhai].[DanhMucs]           dm
			ON     nv.TrangThaiId              = dm.Id
			AND    dm.TenVN                    = N'Chính thức'
			WHERE  YEAR(nv.NgayBatDauLamViec)  = @YEAR
			AND    ISNULL(nv.Deleted,0)       != 1 
		) h
		GROUP BY h.Thang;

		--Get List nhan vien thoi viec theo thang/nam truyen vao
		SELECT t.Thang     AS Thang
			 , COUNT(t.Id) AS SoLuong
		INTO #tmp_ThoiViec
		FROM(
			SELECT nv.Id
				 , MONTH(tv.NgayThoiViec) AS Thang
			FROM  [Hrm].[NhanViens]           nv
			JOIN  [Hrm].[NhanVien_ThoiViecs]  tv
			ON    nv.Id = tv.NhanVienId
			WHERE YEAR(nv.NgayBatDauLamViec) <= @YEAR 
			AND   YEAR(tv.NgayThoiViec)       = @YEAR
			AND   ISNULL(tv.Deleted,0)       != 1 
			AND   ISNULL(nv.Deleted,0)       != 1 
		) t
		GROUP BY t.Thang;

		--Lay du lieu theo tung thang trong nam truyen vao
		WITH NUMBER AS
		(
			--Tao ra 12 empty row tuong ung voi 12 thang
			SELECT 1 AS MONTHS
			UNION ALL
			SELECT MONTHS+1
			FROM NUMBER
			WHERE MONTHS<12
		)
        SELECT MONTHS AS Thang, 
               CASE WHEN MONTHS = 1 THEN TongNamTruoc.SoLuong
		       					    + ISNULL((SELECT sum(ISNULL(#tmp_BatDauLamViec.SoLuong,0)) AS SoLuong
		       								  FROM   #tmp_BatDauLamViec
		       								  WHERE  #tmp_BatDauLamViec.Thang = 1),0)
		       					    - ISNULL((SELECT sum(ISNULL(#tmp_ThoiViec.SoLuong,0)) AS SoLuong
		       								  FROM   #tmp_ThoiViec
		       								  WHERE  #tmp_ThoiViec.Thang = 1),0) 
		       
                    WHEN MONTHS = 2 THEN TongNamTruoc.SoLuong
		       				        + ISNULL((SELECT sum(ISNULL(#tmp_BatDauLamViec.SoLuong,0)) AS SoLuong
		       						          FROM   #tmp_BatDauLamViec
		       						          WHERE  #tmp_BatDauLamViec.Thang <= 2),0)
		       				        - ISNULL((SELECT sum(ISNULL(#tmp_ThoiViec.SoLuong,0)) AS SoLuong
		       								  FROM   #tmp_ThoiViec
		       								  WHERE  #tmp_ThoiViec.Thang <= 2),0)
		       
		       	 WHEN MONTHS = 3 THEN TongNamTruoc.SoLuong
		       			            + ISNULL((SELECT sum(ISNULL(#tmp_BatDauLamViec.SoLuong,0)) AS SoLuong
		       						          FROM   #tmp_BatDauLamViec 
		       						          WHERE  #tmp_BatDauLamViec.Thang <= 3),0)
                                    - ISNULL((SELECT sum(ISNULL(#tmp_ThoiViec.SoLuong,0)) AS SoLuong
		       								  FROM   #tmp_ThoiViec
		       								  WHERE  #tmp_ThoiViec.Thang <= 3),0)
		       
		       	 WHEN MONTHS = 4 THEN TongNamTruoc.SoLuong
		       			            + ISNULL((SELECT sum(ISNULL(#tmp_BatDauLamViec.SoLuong,0)) AS SoLuong
		       						          FROM   #tmp_BatDauLamViec 
		       						          WHERE  #tmp_BatDauLamViec.Thang <= 4),0)
		       			            - ISNULL((SELECT sum(ISNULL(#tmp_ThoiViec.SoLuong,0)) AS SoLuong
		       								  FROM   #tmp_ThoiViec
		       								  WHERE  #tmp_ThoiViec.Thang <= 4),0)
		       
		       	 WHEN MONTHS = 5 THEN TongNamTruoc.SoLuong
		       	                    + ISNULL((SELECT sum(ISNULL(#tmp_BatDauLamViec.SoLuong,0)) AS SoLuong
		       	 		                      FROM   #tmp_BatDauLamViec 
		       	 			                  WHERE  #tmp_BatDauLamViec.Thang <= 5),0)
		       			            - ISNULL((SELECT sum(ISNULL(#tmp_ThoiViec.SoLuong,0)) AS SoLuong
		       								  FROM   #tmp_ThoiViec
		       								  WHERE  #tmp_ThoiViec.Thang <= 5),0)
		       
		       	 WHEN MONTHS = 6 THEN TongNamTruoc.SoLuong
		       			            + ISNULL((SELECT sum(ISNULL(#tmp_BatDauLamViec.SoLuong,0)) AS SoLuong
		       						          FROM   #tmp_BatDauLamViec 
		       						          WHERE  #tmp_BatDauLamViec.Thang <= 6),0)
		       			            - ISNULL((SELECT sum(ISNULL(#tmp_ThoiViec.SoLuong,0)) AS SoLuong
		       								  FROM   #tmp_ThoiViec
		       								  WHERE  #tmp_ThoiViec.Thang <= 6),0)
		       
		       	 WHEN MONTHS = 7 THEN TongNamTruoc.SoLuong
		       			            + ISNULL((SELECT sum(ISNULL(#tmp_BatDauLamViec.SoLuong,0)) AS SoLuong
		       						          FROM   #tmp_BatDauLamViec 
		       						          WHERE  #tmp_BatDauLamViec.Thang <= 7),0)
		       				        - ISNULL((SELECT sum(ISNULL(#tmp_ThoiViec.SoLuong,0)) AS SoLuong
		       								  FROM   #tmp_ThoiViec
		       								  WHERE  #tmp_ThoiViec.Thang <= 7),0)
		       
		       	 WHEN MONTHS = 8 THEN TongNamTruoc.SoLuong
		       			            + ISNULL((SELECT sum(ISNULL(#tmp_BatDauLamViec.SoLuong,0)) AS SoLuong
		       						          FROM   #tmp_BatDauLamViec 
		       						          WHERE  #tmp_BatDauLamViec.Thang <= 8),0)
                                       - ISNULL((SELECT sum(ISNULL(#tmp_ThoiViec.SoLuong,0)) AS SoLuong
		       								  FROM   #tmp_ThoiViec
		       								  WHERE  #tmp_ThoiViec.Thang <= 8),0)
		       
		       	 WHEN MONTHS = 9 THEN TongNamTruoc.SoLuong
		       			            + ISNULL((SELECT sum(ISNULL(#tmp_BatDauLamViec.SoLuong,0)) AS SoLuong
		       						          FROM   #tmp_BatDauLamViec 
		       						          WHERE  #tmp_BatDauLamViec.Thang <= 9),0)
		       
		       				        - ISNULL((SELECT sum(ISNULL(#tmp_ThoiViec.SoLuong,0)) AS SoLuong
		       					              FROM   #tmp_ThoiViec
		       						          WHERE  #tmp_ThoiViec.Thang <= 9),0)
		       	 WHEN MONTHS = 10 THEN TongNamTruoc.SoLuong
		       			             + ISNULL((SELECT sum(ISNULL(#tmp_BatDauLamViec.SoLuong,0)) AS SoLuong
		       						           FROM   #tmp_BatDauLamViec 
		       						           WHERE  #tmp_BatDauLamViec.Thang <= 10),0)
		       				         - ISNULL((SELECT sum(ISNULL(#tmp_ThoiViec.SoLuong,0)) AS SoLuong
		       							       FROM   #tmp_ThoiViec
		       								   WHERE  #tmp_ThoiViec.Thang <= 10),0)
		       
		       	 WHEN MONTHS = 11 THEN TongNamTruoc.SoLuong
		       			             + ISNULL((SELECT sum(ISNULL(#tmp_BatDauLamViec.SoLuong,0)) AS SoLuong
		       						           FROM   #tmp_BatDauLamViec 
		       						           WHERE  #tmp_BatDauLamViec.Thang <= 11),0)
		       				         - ISNULL((SELECT sum(ISNULL(#tmp_ThoiViec.SoLuong,0)) AS SoLuong
		       								   FROM   #tmp_ThoiViec
		       								   WHERE  #tmp_ThoiViec.Thang <= 11),0)
		       
		       	 WHEN MONTHS = 12 THEN TongNamTruoc.SoLuong
		       			             + ISNULL((SELECT sum(ISNULL(#tmp_BatDauLamViec.SoLuong,0)) AS SoLuong
		       						           FROM   #tmp_BatDauLamViec 
		       						           WHERE  #tmp_BatDauLamViec.Thang <= 12),0)
		       				         - ISNULL((SELECT sum(ISNULL(#tmp_ThoiViec.SoLuong,0)) AS SoLuong
		       								   FROM   #tmp_ThoiViec
		       								   WHERE  #tmp_ThoiViec.Thang <= 12),0)
	           END AS TongSo
			  ,ISNULL(bd.SoLuong,0) AS TongNhanViec
			  ,ISNULL(kt.SoLuong,0) AS TongThoiViec
        FROM NUMBER mo
		     LEFT JOIN #tmp_BatDauLamViec bd
			 ON mo.MONTHS  =  bd.Thang
			 LEFT JOIN #tmp_ThoiViec      kt
			 ON mo.MONTHS  =  kt.Thang
           ,(SELECT COUNT(*)      AS SoLuong	
			 FROM   Hrm.NhanViens nv
			 WHERE  YEAR(NgayBatDauLamViec) < @YEAR
			 AND    ISNULL(nv.Deleted,0)    != 1 
			 AND    NOT EXISTS (SELECT 1
			 				    FROM   Hrm.NhanVien_ThoiViecs    tv
			 				    WHERE  tv.NhanVienId          =  nv.Id
			 				    AND    YEAR(tv.NgayThoiViec)  <  @YEAR
			 				    AND    ISNULL(tv.Deleted,0)   != 1)
			)TongNamTruoc;
        DROP TABLE #tmp_BatDauLamViec;
        DROP TABLE #tmp_ThoiViec;
	END

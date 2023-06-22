USE [Esuhai.HRMDev]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetNghiLes]    Script Date: 12/26/2022 2:36:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [Hrm].[SP_GetNghiLes]
	@nam datetime
as
begin
	select * 
	from Hrm.NghiLes
	where 
		year(Ngay) = year(@nam)
	
	order by Ngay
end
GO



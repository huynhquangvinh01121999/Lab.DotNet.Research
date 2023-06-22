USE [Esuhai.HRMDev]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetCauHinhNgayCongs]    Script Date: 12/26/2022 4:24:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [Hrm].[SP_GetCauHinhNgayCongs]
	@nam int
as
begin
	select * from Hrm.CauHinhNgayCongs where nam = @nam
	order by Thang
end
GO



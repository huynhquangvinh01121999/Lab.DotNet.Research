USE [Esuhai.HRM]
GO

/****** Object:  UserDefinedFunction [Hrm].[ufn_RoundingDate]    Script Date: 11/2/2022 10:25:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*==============================================================================================
   Author			Date			Updated					Description
   -----------------------------------------------------------------------------------------
   Vinh Huynh		2022.11.02								Lam tron ngay theo tieu chuan Esuhai
============================================================================================= */

CREATE FUNCTION [Hrm].[ufn_RoundingDate]
(	
	@date DATETIME
)
RETURNS DATETIME
AS
BEGIN
	DECLARE @ResultVar DATETIME		
	SET @ResultVar = CAST(@date AS DATETIME)
	
	DECLARE @minute INT
	SET @minute = DATEPART(MINUTE, @ResultVar)
	
	SET @ResultVar = (CASE
						WHEN @minute % 15 = 0 THEN @ResultVar
						WHEN @minute < 25 THEN DATEADD(MINUTE,@minute * -1, @ResultVar)
						WHEN @minute >= 55 THEN DATEADD(MINUTE,60-@minute, @ResultVar)
						ELSE DATEADD(MINUTE, 30 - @minute, @ResultVar)
					END)
					
	RETURN @ResultVar 
END

GO



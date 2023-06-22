USE [Esuhai.HRM]
GO

/****** Object:  StoredProcedure [Hrm].[SP_PostAudTransfers]    Script Date: 11/9/2022 11:18:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [Hrm].[SP_PostAudTransfers]
as
begin

	SET NOCOUNT ON;

	declare @Token int
	exec sp_OACreate 'MSXML2.XMLHTTP', @Token OUT

	declare @Parameters nvarchar(4000) = N'{'

	set @Parameters +=	case 
							when '3fa85f64-5717-4562-b3fc-2c963f66afa6' is NULL then '"listId": null,' else '"listId":''' + '3fa85f64-5717-4562-b3fc-2c963f66afa6' + ''','
						end

	set @Parameters +=	case 
							when '2022-11-09T02:29:55.183Z' is NULL then '"tgBatDau":null,' else '"tgBatDau":''' + cast('2022-11-09T02:29:55.183Z' as nvarchar) + ''','
						end

	set @Parameters +=	case
							when '2022-11-09T02:29:55.183Z' is NULL then '"tgKetThuc":null,' else '"tgKetThuc":''' + cast('2022-11-09T02:29:55.183Z' as nvarchar) + ''','
						end

	set @Parameters +=	case
							when 0 is NULL then '"soGio":null,' else '"soGio":''' + cast(0 as nvarchar) + ''','
						end

	set @Parameters +=	case
							when N'Tôi phải đi tái khám định kỳ nên xin phép nghỉ 1 ngày' is NULL then '"lyDo":null,' else '"lyDo":''' + N'Tôi phải đi tái khám định kỳ nên xin phép nghỉ 1 ngày' + ''','
						end

	set @Parameters +=	case 
							when N'Quản trị hệ thống' is NULL then '"cvThayThe":null,' else '"cvThayThe":''' + N'Quản trị hệ thống' + ''','
						end

	set @Parameters +=	case  
							when N'Nghỉ đột xuất' is NULL then '"trangThaiNghi":null,' else '"trangThaiNghi":''' + N'Nghỉ đột xuất' + ''','
						end

	set @Parameters +=	case  
							when 'approved' is NULL then '"trangThaiDangKy":null,' else '"trangThaiDangKy":''' + 'approved' + ''','
						end

	set @Parameters +=	case  
							when '3fa85f64-5717-4562-b3fc-2c963f66afa6' is NULL then '"spId":null,' else '"spId":''' + '3fa85f64-5717-4562-b3fc-2c963f66afa6' + ''','
						end

	set @Parameters +=	case  
							when N'Tôi đồng ý' is NULL then '"hr_GhiChu":null,' else '"hr_GhiChu":''' + N'Tôi đồng ý' + ''','
						end

	set @Parameters +=	case  
							when 'approved' is NULL then '"hr_TrangThai":null,' else '"hr_TrangThai":''' + 'approved' + ''','
						end

	set @Parameters +=	case  
							when 'rejected' is NULL then '"nxD1_TrangThai":null,' else '"nxD1_TrangThai":''' + 'rejected' + ''','
						end

	set @Parameters +=	case  
							when N'Tôi không đồng ý' is NULL then '"nxD1_GhiChu":null,' else '"nxD1_GhiChu":''' + N'Tôi không đồng ý' + ''','
						end

	set @Parameters +=	case  
							when 'approved' is NULL then '"nxD2_TrangThai":null,' else '"nxD2_TrangThai":''' + 'approved' + ''','
						end

	set @Parameters +=	case  
							when N'Tôi hoàn toàn đồng ý' is NULL then '"nxD2_GhiChu":null,' else '"nxD2_GhiChu":''' + N'Tôi hoàn toàn đồng ý' + ''','
						end

	set @Parameters +=	case  
							when '2022-11-09T02:29:55.183Z' is NULL then '"modified":null,' else '"modified":''' + cast('2022-11-09T02:29:55.183Z' as nvarchar) + ''','
						end

	set @Parameters +=	case  
							when '3fa85f64-5717-4562-b3fc-2c963f66afa6' is NULL then '"maNv":null,' else '"maNv":''' + '3fa85f64-5717-4562-b3fc-2c963f66afa6' + ''','
						end

	set @Parameters +=	case  
							when N'Lê Thị Lộ Liễu' is NULL then '"tenNv":null,' else '"tenNv":''' + N'Lê Thị Lộ Liễu' + ''','
						end

	set @Parameters +=	case  
							when '3fa85f64-5717-4562-b3fc-2c963f66afa6' is NULL then '"nguoiXD1":null,' else '"nguoiXD1":''' + '3fa85f64-5717-4562-b3fc-2c963f66afa6' + ''','
						end

	set @Parameters +=	case  
							when '3fa85f64-5717-4562-b3fc-2c963f66afa6' is NULL then '"nguoiXD2":null,' else '"nguoiXD2":''' + '3fa85f64-5717-4562-b3fc-2c963f66afa6' + ''','
						end

	set @Parameters +=	case  
							when N'Nguyễn Trần Kín Đáo' is NULL then '"nvTT":null,' else '"nvTT":''' + N'Nguyễn Trần Kín Đáo' + ''','
						end

	set @Parameters +=	case  
							when '3fa85f64-5717-4562-b3fc-2c963f66afa6' is NULL then '"hrXDId":null}' else '"hrXDId":''' + '3fa85f64-5717-4562-b3fc-2c963f66afa6' + '''}'
						end

	exec sp_OAMethod @Token, 'open', NULL, 'POST', 'http://localhost:5000/api/v1/AudTransfer', 'false'
	exec sp_OAMethod @Token, 'setRequestHeader', NULL, 'Content-Type', 'application/json'
	exec sp_OAMethod @Token, 'send', NULL, @Parameters

	declare @Responses nvarchar(4000)
	exec sp_OAMethod @Token, 'responseText', @Responses OUTPUT
	select @Responses as [Responses]

	exec sp_OADestroy @Token

end
GO



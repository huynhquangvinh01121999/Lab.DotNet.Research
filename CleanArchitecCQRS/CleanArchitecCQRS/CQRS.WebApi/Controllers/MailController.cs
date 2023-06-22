using System;
using System.Threading.Tasks;
using EsuhaiHRM.Application.DTOs.Account;
using EsuhaiHRM.Application.DTOs.Email;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EsuhaiHRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly INhanVienRepositoryAsync _nhanVienRepositoryAsync;
        public MailController(IAccountService accountService, INhanVienRepositoryAsync nhanVienRepositoryAsync)
        {
            _accountService = accountService;
            _nhanVienRepositoryAsync = nhanVienRepositoryAsync;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMail(EmailRequest request,Guid nhanVienId)
        {
            try
            {
                var strMail = await _nhanVienRepositoryAsync.GetEmailById(nhanVienId);
                if(strMail == null || strMail == string.Empty)
                {
                    return Ok(new Response<string>("NhanVien Email Not Found."));
                }
                else
                {
                    request.To = strMail;
                    await _accountService.SendMail(request);
                    return Ok(new Response<string>("Send Success.", null));
                }
            }
            catch(Exception ex)
            {
                return Ok(new Response<string>(ex.Message.ToString()));
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EsuhaiHRM.Application.Features.GenerateCodes.Commands;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.WebApi.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace EsuhaiHRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateController : BaseApiController
    {
        private readonly IGenerateService _generateService;

        public GenerateController(IGenerateService generateService)
        {
            _generateService = generateService;
        }
        [HttpGet("MaNhanVien")]
        public async Task<IActionResult> GenerateMaNhanVien(int CongTyId)
        {
            return Ok(await _generateService.GenerateMaNhanVien(CongTyId));
        }

        [HttpPost("CodeHopDong")]
        public async Task<IActionResult> Generate(GenerateCodeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("HopDongThuViec")]
        public async Task<IActionResult> GenerateHopDongThuViec(Guid nhanvienId)
        {
            var infoHopDong = await _generateService.GenerateHopDongThuViec(nhanvienId);
            return Ok(infoHopDong);
        }

        [HttpPost("HopDongDaoTao")]
        public async Task<IActionResult> GenerateHopDongDaoTao(Guid nhanvienId)
        {
            var infoHopDong = await _generateService.GenerateHopDongDaoTao(nhanvienId);
            return Ok(infoHopDong);
        }

        [HttpPost("HopDongLaoDong")]
        public async Task<IActionResult> GenerateHopDongLaoDong(Guid nhanvienId)
        {
            var infoHopDong = await _generateService.GenerateHopDongLaoDong(nhanvienId);
            return Ok(infoHopDong);
        }

        [HttpPost("PhuLucHopDong")]
        public async Task<IActionResult> GeneratePhuLucHopDong(Guid nhanvienId, int hopDongId)
        {
            var infoHopDong = await _generateService.GeneratePhuLucHopDong(nhanvienId, hopDongId);
            return Ok(infoHopDong);
        }

        [HttpPost("QuyetDinhTuyenDung")]
        public async Task<IActionResult> GenerateQuyetDinhTuyenDung(Guid nhanvienId)
        {
            var infoHopDong = await _generateService.GenerateQuyetDinhTuyenDung(nhanvienId);
            return Ok(infoHopDong);
        }

        [HttpPost("PhuLucHopDongDieuChinh")]
        public async Task<IActionResult> GeneratePhuLucHopDongDieuChinh(Guid nhanvienId, int hopDongId, int phulucId)
        {
            var infoHopDong = await _generateService.GeneratePhuLucHopDongDieuChinh(nhanvienId, hopDongId, phulucId);
            return Ok(infoHopDong);
        }

        [HttpPost("QuyetDinhThoiViec")]
        public async Task<IActionResult> GenerateQuyetDinhThoiViec(Guid nhanvienId, int thoiviecId)
        {
            var infoHopDong = await _generateService.GenerateQuyetDinhThoiViec(nhanvienId, thoiviecId);
            return Ok(infoHopDong);
        }

        [HttpPost("BienBanThoaThuan")]
        public async Task<IActionResult> GenerateBienBanThoaThuan(Guid nhanvienId, int quyetDinhThoiViecId)
        {
            var infoHopDong = await _generateService.GenerateBienBanThoaThuan(nhanvienId, quyetDinhThoiViecId);
            return Ok(infoHopDong);
        }
    }
}
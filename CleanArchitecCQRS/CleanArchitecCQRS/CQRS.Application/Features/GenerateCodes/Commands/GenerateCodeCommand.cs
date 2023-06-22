using AutoMapper;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.GenerateCodes.Commands
{
    public partial class GenerateCodeCommand : IRequest<Response<string>>
    {
        public int CongTyId { get; set; }
        public DateTime NgayKy { get; set; }
        public string TenLoaiVanBan { get; set; }
    }
    public class GenerateCodeCommandHandler : IRequestHandler<GenerateCodeCommand, Response<string>>
    {
        private readonly IGenerateService _generateCodeService;
        public GenerateCodeCommandHandler(IGenerateService generateCodeService)
        {
            _generateCodeService = generateCodeService;
        }
        public async Task<Response<string>> Handle(GenerateCodeCommand request, CancellationToken cancellationToken)
        {
            var resultCode = await _generateCodeService.GenerateResult(request.CongTyId,request.NgayKy,request.TenLoaiVanBan);
            return resultCode;
        }
    }

}

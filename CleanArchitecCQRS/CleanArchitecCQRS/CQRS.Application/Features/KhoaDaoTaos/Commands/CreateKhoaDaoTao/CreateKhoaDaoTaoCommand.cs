using AutoMapper;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.KhoaDaoTaos.Commands.CreateKhoaDaoTao
{
    public partial class CreateKhoaDaoTaoCommand : IRequest<Response<int>>
    {
        public string TenVN { get; set; }
        public string TenJP { get; set; }
        public DateTime? NgayDaoTao { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string NguoiDaoTao { get; set; }
        public string MucDich { get; set; }
        public string DiaDiem { get; set; }
        public string GhiChu { get; set; }
    }
    public class CreateKhoaDaoTaoCommandHandler : IRequestHandler<CreateKhoaDaoTaoCommand, Response<int>>
    {
        private readonly IKhoaDaoTaoRepositoryAsync _khoaDaoTaoRepository;
        private readonly IMapper _mapper;
        public CreateKhoaDaoTaoCommandHandler(IKhoaDaoTaoRepositoryAsync khoaDaoTaoRepository, IMapper mapper)
        {
            _khoaDaoTaoRepository = khoaDaoTaoRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateKhoaDaoTaoCommand request, CancellationToken cancellationToken)
        {
            var khoadaotao = _mapper.Map<KhoaDaoTao>(request);
            await _khoaDaoTaoRepository.AddAsync(khoadaotao);
            return new Response<int>(khoadaotao.Id);
        }
    }

}

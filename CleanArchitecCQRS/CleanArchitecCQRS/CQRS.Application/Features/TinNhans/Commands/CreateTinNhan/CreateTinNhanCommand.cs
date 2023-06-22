using AutoMapper;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TinNhans.Commands.CreateTinNhan
{
    public partial class CreateTinNhanCommand : IRequest<Response<TinNhan>>
    {
        public Guid Id { get; set; } = new Guid();
        public Guid? NhanVienGuiId { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string NhanHieu { get; set; }
        public string UrlThongTin { get; set; }
        public DateTime NgayGui { get; set; }
        public DateTime NgayTao { get; set; }
        public string GhiChu { get; set; }
    }
    public class CreateTinNhanCommandHandler : IRequestHandler<CreateTinNhanCommand, Response<TinNhan>>
    {
        private readonly ITinNhanRepositoryAsync _tinNhanRepository;
        private readonly IMapper _mapper;
        public CreateTinNhanCommandHandler(ITinNhanRepositoryAsync tinNhanRepository, IMapper mapper)
        {
            _tinNhanRepository = tinNhanRepository;
            _mapper = mapper;
        }
        public async Task<Response<TinNhan>> Handle(CreateTinNhanCommand request, CancellationToken cancellationToken)
        {
            var tinNhanRepository = _mapper.Map<TinNhan>(request);

            await _tinNhanRepository.AddAsync(tinNhanRepository);

            return new Response<TinNhan>(tinNhanRepository);
        }
    }

}

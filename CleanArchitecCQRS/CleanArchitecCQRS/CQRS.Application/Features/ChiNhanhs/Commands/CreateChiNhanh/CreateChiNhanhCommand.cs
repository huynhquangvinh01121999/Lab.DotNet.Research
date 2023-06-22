using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace EsuhaiHRM.Application.Features.ChiNhanhs.Commands.CreateChiNhanh
{
    public partial class CreateChiNhanhCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; } = new Guid();
        public string TenVN { get; set; }
        public string TenJP { get; set; }
        public int? LoaiChiNhanhId { get; set; }
        public Guid? Parent { get; set; }
        public string DiaChi { get; set; }
        public int? TinhId { get; set; }
        public bool? TrangThai { get; set; }
        public string GhiChu { get; set; }
    }
    public class CreateChiNhanhCommandHandler : IRequestHandler<CreateChiNhanhCommand, Response<Guid>>
    {
        private readonly IChiNhanhRepositoryAsync _chiNhanhRepository;
        private readonly IMapper _mapper;
        public CreateChiNhanhCommandHandler(IChiNhanhRepositoryAsync chiNhanhRepository, IMapper mapper)
        {
            _chiNhanhRepository = chiNhanhRepository;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreateChiNhanhCommand request, CancellationToken cancellationToken)
        {
            var chiNhanh = _mapper.Map<ChiNhanh>(request);
            await _chiNhanhRepository.AddAsync(chiNhanh);
            return new Response<Guid>(chiNhanh.Id);
        }
    }
}

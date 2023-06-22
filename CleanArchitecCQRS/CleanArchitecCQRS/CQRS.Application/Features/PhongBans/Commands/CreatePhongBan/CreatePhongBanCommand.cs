using AutoMapper;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.PhongBans.Commands.CreatePhongBan
{
    public partial class CreatePhongBanCommand : IRequest<Response<int>>
    {
        public string TenVN { get; set; }
        public string TenEN { get; set; }
        public string TenJP { get; set; }
        public string PhanLoai { get; set; }
        public Guid? TruongBoPhanId { get; set; }
        public int? Parent { get; set; }
        public int? GroupMailId { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }
    }
    public class CreatePhongBanCommandHandler : IRequestHandler<CreatePhongBanCommand, Response<int>>
    {
        private readonly IPhongBanRepositoryAsync _phongBanRepository;
        private readonly IMapper _mapper;
        public CreatePhongBanCommandHandler(IPhongBanRepositoryAsync phongBanRepository, IMapper mapper)
        {
            _phongBanRepository = phongBanRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreatePhongBanCommand request, CancellationToken cancellationToken)
        {
            var phongBan = _mapper.Map<PhongBan>(request);
            await _phongBanRepository.AddAsync(phongBan);
            return new Response<int>(phongBan.Id);
        }
    }

}

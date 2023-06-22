using AutoMapper;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.CaLamViecs.Commands.CreateCaLamViec
{
    public partial class CreateCaLamViecCommand : IRequest<Response<int>>
    {
        public string TenVN { get; set; }
        public string TenJP { get; set; }
        public DateTime? GioBatDau { get; set; }
        public DateTime? GioKetThuc { get; set; }
        public DateTime? BatDauNghi { get; set; }
        public DateTime? KetThucNghi { get; set; }
        public bool? KhacNgay { get; set; }
        public string GhiChu { get; set; }
    }
    public class CreateCaLamViecCommandHandler : IRequestHandler<CreateCaLamViecCommand, Response<int>>
    {
        private readonly ICaLamViecRepositoryAsync _calamviecRepository;
        private readonly IMapper _mapper;
        public CreateCaLamViecCommandHandler(ICaLamViecRepositoryAsync calamviecRepository, IMapper mapper)
        {
            _calamviecRepository = calamviecRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateCaLamViecCommand request, CancellationToken cancellationToken)
        {
            var calamviec = _mapper.Map<CaLamViec>(request);
            await _calamviecRepository.AddAsync(calamviec);
            return new Response<int>(calamviec.Id);
        }
    }

}

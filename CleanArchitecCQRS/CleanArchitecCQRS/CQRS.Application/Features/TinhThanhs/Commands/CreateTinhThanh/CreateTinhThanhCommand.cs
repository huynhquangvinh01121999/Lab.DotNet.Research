using AutoMapper;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TinhThanhs.Commands.CreateTinhThanh
{
    public partial class CreateTinhThanhCommand : IRequest<Response<int>>
    {
        public string TenTinhVN { get; set; }
        public string TenTinhEN { get; set; }
        public string TenTinhJP { get; set; }
        public string MaTinh { get; set; }
    }
    public class CreateTinhThanhCommandHandler : IRequestHandler<CreateTinhThanhCommand, Response<int>>
    {
        private readonly ITinhThanhRepositoryAsync _tinhThanhRepository;
        private readonly IMapper _mapper;
        public CreateTinhThanhCommandHandler(ITinhThanhRepositoryAsync tinhThanhRepository, IMapper mapper)
        {
            _tinhThanhRepository = tinhThanhRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateTinhThanhCommand request, CancellationToken cancellationToken)
        {
            var tinhThanh = _mapper.Map<TinhThanh>(request);
            await _tinhThanhRepository.AddAsync(tinhThanh);
            return new Response<int>(tinhThanh.Id);
        }
    }

}

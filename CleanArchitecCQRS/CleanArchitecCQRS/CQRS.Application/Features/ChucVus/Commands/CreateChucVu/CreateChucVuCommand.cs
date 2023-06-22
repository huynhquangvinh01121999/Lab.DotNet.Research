using AutoMapper;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.ChucVus.Commands.CreateChucVu
{
    public partial class CreateChucVuCommand : IRequest<Response<int>>
    {
        public string TenVN { get; set; }
        public string TenEN { get; set; }
        public string TenJP { get; set; }
        public int? CapBac { get; set; }
        public string PhanLoai { get; set; }
        public string GhiChu { get; set; }
    }
    public class CreateChucVuCommandHandler : IRequestHandler<CreateChucVuCommand, Response<int>>
    {
        private readonly IChucVuRepositoryAsync _chucvuRepository;
        private readonly IMapper _mapper;
        public CreateChucVuCommandHandler(IChucVuRepositoryAsync chucvuRepository, IMapper mapper)
        {
            _chucvuRepository = chucvuRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateChucVuCommand request, CancellationToken cancellationToken)
        {
            var chucvu = _mapper.Map<ChucVu>(request);
            await _chucvuRepository.AddAsync(chucvu);
            return new Response<int>(chucvu.Id);
        }
    }

}

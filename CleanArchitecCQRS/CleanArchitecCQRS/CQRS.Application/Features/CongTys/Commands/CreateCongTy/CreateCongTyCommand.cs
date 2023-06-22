using AutoMapper;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.CongTys.Commands.CreateCongTy
{
    public partial class CreateCongTyCommand : IRequest<Response<int>>
    {
        public string Code { get; set; }
        public string MaSoThue { get; set; }
        public string TenCongTyVN { get; set; }
        public string TenCongTyEn { get; set; }
        public string TenCongTyJP { get; set; }
        public string TenVietTat { get; set; }
        public string TenGiamDoc { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }
    }
    public class CreateCongTyCommandHandler : IRequestHandler<CreateCongTyCommand, Response<int>>
    {
        private readonly ICongTyRepositoryAsync _congtyRepository;
        private readonly IMapper _mapper;
        public CreateCongTyCommandHandler(ICongTyRepositoryAsync congtyRepository, IMapper mapper)
        {
            _congtyRepository = congtyRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateCongTyCommand request, CancellationToken cancellationToken)
        {
            var congty = _mapper.Map<CongTy>(request);
            await _congtyRepository.AddAsync(congty);
            return new Response<int>(congty.Id);
        }
    }

}

using AutoMapper;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.CreateTangCa
{
    public partial class CreateTangCaCommand : IRequest<Response<string>>
    {
        public Guid NhanVienId { get; set; }
        public DateTime NgayTangCa { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string MoTa { get; set; }
        public Guid? NguoiXetDuyetCap1Id { get; set; }
        public Guid? NguoiXetDuyetCap2Id { get; set; }
        public float? SoGioDangKy { get; set; }
    }
    public class CreateTangCaCommandHandler : IRequestHandler<CreateTangCaCommand, Response<string>>
    {
        private readonly ITangCaRepositoryAsync _tangCaRepository;
        private readonly IMapper _mapper;
        public CreateTangCaCommandHandler(ITangCaRepositoryAsync tangCaRepository, IMapper mapper)
        {
            _tangCaRepository = tangCaRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(CreateTangCaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ot = _mapper.Map<TangCa>(request);
                ot.TrangThai = "Submitted";
                await _tangCaRepository.AddAsync(ot);
                return new Response<string>(ot.Id.ToString(), null);
            }
            catch(Exception ex)
            {
                return new Response<string>($"An exception error has occurred: {ex.Message}");
            }
        }
    }

}

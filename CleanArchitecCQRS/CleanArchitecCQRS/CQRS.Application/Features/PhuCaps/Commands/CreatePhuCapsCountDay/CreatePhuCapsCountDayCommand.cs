using AutoMapper;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.CreatePhuCapsCountDay
{
    public partial class CreatePhuCapsCountDayCommand : IRequest<Response<string>>
    {
        public Guid NhanVienId { get; set; }
        public Guid? NguoiXetDuyetCap1Id { get; set; }
        public Guid? NguoiXetDuyetCap2Id { get; set; }
        public int LoaiPhuCapId { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string MoTa { get; set; }
    }

    public class CreatePhuCapsCountDayCommandHandler : IRequestHandler<CreatePhuCapsCountDayCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IPhuCapRepositoryAsync _phuCapRepositoryAsync;
        private readonly ILoaiPhuCapRepositoryAsync _loaiPhuCapRepositoryAsync;

        public CreatePhuCapsCountDayCommandHandler(IMapper mapper, IPhuCapRepositoryAsync phuCapRepositoryAsync, ILoaiPhuCapRepositoryAsync loaiPhuCapRepositoryAsync)
        {
            _mapper = mapper;
            _phuCapRepositoryAsync = phuCapRepositoryAsync;
            _loaiPhuCapRepositoryAsync = loaiPhuCapRepositoryAsync;
        }

        public async Task<Response<string>> Handle(CreatePhuCapsCountDayCommand request, CancellationToken cancellationToken)
        {
            var loaiPhuCap = await _loaiPhuCapRepositoryAsync.GetByIdAsync(request.LoaiPhuCapId);

            if (loaiPhuCap == null)
                return new Response<string>($"LoaiPhuCap ID: {request.LoaiPhuCapId} was not found.");
            try
            {
                //var pc = _mapper.Map<PhuCap>(request);
                var pc = new PhuCap
                {
                    NhanVienId = request.NhanVienId,
                    NguoiXetDuyetCap1Id = request.NguoiXetDuyetCap1Id,
                    NguoiXetDuyetCap2Id = request.NguoiXetDuyetCap2Id,
                    LoaiPhuCapId = request.LoaiPhuCapId,
                    ThoiGianBatDau = request.ThoiGianBatDau,
                    ThoiGianKetThuc = request.ThoiGianKetThuc,
                    MoTa = request.MoTa
                };
                await _phuCapRepositoryAsync.AddNewPhuCapsAsync(pc, loaiPhuCap.Code);
                return new Response<string>(pc.Id.ToString(), null);
            }
            catch (Exception ex) {
                return new Response<string>($"An exception error has occurred: {ex.Message}");
            }
        }
    }
}

using AutoMapper;
using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViens;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NhanViens.Queries.GetNhanVienById
{
    public class GetNhanVienByIdQuery : IRequest<Response<NhanViensViewModel>>
    {
        public Guid Guid { get; set; }
        public class GetNhanVienByIdQueryHandler : IRequestHandler<GetNhanVienByIdQuery, Response<NhanViensViewModel>>
        {
            private readonly INhanVienRepositoryAsync _nhanvienRepository;
            private readonly IMapper _mapper;
            public GetNhanVienByIdQueryHandler(INhanVienRepositoryAsync nhanvienRepository, IMapper mapper)
            {
                _nhanvienRepository = nhanvienRepository;
                _mapper = mapper;
            }
            //public async Task<Response<NhanVien>> Handle(GetNhanVienByIdQuery query, CancellationToken cancellationToken)
            //{
            //    var nhanvien = await _nhanvienRepository.S2_GetByIdAsync(query.Guid);
            //    if (nhanvien == null) throw new ApiException($"NhanVien Not Found.");
            //    return new Response<NhanVien>(nhanvien);
            //}

            public async Task<Response<NhanViensViewModel>> Handle(GetNhanVienByIdQuery query, CancellationToken cancellationToken)
            {
                var nhanvien = await _nhanvienRepository.S2_GetByIdAsync(query.Guid);

                if (nhanvien == null) throw new ApiException($"NhanVien Not Found.");

                var nhanvienViewModel = _mapper.Map<NhanViensViewModel>(nhanvien);
                //GetAllNhanViensViewModel af = new GetAllNhanViensViewModel();

                return new Response<NhanViensViewModel>(nhanvienViewModel);
            }
        }
    }
}

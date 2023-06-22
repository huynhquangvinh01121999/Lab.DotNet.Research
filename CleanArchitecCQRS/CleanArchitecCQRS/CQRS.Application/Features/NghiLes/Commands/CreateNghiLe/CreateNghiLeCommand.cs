using AutoMapper;
using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NghiLes.Commands.CreateNghiLe
{
    public class CreateNghiLeCommand : IRequest<Response<string>>
    {
        public DateTime? Ngay { get; set; }
        public DateTime? NgayCoDinh { get; set; }
        public string MoTa { get; set; }
    }

    public class CreateNghiLeCommandHandler : IRequestHandler<CreateNghiLeCommand, Response<string>>
    {
        private readonly INghiLeRepositoryAsync _nghiLeRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateNghiLeCommandHandler(INghiLeRepositoryAsync nghiLeRepositoryAsync, IMapper mapper)
        {
            _nghiLeRepositoryAsync = nghiLeRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateNghiLeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var isExist = await _nghiLeRepositoryAsync.S2_isExistNghiLe(request.Ngay, request.NgayCoDinh);
                if (isExist)
                    return new Response<string>($"NGL001");

                var newNghiLe = _mapper.Map<NghiLe>(request);

                await _nghiLeRepositoryAsync.AddAsync(newNghiLe);

                return new Response<string>(newNghiLe.Id.ToString(), null);
            }
            catch(Exception ex) {
                throw new ApiException(ex.Message);
            }
        }
    }
}

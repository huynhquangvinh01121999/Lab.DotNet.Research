using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NghiLes.Commands.DeleteNghiLe
{
    public class DeleteNghiLeCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }

    public class DeleteNghiLeCommandHandler : IRequestHandler<DeleteNghiLeCommand, Response<string>>
    {
        private readonly INghiLeRepositoryAsync _nghiLeRepositoryAsync;

        public DeleteNghiLeCommandHandler(INghiLeRepositoryAsync nghiLeRepositoryAsync)
        {
            _nghiLeRepositoryAsync = nghiLeRepositoryAsync;
        }

        public async Task<Response<string>> Handle(DeleteNghiLeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var nghile = await _nghiLeRepositoryAsync.S2_GetNghiLeByIdAsync(request.Id);

                if (nghile == null)
                    return new Response<string>($"NGL002");

                await _nghiLeRepositoryAsync.DeleteAsync(nghile);

                return new Response<string>(nghile.Id.ToString(), null);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}

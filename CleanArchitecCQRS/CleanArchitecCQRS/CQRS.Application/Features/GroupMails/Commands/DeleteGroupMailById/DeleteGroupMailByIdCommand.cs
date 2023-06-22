using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.GroupMails.Commands.DeleteGroupMailById
{
    public class DeleteGroupMailByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteGroupMailByIdCommandHandler : IRequestHandler<DeleteGroupMailByIdCommand, Response<int>>
        {
            private readonly IGroupMailRepositoryAsync _groupMailRepository;
            public DeleteGroupMailByIdCommandHandler(IGroupMailRepositoryAsync groupMailRepository)
            {
                _groupMailRepository = groupMailRepository;
            }
            public async Task<Response<int>> Handle(DeleteGroupMailByIdCommand command, CancellationToken cancellationToken)
            {
                var groupmail = await _groupMailRepository.S2_GetByIdAsync(command.Id);
                if (groupmail == null) throw new ApiException($"GroupMail Not Found.");
                await _groupMailRepository.DeleteAsync(groupmail);
                return new Response<int>(groupmail.Id);
            }
        }
    }
}

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
    public class DisableGroupMailByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DisableGroupMailByIdCommandHandler : IRequestHandler<DisableGroupMailByIdCommand, Response<int>>
        {
            private readonly IGroupMailRepositoryAsync _groupmailRepository;
            public DisableGroupMailByIdCommandHandler(IGroupMailRepositoryAsync groupmailRepository)
            {
                _groupmailRepository = groupmailRepository;
            }
            public async Task<Response<int>> Handle(DisableGroupMailByIdCommand command, CancellationToken cancellationToken)
            {
                var groupmail = await _groupmailRepository.S2_GetByIdAsync(command.Id);

                if (groupmail == null)
                {
                    throw new ApiException($"GroupMail Not Found.");
                }
                else
                {
                    var result = await _groupmailRepository.DisableAsync(groupmail);

                    if (result)
                    {
                        return new Response<int>(groupmail.Id);
                    }
                    else
                    {
                        throw new ApiException($"GroupMail is Using, Cannot Delete.");
                    }
                    //groupmail.Deleted = true;

                    //await _groupmailRepository.UpdateAsync(groupmail);
                    //        return new Response<int>(groupmail.Id);
                }
            }
        }
    }
}

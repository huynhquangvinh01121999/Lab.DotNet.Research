using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.GroupMails.Queries.GetGroupMailById
{
    public class GetGroupMailByIdQuery : IRequest<Response<GroupMail>>
    {
        public int Id { get; set; }
        public class GetGroupMailByIdQueryHandler : IRequestHandler<GetGroupMailByIdQuery, Response<GroupMail>>
        {
            private readonly IGroupMailRepositoryAsync _groupMailRepository;
            public GetGroupMailByIdQueryHandler(IGroupMailRepositoryAsync groupMailRepository)
            {
                _groupMailRepository = groupMailRepository;
            }
            public async Task<Response<GroupMail>> Handle(GetGroupMailByIdQuery query, CancellationToken cancellationToken)
            {
                var groupMail = await _groupMailRepository.S2_GetByIdAsync(query.Id);
                if (groupMail == null) throw new ApiException($"GroupMail Not Found.");
                return new Response<GroupMail>(groupMail);
            }
        }
    }
}

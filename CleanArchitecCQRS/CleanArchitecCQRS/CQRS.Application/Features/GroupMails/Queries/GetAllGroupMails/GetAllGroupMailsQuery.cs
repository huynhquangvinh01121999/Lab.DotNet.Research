using EsuhaiHRM.Application.Filters;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EsuhaiHRM.Domain.Entities;

namespace EsuhaiHRM.Application.Features.GroupMails.Queries.GetAllGroupMails
{
    public class GetAllGroupMailsQuery : IRequest<PagedResponse<IEnumerable<GetAllGroupMailsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllGroupMailsQueryHandler : IRequestHandler<GetAllGroupMailsQuery, PagedResponse<IEnumerable<GetAllGroupMailsViewModel>>>
    {
        private readonly IGroupMailRepositoryAsync _groupMailRepository;
        private readonly IMapper _mapper;
        public GetAllGroupMailsQueryHandler(IGroupMailRepositoryAsync groupMailRepository, IMapper mapper)
        {
            _groupMailRepository = groupMailRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllGroupMailsViewModel>>> Handle(GetAllGroupMailsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllGroupMailsParameter>(request);
            //var groupmails = await _groupMailRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var groupmails = await _groupMailRepository.S2_GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var groupmailViewModel = _mapper.Map<IEnumerable<GetAllGroupMailsViewModel>>(groupmails);
            return new PagedResponse<IEnumerable<GetAllGroupMailsViewModel>>(groupmailViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}

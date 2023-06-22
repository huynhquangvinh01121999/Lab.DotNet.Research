using AutoMapper;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.GroupMails.Commands.CreateGroupMail
{
    public partial class CreateGroupMailCommand : IRequest<Response<int>>
    {
        public string Ten { get; set; }
        public Guid? NhanVienDeNghiId { get; set; }
        public DateTime? NgayTao { get; set; }
        public string MucDich { get; set; }
        public string MoTa { get; set; }
        public string PhanLoai { get; set; }
        public string GhiChu { get; set; }
    }
    public class CreateGroupMailCommandHandler : IRequestHandler<CreateGroupMailCommand, Response<int>>
    {
        private readonly IGroupMailRepositoryAsync _groupMailRepository;
        private readonly IMapper _mapper;
        public CreateGroupMailCommandHandler(IGroupMailRepositoryAsync groupMailRepository, IMapper mapper)
        {
            _groupMailRepository = groupMailRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateGroupMailCommand request, CancellationToken cancellationToken)
        {
            var groupmail = _mapper.Map<GroupMail>(request);
            await _groupMailRepository.AddAsync(groupmail);
            return new Response<int>(groupmail.Id);
        }
    }

}

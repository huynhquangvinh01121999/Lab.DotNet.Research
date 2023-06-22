using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.GroupMails.Commands.UpdateGroupMail
{
    public class UpdateGroupMailCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public Guid? NhanVienDeNghiId { get; set; }
        public DateTime? NgayTao { get; set; }
        public string MucDich { get; set; }
        public string MoTa { get; set; }
        public string PhanLoai { get; set; }
        public string GhiChu { get; set; }
        public class UpdateGroupMailCommandHandler : IRequestHandler<UpdateGroupMailCommand, Response<int>>
        {
            private readonly IGroupMailRepositoryAsync _groupMailRepository;
            public UpdateGroupMailCommandHandler(IGroupMailRepositoryAsync groupMailRepository)

            {
                _groupMailRepository = groupMailRepository;
            }
            public async Task<Response<int>> Handle(UpdateGroupMailCommand command, CancellationToken cancellationToken)
            {
                var groupmail = await _groupMailRepository.S2_GetByIdAsync(command.Id);

                if (groupmail == null)
                {
                    throw new ApiException($"GroupMail Not Found.");
                }
                else
                {
                    groupmail.Ten = command.Ten;
                    groupmail.NhanVienDeNghiId = command.NhanVienDeNghiId;
                    groupmail.NgayTao = command.NgayTao;
                    groupmail.MucDich = command.MucDich;
                    groupmail.MoTa = command.MoTa;
                    groupmail.PhanLoai = command.PhanLoai;
                    groupmail.GhiChu = command.GhiChu;
                    await _groupMailRepository.UpdateAsync(groupmail);
                    return new Response<int>(groupmail.Id);
                }
            }
        }
    }
}

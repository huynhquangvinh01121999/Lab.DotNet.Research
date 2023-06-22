using EsuhaiHRM.Application.Features.GroupMails.Queries.GetAllGroupMails;
using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface IGroupMailRepositoryAsync: IGenericRepositoryAsync<GroupMail>
    {
        Task<GroupMail> S2_GetByIdAsync(int id);
        Task<IReadOnlyList<GetAllGroupMailsViewModel>> S2_GetPagedReponseAsync(int pageNumber, int pageSize);
    }
}

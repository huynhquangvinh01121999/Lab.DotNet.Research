using EsuhaiHRM.Application.DTOs.Account;
using EsuhaiHRM.Application.Features.Admin.Queries.GetRoles;
using EsuhaiHRM.Application.Features.Admin.Queries.GetUsers;
using EsuhaiHRM.Application.Wrappers;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces
{
    public interface IAdminRepositoryAsync
    {
        Task<IList<string>> CreateAllRolesForUser(string userId);
        Task<IList<string>> RemoveAllRolesForUser(string userId);
        Task<string> CreateOneRoleForUser(string userId, string roleName);
        Task<string> RemoveOneRoleForUser(string userId, string roleName);
        Task<IList<GetAllRolesViewModel>> GetAllRoles(int pageNumber, int pageSize);
        Task<IList<GetUsersByRoleViewModel>> GetUsersByRole(int pageNumber, int pageSize, string roleName);
        Task<IList<GetAllRolesViewModel>> GetRolesByUser(int pageNumber, int pageSize, string userId);
        Task<IList<string>> CreateRolesForUser(string userId, IList<string> roleNames);
    }
}

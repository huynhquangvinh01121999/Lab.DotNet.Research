using EsuhaiHRM.Infrastructure.Identity.Contexts;
using EsuhaiHRM.Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EsuhaiHRM.Infrastructure.Identity.Models;
using System.Linq;
using System;
using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Features.Admin.Queries.GetRoles;
using EsuhaiHRM.Application.Features.Admin.Queries.GetUsers;

namespace EsuhaiHRM.Infrastructure.Identity.Services
{
    public class AdminRepositoryAsync : IAdminRepositoryAsync
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityContext _idenContext;

        public AdminRepositoryAsync(UserManager<ApplicationUser> userManager
                                  , RoleManager<IdentityRole> roleManager
                                  , IdentityContext idenContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _idenContext = idenContext;
        }

        public async Task<IList<string>> CreateAllRolesForUser(string userId)
        {
            try
            {
                var getUser = await _userManager.FindByIdAsync(userId);

                var removeAllRolesForUser = await RemoveAllRolesForUser(userId);

                var getAllRoles = await _roleManager.Roles.Select(nv => nv.Name).ToListAsync();

                if(getAllRoles.Count > 0)
                {
                    var resultCreate = await _userManager.AddToRolesAsync(getUser, getAllRoles);
                }

                return await Task.FromResult(getAllRoles);
            }
            catch(Exception ex)
            {
                throw new ApiException($"Error:{ex.Message}");
            }
        }

        public async Task<IList<string>> RemoveAllRolesForUser(string userId)
        {
            try
            {
                var getUser = await _userManager.FindByIdAsync(userId);

                var getUserRoles = await _userManager.GetRolesAsync(getUser).ConfigureAwait(false);

                if(getUserRoles.Count > 0)
                {
                    var resultRemove = await _userManager.RemoveFromRolesAsync(getUser, getUserRoles);
                }

                return getUserRoles;
            }
            catch(Exception ex)
            {
                throw new ApiException($"Error:{ex.Message}");
            }
        }

        public async Task<string> CreateOneRoleForUser(string userId, string roleName)
        {
            try
            {
                var getUser = await _userManager.FindByIdAsync(userId);

                var removeOneRoleForUser = await RemoveOneRoleForUser(userId, roleName);

                var roleCreate = _roleManager.Roles.Where(ro => ro.Name.Equals(roleName))
                                                   .Select(ro => ro.Name).FirstOrDefault();
                
                if(roleCreate != null)
                {
                    var resultCreate = await _userManager.AddToRoleAsync(getUser, roleCreate);
                }

                return await Task.FromResult(roleCreate);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error:{ex.Message}");
            }
        }

        public async Task<string> RemoveOneRoleForUser(string userId, string roleName)
        {
            try
            {
                var getUser = await _userManager.FindByIdAsync(userId);

                var getUserRole = await _userManager.GetRolesAsync(getUser).ConfigureAwait(false);

                var getUserRoleScreen = getUserRole.Where(ur => ur.Equals(value: roleName)).FirstOrDefault();

                if(getUserRoleScreen != null)
                {
                    var resultRemove = await _userManager.RemoveFromRoleAsync(getUser, getUserRoleScreen);
                }

                return getUserRoleScreen;
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error:{ex.Message}");
            }
        }

        public async Task<IList<GetAllRolesViewModel>> GetAllRoles(int pageNumber, int pageSize)
        {
            try
            {
                return await (from   ro in _roleManager.Roles
                              join   ri in _idenContext.RoleInfor
                              on     ro.Id equals ri.RoleId into leftjoin
                              from   lf in leftjoin.DefaultIfEmpty()
                              select new GetAllRolesViewModel
                              {
                                  Id = ro.Id,
                                  RoleName = ro.Name,
                                  MoTa = lf.MoTa
                              }).Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .AsNoTracking()
                                .ToListAsync();
            }
            catch(Exception ex)
            {
                throw new ApiException($"Error: {ex.Message}");
            }
        }

        public async Task<IList<GetUsersByRoleViewModel>> GetUsersByRole(int pageNumber, int pageSize, string roleName)
        {
            try
            {
                var checkExists = await _roleManager.RoleExistsAsync(roleName);
                if (!checkExists)
                {
                    throw new ApiException("RoleName Not Found!");
                }
                var result = new List<GetUsersByRoleViewModel>();
                var getUsers = await _userManager.GetUsersInRoleAsync(roleName);
                for(int i = 0; i< getUsers.Count; i++)
                {
                    var user = getUsers[i];
                    var item = new GetUsersByRoleViewModel();
                    item.UserId = user.Id;
                    item.FirstName = user.FirstName;
                    item.LastName = user.UserName;
                    item.NhanVienId = user.NhanVienId;
                    item.Username = user.UserName;
                    result.Add(item);
                }
                return result.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error: {ex.Message}");
            }
        }

        public async Task<IList<GetAllRolesViewModel>> GetRolesByUser(int pageNumber, int pageSize, string userId)
        {
            try
            {
                var getUser = await _userManager.FindByIdAsync(userId);
                if(getUser == null)
                {
                    throw new ApiException("User Not Found!");
                }
                var userRoles = await _userManager.GetRolesAsync(getUser);
                return await (from ro in _roleManager.Roles
                              join ri in _idenContext.RoleInfor 
                              on ro.Id equals ri.RoleId into leftjoin
                              from lf in leftjoin.DefaultIfEmpty()
                              where userRoles.Contains(ro.Name)
                              select new GetAllRolesViewModel
                              {
                                  Id = ro.Id,
                                  RoleName = ro.Name,
                                  MoTa = lf.MoTa
                              }).Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .AsNoTracking()
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error: {ex.Message}");
            }
        }

        public async Task<IList<string>> CreateRolesForUser(string userId ,IList<string> roleNames)
        {
            try
            {
                var getUser = await _userManager.FindByIdAsync(userId);
                if (getUser == null)
                {
                    throw new ApiException("User Not Found!");
                }
                if (roleNames.Count > 0)
                {
                    var resultCreate = await _userManager.AddToRolesAsync(getUser, roleNames);
                }
                return await Task.FromResult(roleNames);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error:{ex.Message}");
            }
        }
    }
}

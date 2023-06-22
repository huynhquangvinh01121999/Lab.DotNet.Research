﻿using Application.DTOs.EmployeeDto;
using Application.Features.Employee.Commands.CreateEmployees;
using Application.Features.Employee.Commands.DeleteEmployees;
using Application.Features.Employee.Commands.UpdateImageEmployee;
using Application.Features.Employee.Commands.UpdateInfoEmployee;
using Application.Features.Employee.Queries.GetEmployeeById;
using Application.Features.Employee.Queries.GetEmployeeByIdFromAdmin;
using Application.Features.Employee.Queries.GetListEmployeeLimitField;
using Application.Features.Employee.Queries.GetListEmployees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Utilities.Helpers;

namespace API.Controllers
{
    [ApiVersion("1.0")]
    public class EmployeesController : BaseApiController
    {

        [HttpGet("getListFromAdmin")]
        [Authorize(Roles = Constant.RoleValue.Admin)]
        public async Task<IActionResult> GetListEmployeeFromAdmin()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString();

            //if (string.IsNullOrEmpty(_bearer_token))
            //    return Unauthorized();

            return Ok(await Mediator.Send(new GetListEmployeeQuery { token = _bearer_token.Replace("Bearer ", "") }));
        }

        [HttpGet("getListFromUser")]
        //[Authorize(Roles = Constant.RoleValue.Admin + "," + Constant.RoleValue.User)]
        [Authorize(Roles = Constant.RoleValue.User)]
        public async Task<IActionResult> GetListEmployeeFromUser()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString();

            //if (string.IsNullOrEmpty(_bearer_token))
            //    return Unauthorized();

            return Ok(await Mediator.Send(new GetListLimitFieldQuery { token = _bearer_token.Replace("Bearer ", "") }));
        }

        [HttpGet("getFromUser/{id}")]
        [Authorize(Roles = Constant.RoleValue.User)]
        public async Task<IActionResult> GetEmployeeFromUser(int id)
        {
            return Ok(await Mediator.Send(new GetEmployeeByIdQuery { Id = id }));
        }

        [HttpGet("getFromAdmin/{id}")]
        [Authorize(Roles = Constant.RoleValue.Admin)]
        public async Task<IActionResult> GetEmployeeFromAdmin(int id)
        {
            return Ok(await Mediator.Send(new GetEmployeeByIdFromAdminQuery { Id = id }));
        }

        [HttpPost("createNew")]
        //[Authorize(Roles = Constant.RoleValue.Admin)]
        public async Task<IActionResult> Create([FromQuery] EmployeeRequest request, IFormFile file)
        {
            //if (!ModelState.IsValid)
            //{
            //    //string messages = string.Join("; ", ModelState.Values
            //    //                        .SelectMany(x => x.Errors)
            //    //                        .Select(x => x.ErrorMessage));
            //    string messages = "";
            //    foreach (var item in ModelState.Values)
            //    {
            //        foreach(var error in item.Errors)
            //        {
            //            messages += error.ErrorMessage;
            //        }
            //    }
            //    return BadRequest(messages);
            //}

            return Ok(await Mediator.Send(new CreateNewEmployeeCommand { Image = file, EmployeeInfo = request}));
        }

        [HttpPut("updateImage/{id}")]
        [Authorize(Roles = Constant.RoleValue.Admin)]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateImageEmployeeCommand command)
        {
            command.Id = id;

            return Ok(await Mediator.Send(command));
        }

        [HttpPut("updateInfo/{id}")]
        [Authorize(Roles = Constant.RoleValue.Admin)]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateInfoEmployeeCommand command)
        {
            command.Id = id;

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("removeById")]
        //[Authorize(Roles = Constant.RoleValue.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteEmployeeCommand { Id = id }));
        }

        [HttpGet("GetValueFromJwt")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Test()
        {
            ClaimsPrincipal currentUser = this.User;
            var name = currentUser.FindFirst(ClaimTypes.Name).Value;

            return Ok(new { userName = name });
        }
    }
}

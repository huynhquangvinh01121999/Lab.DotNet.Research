using EsuhaiHRM.Application.DTOs.Account;
using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Domain.Users;
using EsuhaiHRM.Infrastructure.Identity.Contexts;
using EsuhaiHRM.Infrastructure.Identity.Helpers;
using EsuhaiHRM.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EsuhaiHRM.WebApi.Services
{
    public class TrackingUserActivityService : IActionFilter
    {
        private readonly IdentityContext _context;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private UserActivity _userActivity;

        public TrackingUserActivityService(IdentityContext context
                                         , IAuthenticatedUserService authenticatedUserService)
        {
            _context = context;
            _authenticatedUserService = authenticatedUserService;
            _userActivity = new UserActivity();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userIdLogin = _authenticatedUserService.UserId;
            _userActivity.UserId = userIdLogin;
            var userInfo = _context.Users.Where(us => us.Id == userIdLogin).Select(us => us.Email).FirstOrDefault();
            _userActivity.Email = userInfo;
            _userActivity.IpAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            _userActivity.ControllerName = Convert.ToString(context.RouteData.Values["controller"]);
            _userActivity.ControllerAction = Convert.ToString(context.HttpContext.Request.Method);
            _userActivity.ControllerFunction = Convert.ToString(context.RouteData.Values["action"]);

            var getArgument = context.ActionArguments;

            //GET || PUT || DELETE by KEY
            if (_userActivity.ControllerAction != "POST")
            {
                _userActivity.DataKey = GetJsonString(getArgument).ToString();
            }

            switch (_userActivity.ControllerAction)
            {
                case "POST":
                    if(_userActivity.ControllerFunction == "Authenticate" || _userActivity.ControllerFunction == "AuthenticateWithLdap")
                    {
                        var getData = GetJsonString(GetStringByIndex(getArgument, 0));
                        string[] listObj = null;
                        if(getData != "")
                        {
                            listObj = getData.Split(",").FirstOrDefault().Split(":");
                        }
                        if(listObj.Length > 1)
                        {
                            getData = listObj.ElementAt(1).Replace("\"", "");
                        }
                        _userActivity.Email = getData;
                        _userActivity.DataBefore = getData;
                    }
                    else
                    {
                        _userActivity.DataBefore = GetJsonString(getArgument).ToString();
                    }
                    break;
                case "PUT":
                    _userActivity.DataBefore = GetJsonString(getArgument);
                    break;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                _userActivity.IsSuccess = false;
                var result = context.Result as OkObjectResult;
                if (result != null)
                {
                    var IsResult = GetPropertyString(result.Value, "Succeeded");
                    _userActivity.IsSuccess = IsResult == null ? true : (bool)IsResult;
                    if (_userActivity.ControllerAction != "GET")
                    {
                        _userActivity.DataAfter = GetJsonString(result.Value);
                    }
                }
                else
                {
                    var errExcepValid = context.Exception as ValidationException;
                    if (errExcepValid != null)
                    {
                        _userActivity.DataAfter = GetJsonString(errExcepValid.Errors);
                    }
                    var errException = context.Exception;
                    if (errException != null && _userActivity.DataAfter == null)
                    {
                        _userActivity.DataAfter = GetErrorString(errException);
                    }
                    var resultErr = context.Result;
                    if (resultErr != null && _userActivity.DataAfter == null)
                    {
                        _userActivity.DataAfter = resultErr.ToString();
                    }
                }
                _context.UserActivities.Add(_userActivity);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                _userActivity.IsSuccess = false;
                _userActivity.DataAfter = "System Error : " + ex.Message.ToString();
                _context.UserActivities.Add(_userActivity);
                _context.SaveChanges();
            }
        }

        private string GetJsonString(object data)
        {
            if(data == null)
            {
                return "";
            }
            return JsonConvert.SerializeObject(data);
        }

        private object GetStringByIndex(IDictionary<string,object> data, int index)
        {
            if (data == null)
            {
                return "";
            }
            if(index == 0)
            {
                return data.FirstOrDefault().Value;
            }
            return data.ElementAt(index);
        }

        private string GetErrorString(Exception ex)
        {
            var innerErr = "";
            if (ex == null)
            {
                return "";
            }
            if(ex.InnerException != null)
            {
                innerErr = ex.InnerException.Message.ToString();
            }
            return ex.Message.ToString() + " " + innerErr;
        }

        private object GetPropertyString(object data, string propName)
        {
            if(data == null)
            {
                return "";
            }
            var dataType = data.GetType();
            var dataProps = dataType.GetProperty(propName);
            if(dataProps != null)
            {
                return dataProps.GetValue(data);
            }
            return "";
        }
    }
}

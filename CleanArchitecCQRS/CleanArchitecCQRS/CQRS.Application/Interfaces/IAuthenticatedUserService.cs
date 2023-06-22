using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
    }
}

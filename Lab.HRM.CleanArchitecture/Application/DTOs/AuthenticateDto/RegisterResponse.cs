﻿using System;

namespace Application.DTOs.AuthenticateDto
{
    public class RegisterResponse
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Request
{
    public class LoginRequest
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}

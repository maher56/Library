﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services.Token
{
    public interface ITokenService
    {
        Task<string> GenerateJwtToken(Guid userId);
    }
}

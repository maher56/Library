﻿using Library.Domain.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Abstraction
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>> where TResponse : class
    {

    }
}

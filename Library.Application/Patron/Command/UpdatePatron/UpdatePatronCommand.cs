﻿using Library.Application.Abstraction;
using Library.Domain.Attribute;
using Library.Domain.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Patron.Command.UpdatePatron
{
    public class UpdatePatronCommand
    {
        public sealed record Request(
            Guid Id,
            [IsRequired] string Name,
            [IsRequired] string Email,
            [IsRequired] string PhoneNumber,
            AddressEntity Address) : ICommand;
    }
}

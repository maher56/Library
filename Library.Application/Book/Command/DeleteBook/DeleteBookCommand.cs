﻿using Library.Application.Abstraction;
using Library.Domain.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Book.Command.DeleteBook
{
    public class DeleteBookCommand
    {
        public sealed record Request([IsRequired] Guid Id) : ICommand;
    }
}

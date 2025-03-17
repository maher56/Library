using Library.Application.Abstraction;
using Library.Domain.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Application.Patron.Command.DeletePatron
{
    public class DeletePatronCommand
    {
        public sealed record Request([IsRequired] Guid Id) : ICommand;
    }
}

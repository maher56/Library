using Library.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Application.Account.Command
{
    public class LoginCommand
    {
        public sealed record Request(string UserName,string Password) : ICommand<Response>;
        public sealed class Response { 
            public string Token { get; set; }
        };
    }
}

using Library.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Library.Domain.Main
{
    public class AdminEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }

    }
}

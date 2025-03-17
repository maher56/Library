using Library.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Main
{
    public class PatronEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public AddressEntity? Address { get; set; }
        public ICollection<BorrowEnttity> BorrowedBooks { get; set; }
    }
}

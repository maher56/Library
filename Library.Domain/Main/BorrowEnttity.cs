using Library.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Main
{
    public class BorrowEnttity : BaseEntity
    {
        public Guid BookId { get; set; }
        public BookEntity Book { get; set; }
        public Guid PatronId { get; set; }
        public PatronEntity Patron { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }
}

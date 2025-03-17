using Library.Domain.Base;
using Library.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Main
{
    public class BookEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public Genre Genre { get; set; }
        public int CopiesAvailable { get; set; }
        public int TotalCopies { get; set; }
        public ICollection<BorrowEnttity> BorrowedBooks { get; set; }
    }
}

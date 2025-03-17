using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Enum
{
    public enum ErrorKey
    {
        InternalServerError,
        BookNotExist,
        TitleAlreadyTaken,
        ISBNAlreadyTaken,
        ISBNLengthMustBe13,
        TotalCopiesIsLessThanBorrowedCopies,
        EmailNotValid,
        SomeFieldsIsRequired,
        PatronNotExist,
        ThereAreBooksNotReturned,
        ThereIsNoRemainingCopies,
        ThebookAlreadyBorrowedFromThisReader,
        TheBookNotBorrowedByThisPatron,
        UserNameOrPasswordNotExist,
        ThereAreBorrowedCopiesOfThisBook,
    }
}

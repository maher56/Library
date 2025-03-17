using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Enum
{
    public enum ResultStatus
    {
        Success,
        ValidationError,
        Failed,
        NotFound,
        IsExist,
        InternalServerError,
        UnAuthenticated
    }
}

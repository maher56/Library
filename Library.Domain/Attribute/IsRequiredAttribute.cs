using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Attribute
{
    public class IsRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null
                || (value is string && string.IsNullOrEmpty(value.ToString()))
                || (value is Guid guid && guid == Guid.Empty))
                throw new ValidationException($"{validationContext.MemberName} Is Required");
            return ValidationResult.Success;
        }
    }
}

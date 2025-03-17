using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Attribute
{
    public class GreaterThanAttribute : ValidationAttribute
    {
        private int vi;
        private float vf;
        private decimal vd;
        public GreaterThanAttribute(int v)
        {
            vi = v;
        }
        public GreaterThanAttribute(float v)
        {
            vf = v;
        }
        public GreaterThanAttribute(decimal v)
        {
            vd = v;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int val && val <= vi)
                throw new ValidationException($"{validationContext.MemberName} must be greater than {vi}");

            if (value is decimal vald && vald <= vd)
                throw new ValidationException($"{validationContext.MemberName} must be greater than {vd}");

            if (value is float valf && valf <= vf)
                throw new ValidationException($"{validationContext.MemberName} must be greater than {vf}");

            return ValidationResult.Success;
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EmployeeManagement.Utilities
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        private readonly string _allowedDomain;

        public ValidEmailDomainAttribute(string allowedDomain)
        {
            _allowedDomain = allowedDomain;
        }

        public override bool IsValid(object value)
        {
            return value.ToString().Split("@").Last().ToUpper() == _allowedDomain.ToUpper();
        }
    }
}

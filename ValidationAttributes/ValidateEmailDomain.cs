using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPTutorial.ValidationAttributes
{
    public class ValidateEmailDomain : ValidationAttribute
    {
        private readonly string allowedDomain;

        public ValidateEmailDomain(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }
        public override bool IsValid(object value)
        {
            string[] words = value.ToString().Split('@');
            if (words.Length != 2)
                return false;
            return words[1].ToUpper() == allowedDomain.ToUpper();
        }


    }
}

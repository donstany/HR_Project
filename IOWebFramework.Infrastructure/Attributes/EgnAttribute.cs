using IOWebFramework.Infrastructure.Helper_Classes.EGN_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IOWebFramework.Infrastructure.Attributes
{
   public class EgnAttribute : ValidationAttribute
    {
        public EgnAttribute()
        {



        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }
            BasicEGNValidation egnValidation = new BasicEGNValidation(value.ToString());
            if (!egnValidation.Validate())
            {
                return new ValidationResult(egnValidation.ErrorMessage);
            }
            return null;
        }
    }
}

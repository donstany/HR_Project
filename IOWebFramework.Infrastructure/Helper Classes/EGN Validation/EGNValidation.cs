using System;
using System.Collections.Generic;
using System.Text;

namespace IOWebFramework.Infrastructure.Helper_Classes.EGN_Validation
{
    public abstract class EGNValidation
    {
        public string ErrorMessage { get; set; }
        public bool IsValid { get; set; }
        public DateTime EGNDate { get; set; }
        public abstract string GetMessage();
        public abstract bool Validate();
    }
}

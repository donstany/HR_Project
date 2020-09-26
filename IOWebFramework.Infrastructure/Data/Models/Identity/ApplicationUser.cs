using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IOWebFramework.Infrastructure.Data.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public bool MustChangePassword { get; set; }

        // SAMAccountName 
        public string UserNameFromActiveDirectory { get; set; }
    }
}

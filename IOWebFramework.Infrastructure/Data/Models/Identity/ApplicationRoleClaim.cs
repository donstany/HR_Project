using Microsoft.AspNetCore.Identity;

namespace IOWebFramework.Infrastructure.Data.Models.Identity
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public virtual ApplicationRole Role { get; set; }
    }
}

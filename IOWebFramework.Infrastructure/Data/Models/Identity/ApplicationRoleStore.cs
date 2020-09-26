using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models.Identity
{
    [Table("roles")]
    public class ApplicationRoleStore : RoleStore<ApplicationRole, ApplicationDbContext, string, ApplicationUserRole, ApplicationRoleClaim>
    {
        public ApplicationRoleStore(ApplicationDbContext context) : base(context)
        {
        }
    }
}

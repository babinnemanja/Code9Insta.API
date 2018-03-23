using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Code9Insta.API.Infrastructure.Identity
{
    public class CodeNineDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {

      public CodeNineDbContext(DbContextOptions<CodeNineDbContext> options)
          : base(options)
      {
      }

      protected override void OnModelCreating(ModelBuilder builder)
      {
          base.OnModelCreating(builder);
          // Customize the ASP.NET Identity model and override the defaults if needed.
          // For example, you can rename the ASP.NET Identity table names and more.
          // Add your customizations after calling base.OnModelCreating(builder);
      }
        
    }
}

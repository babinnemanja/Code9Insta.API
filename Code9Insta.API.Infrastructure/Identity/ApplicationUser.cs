using System;
using Microsoft.AspNetCore.Identity;

namespace Code9Insta.API.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public byte[] Salt { get; set; }
    }

    public class ApplicationRole : IdentityRole<Guid>
    {
    }
}
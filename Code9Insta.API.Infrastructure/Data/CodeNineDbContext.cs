using System;
using Code9Insta.API.Infrastructure.Entities;
using Code9Insta.API.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Code9Insta.API.Infrastructure.Data
{
    public class CodeNineDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {

        public CodeNineDbContext(DbContextOptions<CodeNineDbContext> options)
          : base(options)
      {
      }

      public DbSet<Post> Posts { get; set; }
      public DbSet<Comment> Comments { get; set; }
      public DbSet<Image> Images { get; set; }
      public DbSet<Profile> Profiles { get; set; }
      public new DbSet<ApplicationUser> Users { get; set; }
      public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
      {
          base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<PostTag>()
           .HasKey(t => new { t.PostId, t.TagId });

            builder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId);

            builder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId);
        }


    }
        
    
}

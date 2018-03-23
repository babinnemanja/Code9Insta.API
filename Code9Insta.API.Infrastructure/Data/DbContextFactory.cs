using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Code9Insta.API.Infrastructure.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<CodeNineDbContext>
    {
        public CodeNineDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CodeNineDbContext>();


           // builder.UseSqlServer();
            return new CodeNineDbContext(builder.Options);
        }
    }
}

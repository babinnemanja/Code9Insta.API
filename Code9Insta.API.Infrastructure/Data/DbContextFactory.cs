using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Code9Insta.API.Infrastructure.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<CodeNineDbContext>
    {
        public CodeNineDbContext CreateDbContext(string[] args)
        {
            
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=CodeNineDB;Trusted_Connection=True;";
            var builder = new DbContextOptionsBuilder<CodeNineDbContext>();
            builder.UseSqlServer(connectionString);

            return new CodeNineDbContext(builder.Options);
        }
    }
}

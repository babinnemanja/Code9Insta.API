using System.Text;
using Code9Insta.API.Infrastructure.Data;
using Code9Insta.API.Infrastructure.Identity;
using Code9Insta.API.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Code9Insta.API.Infrastructure.Interfaces;
using Code9Insta.API.Core.DTO;
using Code9Insta.API.Infrastructure.Entities;

namespace Code9Insta.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // register the DbContext on the container, getting the connection string from
            // appSettings (note: use this during development; in a production environment,
            // it's better to store the connection string in an environment variable)
            var connectionString = Configuration["connectionStrings:CodeNineDBConnectionString"];
            services.AddDbContext<CodeNineDbContext>(o => o.UseSqlServer(connectionString));

            // ===== Add Identity ========
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<CodeNineDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "code9.com",
                        ValidAudience = "code9.com",
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
                    };
                });

            // register the repository
            services.AddScoped<IDataRepository, DataRepository>();
            services.AddScoped<IValidate, ValidateRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();

            //AutoMapper configuration
            AutoMapper.Mapper.Initialize(conf => {
                conf.CreateMap<ProfileDto, Profile>();
                conf.CreateMap<AccountDto, ApplicationUser>();
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CodeNineDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();

            dbContext.Database.EnsureCreated();
        }
    }
}

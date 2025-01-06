using GabionCalculator.BAL.MappingProfiles;
using GabionCalculator.BAL.Services;
using GabionCalculator.BAL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using GabionCalculator.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using GabionCalculator.BAL.Services.JwtFeatures;
using GabionCalculator.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Google.Protobuf.WellKnownTypes;

namespace GabionCalculator.BAL
{
    public static class ApplicationDependencyInjection
    {
      public static IServiceCollection AddApplication(this IServiceCollection services)
      {
          services.AddServices();
          services.RegisterAutoMapper();
          services.Configure<ApiBehaviorOptions>(options =>
          {
              options.SuppressModelStateInvalidFilter = true;
          });
          services.AddControllers();
          services.AddEndpointsApiExplorer();
          services.AddScoped<JwtHandler>();
          services.AddCors();
          return services;
      }

      private static void AddCors(this IServiceCollection services)
      {
        string MyAllowSpecificOrigins = "_MyAllowSpecificOrigins";
        //Enable Cross-Origin Requests (CORS)
        services.AddCors(options =>
        {
         options.AddPolicy(name: MyAllowSpecificOrigins,
                           policy => policy.WithOrigins("http://localhost:5002", "http://192.168.0.232", "http://calculator.ztta.kg")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials());
        });
      }

      private static void AddServices(this IServiceCollection services)
      {
          services.AddScoped<IGabionService, GabionService>();
          services.AddScoped<IMaterialService, MaterialService>();
          services.AddScoped<IUserService, UserService>();
          services.AddScoped<ICostWorkService, CostWorkService>();
        }
      
      private static void RegisterAutoMapper(this IServiceCollection services)
      {
          services.AddAutoMapper(typeof(IMappingProfilesMarker));
      }

        public static IdentityOptions MakeOptionsIdentity(this IdentityOptions options)
        {
            options.Password.RequiredLength = 12;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+ " +
                                                     "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            return options;
        }

        public static void AddDbContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseMySql(connection, ServerVersion.AutoDetect(connection));
            });
        }

        public static void AddAuthenticationJWT(this IServiceCollection services, IConfigurationSection jwtSettings)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["securityKey"]))
                };
            });
        }

        public static async Task InitializeRoleAsync(this IServiceProvider services)
        {                  
           var userManager = services.GetRequiredService<UserManager<User>>();
           var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
           string adminEmail = "admin@adminov.ru";
           string password = "WH90LeZ5uMe9";
           if (await roleManager.FindByNameAsync("admin") == null)
               await roleManager.CreateAsync(new IdentityRole("admin"));
           if (await roleManager.FindByNameAsync("employee") == null)
               await roleManager.CreateAsync(new IdentityRole("employee"));
           if (await userManager.FindByNameAsync(adminEmail) == null)
           {
               User admin = new() { Email = adminEmail, UserName = adminEmail };
               IdentityResult result = await userManager.CreateAsync(admin, password);
               if (result.Succeeded)
               {
                   await userManager.AddToRoleAsync(admin, "admin");
               }
           }        
        }

#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
        public static async Task<User> GetCurrentUser(UserManager<User> UserManager, string nameUser) => await UserManager.FindByNameAsync(nameUser);
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
    }
}

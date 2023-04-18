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
          return services;
      }
      
      private static void AddServices(this IServiceCollection services)
      {
          services.AddScoped<IGabionService, GabionService>();
          services.AddScoped<IMaterialService, MaterialService>();
          services.AddScoped<IUserService, UserService>();
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

        public static async Task InitializeRoleAsync(this IServiceProvider services)
      {                  
           var userManager = services.GetRequiredService<UserManager<User>>();
           var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
           string adminEmail = "admin@adminov.ru";
           string password = "WH90LeZ5uMe9";
           if (await roleManager.FindByNameAsync("admin") == null)
           {
               await roleManager.CreateAsync(new IdentityRole("admin"));
           }
           if (await roleManager.FindByNameAsync("employee") == null)
           {
               await roleManager.CreateAsync(new IdentityRole("employee"));
           }
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
    }
}

using GabionCalculator.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Utils
{
    public static class CurrentUser
    {
        public static async Task<User> Get(UserManager<User> UserManager, string nameUser) => await UserManager.FindByNameAsync(nameUser);
    }
}

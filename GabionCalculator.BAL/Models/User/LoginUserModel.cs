using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GabionCalculator.BAL.Models.User
{
    public class LoginUserModel
    {
        public string EmailLogin { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}

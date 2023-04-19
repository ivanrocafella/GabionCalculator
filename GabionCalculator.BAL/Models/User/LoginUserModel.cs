using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GabionCalculator.BAL.Models.User
{
    public class LoginUserModel
    {
        [JsonPropertyName("EmailLogin")]
        [Required(ErrorMessage = "Введите email или логин")]
        public string EmailLogin { get; set; }
        [JsonPropertyName("Password")]
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
        [JsonPropertyName("RememberMe")]
        public bool RememberMe { get; set; }
        [JsonPropertyName("ReturnUrl")]
        public string? ReturnUrl { get; set; }
    }
}

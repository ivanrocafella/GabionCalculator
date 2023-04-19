using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace GabionCalculator.BAL.Models.User
{
    public class RegisterUserModel
    {
        [Required(ErrorMessage = "Укажите логин")]
       // [Remote(action: "CheckExistAccountByUserName", controller: "User", ErrorMessage = "Такой пользователь уже есть")]
        [JsonPropertyName("UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Укажите email")]
      //  [Remote(action: "CheckExistAccountByEmail", controller: "User", ErrorMessage = "Такой пользователь уже есть")]
        [JsonPropertyName("Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [JsonPropertyName("Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [JsonPropertyName("PasswordConfirm")]
        public string PasswordConfirm { get; set; }
    }
}

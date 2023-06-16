using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Models.User
{
    public class ChangePasswordUserModel
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        [JsonPropertyName("UserName")]
        public string UserName { get; set; }
        [JsonPropertyName("OldPassword")]
        public string OldPassword { get; set; }
        [JsonPropertyName("NewPassword")]
        public string NewPassword { get; set; }
        [JsonPropertyName("NewPasswordConfirm")]
        public string NewPasswordConfirm { get; set; }
    }
}

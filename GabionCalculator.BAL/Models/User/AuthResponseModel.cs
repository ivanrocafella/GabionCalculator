using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Models.User
{
    public class AuthResponseModel
    {
        [JsonPropertyName("IsAuthSuccessful")]
        public bool IsAuthSuccessful { get; set; }
        [JsonPropertyName("ErrorMessage")]
        public string? ErrorMessage { get; set; }
        [JsonPropertyName("Token")]
        public string? Token { get; set; }
    }
}

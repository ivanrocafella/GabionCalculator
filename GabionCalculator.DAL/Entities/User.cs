using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GabionCalculator.DAL.Entities
{
    public class User : IdentityUser
    {
        [JsonPropertyName("Id")]
        public override string Id { get; set; } = default!;
        [ProtectedPersonalData]
        [JsonPropertyName("UserName")]
        public override string? UserName { get; set; }
        [JsonPropertyName("NormalizedUserName")]
        public override string? NormalizedUserName { get; set; }
        [ProtectedPersonalData]
        [JsonPropertyName("Email")]
        public override string? Email { get; set; }
        [JsonPropertyName("NormalizedEmail")]
        public override string? NormalizedEmail { get; set; }
        [PersonalData]
        [JsonPropertyName("EmailConfirmed")]
        public override bool EmailConfirmed { get; set; }
        [JsonPropertyName("PasswordHash")]
        public override string? PasswordHash { get; set; }
        [JsonPropertyName("SecurityStamp")]
        public override string? SecurityStamp { get; set; }
        [JsonPropertyName("ConcurrencyStamp")]
        public override string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
        [ProtectedPersonalData]
        [JsonPropertyName("PhoneNumber")]
        public override string? PhoneNumber { get; set; }
        [PersonalData]
        [JsonPropertyName("PhoneNumberConfirmed")]
        public override bool PhoneNumberConfirmed { get; set; }
        [PersonalData]
        [JsonPropertyName("TwoFactorEnabled")]
        public override bool TwoFactorEnabled { get; set; }
        [JsonPropertyName("LockoutEnd")]
        public override DateTimeOffset? LockoutEnd { get; set; }
        [JsonPropertyName("LockoutEnabled")]
        public override bool LockoutEnabled { get; set; }
        [JsonPropertyName("AccessFailedCount")]
        public override int AccessFailedCount { get; set; }
        [JsonPropertyName("Gabions")]
        public ICollection<Gabion>? Gabions { get; set; }

        public User()
        {
            Gabions = new HashSet<Gabion>();
        }
    }
}

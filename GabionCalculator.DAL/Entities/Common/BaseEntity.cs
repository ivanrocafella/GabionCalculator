using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GabionCalculator.DAL.Entities.Common
{
    public class BaseEntity
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
    }
}

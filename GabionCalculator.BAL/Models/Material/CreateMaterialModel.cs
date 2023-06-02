using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace GabionCalculator.BAL.Models.Material
{
    public class CreateMaterialModel
    {
        [Required]
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [Range(0.5, double.PositiveInfinity)]
        [JsonPropertyName("Size")]
        public float Size { get; set; }
        [Range(0, float.PositiveInfinity)]
        [JsonPropertyName("PricePerKg")]
        public double PricePerKg { get; set; }
        [JsonPropertyName("MaterialKindId")]
        [Range(0,int.MaxValue)]
        public int MaterialKindId { get; set; }
        [JsonPropertyName("KindsMaterial")]
        public List<string>? KindsMaterial { get; set; }
        [JsonPropertyName("Names")]
        public string[]? Names { get; set; }
    }
}

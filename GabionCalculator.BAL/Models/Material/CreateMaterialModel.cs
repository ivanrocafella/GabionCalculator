﻿using System;
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
        [Range(1, double.PositiveInfinity)]
        [JsonPropertyName("Size")]
        public double Size { get; set; }
        [Range(0, double.PositiveInfinity)]
        [JsonPropertyName("PricePerKg")]
        public double PricePerKg { get; set; }
        [JsonPropertyName("MaterialKindId")]
        [Range(1,double.PositiveInfinity)]
        public int MaterialKindId { get; set; }
    }
}

using GabionCalculator.DAL.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Models.Gabion
{
    public class CreateGabionModel
    {
        [JsonPropertyName("Height")]
        [ValidateNever]
        public int Height { get; set; }
        [JsonPropertyName("Length")]
        [ValidateNever]
        public int Length { get; set; }
        [JsonPropertyName("Width")]
        [ValidateNever]
        public int Width { get; set; }
        [JsonPropertyName("CellHeight")]
        [ValidateNever]
        public int CellHeight { get; set; }
        [JsonPropertyName("CellWidth")]
        [ValidateNever]
        public int CellWidth { get; set; }
        [JsonPropertyName("MaterialDiameter")]
        [ValidateNever]
        public float MaterialDiameter { get; set; }
        [JsonPropertyName("Quantity")]
        [ValidateNever]
        public int Quantity { get; set; }
        [JsonPropertyName("MaterialId")]
        [ValidateNever]
        public int MaterialId { get; set; }
        [JsonPropertyName("UserName")]
        [ValidateNever]
        public string? UserName { get; set; }
        [JsonPropertyName("Materials")]
        [ValidateNever]
        public IEnumerable<GabionCalculator.DAL.Entities.Material>? Materials { get; set; }
    }
}

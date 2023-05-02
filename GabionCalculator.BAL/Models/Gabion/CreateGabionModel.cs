using GabionCalculator.DAL.Entities;
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
        public int Height { get; set; }
        [JsonPropertyName("Length")]
        public int Length { get; set; }
        [JsonPropertyName("Width")]
        public int Width { get; set; }
        [JsonPropertyName("CellHeight")]
        public int CellHeight { get; set; }
        [JsonPropertyName("CellWidth")]
        public int CellWidth { get; set; }
        [JsonPropertyName("MaterialDiameter")]
        public float MaterialDiameter { get; set; }
        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("MaterialId")]
        public int MaterialId { get; set; }
        [JsonPropertyName("UserName")]
        public string? UserName { get; set; }
        [JsonPropertyName("Materials")]
        public IEnumerable<GabionCalculator.DAL.Entities.Material>? Materials { get; set; }
    }
}

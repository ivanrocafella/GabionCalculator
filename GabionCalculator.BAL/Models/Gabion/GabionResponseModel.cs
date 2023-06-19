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
    public class GabionResponseModel : BaseResponseModel
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
        [JsonPropertyName("OutletVert")]
        public int OutletVert { get; set; }
        [JsonPropertyName("OutletHoriz")]
        public int OutletHoriz { get; set; }
        [JsonPropertyName("BendRadius")]
        public float BendRadius { get; set; }
        [JsonPropertyName("Kfactor")]
        public double Kfactor { get; set; } // K-factor 
        [JsonPropertyName("MaterialDiameter")]
        public float MaterialDiameter { get; set; }
        [JsonPropertyName("BarBilletVert")]
        public int BarBilletVert { get; set; }
        [JsonPropertyName("BarBilletHoriz")]
        public int BarBilletHoriz { get; set; }
        [JsonPropertyName("CardWidth")]
        public int CardWidth { get; set; } // Ширина карты 
        [JsonPropertyName("CardHeight")]
        public int CardHeight { get; set; } // Высота карты
        [JsonPropertyName("Weight")]
        public double Weight { get; set; } // кг
        [JsonPropertyName("BatchWeight")]
        public double BatchWeight { get; set; } // кг
        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("BarsQtyVert")]
        public int BarsQtyVert { get; set; }
        [JsonPropertyName("BarsQtyHoriz")]
        public int BarsQtyHoriz { get; set; }
        [JsonPropertyName("MaterialTotalLength")]
        public double MaterialTotalLength { get; set; } // м
        [JsonPropertyName("Svg")]
        public string? Svg { get; set; }
        [JsonPropertyName("MaterialJson")]
        public string? MaterialJson { get; set; } // json
        [JsonPropertyName("UserJson")]
        public string? UserJson { get; set; } // json
        [JsonPropertyName("Sebes")]
        public double Sebes { get; set; }
        [JsonPropertyName("BatchSebes")]
        public double BatchSebes { get; set; }
        [JsonPropertyName("Price")]
        public double Price { get; set; }
        [JsonPropertyName("BatchPrice")]
        public double BatchPrice { get; set; }
        [JsonPropertyName("PriceMaterial")]
        public double PriceMaterial { get { return PriceMaterialBatch / Quantity; } }
        [JsonPropertyName("PriceMaterialBatch")]
        public double PriceMaterialBatch { get; set; }
        [JsonPropertyName("DateStart")]
        public DateTime DateStart { get; set; }
        [JsonPropertyName("DateUpdate")]
        public DateTime DateUpdate { get; set; }
        [JsonPropertyName("Material")]
        public DAL.Entities.Material? Material { get; set; }
        [JsonPropertyName("MaterialId")]
        public int? MaterialId { get; set; }
        [JsonPropertyName("User")]
        public DAL.Entities.User? User { get; set; }
        [JsonPropertyName("UserId")]
        public string? UserId { get; set; }
    }
}

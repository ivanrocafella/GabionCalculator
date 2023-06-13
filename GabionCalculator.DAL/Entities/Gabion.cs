using GabionCalculator.DAL.Entities.Common;
using GabionCalculator.DAL.Entities.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GabionCalculator.DAL.Entities
{
    public class Gabion : BaseEntity, IDateFixEntity
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
        public int OutletVert { get; } = 30;
        [JsonPropertyName("OutletHoriz")]
        public int OutletHoriz { get; } = 15;
        [JsonPropertyName("BendRadius")]
        public float BendRadius { get; } = 17.5f;
        [JsonPropertyName("Kfactor")]
        public double Kfactor
        {
            get
            {
                    return 1 / (Math.Log(1 + (double)MaterialDiameter / BendRadius)) - BendRadius / MaterialDiameter;
            }
        } // K-factor 
        [JsonPropertyName("MaterialDiameter")]
        public float MaterialDiameter { get; set; }
        [JsonPropertyName("BarBilletVert")]
        public int BarBilletVert
        {
            get
            {
                    return Height;
            }
        }
        [JsonPropertyName("BarBilletHoriz")]
        public int BarBilletHoriz
        {
            get
            {
                    return (int)Math.Ceiling(Length - 2 * (MaterialDiameter + BendRadius) +
                                      Width - 2 * (MaterialDiameter + BendRadius) +
                                      3 * (Math.PI * (BendRadius + Kfactor * MaterialDiameter) * 1 / 2) +
                                      Length - MaterialDiameter - BendRadius +
                                      Width - MaterialDiameter - BendRadius);
            }
        }
        [NotMapped]
        [JsonPropertyName("CardWidthInterm")]
        private int CardWidthInterm { get; set; }
        [NotMapped]
        [JsonPropertyName("CardHeightInterm")]
        private int CardHeightInterm { get; set; }
        [JsonPropertyName("CardWidth")]
        public int CardWidth // Ширина карты 
        {
            get
            {
                CardWidthInterm = (BarBilletHoriz / 100) * 100;
               
                if (BarBilletHoriz - CardWidthInterm >= 25 && BarBilletHoriz - CardWidthInterm < 50)
                    CardWidthInterm += 50;
                else if (BarBilletHoriz - CardWidthInterm >= 50 && BarBilletHoriz - CardWidthInterm < 100)
                    CardWidthInterm += 100;
               
                if (CardWidthInterm % CellWidth == 0)
                    return CardWidthInterm + 2 * OutletHoriz;
                else
                {
                    for (int i = 50; i < int.MaxValue; i += 50)
                    {
                        if ((CardWidthInterm + i) % CellWidth == 0)
                        {
                            CardWidthInterm += i;
                            break;
                        }
                    };
                    return CardWidthInterm + 2 * OutletHoriz;
                }
            }
        }
        [JsonPropertyName("CardHeight")]
        public int CardHeight // Высота карты
        {
            get
            {
                    return Height + 2 * OutletVert;
            }
        }
        [JsonPropertyName("Weight")]
        public double Weight
        {
            get
            {
                    return 7850 * (double)((CardHeight - 2 * OutletVert) * BarsQtyVert + (CardWidth - 2 * OutletHoriz) * BarsQtyHoriz)
                    * (Math.PI * Math.Pow(MaterialDiameter, 2) / 4) / Math.Pow(10, 9);
            }
        } // кг
        [JsonPropertyName("WeightCard")]
        public double WeightCard
        {
            get
            {
                return 7850 * (double)(CardHeight * BarsQtyVert + CardWidth * BarsQtyHoriz)
                * (Math.PI * Math.Pow(MaterialDiameter, 2) / 4) / Math.Pow(10, 9);
            }
        } // кг
        [JsonPropertyName("BatchWeight")]
        public double BatchWeight
        {
            get
            {
                return Weight * Quantity;
            }
        } // кг
        [JsonPropertyName("BatchWeightCard")]
        public double BatchWeightCard
        {
            get
            {
                return WeightCard * Quantity;
            }
        } // кг
        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
        [NotMapped]
        [JsonPropertyName("BarsQtyVert")]
        public int BarsQtyVert { get { return (CardWidth - 2 * OutletHoriz) / CellWidth + 1; } }
        [NotMapped]
        [JsonPropertyName("BarsQtyHoriz")]
        public int BarsQtyHoriz { get { return (CardHeight - 2 * OutletVert) / CellHeight + 1; } }
        [JsonPropertyName("MaterialTotalLength")]
        public double MaterialTotalLength
        {
            get
            {
                    return (double)(CardHeight * BarsQtyVert + CardWidth * BarsQtyHoriz) / 1000;
            }
        } // м
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
        [JsonPropertyName("DateStart")]
        public DateTime DateStart { get; set; }
        [JsonPropertyName("DateUpdate")]
        public DateTime DateUpdate { get; set; }

        [JsonPropertyName("MaterialId")]
        public int? MaterialId { get; set; }
        [JsonPropertyName("Material")]
        public Material? Material { get; set; }
        [JsonPropertyName("UserId")]
        public string? UserId { get; set; }
        [JsonPropertyName("User")]
        public User? User { get; set; }
    }
}

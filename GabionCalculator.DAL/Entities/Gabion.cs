using GabionCalculator.DAL.Entities.Common;
using GabionCalculator.DAL.Entities.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.DAL.Entities
{
    public class Gabion : BaseEntity, IDateFixEntity
    {
        public int Height { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int CellHeight { get; set; }
        public int CellWidth { get; set; }
        public int OutletVert { get; } = 30;
        public int OutletHoriz { get; } = 15;
        public float BendRadius { get; } = 17.5f;
        public double Kfactor
        {
            get
            {
                if (Kfactor != 0)
                    return Kfactor;
                else
                    return 1 / (Math.Log(1 + (double)MaterialDiameter / BendRadius)) - BendRadius / MaterialDiameter;
            }
        } // K-factor 
        public float MaterialDiameter { get; set; }
        public int BarBilletVert
        {
            get
            {
                if (BarBilletVert != 0)
                    return BarBilletVert;
                else
                    return Height;
            }
        }
        public int BarBilletHoriz
        {
            get
            {
                if (BarBilletHoriz != 0)
                    return BarBilletHoriz;
                else
                    return (int)Math.Ceiling(Length - 2 * (MaterialDiameter + BendRadius) +
                                      Width - 2 * (MaterialDiameter + BendRadius) +
                                      3 * (Math.PI * (BendRadius + Kfactor * MaterialDiameter) * 1 / 2) +
                                      Length - MaterialDiameter - BendRadius +
                                      Width - MaterialDiameter - BendRadius);
            }
        }
        [NotMapped]
        public int CardWidthInterm { get; set; }
        [NotMapped]
        public int CardHeightInterm { get; set; }
        public int CardWidth // Ширина карты 
        {
            get
            {
                if (CardWidth != 0)
                    return CardWidth;
                else
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
        }
        public int CardHeight // Высота карты
        {
            get
            {
                if (CardHeight != 0)
                    return CardHeight;
                else
                    return Height + 2 * OutletVert;
            }
        }
        public double Weight
        {
            get
            {
                if (Weight != 0)
                    return Weight;
                else
                    return 7850 * (double)((CardHeight - 2 * OutletVert) * BarsQtyVert + (CardWidth - 2 * OutletHoriz) * BarsQtyHoriz)
                    * (Math.PI * Math.Pow(MaterialDiameter, 2) / 4) / Math.Pow(10, 9);
            }
        } // кг
        public int Quantity { get; set; }
        [NotMapped]
        public int BarsQtyVert { get { return (CardWidth - 2 * OutletHoriz) / CellWidth + 1; } }
        [NotMapped]
        public int BarsQtyHoriz { get { return (CardHeight - 2 * OutletVert) / CellHeight + 1; } }
        public double MaterialTotalLength
        {
            get
            {
                if (MaterialTotalLength != 0)
                    return MaterialTotalLength;
                else
                    return (double)(CardHeight * BarsQtyVert + CardWidth * BarsQtyHoriz) / 1000;
            }
        } // м
        public string? Svg { get; set; }
        public string? MaterialJson { get; set; } // json
        public string? UserJson { get; set; } // json
        public double Sebes { get; set; }
        public double BatchSebes { get; set; }
        public double Price { get; set; }
        public double BatchPrice { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateUpdate { get; set; }

        public int? MaterialId { get; set; }
        public Material? Material { get; set; }
        public int? UserlId { get; set; }
        public User? User { get; set; }
    }
}

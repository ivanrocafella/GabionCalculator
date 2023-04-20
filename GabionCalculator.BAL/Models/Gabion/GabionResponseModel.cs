using GabionCalculator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Models.Gabion
{
    public class GabionResponseModel : BaseResponseModel
    {
        public int Height { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int CellHeight { get; set; }
        public int CellWidth { get; set; }
        public int OutletVert { get; set; } 
        public int OutletHoriz { get; set; } 
        public float BendRadius { get; set; } 
        public double Kfactor { get; set; } // K-factor 
        public float MaterialDiameter { get; set; }
        public int BarBilletVert { get; set; }
        public int BarBilletHoriz { get; set; }
        public int CardWidth { get; set; } // Ширина карты 

        public int CardHeight { get; set; } // Высота карты
        public double Weight { get; set; } // кг
        public int Quantity { get; set; }
        public int BarsQtyVert { get; set; }
        public int BarsQtyHoriz { get; set; }
        public double MaterialTotalLength { get; set; } // м
        public string? Svg { get; set; }
        public string? MaterialJson { get; set; } // json
        public string? UserJson { get; set; } // json
        public double Sebes { get; set; }
        public double BatchSebes { get; set; }
        public double Price { get; set; }
        public double BatchPrice { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}

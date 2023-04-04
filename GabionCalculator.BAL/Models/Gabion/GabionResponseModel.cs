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
        public int OutletVert { get; } 
        public int OutletHoriz { get; } 
        public float BendRadius { get; } 
        public double Kfactor { get; } // K-factor 
        public float MaterialDiameter { get; set; }
        public int BarBilletVert { get; }
        public int BarBilletHoriz { get; }
        public int CardWidth { get; } // Ширина карты 

        public int CardHeight { get; } // Высота карты
        public double Weight { get; } // кг
        public int Quantity { get; set; }
        public int BarsQtyVert { get; }
        public int BarsQtyHoriz { get; }
        public double MaterialTotalLength { get; } // м
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

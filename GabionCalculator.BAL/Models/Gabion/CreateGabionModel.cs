using GabionCalculator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Models.Gabion
{
    public class CreateGabionModel
    {
        public int Height { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int CellHeight { get; set; }
        public int CellWidth { get; set; }
        public float MaterialDiameter { get; set; }
        public int Quantity { get; set; }
        public int MaterialId { get; set; }
        public string? UserName { get; set; }
    }
}

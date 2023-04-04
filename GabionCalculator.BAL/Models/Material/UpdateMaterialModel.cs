﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Models.Material
{
    public class UpdateMaterialModel : BaseResponseModel
    {
        public string Name { get; set; }
        public double Size { get; set; }
        public double PricePerKg { get; set; }
        public int MaterialKindId { get; set; }
    }
}

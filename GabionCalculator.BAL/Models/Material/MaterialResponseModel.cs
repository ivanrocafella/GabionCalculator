using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GabionCalculator.DAL.Entities.Enums;

namespace GabionCalculator.BAL.Models.Material
{
    public class MaterialResponseModel : BaseResponseModel
    {
        public string? Name { get; set; }
        public string? FullName { get { return $"{Name} ⌀{Size} {MaterialKind}"; } }
        public double Size { get; set; }
        public double PricePerKg { get; set; }
        public virtual int MaterialKindId
        {
            get => (int)MaterialKind;
            set => MaterialKind = (MaterialKind)value;
        }
        [EnumDataType(typeof(MaterialKind))]
        public MaterialKind MaterialKind { get; set; }
    }
}

using GabionCalculator.DAL.Entities.Common;
using GabionCalculator.DAL.Entities.Common.Interfaces;
using GabionCalculator.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.DAL.Entities
{
    public class Material : BaseEntity, IDateFixEntity
    {
        public string? Name { get; set; }
        [NotMapped]
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
        public DateTime DateStart { get; set; }
        public DateTime DateUpdate { get; set; }

        public ICollection<Gabion>? Gabions { get; set; }

        public Material()
        {
            Gabions = new HashSet<Gabion>();
        }
    }
}

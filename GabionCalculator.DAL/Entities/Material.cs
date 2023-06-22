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
using System.Text.Json.Serialization;

namespace GabionCalculator.DAL.Entities
{
    public class Material : BaseEntity, IDateFixEntity
    {
        [JsonPropertyName("Name")]
        public string? Name { get; set; }
        [NotMapped]
        [JsonPropertyName("FullName")]
        public string? FullName { get { return $"{Name} Ø{Size} {MaterialKind}"; } }
        [JsonPropertyName("Size")]
        public float Size { get; set; }
        [JsonPropertyName("PricePerKg")]
        public double PricePerKg { get; set; }
        [JsonPropertyName("MaterialKindId")]
        public virtual int MaterialKindId
        {
            get => (int)MaterialKind;
            set => MaterialKind = (MaterialKind)value;
        }
        [EnumDataType(typeof(MaterialKind))]
        [JsonPropertyName("MaterialKind")]
        public MaterialKind MaterialKind { get; set; }
        [JsonPropertyName("DateStart")]
        public DateTime DateStart { get; set; }
        [JsonPropertyName("DateUpdate")]
        public DateTime DateUpdate { get; set; }
        [JsonPropertyName("Gabions")]
        public ICollection<Gabion>? Gabions { get; set; }

        public Material()
        {
            Gabions = new HashSet<Gabion>();
        }
    }
}

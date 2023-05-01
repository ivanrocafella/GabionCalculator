using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GabionCalculator.DAL.Entities.Enums;
using System.Text.Json.Serialization;

namespace GabionCalculator.BAL.Models.Material
{
    public class MaterialResponseModel : BaseResponseModel
    {
        [JsonPropertyName("Name")]
        public string? Name { get; set; }
        [JsonPropertyName("FullName")]
        public string? FullName { get { return $"{Name} ⌀{Size} {MaterialKind}"; } }
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
        [JsonPropertyName("MaterialKind")]
        [EnumDataType(typeof(MaterialKind))]
        public MaterialKind MaterialKind { get; set; }
        [JsonPropertyName("DateStart")]
        public DateTime DateStart { get; set; }
        [JsonPropertyName("DateUpdate")]
        public DateTime DateUpdate { get; set; }
    }
}

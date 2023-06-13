using GabionCalculator.DAL.Entities.Common;
using GabionCalculator.DAL.Entities.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GabionCalculator.DAL.Entities
{
    public class CostWork : BaseEntity, IDateFixEntity
    {
        [JsonPropertyName("ExchangeDollar")]
        public double ExchangeDollar { get; set; } // unity of measure = som / $
        [JsonPropertyName("TimeWeldingOneCrossBar")]
        public double TimeWeldingOneCrossBar { get; set; } // unity of measure = h
        [JsonPropertyName("TimeSettingEguip")]
        public double TimeSettingEguip { get; set; } // unity of measure = h
        [JsonPropertyName("PNR")]
        public double PNR { get; set; } // unity of measure = $
        [JsonPropertyName("Margin")]
        public double Margin { get; set; } // unity of measure = _
        public DateTime DateStart { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}

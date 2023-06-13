using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Models.CostWork
{
    public class CreateCostWorkModel
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
        public double Margin { get; set; } // unity of measure = %
    }
}

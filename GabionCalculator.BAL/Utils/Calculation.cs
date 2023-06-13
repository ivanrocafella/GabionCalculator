using GabionCalculator.BAL.Services;
using GabionCalculator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Utils
{
    public static class Calculation
    {
        public static void Calculate(Gabion gabion, CostWork costWork)
        {
           double timeProdiction = costWork.TimeWeldingOneCrossBar * gabion.BarsQtyHoriz * gabion.Quantity;  // unity of measure = h
           gabion.BatchSebes = gabion.BatchWeightCard * gabion.Material.PricePerKg
                + (costWork.TimeSettingEguip + timeProdiction) * costWork.PNR / costWork.ExchangeDollar; // unity of measure = som
           gabion.Sebes = gabion.BatchSebes / gabion.Quantity; // unity of measure = som
           gabion.BatchPrice = gabion.BatchSebes * costWork.Margin; // unity of measure = som 
           gabion.Price = gabion.Sebes * costWork.Margin; // unity of measure = som 
        }
    }
}

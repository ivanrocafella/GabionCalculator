using GabionCalculator.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.DAL.Data
{
    public class CostWorkSeedConfiguration : IEntityTypeConfiguration<CostWork>
    {
        public void Configure(EntityTypeBuilder<CostWork> builder)
        {
            builder.HasData(
                new CostWork { Id = 1, ExchangeDollar = 87, TimeWeldingOneCrossBar = 0.005, TimeSettingEguip = 5, PNR = 7, Margin = 1.2 }
                );
        }
    }
}

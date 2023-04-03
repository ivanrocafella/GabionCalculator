using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.DAL.Entities.Common.Interfaces
{
    public interface IDateFixEntity
    {
        public DateTime DateStart { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}

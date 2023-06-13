using GabionCalculator.BAL.Models.CostWork;
using GabionCalculator.BAL.Models.Material;
using GabionCalculator.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Services.Interfaces
{
    public interface ICostWorkService
    {
        Task<CostWork> PostAsync(CreateCostWorkModel createCostWork);
        Task<CostWork> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ResponseCostWorkModel> UpdateAsync(CostWork costWork, UpdateCostWorkModel updateCostWorkModel, CancellationToken cancellationToken = default);
    }
}

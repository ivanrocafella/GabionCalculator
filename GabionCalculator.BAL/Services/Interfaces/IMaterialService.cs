using GabionCalculator.BAL.Models.Gabion;
using GabionCalculator.BAL.Models;
using GabionCalculator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GabionCalculator.BAL.Models.Material;

namespace GabionCalculator.BAL.Services.Interfaces
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialResponseModel>> GetAllAsync(Expression<Func<Gabion, bool>> predicate);
        Task<MaterialResponseModel> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<MaterialResponseModel> CreateAsync(CreateMaterialModel createMaterialModel, CancellationToken cancellationToken = default);
        Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<BaseResponseModel> UpdateAsync(int id, UpdateMaterialModel updateMaterialModel, CancellationToken cancellationToken = default);
    }
}

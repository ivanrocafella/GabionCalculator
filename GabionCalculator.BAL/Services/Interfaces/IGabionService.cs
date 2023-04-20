using GabionCalculator.BAL.Models;
using GabionCalculator.BAL.Models.Gabion;
using GabionCalculator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Services.Interfaces
{
    public interface IGabionService
    {
        Task<IEnumerable<GabionResponseModel>> GetAllAsync(Expression<Func<Gabion, bool>> predicate);
        Task<GabionResponseModel> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<GabionResponseModel> CreateAsync(CreateGabionModel createGabionModel, Material material, User user, CancellationToken cancellationToken = default);
        Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<GabionResponseModel>> GetAllByMaterialIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
        
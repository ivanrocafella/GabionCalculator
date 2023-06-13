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
        Task<IEnumerable<Gabion>> GetAllAsync();
        Task<Gabion> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Gabion GetTemporaryGabion(CreateGabionModel createGabionModel, Material material, User user, CostWork costWork, CancellationToken cancellationToken = default);
        Task<Gabion> PostAsync(GabionResponseModel gabionResponseModel, CancellationToken cancellationToken = default);
        Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<GabionResponseModel>> GetAllByMaterialIdAsync(int id, CancellationToken cancellationToken = default);
        IQueryable<Gabion> Pagination(IQueryable<Gabion> queryGabions, int itemsPerPage, int currentPage, CancellationToken cancellationToken = default);
        IQueryable<Gabion> GetAllinQeryable();
        Task<IEnumerable<Gabion>> QueryGabionsToList(IQueryable<Gabion> queryGabions, CancellationToken cancellationToken = default);
    }
}
        
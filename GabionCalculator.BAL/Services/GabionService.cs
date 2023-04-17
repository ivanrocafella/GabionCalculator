using AutoMapper;
using GabionCalculator.BAL.Models;
using GabionCalculator.BAL.Models.Gabion;
using GabionCalculator.BAL.Services.Interfaces;
using GabionCalculator.DAL.Data;
using GabionCalculator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Services
{
    public class GabionService : IGabionService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public GabionService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<CreateGabionModel> CreateAsync(CreateGabionModel createGabionModel, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GabionResponseModel>> GetAllAsync(Expression<Func<Gabion, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GabionResponseModel>> GetAllByMaterialIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<GabionResponseModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

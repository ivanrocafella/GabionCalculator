using AutoMapper;
using GabionCalculator.BAL.Models;
using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Services.Interfaces;
using GabionCalculator.DAL.Data;
using GabionCalculator.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public MaterialService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MaterialResponseModel> CreateAsync(CreateMaterialModel createMaterialModel, CancellationToken cancellationToken = default)
        {
            Material material = _mapper.Map<Material>(createMaterialModel);
            var id = (await _context.Materials.AddAsync(material)).Entity.Id;
            await _context.SaveChangesAsync();
            return new MaterialResponseModel
            {
                Id = id
            };
        }

        public Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MaterialResponseModel>> GetAllAsync(Expression<Func<Gabion, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<MaterialResponseModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            Material material = await _context.Materials.FindAsync(id, cancellationToken);
            return _mapper.Map<MaterialResponseModel>(material);
        }

        public Task<BaseResponseModel> UpdateAsync(int id, UpdateMaterialModel updateMaterialModel, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

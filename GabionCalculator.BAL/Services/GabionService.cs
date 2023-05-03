using AutoMapper;
using GabionCalculator.BAL.Models;
using GabionCalculator.BAL.Models.Gabion;
using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Services.Interfaces;
using GabionCalculator.BAL.Utils;
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
    public class GabionService : IGabionService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public GabionService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Gabion GetTemporaryGabion(CreateGabionModel createGabionModel, Material material, User user, CancellationToken cancellationToken = default)
        {
            string userJson = string.Empty;
            if (user != null)
                userJson = FileAction<User>.Serialize(user);
            string materialJson = FileAction<Material>.Serialize(material); 
            Gabion gabion = _mapper.Map<Gabion>(createGabionModel);
            gabion.UserJson = userJson;
            gabion.MaterialJson = materialJson;
            gabion.Material = material;
            SVG.Get(gabion);  
            return gabion;
        }

        public async Task<GabionResponseModel> PostAsync(Gabion gabion, CancellationToken cancellationToken = default)
        {
            await _context.Gabions.AddAsync(gabion);
            await _context.SaveChangesAsync();
            GabionResponseModel gabionResponseModel = _mapper.Map<GabionResponseModel>(gabion);
            return gabionResponseModel;
        }

        public Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Gabion>> GetAllAsync() => await _context.Gabions.ToListAsync();
            

        public Task<IEnumerable<GabionResponseModel>> GetAllByMaterialIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Gabion> GetByIdAsync(int id, CancellationToken cancellationToken = default) => await _context.Gabions.FindAsync(id, cancellationToken);
    }
}

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

        public Gabion GetTemporaryGabion(CreateGabionModel createGabionModel, Material material, User user, CostWork costWork, CancellationToken cancellationToken = default)
        {
            string userJson = string.Empty;
            if (user != null)
                userJson = FileAction<User>.Serialize(user);
            string materialJson = FileAction<Material>.Serialize(material); 
            Gabion gabion = _mapper.Map<Gabion>(createGabionModel);
            gabion.UserJson = userJson;
            gabion.MaterialJson = materialJson;
            gabion.Material = material;
            gabion.User = user; 
            gabion.UserId = user.Id;
            SVG.Get(gabion);
            Calculation.Calculate(gabion, costWork);
            return gabion;
        }

        public async Task<Gabion> PostAsync(GabionResponseModel gabionResponseModel, CancellationToken cancellationToken = default)
        {
            Gabion gabion = _mapper.Map<Gabion>(gabionResponseModel);
            await _context.Gabions.AddAsync(gabion);
            await _context.SaveChangesAsync();
            return gabion;
        }

        public Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Gabion>> GetAllAsync()
        {
            List<Gabion> Gabions = await _context.Gabions.ToListAsync();
            for (int i = 0; i < Gabions.Count; i++)
            {
                Gabions[i].User = FileAction<User>.Deserialize(Gabions[i].UserJson);
                Gabions[i].Material = FileAction<Material>.Deserialize(Gabions[i].MaterialJson);
            }
            return Gabions;
        }

        public Task<IEnumerable<GabionResponseModel>> GetAllByMaterialIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Gabion> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
           Gabion gabion = await _context.Gabions.FindAsync(id, cancellationToken);
           gabion.User = FileAction<User>.Deserialize(gabion.UserJson);
           gabion.Material = FileAction<Material>.Deserialize(gabion.MaterialJson);
           return gabion;
        } 

        public IQueryable<Gabion> GetAllinQeryable() => _context.Gabions;

        public IQueryable<Gabion> Pagination(IQueryable<Gabion> queryGabions
            , int itemsPerPage
            , int currentPage
            , CancellationToken cancellationToken = default) => queryGabions.Skip(currentPage * itemsPerPage).Take(itemsPerPage);

        //Method for filtration gabion
     //  public void Filter(ref IQueryable<Gabion> queryGabions, string? SelectedMaterial, string SelectedUserName
     //      , DateTime DateTimeFrom, DateTime DateTimeTill, double PriceFrom, double PriceTill)
     //  {
     //      if (!String.IsNullOrEmpty(SelectedMaterial))
     //          anchors = anchors.Where(e => e.MaterialJson.Contains(SelectedMaterial));
     //      if (!String.IsNullOrEmpty(SelectedUserName))
     //          anchors = anchors.Where(e => e.User.UserName.Contains(SelectedUserName));
     //      if (DateTimeFrom > DateTime.MinValue && DateTimeFrom < DateTime.MaxValue && DateTimeTill <= DateTime.MinValue)
     //          anchors = anchors.Where(e => e.DateCreate >= DateTimeFrom);
     //      if (DateTimeTill > DateTime.MinValue && DateTimeTill < DateTime.MaxValue && DateTimeFrom <= DateTime.MinValue)
     //          anchors = anchors.Where(e => e.DateCreate <= DateTimeTill);
     //      if (DateTimeFrom > DateTime.MinValue && DateTimeFrom < DateTime.MaxValue && DateTimeTill > DateTime.MinValue && DateTimeTill < DateTime.MaxValue)
     //          anchors = anchors.Where(e => e.DateCreate >= DateTimeFrom && e.DateCreate <= DateTimeTill);
     //      if (PriceFrom > 0 && PriceFrom < Double.PositiveInfinity && PriceTill == 0)
     //          anchors = anchors.Where(e => e.Price >= PriceFrom);
     //      if (PriceTill > 0 && PriceTill < Double.PositiveInfinity && PriceFrom == 0)
     //          anchors = anchors.Where(e => e.Price <= PriceTill);
     //      if (PriceFrom > 0 && PriceFrom < Double.PositiveInfinity && PriceTill > 0 && PriceTill < Double.PositiveInfinity)
     //          anchors = anchors.Where(e => e.Price >= PriceFrom && e.Price <= PriceTill);
     //  }

        public async Task<IEnumerable<Gabion>> QueryGabionsToList(IQueryable<Gabion> queryGabions
            , CancellationToken cancellationToken = default)
        {
            List<Gabion> Gabions = await queryGabions.ToListAsync(cancellationToken);
            foreach (var item in Gabions)
            {
                item.User = FileAction<User>.Deserialize(item.UserJson);
                item.Material = FileAction<Material>.Deserialize(item.MaterialJson);
            }
            return Gabions;
        } 
    }
}

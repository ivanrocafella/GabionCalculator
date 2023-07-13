using AutoMapper;
using GabionCalculator.BAL.Models;
using GabionCalculator.BAL.Models.Gabion;
using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Services.Interfaces;
using GabionCalculator.BAL.Utils;
using GabionCalculator.DAL.Data;
using GabionCalculator.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
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
        public void Filter(ref IQueryable<Gabion> queryGabions, string filterDateFrom, string filterDateBefore, string filterByExecut, string filterMaterialName)
        {
            DateOnly dateTimeFrom;
            DateOnly dateTimeBefore;
            if (!string.IsNullOrEmpty(filterDateFrom))
                filterDateFrom = filterDateFrom.Substring(1, 10);
            if (!string.IsNullOrEmpty(filterDateBefore))
                filterDateBefore = filterDateBefore.Substring(1, 10);

            DateOnly.TryParse(filterDateFrom, out dateTimeFrom);
            DateOnly.TryParse(filterDateBefore, out dateTimeBefore);

            if (!String.IsNullOrEmpty(filterMaterialName))
                queryGabions = queryGabions.Where(e => e.MaterialJson.Contains(filterMaterialName));
            if (!String.IsNullOrEmpty(filterByExecut))
                queryGabions = queryGabions.Where(e => e.User.UserName.Contains(filterByExecut));
            if (dateTimeFrom > DateOnly.MinValue && dateTimeFrom < DateOnly.MaxValue && dateTimeBefore <= DateOnly.MinValue)
                queryGabions = queryGabions.Where(e => DateOnly.FromDateTime(e.DateStart) >= dateTimeFrom);
            if (dateTimeBefore > DateOnly.MinValue && dateTimeBefore < DateOnly.MaxValue && dateTimeFrom <= DateOnly.MinValue)
                queryGabions = queryGabions.Where(e => DateOnly.FromDateTime(e.DateStart) <= dateTimeBefore);
            if (dateTimeFrom > DateOnly.MinValue && dateTimeFrom < DateOnly.MaxValue && dateTimeBefore > DateOnly.MinValue && dateTimeBefore < DateOnly.MaxValue)
                queryGabions = queryGabions.Where(e => DateOnly.FromDateTime(e.DateStart) >= dateTimeFrom && DateOnly.FromDateTime(e.DateStart) <= dateTimeBefore);
        }

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

        public async Task DeleteByObject(Gabion gabion, CancellationToken cancellationToken = default)
        {
            gabion.User = null; gabion.Material = null;
            _context.Gabions.Remove(gabion);
            await _context.SaveChangesAsync();
        }
    }
}

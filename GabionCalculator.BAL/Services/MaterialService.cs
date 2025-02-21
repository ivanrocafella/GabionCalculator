﻿using AutoMapper;
using GabionCalculator.BAL.Models;
using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Services.Interfaces;
using GabionCalculator.DAL.Data;
using GabionCalculator.DAL.Entities;
using GabionCalculator.DAL.Entities.Enums;
using Microsoft.AspNetCore.Identity;
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
            await _context.Materials.AddAsync(material);
            await _context.SaveChangesAsync();
            MaterialResponseModel materialResponseModel = _mapper.Map<MaterialResponseModel>(material);
            return materialResponseModel;
        }

        public CreateMaterialModel GetCreateMaterialModel()
        {
            List<string> kinds = new();
            foreach (MaterialKind kind in Enum.GetValues(typeof(MaterialKind)))
                kinds.Add(kind.ToString());

            CreateMaterialModel createMaterialModel = new()
            {
                KindsMaterial = kinds,
                Names = new string[] { "Проволока", "Круг" }
            };
            return createMaterialModel;
        }

        public UpdateMaterialModel GetUpdateMaterialModel(Material material, CancellationToken cancellationToken = default)
        {
            List<string> kinds = new();
            foreach (MaterialKind kind in Enum.GetValues(typeof(MaterialKind)))
                kinds.Add(kind.ToString());

            UpdateMaterialModel updateMaterialModel = _mapper.Map<UpdateMaterialModel>(material);
            updateMaterialModel.KindsMaterial = kinds;
            updateMaterialModel.Names = new string[] { "Проволока", "Круг" };
            return updateMaterialModel;
        }

        public Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteByObject(Material material, CancellationToken cancellationToken = default)
        {
            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Material>> GetAllAsync() => await _context.Materials.ToListAsync();

        public async Task<Material> GetByIdAsync(int id, CancellationToken cancellationToken = default) => await _context.Materials.FindAsync(id, cancellationToken);

        public async Task<MaterialResponseModel> UpdateAsync(Material material, UpdateMaterialModel updateMaterialModel, CancellationToken cancellationToken = default)
        {
            material.Name = updateMaterialModel.Name;
            material.Size = updateMaterialModel.Size;
            material.PricePerKg = updateMaterialModel.PricePerKg;
            material.MaterialKindId = updateMaterialModel.MaterialKindId;
            _context.Materials.Update(material);
            await _context.SaveChangesAsync();
            MaterialResponseModel materialResponseModel = _mapper.Map<MaterialResponseModel>(material);
            return materialResponseModel;
        }
    }
}

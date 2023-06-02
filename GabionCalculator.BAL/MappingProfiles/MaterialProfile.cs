using AutoMapper;
using GabionCalculator.BAL.Models.Gabion;
using GabionCalculator.BAL.Models.Material;
using GabionCalculator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.MappingProfiles
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<CreateMaterialModel, Material>();
            CreateMap<UpdateMaterialModel, Material>();
            CreateMap<Material, UpdateMaterialModel>();
            CreateMap<Material, MaterialResponseModel>();
        }
    }
}

using AutoMapper;
using GabionCalculator.BAL.Models.CostWork;
using GabionCalculator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.MappingProfiles
{
    public class CostWorkProfile : Profile
    {
        public CostWorkProfile()
        {
            CreateMap<CostWork, CreateCostWorkModel>();
            CreateMap<CreateCostWorkModel, CostWork>().ForMember(dest => dest.Margin, act => act.MapFrom(src => src.Margin / 100)); 
            CreateMap<CostWork, UpdateCostWorkModel>().ForMember(dest => dest.Margin, act => act.MapFrom(src => src.Margin * 100));
            CreateMap<UpdateCostWorkModel, CostWork>().ForMember(dest => dest.Margin, act => act.MapFrom(src => src.Margin / 100));
            CreateMap<CostWork, ResponseCostWorkModel>();
        }     
    }
}

using AutoMapper;
using GabionCalculator.BAL.Models.Gabion;
using GabionCalculator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.MappingProfiles
{
    public class GabionProfile : Profile
    {
        public GabionProfile()
        {
            CreateMap<CreateGabionModel, Gabion>();
            CreateMap<Gabion, GabionResponseModel>();
            CreateMap<GabionResponseModel, Gabion>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Material, opt => opt.Ignore()); ;
        }
    }
}

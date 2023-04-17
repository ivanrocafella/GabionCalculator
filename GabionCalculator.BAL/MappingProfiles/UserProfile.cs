using AutoMapper;
using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Models.User;
using GabionCalculator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserModel, User>();
            CreateMap<LoginUserModel, User>();
            CreateMap<User, UserResponseModel>();
        }
    }
}

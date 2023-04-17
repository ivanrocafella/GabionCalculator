using AutoMapper;
using GabionCalculator.BAL.Models.User;
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
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<string> DeleteByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteByUserNameAsync(string UserName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserResponseModel>> GetAllAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponseModel> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponseModel> GetByUserNameAsync(string UserName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<RegisterUserModel> RegisterAsync(RegisterUserModel registerUserModel, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync(string id, UpdateUserModel updateUserModel, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

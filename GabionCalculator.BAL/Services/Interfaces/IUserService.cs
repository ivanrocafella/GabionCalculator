using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Models;
using GabionCalculator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GabionCalculator.BAL.Models.User;

namespace GabionCalculator.BAL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseModel>> GetAllAsync(Expression<Func<User, bool>> predicate);
        Task<UserResponseModel> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<UserResponseModel> GetByUserNameAsync(string UserName, CancellationToken cancellationToken = default);
        Task<RegisterUserModel> RegisterAsync(RegisterUserModel registerUserModel, CancellationToken cancellationToken = default);
        Task<string> DeleteByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<string> DeleteByUserNameAsync(string UserName, CancellationToken cancellationToken = default);
        Task<string> UpdateAsync(string id, UpdateUserModel updateUserModel, CancellationToken cancellationToken = default);
    }
}

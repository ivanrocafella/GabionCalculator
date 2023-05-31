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
using Microsoft.AspNetCore.Identity;

namespace GabionCalculator.BAL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllExceptCurUserAsync(Expression<Func<User, bool>> predicate);
        Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<User> GetByUserNameAsync(string UserName, CancellationToken cancellationToken = default);
        Task<IdentityResult> RegisterAsync(RegisterUserModel registerUserModel, CancellationToken cancellationToken = default);
        Task<string> DeleteByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<string> DeleteByUserNameAsync(string UserName, CancellationToken cancellationToken = default);
        Task DeleteByObject(User User, CancellationToken cancellationToken = default);
        Task<string> UpdateAsync(string id, UpdateUserModel updateUserModel, CancellationToken cancellationToken = default);
        UserResponseModel GetResponseModel(User user, CancellationToken cancellationToken = default);
        Task AddRoleAsync(User user, string role, CancellationToken cancellationToken = default);
        Task<User> GetUserByNameOrEmailAsync(LoginUserModel loginUserModel, CancellationToken cancellationToken = default);
        Task<SignInResult> GetSignInAsync(LoginUserModel loginUserModel, User user, CancellationToken cancellationToken = default);
        Task GetSignOutAsync(CancellationToken cancellationToken = default);
        Task<User> GetByEmailAsync(string Email, CancellationToken cancellationToken = default);
    }
}

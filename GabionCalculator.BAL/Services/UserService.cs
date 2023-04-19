using AutoMapper;
using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Models.User;
using GabionCalculator.BAL.Services.Interfaces;
using GabionCalculator.DAL.Data;
using GabionCalculator.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace GabionCalculator.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(ApplicationContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
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
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
        public async Task<User> GetByUserNameAsync(string UserName, CancellationToken cancellationToken = default) =>  await _userManager.FindByNameAsync(UserName);
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.

        public UserResponseModel GetResponseModel(User user, CancellationToken cancellationToken = default) => _mapper.Map<UserResponseModel>(user);
 
        public async Task<IdentityResult> RegisterAsync(RegisterUserModel registerUserModel, CancellationToken cancellationToken = default)
        {
            User user = new() { Email = registerUserModel.Email, UserName = registerUserModel.UserName };
            IdentityResult result = await _userManager.CreateAsync(user, registerUserModel.Password);
            return result;
        }

        public async Task<SignInResult> GetSignInAsync(LoginUserModel loginUserModel, User user, CancellationToken cancellationToken = default)
            => await _signInManager.PasswordSignInAsync(
               user,
               loginUserModel.Password,
               loginUserModel.RememberMe,
               false);
        public async Task<User> GetUserByLoginAsync(LoginUserModel loginUserModel, CancellationToken cancellationToken = default)
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
            => await _userManager.FindByNameAsync(loginUserModel.EmailLogin) ?? await _userManager.FindByEmailAsync(loginUserModel.EmailLogin);
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.

        public async Task AddRoleAsync(User user, string role, CancellationToken cancellationToken = default)
            => await _userManager.AddToRoleAsync(user, role);

        public Task<string> UpdateAsync(string id, UpdateUserModel updateUserModel, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

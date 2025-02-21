﻿using AutoMapper;
using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Models.User;
using GabionCalculator.BAL.Services.Interfaces;
using GabionCalculator.DAL.Data;
using GabionCalculator.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteByObject(User User, CancellationToken cancellationToken = default)
        {
            await _userManager.DeleteAsync(User);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllExceptCurUserAsync(Expression<Func<User, bool>> predicate)
            => await _userManager.Users.Where(predicate).ToListAsync();

        public async Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default) => await _userManager.FindByIdAsync(id);

        public async Task<User> GetByUserNameAsync(string UserName, CancellationToken cancellationToken = default) =>  await _userManager.FindByNameAsync(UserName);
        public async Task<User> GetByEmailAsync(string Email, CancellationToken cancellationToken = default) => await _userManager.FindByEmailAsync(Email);
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

        public async Task GetSignOutAsync(CancellationToken cancellationToken = default) => await _signInManager.SignOutAsync();

        public async Task<User> GetUserByNameOrEmailAsync(LoginUserModel loginUserModel, CancellationToken cancellationToken = default)
            => await _userManager.FindByNameAsync(loginUserModel.EmailLogin) ?? await _userManager.FindByEmailAsync(loginUserModel.EmailLogin);

        public async Task AddRoleAsync(User user, string role, CancellationToken cancellationToken = default)
            => await _userManager.AddToRoleAsync(user, role);

        public Task<string> UpdateAsync(string id, UpdateUserModel updateUserModel, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

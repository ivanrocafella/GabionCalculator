﻿using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Models;
using Microsoft.AspNetCore.Mvc;
using GabionCalculator.BAL.Services.Interfaces;
using GabionCalculator.BAL.Models.User;
using GabionCalculator.BAL.Services;
using Microsoft.AspNetCore.Identity;
using GabionCalculator.BAL;
using GabionCalculator.DAL.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Microsoft.EntityFrameworkCore;
using GabionCalculator.DAL.Data;
using GabionCalculator.BAL.Models.Gabion;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using GabionCalculator.BAL.Services.JwtFeatures;
using System.IdentityModel.Tokens.Jwt;
using GabionCalculator.BAL.Utils;

namespace GabionCalculator.API.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, IMapper mapper, JwtHandler jwtHandler, UserManager<User> userManager)
        {
            _userService = userService;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _userManager = userManager;
        }

        // POST: api/User/Post
        [HttpPost("Register")]
        public async Task<IActionResult> PostAsync([FromBody] RegisterUserModel registerUserModel)
        {
            if (await _userService.GetByUserNameAsync(registerUserModel.UserName) != null)
                ModelState.AddModelError("UserName", "Пользователь с таким логином уже есть");
            if (await _userService.GetByEmailAsync(registerUserModel.Email) != null)
                ModelState.AddModelError("Email", "Пользователь с такой почтой уже есть");
            if (ModelState.IsValid)
            {
                IdentityResult result = await _userService.RegisterAsync(registerUserModel);
                if (result.Succeeded)
                {
                    User user = await _userService.GetByUserNameAsync(registerUserModel.UserName);
                    if (user != null)
                    {
                        await _userService.AddRoleAsync(user, "employee");
                        return Ok(ApiResult<UserResponseModel>.Success(_userService.GetResponseModel(user)));
                    };
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        var message = error.Description;
                        message = message == "Passwords must be at least 12 characters." ? "Пароль должен содержать не менее 12 символов." : default;
                        ModelState.AddModelError(string.Empty, message);
                    }
                }
            }
            return BadRequest(ApiResult<UserResponseModel>.Failure(ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)));
        }

        // POST: api/User/Login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserModel loginUserModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userService.GetUserByNameOrEmailAsync(loginUserModel);                
                if (user != null)
                {
                   bool isCorrectPassword = await _userManager.CheckPasswordAsync(user, loginUserModel.Password);
                   if (isCorrectPassword)
                   {
                        var signinCredentials = _jwtHandler.GetSigningCredentials();
                        var claims = await _jwtHandler.GetClaims(user);
                        var tokenOptions = _jwtHandler.GenerateTokenOptions(signinCredentials, claims);
                        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                        User curUser = await CurrentUser.Get(_userManager, user.UserName);
                        return Ok(new AuthResponseModel { IsAuthSuccessful = true, Token = token });
                   }
                          
                }
                ModelState.AddModelError("", "Неправильный логин, электронная почта и (или) пароль"); 
            }            
            return Unauthorized(new AuthResponseModel { Errors = ModelState.Values
                       .SelectMany(v => v.Errors)
                       .Select(e => e.ErrorMessage) });
        }

        // POST: api/User
        [HttpPost]
 
        public async Task<IActionResult> LogOutAsync()
        {
            bool isAuth = User.Identity.IsAuthenticated;
            await _userService.GetSignOutAsync();
            return RedirectToAction("GetAsync","Gabion");
        }

        // POST: api/User/Remove/skf34l41ksm
        [HttpPost("Remove/{id}")]

        public async Task<IActionResult> Remove([FromRoute] string id)
        {
            if (!string.IsNullOrEmpty(id)) 
            {
                User user = await _userService.GetByIdAsync(id);
                if (user != null) 
                {
                    await _userService.DeleteByObject(user); 
                    return Ok(ApiResult<UserResponseModel>.Success(_mapper.Map<UserResponseModel>(user)));
                }
                return StatusCode(500, ApiResult<User>.Failure(new List<string>() { "Пользователь не найден" }));
            }
            return StatusCode(500, ApiResult<User>.Failure(new List<string>() { "Id пользователя не передан в параметры" }));
        }

        // POST: api/User/GetUser
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            User user = await CurrentUser.Get(_userManager, User.Identity.Name);
            if (user != null)
                return Ok(ApiResult<UserResponseModel>.Success(_mapper.Map<UserResponseModel>(user)));
            return StatusCode(500, ApiResult<User>.Failure(new List<string>() { "Пользователь не найден" }));
        }

        // POST: api/User/ChangePassword
        [HttpGet("ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword([FromRoute] string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                User user = await _userService.GetByIdAsync(id);
                if (user != null)
                    return Ok(ApiResult<ChangePasswordUserModel>.Success(_mapper.Map<ChangePasswordUserModel>(user)));
                return StatusCode(500, ApiResult<User>.Failure(new List<string>() { "Пользователь не найден" }));
            }
            return StatusCode(500, ApiResult<User>.Failure(new List<string>() { "Id пользователя не передан в параметры" }));
        }

        // POST: api/User/ChangePassword/324hbds3
        [HttpPut("ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword([FromRoute] string id, [FromBody] ChangePasswordUserModel changePasswordUserModel)
        {
            if (!string.IsNullOrEmpty(id))
            {
                User user = await _userService.GetByIdAsync(id);
                if (user != null)
                {
                    if (ModelState.IsValid)
                    {
                       IdentityResult result = await _userManager.ChangePasswordAsync(user, changePasswordUserModel.OldPassword, changePasswordUserModel.NewPassword);
                       if (result.Succeeded)
                           return Ok(user);
                       else
                       {
                             foreach (var error in result.Errors)
                             {
                                   var message = error.Description switch
                                   {
                                       "Incorrect password." => "Вы ввели неправильный пароль",
                                       "Passwords must be at least 12 characters." => "Пароль должен содержать не менее 12 символов.",
                                       _ => error.Description

                                   }; 
                                 ModelState.AddModelError(string.Empty, message);
                             }
                       }
                    }
                    return BadRequest(ApiResult<UserResponseModel>.Failure(ModelState.Values
                                                           .SelectMany(v => v.Errors)
                                                           .Select(e => e.ErrorMessage)));
                }
                return StatusCode(500, ApiResult<User>.Failure(new List<string>() { "Пользователь не найден" }));    
            }
            return StatusCode(500, ApiResult<User>.Failure(new List<string>() { "Id пользователя не передан в параметры" }));
        }

        // GET: api/User/Users
        [HttpGet("Users")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllExceptCurUserAsync(e => e.UserName != User.Identity.Name);
            return Ok(ApiResult<IEnumerable<User>>.Success(users));
        }
    }
}

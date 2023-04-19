using GabionCalculator.BAL.Models.Material;
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

namespace GabionCalculator.API.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/User/Post
        [HttpPost("Post")]
        public async Task<IActionResult> PostAsync([FromBody] RegisterUserModel registerUserModel)
        {
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
                        if (message == "Passwords must be at least 12 characters.")
                            message = "Пароль должен состоять из не менее 12 символов.";
                        ModelState.AddModelError(string.Empty, message);
                    }        
                }
            }               
            return StatusCode(500, ApiResult<UserResponseModel>.Failure(ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)));
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserModel loginUserModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userService.GetUserByLoginAsync(loginUserModel);
                if (user != null)
                {
                    SignInResult result = await _userService.GetSignInAsync(loginUserModel, user);
                    if (result.Succeeded)
                        return Ok(ApiResult<UserResponseModel>.Success(_userService.GetResponseModel(user)));
                }
                ModelState.AddModelError("", "Неправильный логин, электронная почта и (или) пароль");
            }
            return StatusCode(500, ApiResult<LoginUserModel>.Failure(ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)));
        }

        // VAlidation
     //  [AcceptVerbs("GET", "POST")]
     //  public bool CheckExistAccountByEmail(string Email) => !_context.Users.Any(e => e.Email == Email);
     //
     //  [AcceptVerbs("GET", "POST")]
     //  public bool CheckExistAccountByUserName(string UserName) => !_context.Users.Any(e => e.UserName == UserName);
    }
}

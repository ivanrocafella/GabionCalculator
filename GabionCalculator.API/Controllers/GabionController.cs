using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Models;
using GabionCalculator.BAL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GabionCalculator.BAL.Models.Gabion;
using GabionCalculator.DAL.Entities;
using GabionCalculator.BAL.Utils;

namespace GabionCalculator.API.Controllers
{
    public class GabionController : ApiController
    {
        private readonly IGabionService _gabionService;
        private readonly IMaterialService _materialService;
        private readonly IUserService _userService;

        public GabionController(IGabionService gabionService, IMaterialService materialService, IUserService userService)
        {
            _gabionService = gabionService;
            _materialService = materialService;
            _userService = userService;
        }

        // POST: api/
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateGabionModel createGabionModel)
        {
            if (ModelState.IsValid)
            {
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                return Ok(ApiResult<GabionResponseModel>.Success(await _gabionService.CreateAsync(createGabionModel
                    , await _materialService.GetByIdAsync(createGabionModel.MaterialId)
                    , await _userService.GetByUserNameAsync(User.Identity.Name))));
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
            }            
            else
                return StatusCode(500, ApiResult<MaterialResponseModel>.Failure(ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)));
        }
    }
}

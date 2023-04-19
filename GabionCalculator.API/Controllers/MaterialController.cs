using GabionCalculator.BAL.Models;
using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Services.Interfaces;
using GabionCalculator.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace GabionCalculator.API.Controllers
{
    public class MaterialController : ApiController
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        // POST: api/Material/Post
        [HttpPost("Post")]
        public async Task<IActionResult> PostAsync([FromBody] CreateMaterialModel createMaterialModel)
        {
            if (ModelState.IsValid)
                return Ok(ApiResult<MaterialResponseModel>.Success(await _materialService.CreateAsync(createMaterialModel)));
            else
            {
                return StatusCode(500, ApiResult<MaterialResponseModel>.Failure(ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)));
            }
                     
        }

        // GET: api/Material/GetById/5
        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(ApiResult<MaterialResponseModel>.Success(await _materialService.GetByIdAsync(id)));
        }
    }
}

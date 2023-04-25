using AutoMapper;
using GabionCalculator.BAL.Models;
using GabionCalculator.BAL.Models.Gabion;
using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Services;
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
        private readonly IMapper _mapper;

        public MaterialController(IMaterialService materialService, IMapper mapper)
        {
            _materialService = materialService;
            _mapper = mapper;
        }

        // POST: api/Material/Post
        [HttpPost("Post")]
        public async Task<IActionResult> PostAsync([FromBody] CreateMaterialModel createMaterialModel)
        {
            if (ModelState.IsValid)
                return Ok(ApiResult<MaterialResponseModel>.Success(await _materialService.CreateAsync(createMaterialModel)));
            else
                return StatusCode(500, ApiResult<MaterialResponseModel>.Failure(ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)));            
        }

        // POST: api/Material/Update/5
        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateMaterialModel updateMaterialModel)
        {
            if (ModelState.IsValid)
                return Ok(ApiResult<MaterialResponseModel>.Success(await _materialService.UpdateAsync(id, updateMaterialModel)));
            else
                return StatusCode(500, ApiResult<MaterialResponseModel>.Failure(ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)));
        }

        // GET: api/Material/GetById/5
        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            MaterialResponseModel materialResponseModel = _mapper.Map<MaterialResponseModel>(await _materialService.GetByIdAsync(id));
            if (materialResponseModel != null)
                return Ok(ApiResult<MaterialResponseModel>.Success(materialResponseModel));
            else
                return StatusCode(500, ApiResult<MaterialResponseModel>.Failure(new List<string>() { "Объект не найден." }));              
        }

        // GET: api/Materials
        [HttpGet("Materials")]
        public async Task<IActionResult> GetAllAsync()
        {
            var materials = await _materialService.GetAllAsync();
            if (materials.Any())
                return Ok(ApiResult<IEnumerable<MaterialResponseModel>>.Success(_mapper.Map<IEnumerable<MaterialResponseModel>>(materials)));
            else
                return StatusCode(500, ApiResult<IEnumerable<MaterialResponseModel>>.Failure(new List<string>() { "Объекты ещё не добавлены." }));
        }
    }
}

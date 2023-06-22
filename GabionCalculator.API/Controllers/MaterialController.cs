using AutoMapper;
using GabionCalculator.BAL.Models;
using GabionCalculator.BAL.Models.Gabion;
using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Models.User;
using GabionCalculator.BAL.Services;
using GabionCalculator.BAL.Services.Interfaces;
using GabionCalculator.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
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
    //    [Authorize]
        public async Task<IActionResult> PostAsync([FromBody] CreateMaterialModel createMaterialModel)
        {
            if (ModelState.IsValid)
                return Ok(ApiResult<MaterialResponseModel>.Success(await _materialService.CreateAsync(createMaterialModel)));
            else
                return StatusCode(500, ApiResult<MaterialResponseModel>.Failure(ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)));            
        }

        // GET: api/Material/Post
        [HttpGet("Post")]
        [Authorize]
        public IActionResult Post() => Ok(ApiResult<CreateMaterialModel>.Success(_materialService.GetCreateMaterialModel()));

        // POST: api/Material/Update/5
        [HttpGet("Update/{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            Material material = await _materialService.GetByIdAsync(id);
            return Ok(ApiResult<UpdateMaterialModel>.Success(_materialService.GetUpdateMaterialModel(material)));
        }

        // POST: api/Material/Update/5
        [HttpPut("Update/{id:int}")]
       // [Authorize]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody]UpdateMaterialModel updateMaterialModel)
        {
            if (ModelState.IsValid)
            {
                Material material = await _materialService.GetByIdAsync(id);
                return Ok(ApiResult<MaterialResponseModel>.Success(await _materialService.UpdateAsync(material, updateMaterialModel)));
            }               
            else
                return StatusCode(500, ApiResult<MaterialResponseModel>.Failure(ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)));
        }

        // GET: api/Material/GetById/5
        [HttpGet("GetById/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            MaterialResponseModel materialResponseModel = _mapper.Map<MaterialResponseModel>(await _materialService.GetByIdAsync(id));
            if (materialResponseModel != null)
                return Ok(ApiResult<MaterialResponseModel>.Success(materialResponseModel));
            else
                return StatusCode(500, ApiResult<MaterialResponseModel>.Failure(new List<string>() { "Объект не найден." }));
        }

        // POST: api/Material/Remove/5
        [HttpPost("Remove/{id}")]

        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            if (id > 0)
            {
                Material material = await _materialService.GetByIdAsync(id);
                if (material != null)
                {
                    await _materialService.DeleteByObject(material);
                    return Ok(ApiResult<MaterialResponseModel>.Success(_mapper.Map<MaterialResponseModel>(material)));
                }
                return StatusCode(500, ApiResult<Material>.Failure(new List<string>() { "Материал не найден" }));
            }
            return StatusCode(500, ApiResult<Material>.Failure(new List<string>() { "Id материала не передан в параметры" }));
        }



        // GET: api/Material/Materials
        [HttpGet("Materials")]
       // [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var materials = await _materialService.GetAllAsync();
            if (materials.Any())
                return Ok(ApiResult<IEnumerable<MaterialResponseModel>>.Success(_mapper.Map<IEnumerable<MaterialResponseModel>>(materials)));
            else
                return StatusCode(500, ApiResult<IEnumerable<MaterialResponseModel>>.Failure(new List<string>() { "Объекты ещё не добавлены." }));
        }

        // GET: api/Material/Privacy
        [HttpGet("Privacy")]
        [Authorize(Roles = "admin")]
        public IActionResult Privacy()
        {
            var claims = User.Claims
                .Select(c => new { c.Type, c.Value })
                .ToList();
            return Ok(claims);
        }
    }
}

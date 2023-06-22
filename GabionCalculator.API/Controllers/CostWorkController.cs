using GabionCalculator.BAL.Models.Gabion;
using GabionCalculator.BAL.Models;
using GabionCalculator.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Models.CostWork;
using GabionCalculator.BAL.Services;
using GabionCalculator.BAL.Services.Interfaces;
using AutoMapper;

namespace GabionCalculator.API.Controllers
{
    public class CostWorkController : ApiController
    {
        private readonly ICostWorkService _costWorkService;
        private readonly IMapper _mapper;

        public CostWorkController(ICostWorkService costWorkService, IMapper mapper)
        {
            _costWorkService = costWorkService;
            _mapper = mapper;
        }

        // POST: api/CostWork/Post
        [HttpPost("Post")]
        public async Task<IActionResult> PostAsync([FromBody] CreateCostWorkModel createCostWorkModel)
        {
            if (ModelState.IsValid)
                return Ok(ApiResult<CostWork>.Success(await _costWorkService.PostAsync(createCostWorkModel)));
            else
                return BadRequest(ApiResult<ResponseCostWorkModel>.Failure(ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)));
        }

        // POST: api/CostWork/Update/5
        [HttpGet("Update/{id:int}")]
      //[Authorize]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            CostWork costWork = await _costWorkService.GetByIdAsync(id);
            return Ok(ApiResult<UpdateCostWorkModel>.Success(_mapper.Map<UpdateCostWorkModel>(costWork)));
        }

        // POST: api/CostWork/Update/5
        [HttpPut("Update/{id:int}")]
       // [Authorize]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCostWorkModel updateCostWorkModel)
        {
            if (ModelState.IsValid)
            {
                CostWork costWork = await _costWorkService.GetByIdAsync(id);
                return Ok(ApiResult<ResponseCostWorkModel>.Success(await _costWorkService.UpdateAsync(costWork, updateCostWorkModel)));
            }
            else
                return StatusCode(500, ApiResult<ResponseCostWorkModel>.Failure(ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)));
        }
    }
}

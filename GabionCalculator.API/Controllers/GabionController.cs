using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Models;
using GabionCalculator.BAL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GabionCalculator.BAL.Models.Gabion;
using GabionCalculator.DAL.Entities;
using GabionCalculator.BAL.Utils;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using GabionCalculator.BAL.Models.User;
using Google.Protobuf;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.IdentityModel.Tokens;

namespace GabionCalculator.API.Controllers
{
    public class GabionController : ApiController
    {
        private readonly IGabionService _gabionService;
        private readonly IMaterialService _materialService;
        private readonly IUserService _userService;
        private readonly ICostWorkService _costWorkService;
        private readonly IMapper _mapper;

        public GabionController(IGabionService gabionService, IMaterialService materialService, IUserService userService, IMapper mapper, ICostWorkService costWorkService)
        {
            _gabionService = gabionService;
            _materialService = materialService;
            _userService = userService;
            _mapper = mapper;
            _costWorkService = costWorkService;
        }

        // POST: api/Gabion
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync([FromBody] GabionResponseModel gabionResponseModel)
        {           
            if (ModelState.IsValid)
                return Ok(ApiResult<Gabion>.Success(await _gabionService.PostAsync(gabionResponseModel)));            
            else
                return BadRequest(ApiResult<GabionResponseModel>.Failure(ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)));
        }

        // POST: api/Gabion/GetTemporaryGabion
        [HttpPost("GetTemporaryGabion")]
        public async Task<IActionResult> GetTemporaryGabionAsync([FromBody] CreateGabionModel createGabionModel)
        {
            int CardWidthMax = 4030;
            User user = new();
            CostWork costWork = await _costWorkService.GetByIdAsync(1);
            if (User.Identity.IsAuthenticated)
              user = await _userService.GetByUserNameAsync(User.Identity.Name);
                
            if (ModelState.IsValid)
            {
                Gabion gabion = _gabionService.GetTemporaryGabion(createGabionModel
                   , await _materialService.GetByIdAsync(createGabionModel.MaterialId)
                   , user
                   , costWork);
                if (gabion.CardWidth <= CardWidthMax)
                    return Ok(ApiResult<GabionResponseModel>.Success(_mapper.Map<GabionResponseModel>(gabion)));
                else
                {
                    ModelState.AddModelError("IncorrectLengthOrWidth", $"Необходимо уменьшить габариты на {gabion.CardWidth - CardWidthMax}");
                    return BadRequest(ApiResult<GabionResponseModel>.Failure(ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)));
                }
            }
            else
                return BadRequest(ApiResult<GabionResponseModel>.Failure(ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)));
        }

        // POST: api/Gabion/Gabions
        [HttpGet("Gabions")]
       // [Authorize]
        public async Task<IActionResult> GetAllAsync([FromQuery]int itemsPerPage, [FromQuery]int currentPage
                                                    , [FromQuery]string filterDateFrom, [FromQuery]string filterDateBefore
                                                    , [FromQuery] string filterByExecut, [FromQuery] string filterMaterialName)
        {
   





            IQueryable<Gabion> queryGabions = _gabionService.GetAllinQeryable();
            IQueryable<Gabion> allQueryGabions = queryGabions;

            DateTime dateTimeFrom;
            DateTime dateTimeBefore;
            if (!string.IsNullOrEmpty(filterDateFrom))
                filterDateFrom = filterDateFrom.Substring(1, filterDateFrom.Length - 2);
            if (!string.IsNullOrEmpty(filterDateBefore))
                filterDateBefore = filterDateBefore.Substring(1, filterDateBefore.Length - 2);

            DateTime.TryParse(filterDateFrom, out dateTimeFrom);
            DateTime.TryParse(filterDateBefore, out dateTimeBefore);

            if (!String.IsNullOrEmpty(filterMaterialName))
                queryGabions = queryGabions.Where(e => e.MaterialJson.Contains(filterMaterialName));
            if (!String.IsNullOrEmpty(filterByExecut))
                queryGabions = queryGabions.Where(e => e.User.UserName.Contains(filterByExecut));
            if (dateTimeFrom > DateTime.MinValue && dateTimeFrom < DateTime.MaxValue && dateTimeBefore <= DateTime.MinValue)
                queryGabions = queryGabions.Where(e => e.DateStart >= dateTimeFrom);
            if (dateTimeBefore > DateTime.MinValue && dateTimeBefore < DateTime.MaxValue && dateTimeFrom <= DateTime.MinValue)
                queryGabions = queryGabions.Where(e => e.DateStart <= dateTimeFrom);
            if (dateTimeFrom > DateTime.MinValue && dateTimeFrom < DateTime.MaxValue && dateTimeBefore > DateTime.MinValue && dateTimeBefore < DateTime.MaxValue)
                queryGabions = queryGabions.Where(e => e.DateStart >= dateTimeFrom && e.DateStart <= dateTimeBefore);

            

            int totalItems = queryGabions.Count();
            queryGabions = _gabionService.Pagination(queryGabions, itemsPerPage, currentPage);
            IEnumerable<Gabion> gabions = await _gabionService.QueryGabionsToList(queryGabions);
            if (gabions.Any())
            {
                ApiResult<IEnumerable<GabionResponseModel>> apiResult = ApiResult<IEnumerable<GabionResponseModel>>
                    .SuccessWithAdditNum(_mapper.Map<IEnumerable<GabionResponseModel>>(gabions), totalItems);
                return Ok(apiResult);
            }                
            else
                return StatusCode(500, ApiResult<IEnumerable<GabionResponseModel>>.Failure(new List<string>() { "Объекты ещё не добавлены." }));
        }

        // GET: api/Gabion/GetById/5
        [HttpGet("Details/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            GabionResponseModel gabionResponseModel = _mapper.Map<GabionResponseModel>(await _gabionService.GetByIdAsync(id));
            if (gabionResponseModel != null)
                return Ok(ApiResult<GabionResponseModel>.Success(gabionResponseModel));
            else
                return StatusCode(500, ApiResult<GabionResponseModel>.Failure(new List<string>() { "Объект не найден." }));
        }

        // GET: api/Gabion
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var materials = await _materialService.GetAllAsync();
            CreateGabionModel model = new();
            if (materials.Any())
                model.Materials = materials;  
            return Ok(ApiResult<CreateGabionModel>.Success(model));
        }
    }
}

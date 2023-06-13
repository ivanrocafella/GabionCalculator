using AutoMapper;
using GabionCalculator.BAL.Models.CostWork;
using GabionCalculator.BAL.Models.Material;
using GabionCalculator.BAL.Services.Interfaces;
using GabionCalculator.DAL.Data;
using GabionCalculator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Services
{
    public class CostWorkService : ICostWorkService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CostWorkService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CostWork> GetByIdAsync(int id, CancellationToken cancellationToken = default) => await _context.CostWorks.FindAsync(id, cancellationToken);

        public async Task<CostWork> PostAsync(CreateCostWorkModel createCostWork)
        {
            CostWork costWork = _mapper.Map<CostWork>(createCostWork);
            await _context.CostWorks.AddAsync(costWork);
            await _context.SaveChangesAsync();
            return costWork;
        }

        public async Task<ResponseCostWorkModel> UpdateAsync(CostWork costWork, UpdateCostWorkModel updateCostWorkModel, CancellationToken cancellationToken = default)
        {
            costWork.ExchangeDollar = updateCostWorkModel.ExchangeDollar;
            costWork.TimeSettingEguip = updateCostWorkModel.TimeSettingEguip;
            costWork.TimeWeldingOneCrossBar = updateCostWorkModel.TimeWeldingOneCrossBar;
            costWork.PNR = updateCostWorkModel.PNR;
            costWork.Margin = updateCostWorkModel.Margin / 100; 
            _context.CostWorks.Update(costWork);
            await _context.SaveChangesAsync();
            ResponseCostWorkModel responseCostWorkModel = _mapper.Map<ResponseCostWorkModel>(costWork);
            return responseCostWorkModel; ;
        }
    }
}

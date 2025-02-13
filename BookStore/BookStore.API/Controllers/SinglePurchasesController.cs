using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore.API.Repositories;
using AutoMapper;
using BookStore.API.models.Domain;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinglePurchasesController : ControllerBase
    {
        private readonly ISinglePurchaseRepository singlePurchaseRepository;
        private readonly IMapper mapper;

        public SinglePurchasesController(ISinglePurchaseRepository singlePurchaseRepository, IMapper mapper)
        {
            this.singlePurchaseRepository = singlePurchaseRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSinglePurchasesAsync()
        {
            var singlePurchases = await singlePurchaseRepository.GetAllAsync();

            var singlePurchasesDTO = mapper.Map<List<models.DTO.SinglePurchase>>(singlePurchases);
            return Ok(singlePurchasesDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetSinglePurchaseAsync(Guid id)
        {
            var singlePurchase = await singlePurchaseRepository.GetByIdAsync(id);
            if (singlePurchase == null)
            {
                return NotFound();
            }

            var singlePurchaseDTO = mapper.Map<models.DTO.SinglePurchase>(singlePurchase);
            return Ok(singlePurchaseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddSinglePurchaseAsync(models.DTO.AddSinglePurchaseRequest addSinglePurchaseRequest)
        {
            var singlePurchase = new SinglePurchase()
            {
                BookId = addSinglePurchaseRequest.BookId,
                PurchaseId = addSinglePurchaseRequest.PurchaseId,
                Quantity = addSinglePurchaseRequest.Quantity,
            };

            singlePurchase = await singlePurchaseRepository.AddSinglePurchaseAsync(singlePurchase);

            var singlePurchaseDTO = mapper.Map<models.DTO.SinglePurchase>(singlePurchase);
            return Ok(singlePurchaseDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteSinglePurhaseAsync(Guid id)
        {
            var singlePurchase = await singlePurchaseRepository.DeleteAsync(id);

            if(singlePurchase == null)
            {
                return NotFound();
            }

            var singlePurchaseDTO = mapper.Map<models.DTO.SinglePurchase>(singlePurchase);
            return Ok(singlePurchaseDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateSinglePurchaseAsync(Guid id, models.DTO.UpdateSinglePurchaseRequest updateSinglePurchaseRequest)
        {
            var singlePurchase = new SinglePurchase
            {
                Quantity = updateSinglePurchaseRequest.Quantity,
                BookId = updateSinglePurchaseRequest.BookId,
                PurchaseId = updateSinglePurchaseRequest.PurchaseId,
            };

            singlePurchase = await singlePurchaseRepository.UpdateAsync(id, singlePurchase);
            if (singlePurchase == null)
            {
                return NotFound();
            }

            var singlePurchaseDTO = mapper.Map<models.DTO.SinglePurchase>(singlePurchase);
            return Ok(singlePurchaseDTO);

        }
    }
}

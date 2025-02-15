using AutoMapper;
using BookStore.API.models.Domain;
using BookStore.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseRepository purchaseRepository;
        private readonly IMapper mapper;

        public PurchasesController(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            this.purchaseRepository = purchaseRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPurchasesAsync()
        {
            var purchases = await purchaseRepository.GetAllAsync();

            var purchasesDTO = mapper.Map<List<models.DTO.Purchase>>(purchases);

            return Ok(purchasesDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetPurchaseAsync")]
        public async Task<IActionResult> GetPurchaseAsync(Guid id)
        {
            var purchase = await purchaseRepository.GetByIdAsync(id);
            if(purchase == null)
            {
                return NotFound();
            }

            var purchasesDTO = mapper.Map<models.DTO.Purchase>(purchase);
            return Ok(purchasesDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddPurchaseAsync(models.DTO.AddPurchaseRequest addPurchaseRequest)
        {

            var purchase = new Purchase()
            {
                PurchaseTime = addPurchaseRequest.PurchaseTime,
            };

            purchase = await purchaseRepository.AddPurchaseAsync(purchase);

            var purchaseDTO = new models.DTO.Purchase
            {
                Id = purchase.Id,
                PurchaseTime = purchase.PurchaseTime,
            };
            return CreatedAtAction(nameof(GetPurchaseAsync), new { id = purchaseDTO.Id }, purchaseDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeletePurchaseAsync(Guid id)
        {
            var purchase = await purchaseRepository.DeleteAsync(id);

            if(purchase == null)
            {
                return NotFound();
            }
            var purchaseDTO = mapper.Map<models.DTO.Purchase>(purchase);
            return Ok(purchaseDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatePurchaseAsync([FromRoute] Guid id,[FromBody] models.DTO.UpdatePurchaseRequest updatePurchaseRequest)
        {
            var purchase = new Purchase()
            {
                PurchaseTime = updatePurchaseRequest.PurchaseTime,
            };
            purchase = await purchaseRepository.UpdateAsync(id, purchase);

            if(purchase == null)
            {
                return NotFound();
            }

            var purchaseDTO = mapper.Map<models.DTO.Purchase>(purchase);
            return Ok(purchaseDTO);
        }

    }
}

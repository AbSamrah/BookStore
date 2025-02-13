using AutoMapper;
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
        public async Task<IActionResult> GetPurchaseByIdAsync(Guid id)
        {
            var purchase = await purchaseRepository.GetByIdAsync(id);
            if(purchase == null)
            {
                return NotFound();
            }

            var purchasesDTO = mapper.Map<models.DTO.Purchase>(purchase);
            return Ok(purchasesDTO);
        }

      
    }
}

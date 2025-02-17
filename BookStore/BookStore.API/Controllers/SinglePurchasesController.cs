using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore.API.Repositories;
using AutoMapper;
using BookStore.API.models.Domain;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinglePurchasesController : Controller
    {
        private readonly ISinglePurchaseRepository singlePurchaseRepository;
        private readonly IMapper mapper;
        private readonly IPurchaseRepository purchaseRepository;
        private readonly IBookRepository bookRepository;

        public SinglePurchasesController(ISinglePurchaseRepository singlePurchaseRepository, IMapper mapper,
            IPurchaseRepository purchaseRepository, IBookRepository bookRepository)
        {
            this.singlePurchaseRepository = singlePurchaseRepository;
            this.mapper = mapper;
            this.purchaseRepository = purchaseRepository;
            this.bookRepository = bookRepository;
        }
        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllSinglePurchasesAsync()
        {
            var singlePurchases = await singlePurchaseRepository.GetAllAsync();

            var singlePurchasesDTO = mapper.Map<List<models.DTO.SinglePurchase>>(singlePurchases);
            return Ok(singlePurchasesDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "reader")]
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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddSinglePurchaseAsync(models.DTO.AddSinglePurchaseRequest addSinglePurchaseRequest)
        {
            if (!await ValidateAddSinglePurchaseAsync(addSinglePurchaseRequest))
            {
                return BadRequest(ModelState);
            }

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
        [Authorize(Roles = "writer")]
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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateSinglePurchaseAsync(Guid id, models.DTO.UpdateSinglePurchaseRequest updateSinglePurchaseRequest)
        {
            if (!await ValidateUpdateSinglePurchaseAsync(updateSinglePurchaseRequest))
            {
                return BadRequest(ModelState);
            }
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

        #region private methods

        private async Task<bool> ValidateAddSinglePurchaseAsync(models.DTO.AddSinglePurchaseRequest addSinglePurchaseRequest)
        {
            if (addSinglePurchaseRequest == null)
            {
                ModelState.AddModelError(nameof(addSinglePurchaseRequest), $"{nameof(addSinglePurchaseRequest)} cannot be null.");
                return false;
            }

            if(addSinglePurchaseRequest.Quantity < 1)
            {
                ModelState.AddModelError(nameof(addSinglePurchaseRequest.Quantity), $"You have to put at least one book.");
            }

            if(await bookRepository.GetByIdAsync(addSinglePurchaseRequest.BookId) == null)
            {
                ModelState.AddModelError(nameof(addSinglePurchaseRequest.BookId), " There is no book with this Id.");
            }

            if(await purchaseRepository.GetByIdAsync(addSinglePurchaseRequest.PurchaseId) == null)
            {
                ModelState.AddModelError(nameof(addSinglePurchaseRequest.PurchaseId), "There is no purchase with this Id.");
            }

            if(ModelState.Count > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUpdateSinglePurchaseAsync(models.DTO.UpdateSinglePurchaseRequest updateSinglePurchaseRequest)
        {
            if (updateSinglePurchaseRequest == null)
            {
                ModelState.AddModelError(nameof(updateSinglePurchaseRequest), $"{nameof(updateSinglePurchaseRequest)} cannot be null.");
                return false;
            }

            if (updateSinglePurchaseRequest.Quantity < 1)
            {
                ModelState.AddModelError(nameof(updateSinglePurchaseRequest.Quantity), $"You have to put at least one book.");
            }

            if (await bookRepository.GetByIdAsync(updateSinglePurchaseRequest.BookId) == null)
            {
                ModelState.AddModelError(nameof(updateSinglePurchaseRequest.BookId), $"There is no book with this Id.");
            }

            if (await purchaseRepository.GetByIdAsync(updateSinglePurchaseRequest.PurchaseId) == null)
            {
                ModelState.AddModelError(nameof(updateSinglePurchaseRequest.PurchaseId), $"There is no purchase with this Id.");
            }

            if (ModelState.Count > 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}

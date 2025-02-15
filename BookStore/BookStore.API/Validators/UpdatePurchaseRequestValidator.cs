using FluentValidation;

namespace BookStore.API.Validators
{
    public class UpdatePurchaseRequestValidator: AbstractValidator<models.DTO.UpdatePurchaseRequest>
    {
        public UpdatePurchaseRequestValidator()
        {
            RuleFor(x => x.PurchaseTime).LessThanOrEqualTo(DateTime.Now);
        }
    }
}

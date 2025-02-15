using FluentValidation;

namespace BookStore.API.Validators
{
    public class AddPurchaseRequestValidator: AbstractValidator<models.DTO.AddPurchaseRequest>
    {
        public AddPurchaseRequestValidator()
        {
            RuleFor(x => x.PurchaseTime).LessThanOrEqualTo(DateTime.Now);
        }
    }
}

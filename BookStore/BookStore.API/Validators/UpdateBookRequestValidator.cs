using FluentValidation;

namespace BookStore.API.Validators
{
    public class UpdateBookRequestValidator: AbstractValidator<models.DTO.UpdateBookRequest>
    {
        public UpdateBookRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.AuthorName).NotEmpty();
            RuleFor(x => x.PriceInSYR).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CreatedDate).LessThanOrEqualTo(DateTime.Now);
        }
    }
}

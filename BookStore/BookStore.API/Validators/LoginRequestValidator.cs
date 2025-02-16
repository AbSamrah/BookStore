using FluentValidation;

namespace BookStore.API.Validators
{
    public class LoginRequestValidator: AbstractValidator<models.DTO.LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x=>x.UserName).NotEmpty();
            RuleFor(x=>x.Password).NotEmpty();
        }
    }
}

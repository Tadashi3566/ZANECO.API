namespace ZANECO.API.Application.Identity.Tokens;

public record TokenRequest(string UserName, string Email, string Password);

public class TokenRequestValidator : CustomValidator<TokenRequest>
{
    public TokenRequestValidator()
    {
        RuleFor(p => p.UserName).Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
           .NotEmpty()
           .EmailAddress()
               .WithMessage("Invalid Email Address.");

        RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}
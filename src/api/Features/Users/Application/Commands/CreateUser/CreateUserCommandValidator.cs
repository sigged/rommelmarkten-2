using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.Users.Application.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        protected readonly IUsersDbContext userContext;
        private readonly UserManager<ApplicationUser> userManager;

        public CreateUserCommandValidator(IUsersDbContext userContext, UserManager<ApplicationUser> userManager)
        {
            this.userContext = userContext;
            this.userManager = userManager;

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Please enter your e-mail adress.")
                .EmailAddress().WithMessage("Please enter a valid e-mail adress.")
                .MaximumLength(100).WithMessage("Your e-mai must not exceed 100 characters.")
                .MustAsync(BeUniqueEmail).WithMessage("This e-mail adress is already in use.");

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Please enter your name.")
                .MaximumLength(100).WithMessage("Your name must not exceed 100 characters.");
        }

        public async Task<bool> BeUniqueEmail(CreateUserCommand entity, string email, CancellationToken cancellationToken)
        {
            return !await userManager.Users.AnyAsync(l => l.Email == email, cancellationToken);
        }
    }
}

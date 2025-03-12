using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Features.NewsArticles.Application.Commands;
using Rommelmarkten.Api.Features.NewsArticles.Application.Gateways;

namespace Rommelmarkten.Api.Features.NewsArticles.Application.Commands.Validators
{
    public class CreateNewsArticleCommandValidator : NewsArticleValidatorBase<CreateNewsArticleCommand>
    {
        public CreateNewsArticleCommandValidator(INewsArticlesDbContext context) : base(context)
        {
            RuleFor(v => v.Id)
                .Empty().WithMessage("Use a default Id value when creating a new entity.")
                .MustAsync(BeUniqueId).WithMessage("A configuration with this Id already exists.");
        }
        public async Task<bool> BeUniqueId(Guid id, CancellationToken cancellationToken)
        {
            return await _context.NewsArticles
                .AllAsync(l => l.Id != id);
        }
    }
}

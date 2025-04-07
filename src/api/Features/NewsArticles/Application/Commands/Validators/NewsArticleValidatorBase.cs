using FluentValidation;
using Rommelmarkten.Api.Features.NewsArticles.Application.Gateways;
using Rommelmarkten.Api.Features.NewsArticles.Application.Models;

namespace Rommelmarkten.Api.Features.NewsArticles.Application.Commands.Validators
{
    public abstract class NewsArticleValidatorBase<T> : AbstractValidator<T>
        where T : NewsArticleDto
    {
        protected readonly INewsArticlesDbContext _context;

        public NewsArticleValidatorBase(INewsArticlesDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");


            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(4000).WithMessage("Content must not exceed 4000 characters.");

        }

    }
}

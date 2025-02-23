using FluentValidation;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.NewsArticles.Models;

namespace Rommelmarkten.Api.Application.NewsArticles.Commands.Validators
{
    public abstract class NewsArticleValidatorBase<T> : AbstractValidator<T>
        where T : NewsArticleDto
    {
        protected readonly IApplicationDbContext _context;

        public NewsArticleValidatorBase(IApplicationDbContext context)
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

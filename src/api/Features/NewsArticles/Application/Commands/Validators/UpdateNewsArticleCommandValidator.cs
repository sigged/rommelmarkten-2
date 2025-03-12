using Rommelmarkten.Api.Features.NewsArticles.Application.Gateways;

namespace Rommelmarkten.Api.Features.NewsArticles.Application.Commands.Validators
{

    public class UpdateNewsArticleCommandValidator : NewsArticleValidatorBase<UpdateNewsArticleCommand>
    {
        public UpdateNewsArticleCommandValidator(INewsArticlesDbContext context) : base(context)
        {
        }
    }
}

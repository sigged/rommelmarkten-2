using Rommelmarkten.Api.Application.Common.Interfaces;

namespace Rommelmarkten.Api.Application.NewsArticles.Commands.Validators
{

    public class UpdateNewsArticleCommandValidator : NewsArticleValidatorBase<UpdateNewsArticleCommand>
    {
        public UpdateNewsArticleCommandValidator(IApplicationDbContext context) : base(context)
        {
        }
    }
}

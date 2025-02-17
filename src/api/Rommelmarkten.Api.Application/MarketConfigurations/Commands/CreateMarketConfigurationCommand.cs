using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.Commands
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateMarketConfigurationCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            //RuleFor(v => v.Id)
            //    .Empty().WithMessage("Use a default Id value when creating a new entity.")
            //    .MustAsync(BeUniqueId).WithMessage("A configuration with this Id already exists.");

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
                .MustAsync(BeUniqueName).WithMessage("A configuration with this name already exists.");
        }

        public async Task<bool> BeUniqueName(string title, CancellationToken cancellationToken)
        {
            return await _context.MarketConfigurations
                .AllAsync(l => l.Name != title);
        }
        public async Task<bool> BeUniqueId(Guid id, CancellationToken cancellationToken)
        {
            return await _context.MarketConfigurations
                .AllAsync(l => l.Id != id);
        }
    }

    public class CreateMarketConfigurationCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string? Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public int MaximumThemes { get; set; }

        public int MaximumCharacters { get; set; }

        public bool AllowBanners { get; set; }

        public bool AllowPoster { get; set; }
    }

    public class CreateMarketConfigurationCommandHandler : IRequestHandler<CreateMarketConfigurationCommand, Unit>
    {
        private readonly IEntityRepository<MarketConfiguration> repository;

        public CreateMarketConfigurationCommandHandler(IEntityRepository<MarketConfiguration> repository, IValidator<CreateMarketConfigurationCommand> fuck)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(CreateMarketConfigurationCommand request, CancellationToken cancellationToken)
        {
            var entity = new MarketConfiguration {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                IsActive = request.IsActive,
                MaximumThemes = request.MaximumThemes,
                MaximumCharacters = request.MaximumCharacters,
                AllowBanners = request.AllowBanners,
                AllowPoster = request.AllowPoster,
            };

            await repository.InsertAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}

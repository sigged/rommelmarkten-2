using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using System.Reflection;

namespace Rommelmarkten.Api.Common.Application.Behaviours
{
    public class ResourceAuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;
        private readonly IResourceAuthorizationService resourceAuthorizationService;
        private readonly IServiceProvider serviceProvider;

        public ResourceAuthorizationBehaviour(
            ICurrentUserService currentUserService,
            IIdentityService identityService,
            IResourceAuthorizationService resourceAuthorizationService,
            IServiceProvider serviceProvider)
        {
            _currentUserService = currentUserService;
            _identityService = identityService;
            this.resourceAuthorizationService = resourceAuthorizationService;
            this.serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authorizeResourceAttributes = request.GetType().GetCustomAttributes<AuthorizeResourceAttribute>();

            if (authorizeResourceAttributes.Any())
            {
                // Must be authenticated user
                if (_currentUserService.UserId == null)
                {
                    throw new UnauthorizedAccessException();
                }

                foreach (var authorizeResourceAttribute in authorizeResourceAttributes)
                {
                    var propertyInfo = typeof(TRequest).GetProperty(authorizeResourceAttribute.IdentifierPropertyName);
                    if (propertyInfo == null)
                    {
                        throw new ArgumentException($"Property {authorizeResourceAttribute.IdentifierPropertyName} not found on request object");
                    }

                    object? id = propertyInfo.GetValue(request);
                    if (id == null)
                    {
                        throw new ArgumentException($"Property {authorizeResourceAttribute.IdentifierPropertyName} is null");
                    }

                    Type repositoryType = typeof(IEntityRepository<>).MakeGenericType(authorizeResourceAttribute.ResourceType);
                    IEntityRepository repository = (IEntityRepository) serviceProvider.GetRequiredService(repositoryType);

                    object entity = await repository.GetObjectByIdAsync(id);

                    if (!await resourceAuthorizationService.Authorize(entity, authorizeResourceAttribute.Policy))
                    {
                        throw new UnauthorizedAccessException();
                    }

                }
            }

            // User is authorized / authorization not required
            return await next();
        }
    }
}

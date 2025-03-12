using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Models;

namespace Rommelmarkten.Api.Features.Users.Application.Commands.ResetPassword
{

    public class ResetPasswordCommand : IRequest<Result>
    {
        public required string Email { get; set; }
        public required string ResetCode { get; set; }
        public required string NewPassword { get; set; }
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;

        public ResetPasswordCommandHandler(IIdentityService identityService, IDomainEventService domainEventService)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
        }

        public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ResetPasswordAsync(request.Email, request.ResetCode, request.NewPassword);
            return result;
        }
    }
}

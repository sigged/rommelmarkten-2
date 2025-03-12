using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;

namespace Rommelmarkten.Api.Application.Users.Commands.ForgotPassword
{

    public class ForgotPasswordCommand : IRequest
    {
        public required string Email { get; set; }
    }

    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;

        public ForgotPasswordCommandHandler(IIdentityService identityService, IDomainEventService domainEventService)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
        }

        public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var resetCode = await _identityService.GeneratePasswordResetTokenAsync(request.Email);

            //todo: send email
            //await SendForgotPasswordEmail(user.Email, user);

        }
    }
}

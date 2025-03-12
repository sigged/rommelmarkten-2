using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;

namespace Rommelmarkten.Api.Application.Users.Commands.ResendConfirmationEmail
{
    public class ResendConfirmationEmailCommand : IRequest
    {
        public required string Email { get; set; }
    }

    public class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommand>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;

        public ResendConfirmationEmailCommandHandler(IIdentityService identityService, IDomainEventService domainEventService)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
        }

        public async Task Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var resetCode = await _identityService.GenerateEmailConfirmationTokenAsync(request.Email);

            //todo: send email
            //await SendForgotPasswordEmail(user.Email, user);

        }
    }
}

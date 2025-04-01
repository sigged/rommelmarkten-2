using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;

namespace Rommelmarkten.Api.Features.Users.Application.Commands.ResendConfirmationEmail
{
    public class ResendConfirmationEmailCommand : IRequest
    {
        public required string UserId { get; set; }
    }

    public class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommand>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;
        private readonly IMailer mailer;

        public ResendConfirmationEmailCommandHandler(IIdentityService identityService, IDomainEventService domainEventService, IMailer mailer)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
            this.mailer = mailer;
        }

        public async Task Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var resetCode = await _identityService.GenerateEmailConfirmationTokenAsync(request.UserId);

            var user = await _identityService.FindById(request.UserId);

            await mailer.SendEmailConfirmationLink(user, resetCode);

        }
    }
}

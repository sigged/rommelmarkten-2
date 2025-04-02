using MediatR;
using Rommelmarkten.Api.Common.Application.Exceptions;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Features.Captchas.Application.Gateways;

namespace Rommelmarkten.Api.Features.Users.Application.Commands.ForgotPassword
{

    public class ForgotPasswordCommand : IRequest<Result>
    {
        public required string Email { get; set; }
        public required string Captcha { get; set; }
    }

    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result>
    {
        private readonly IIdentityService identityService;
        private readonly IDomainEventService domainEventService;
        private readonly ICaptchaProvider captchaProvider;
        private readonly IMailer mailer;

        public ForgotPasswordCommandHandler(IIdentityService identityService, IDomainEventService domainEventService, ICaptchaProvider captchaProvider, IMailer mailer)
        {
            this.identityService = identityService;
            this.domainEventService = domainEventService;
            this.captchaProvider = captchaProvider;
            this.mailer = mailer;
        }

        public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var captchaResult = await captchaProvider.VerifyChallenge(request.Captcha);
                if (captchaResult.Succeeded)
                {

                    var resetCode = await identityService.GeneratePasswordResetTokenAsync(request.Email);

                    var user = await identityService.FindByEmail(request.Email);

                    await mailer.SendPasswordResetLink(user, resetCode);

                    return Result.Success();
                }
                else
                {
                    return Result.Failure(captchaResult.Errors);
                }
            }
            catch(NotFoundException)
            {
                return Result.Failure(["Request could not be completed"]);
            }
        }
    }
}

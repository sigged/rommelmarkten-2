using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Models;

namespace Rommelmarkten.Api.Application.Users.Commands.CreateUser
{
    public class ManageTwoFactorAuthenticationResult : Result
    {
        public ManageTwoFactorAuthenticationResult(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {

        }

        public required string SharedKey { get; set; }
        public int RecoveryCodesLeft { get; set; }
        public string[] RecoveryCodes { get; set; } = [];
        public required bool IsTwoFactorEnabled { get; set; }
        public required bool IsMachineRemembered { get; set; }

    }

    public class ManageTwoFactorAuthenticationCommand : IRequest<ManageTwoFactorAuthenticationResult>
    {
        public required bool Enable { get; set; }
        public required string TwoFactorCode { get; set; }
        public required bool ResetSharedKey { get; set; }
        public required bool ResetRecoveryCodes { get; set; }
        public required bool ForgetMachine { get; set; }
    }

    public class ManageTwoFactorAuthenticationCommandHandler : IRequestHandler<ManageTwoFactorAuthenticationCommand, ManageTwoFactorAuthenticationResult>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;

        public ManageTwoFactorAuthenticationCommandHandler(IIdentityService identityService, IDomainEventService domainEventService)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
        }

        public async Task<ManageTwoFactorAuthenticationResult> Handle(ManageTwoFactorAuthenticationCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

    }
}

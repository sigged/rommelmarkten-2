using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Application.MarketConfigurations
{


    public class PersistConfigurationCommand : IRequest<Guid>
    {

    }

    public class PersistConfigurationCommandHandler : IRequestHandler<PersistConfigurationCommand, Guid>
    {
        public PersistConfigurationCommandHandler()
        {
        }

        public async Task<Guid> Handle(PersistConfigurationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException("PersistConfiguration is not implemented");
            await Task.CompletedTask;
            return default;
        }
    }

}

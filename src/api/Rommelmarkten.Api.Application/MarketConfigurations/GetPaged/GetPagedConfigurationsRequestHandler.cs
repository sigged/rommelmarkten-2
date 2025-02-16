using MediatR;

namespace Rommelmarkten.Api.Application.MarketConfigurations.GetPaged
{
    public class GetPagedConfigurationsRequestHandler : IRequestHandler<GetPagedConfigurationsRequest, GetPagedConfigurationsResult>
    {
        public GetPagedConfigurationsRequestHandler()
        {
        }

        public async Task<GetPagedConfigurationsResult> Handle(GetPagedConfigurationsRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException("GetConfigurations is not implemented");
            await Task.CompletedTask;
            return default;
        }
    }


}

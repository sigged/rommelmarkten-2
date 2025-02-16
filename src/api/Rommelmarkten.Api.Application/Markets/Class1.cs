using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Application.Markets
{

    public class GetMarketsRequest : IRequest<GetMarketsResult>
    {

    }

    public class GetMarketsResult
    {

    }


    public class GetMarketsRequestHandler : IRequestHandler<GetMarketsRequest, GetMarketsResult>
    {
        public GetMarketsRequestHandler()
        {
        }

        public async Task<GetMarketsResult> Handle(GetMarketsRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException("GetMarkets is not implemented");
            await Task.CompletedTask;
            return default;
        }
    }


    public class GetMarketRequest : IRequest<GetMarketResult>
    {

    }

    public class GetMarketResult
    {

    }


    public class GetMarketRequestHandler : IRequestHandler<GetMarketRequest, GetMarketResult>
    {
        public GetMarketRequestHandler()
        {
        }

        public async Task<GetMarketResult> Handle(GetMarketRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException("GetMarket is not implemented");
            await Task.CompletedTask;
            return default;
        }
    }

}

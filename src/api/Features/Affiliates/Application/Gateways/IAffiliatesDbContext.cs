using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Features.Affiliates.Domain;

namespace Rommelmarkten.Api.Features.Affiliates.Application.Gateways
{
    public interface IAffiliatesDbContext : IApplicationDbContextBase
    {
        DbSet<AffiliateAd> AffiliateAds { get; set; }
    }
}

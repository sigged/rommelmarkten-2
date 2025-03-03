using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Affiliates;
using System.Data.Common;

namespace V2Importer.Importers
{
    public class AffiliateAdImporter
    {
        private readonly IEntityRepository<AffiliateAd> repository;
        private readonly ICurrentUserService currentUserService;

        public AffiliateAdImporter(
            IEntityRepository<AffiliateAd> repository,
            ICurrentUserService currentUserService)
        {
            this.repository = repository;
            this.currentUserService = currentUserService;
        }

        public async Task Import(DbConnection source)
        {
            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = "SELECT * FROM dingse";

            using (var reader = await sourceCommand.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    //prepare parameters
                    var parms = new Dictionary<string, object?> {
                        { "Id", null },
                        { "ImageUrl", null },
                        { "AffiliateName", null },
                        { "AffiliateURL", null },
                        { "AdContent", null },
                        { "Order", null },
                        { "IsActive", null },
                    };

                    //collect parameters from source record
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        if (parms.ContainsKey(columnName))
                        {
                            parms[columnName] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                        }
                    }

                    //transform parameters
                    parms["Id"] = Guid.NewGuid();

                    //set current user
                    //currentUserService.UserId

                    var affiliateAd = new AffiliateAd
                    {
                        Id = (Guid) parms["Id"]!,
                        ImageUrl = (string)parms["ImageUrl"]!,
                        AffiliateName = (string)parms["AffiliateName"]!,
                        AffiliateURL = (string)parms["AffiliateURL"]!,
                        AdContent = (string)parms["AdContent"]!,
                        Order = (int)parms["Order"]!,
                        IsActive = (bool)parms["IsActive"]!,
                    };

                    await repository.InsertAsync(affiliateAd);

                }
            }
        }
    }
}

using Rommelmarkten.Api.Common.Infrastructure.Security;
using Rommelmarkten.FunctionalTests.WebApi.Common;
using Rommelmarkten.FunctionalTests.WebApi.Extensions;
using Rommelmarkten.FunctionalTests.WebApi.Fixtures;
using System.Net;

namespace WebApiTests.FunctionalTests
{
    [Collection(nameof(WebApiTests.FunctionalTests))]
    public partial class UsersTests : IClassFixture<RommelmarktenWebApiFixture>
    {
        private readonly RommelmarktenWebApiFixture appFixture;

        public UsersTests(RommelmarktenWebApiFixture fixture)
        {
            this.appFixture = fixture;
        }

    }
}
using Rommelmarkten.EndToEndTests.WebApi.Fixtures;

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
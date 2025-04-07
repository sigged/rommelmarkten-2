using Rommelmarkten.EndToEndTests.WebApi.Fixtures;

namespace WebApiTests.EndToEndTests
{
    [Collection(nameof(WebApiTests.EndToEndTests))]
    public partial class UsersTests : IClassFixture<RommelmarktenWebApiFixture>
    {
        private readonly RommelmarktenWebApiFixture appFixture;

        public UsersTests(RommelmarktenWebApiFixture fixture)
        {
            this.appFixture = fixture;
        }

    }
}
namespace Rommelmarkten.Api.Features.ShoppingLists.Application.Security
{
    public class Policies
    {

        public const string MustHaveListAccess = nameof(MustHaveListAccess);
        public const string MustMatchListAssociation = nameof(MustMatchListAssociation);
    }
}

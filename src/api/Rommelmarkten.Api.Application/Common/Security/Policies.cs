namespace Rommelmarkten.Api.Application.Common.Security
{
    public class Policies
    {
        public const string MustBeAdmin = nameof(MustBeAdmin);
        public const string MustBeCreator = nameof(MustBeCreator);
        public const string MustBeCreatorOrAdmin = nameof(MustBeCreatorOrAdmin);
        public const string MustBeLastModifier = nameof(MustBeLastModifier);
        public const string MustHaveListAccess = nameof(MustHaveListAccess);
        public const string MustMatchListAssociation = nameof(MustMatchListAssociation);
    }
}

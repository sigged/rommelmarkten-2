namespace Rommelmarkten.Api.Common.Application.Security
{
    public class CorePolicies
    {
        public const string MustBeAdmin = nameof(MustBeAdmin);
        public const string MustBeCreator = nameof(MustBeCreator);
        public const string MustBeCreatorOrAdmin = nameof(MustBeCreatorOrAdmin);
        public const string MustBeLastModifier = nameof(MustBeLastModifier);
    }
}

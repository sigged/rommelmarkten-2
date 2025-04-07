namespace Rommelmarkten.Api.Common.Application.Security
{
    /// <summary>
    /// The class this this attribute is applied to require resource authorization.
    /// </summary>
    /// <remarks>
    /// The resource will be fetched using the <see cref="nameof(AuthorizeResourceAttribute.IdentifierPropertyName)"/> and using IEntityRepository.
    /// This means that if <see cref="nameof(AuthorizeResourceAttribute.ResourceType)"/> has a type called Foo, there should be an IEntityRepository<Foo> registered in the DI container.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class AuthorizeResourceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeResourceAttribute"/> class. 
        /// </summary>
        public AuthorizeResourceAttribute() { }

        /// <summary>
        /// Gets or sets the policy name that determines access to the resource.
        /// </summary>
        public required string Policy { get; set; }

        /// <summary>
        /// Gets or sets the property name of the IRequest which contains the resource id.
        /// </summary>
        public required string IdentifierPropertyName { get; set; }

        /// <summary>
        /// Gets or sets the resource type, which must be registered with an IEntityRepository<> in the DI container.
        /// </summary>
        public required Type ResourceType { get; set; }

    }


}

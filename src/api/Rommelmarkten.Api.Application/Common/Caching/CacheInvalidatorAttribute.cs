namespace Rommelmarkten.Api.Application.Common.Caching
{
    /// <summary>
    /// Specifies the class this attribute is applied to requires authorization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class CacheInvalidatorAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheInvalidatorAttribute"/> class. 
        /// </summary>
        public CacheInvalidatorAttribute() { }

        /// <summary>
        /// Gets or sets the cache keys that are invalidated when this class is run in the pipeline.
        /// </summary>
        public string[] Tags { get; set; } = [];

    }
}

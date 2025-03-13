using AutoMapper;
using System.Reflection;

namespace Rommelmarkten.Api.Common.Application.Mappings
{
    /// <summary>
    /// Discovers IMapFrom<> implementations and applies them to the <see cref="IMapFrom{T}.Mapping(Profile)"/> method.
    /// </summary>
    public abstract class DiscoverIMapFromProfile : Profile
    {
        protected DiscoverIMapFromProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetCallingAssembly());
        }

        protected virtual void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });

            }
        }
    }
}
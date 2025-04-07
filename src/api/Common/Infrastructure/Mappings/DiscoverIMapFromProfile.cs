using AutoMapper;
using Rommelmarkten.Api.Common.Application.Mappings;
using System.Reflection;
using System.Runtime.Loader;

namespace Rommelmarkten.Api.Common.Infrastructure.Mappings
{

    /// <summary>
    /// Discovers IMapFrom<> implementations and applies them to the <see cref="IMapFrom{T}.Mapping(Profile)"/> method.
    /// </summary>
    public class DiscoverIMapFromProfile : Profile
    {
        public DiscoverIMapFromProfile()
        {
            var assembliesToScan = AssemblyLoadContext.Default.Assemblies;
            foreach (var assembly in assembliesToScan)
            {
                ApplyMappingsFromAssembly(assembly);
            }

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
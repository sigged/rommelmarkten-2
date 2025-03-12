using AutoMapper;
using Rommelmarkten.Api.Common.Application.Mappings;
using System.Reflection;

namespace Rommelmarkten.Api.Common.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");

                if (methodInfo == null)
                {
                    var interfaceType = type.GetInterface("IMapFrom`1");
                    if (interfaceType != null)
                        methodInfo = interfaceType.GetMethod("Mapping");
                }

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
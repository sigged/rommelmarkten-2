using AutoMapper;

namespace Rommelmarkten.Api.Common.Application.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}

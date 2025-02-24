using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.FAQCategories.Models;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Application.FAQCategories.Requests
{
    public struct GetFAQCategoryByIdRequest : IRequest<FAQCategoryDto>
    {
        public Guid Id { get; set; }
    }
    public class GetConfigurationByIdRequestHandler : IRequestHandler<GetFAQCategoryByIdRequest, FAQCategoryDto>
    {
        private readonly IEntityRepository<FAQCategory> repository;
        private readonly IMapper mapper;

        public GetConfigurationByIdRequestHandler(IEntityRepository<FAQCategory> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<FAQCategoryDto> Handle(GetFAQCategoryByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
            return mapper.Map<FAQCategoryDto>(entity);
        }
    }

}

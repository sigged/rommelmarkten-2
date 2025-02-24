using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.FAQItems.Models;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Application.FAQItems.Requests
{
    public struct GetFAQItemByIdRequest : IRequest<FAQItemDto>
    {
        public Guid Id { get; set; }
    }
    public class GetConfigurationByIdRequestHandler : IRequestHandler<GetFAQItemByIdRequest, FAQItemDto>
    {
        private readonly IEntityRepository<FAQItem> repository;
        private readonly IMapper mapper;

        public GetConfigurationByIdRequestHandler(IEntityRepository<FAQItem> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<FAQItemDto> Handle(GetFAQItemByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
            return mapper.Map<FAQItemDto>(entity);
        }
    }

}

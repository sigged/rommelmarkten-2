namespace Rommelmarkten.Api.Features.ShoppingLists.Application.Queries.ExportItems
{

    //[Authorize(Policy = Policies.MustBeAdmin)]
    //public class ExportItemsQuery : IRequest<ExportItemsVm>
    //{
    //    public int ListId { get; set; }
    //}

    //public class ExportItemsQueryHandler : IRequestHandler<ExportItemsQuery, ExportItemsVm>
    //{
    //    private readonly IApplicationDbContext _context;
    //    private readonly IMapper _mapper;
    //    private readonly ICsvFileBuilder _fileBuilder;

    //    public ExportItemsQueryHandler(IApplicationDbContext context, IMapper mapper, ICsvFileBuilder fileBuilder)
    //    {
    //        _context = context;
    //        _mapper = mapper;
    //        _fileBuilder = fileBuilder;
    //    }

    //    public async Task<ExportItemsVm> Handle(ExportItemsQuery request, CancellationToken cancellationToken)
    //    {
    //        var vm = new ExportItemsVm();

    //        var records = await _context.ListItems
    //                .Where(t => t.ListId == request.ListId)
    //                .ProjectTo<ListItemRecord>(_mapper.ConfigurationProvider)
    //                .ToListAsync(cancellationToken);

    //        vm.Content = _fileBuilder.BuildListItemsFile(records);
    //        vm.ContentType = "text/csv";
    //        vm.FileName = "Items.csv";

    //        return await Task.FromResult(vm);
    //    }
    //}
}

using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Features.FAQs.Domain;
using System.Data.Common;

namespace V2Importer.Importers
{
    public class FAQGraphImporter
    {
        private readonly IEntityRepository<FAQCategory> faqCategoryRepository;
        private readonly IEntityRepository<FAQItem> faqItemRepository;
        private readonly ICurrentUserService currentUserService;

        public FAQGraphImporter(
            IEntityRepository<FAQCategory> faqCategoryRepository,
            IEntityRepository<FAQItem> faqItemRepository,
            ICurrentUserService currentUserService)
        {
            this.faqCategoryRepository = faqCategoryRepository;
            this.faqItemRepository = faqItemRepository;
            this.currentUserService = currentUserService;
        }

        private Dictionary<object, Guid> oldCategoryIdLinks = [];

        public async Task Import(DbConnection source)
        {
            oldCategoryIdLinks = [];

            await ImportCategories(source);
            await ImportItems(source);
        }

        private async Task ImportCategories(DbConnection source)
        {
            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = "SELECT * FROM dingse";

            using (var reader = await sourceCommand.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    //prepare parameters
                    var parms = new Dictionary<string, object?> {
                        { "Id", null },
                        { "Name", null },
                        { "Order", null },
                    };

                    //collect parameters from source record
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        if (parms.ContainsKey(columnName))
                        {
                            parms[columnName] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                        }
                    }

                    //remember old ID
                    var id = Guid.NewGuid();
                    oldCategoryIdLinks.Add(parms["Id"]!, id);

                    //transform parameters
                    parms["Id"] = id;

                    //set current user
                    //currentUserService.UserId

                    var entity = new FAQCategory
                    {
                        Id = (Guid)parms["Id"]!,
                        Name = (string)parms["Name"]!,
                        Order = (int)parms["Order"]!,
                    };

                    await faqCategoryRepository.InsertAsync(entity);
                }
            }
        }

        private async Task ImportItems(DbConnection source)
        {
            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = "SELECT * FROM FAQItems";

            using (var reader = await sourceCommand.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    //prepare parameters
                    var parms = new Dictionary<string, dynamic?> {
                        { "Id", null },
                        { "Question", null },
                        { "Answer", null },
                        { "Order", null },
                        { "Category_Id", null },
                    };

                    //collect parameters from source record
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        if (parms.ContainsKey(columnName))
                        {
                            parms[columnName] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                        }
                    }

                    //transform parameters
                    parms["Id"] = Guid.NewGuid();

                    //set current user
                    //currentUserService.UserId

                    var entity = new FAQItem
                    {
                        Id = (Guid)parms["Id"]!,
                        Question = (string)parms["Question"]!,
                        Answer = (string)parms["Answer"]!,
                        Order = (int)parms["Order"]!,
                        CategoryId = oldCategoryIdLinks[reader["Category_Id"]],
                    };

                    await faqItemRepository.InsertAsync(entity);
                }
            }
        }
    }
}

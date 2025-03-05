using Microsoft.Data.SqlClient;
using Rommelmarkten.Api.Domain.Content;
using Rommelmarkten.Api.Domain.Markets;
using System.Data;
using System.Data.Common;

namespace V2Importer.Importers
{

    public partial class Importer
    {
        private async Task ImportBannerTypes(DbConnection source)
        {
            Console.Write("Importing BannerTypes...");
            ConsoleHelpers.ShowCount(0, 0);


            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = "SELECT * FROM RMBannerTypes";

            DbDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sourceCommand;
            DataTable sourceTable = new DataTable();
            adapter.Fill(sourceTable);

            int totalCount = sourceTable.Rows.Count;
            long count = 0;

            foreach (DataRow row in sourceTable.Rows)
            {
                //prepare parameters
                var parms = new Dictionary<string, dynamic?>();

                //collect parameters from source record
                parms.Add("RMBannerTypeID", row.Field<long>("RMBannerTypeID"));
                parms.Add("Name", row.Field<string>("Name"));
                parms.Add("Description", row.Field<string>("Description"));
                parms.Add("Price", row.Field<decimal>("Price"));
                parms.Add("IsActive", row.Field<bool>("IsActive"));

                var entity = new BannerType
                {
                    Id = LongToGuid(parms["RMBannerTypeID"]),
                    Name = parms["Name"]!,
                    Description = parms["Description"]!,
                    Price = parms["Price"]!,
                    IsActive = parms["IsActive"]!,
                };

                await bannerTypeRepository.InsertAsync(entity);

                count++;

                ConsoleHelpers.ShowCount(count, totalCount, jumpback: true);
            }

            Console.WriteLine();

        }

        private async Task ImportFAQCategory(DbConnection source)
        {
            Console.Write("Importing FAQCategories...");
            ConsoleHelpers.ShowCount(0, 0);


            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = "SELECT * FROM FAQCategories";

            DbDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sourceCommand;
            DataTable sourceTable = new DataTable();
            adapter.Fill(sourceTable);

            int totalCount = sourceTable.Rows.Count;
            long count = 0;

            foreach (DataRow row in sourceTable.Rows)
            {
                //prepare parameters
                var parms = new Dictionary<string, dynamic?>();

                //collect parameters from source record
                parms.Add("Id", row.Field<long>("Id"));
                parms.Add("Name", row.Field<string>("Name"));
                parms.Add("Order", row.Field<int>("Order"));

                var entity = new FAQCategory
                {
                    Id = LongToGuid(parms["Id"]),
                    Name = parms["Name"]!,
                    Order = parms["Order"]!,
                };

                await faqCategoryRepository.InsertAsync(entity);

                count++;

                ConsoleHelpers.ShowCount(count, totalCount, jumpback: true);
            }

            Console.WriteLine();

        }


        private async Task ImportFAQItems(DbConnection source)
        {
            Console.Write("Importing FAQItems...");
            ConsoleHelpers.ShowCount(0, 0);


            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = "SELECT * FROM FAQItems";

            DbDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sourceCommand;
            DataTable sourceTable = new DataTable();
            adapter.Fill(sourceTable);

            int totalCount = sourceTable.Rows.Count;
            long count = 0;

            foreach (DataRow row in sourceTable.Rows)
            {
                //prepare parameters
                var parms = new Dictionary<string, dynamic?>();

                //collect parameters from source record
                parms.Add("Id", row.Field<long>("Id"));
                parms.Add("Question", row.Field<string>("Question"));
                parms.Add("Answer", row.Field<string>("Answer"));
                parms.Add("Order", row.Field<int>("Order"));
                parms.Add("Category_Id", row.Field<long>("Category_Id"));

                var entity = new FAQItem
                {
                    Id = LongToGuid(parms["Id"]),
                    Question = parms["Question"]!,
                    Answer = parms["Answer"]!,
                    Order = parms["Order"]!,
                    CategoryId = LongToGuid(parms["Category_Id"]),
                };

                await faqItemRepository.InsertAsync(entity);

                count++;

                ConsoleHelpers.ShowCount(count, totalCount, jumpback: true);
            }

            Console.WriteLine();
        }


        private async Task ImportProvinces(DbConnection source)
        {
            Console.Write("Importing Provinces...");
            ConsoleHelpers.ShowCount(0, 0);


            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = "SELECT * FROM Provinces";

            DbDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sourceCommand;
            DataTable sourceTable = new DataTable();
            adapter.Fill(sourceTable);

            int totalCount = sourceTable.Rows.Count;
            long count = 0;

            foreach (DataRow row in sourceTable.Rows)
            {
                //prepare parameters
                var parms = new Dictionary<string, dynamic?>();

                //collect parameters from source record
                parms.Add("Code", row.Field<string>("Code"));
                parms.Add("Name", row.Field<string>("Name"));
                parms.Add("Language", row.Field<string>("Language"));
                parms.Add("UrlSlug", row.Field<string?>("UrlSlug"));

                var entity = new Province
                {
                    Id = StringToGuid(parms["Code"]),
                    Code = parms["Code"]!,
                    Name = parms["Name"]!,
                    Language = parms["Language"]!,
                    UrlSlug = parms["UrlSlug"]!,
                };

                await provinceRepository.InsertAsync(entity);

                count++;

                ConsoleHelpers.ShowCount(count, totalCount, jumpback: true);
            }

            Console.WriteLine();
        }


        private async Task ImportMarketConfigurations(DbConnection source)
        {
            Console.Write("Importing MarketConfigurations...");
            ConsoleHelpers.ShowCount(0, 0);


            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = "SELECT * FROM RMCategories";

            DbDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sourceCommand;
            DataTable sourceTable = new DataTable();
            adapter.Fill(sourceTable);

            int totalCount = sourceTable.Rows.Count;
            long count = 0;

            foreach (DataRow row in sourceTable.Rows)
            {
                //prepare parameters
                var parms = new Dictionary<string, dynamic?>();

                //collect parameters from source record
                parms.Add("RMCategoryID", row.Field<long>("RMCategoryID"));
                parms.Add("Name", row.Field<string>("Name"));
                parms.Add("Description", row.Field<string>("Description"));
                parms.Add("Price", row.Field<decimal>("Price"));
                parms.Add("IsActive", row.Field<bool>("IsActive"));
                parms.Add("MaximumThemes", row.Field<int>("MaximumThemes"));
                parms.Add("MaxCharacters", row.Field<int>("MaxCharacters"));
                parms.Add("AllowBanners", row.Field<bool>("AllowBanners"));
                parms.Add("AllowPoster", row.Field<bool>("AllowPoster"));

                var entity = new MarketConfiguration
                {
                    Id = LongToGuid(parms["RMCategoryID"]),
                    AllowBanners = parms["AllowBanners"]!,
                    AllowPoster = parms["AllowPoster"]!,
                    Description = parms["Description"]!,
                    IsActive = parms["IsActive"]!,
                    MaximumCharacters = parms["MaxCharacters"]!,
                    MaximumThemes = parms["MaximumThemes"]!,
                    Name = parms["Name"]!,
                    Price = parms["Price"]!,
                };

                await marketConfigRepository.InsertAsync(entity);

                count++;

                ConsoleHelpers.ShowCount(count, totalCount, jumpback: true);
            }

            Console.WriteLine();
        }

        private async Task ImportMarketThemes(DbConnection source)
        {
            Console.Write("Importing MarketThemes...");
            ConsoleHelpers.ShowCount(0, 0);


            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = "SELECT * FROM RMThemes";

            DbDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sourceCommand;
            DataTable sourceTable = new DataTable();
            adapter.Fill(sourceTable);

            int totalCount = sourceTable.Rows.Count;
            long count = 0;

            foreach (DataRow row in sourceTable.Rows)
            {
                //prepare parameters
                var parms = new Dictionary<string, dynamic?>();

                //collect parameters from source record
                parms.Add("RMThemeId", row.Field<long>("RMThemeId"));
                parms.Add("Name", row.Field<string>("Name"));
                parms.Add("Description", row.Field<string>("Description"));
                parms.Add("ImageUrl", row.Field<string>("ImageUrl"));
                parms.Add("IsDefault", row.Field<bool>("IsDefault"));
                parms.Add("IsActive", row.Field<bool>("IsActive"));

                var entity = new MarketTheme
                {
                    Id = LongToGuid(parms["RMThemeId"]),
                    Description = parms["Description"]!,
                    ImageUrl = parms["ImageUrl"]!,
                    IsActive = parms["IsActive"]!,
                    IsDefault = parms["IsDefault"]!,
                    Name = parms["Name"]!,
                };

                await marketThemeRepository.InsertAsync(entity);

                count++;

                ConsoleHelpers.ShowCount(count, totalCount, jumpback: true);
            }

            Console.WriteLine();
        }
    }
}

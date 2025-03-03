using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Domain.Affiliates;
using Rommelmarkten.Api.Domain.Content;
using Rommelmarkten.Api.Domain.Markets;
using Rommelmarkten.Api.Infrastructure.Persistence;
using System.Data;
using System.Data.Common;

namespace V2Importer
{
    public static class DbContextExtensions
    {

        public static string GetTableName<T>(this DbContext context) where T : class
        {
            var model = context.Model;
            var entityType = model.GetEntityTypes().First(t => t.ClrType == typeof(T));
            var tableNameAnnotation = entityType.GetAnnotation("Relational:TableName");
            return tableNameAnnotation?.Value?.ToString() ?? typeof(T).Name;
        }

    }

    public class Importer : IDisposable
    {
        public const string sourceDatabase = "Server=.\\SQLEXPRESS;Database=RommelmarktenV1;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
        public const string targetDatabase = "Server=.\\SQLEXPRESS;Database=RommelmarktenV2;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
        private readonly ApplicationDbContext target;

        public Importer(ApplicationDbContext target)
        {
            this.target = target;
        }

        public void Dispose()
        {
            target?.Dispose();
        }

        public async Task Import()
        {

            Console.Write("Connecting to source...");

            using (var source = new SqlConnection(sourceDatabase))
            {
                source.Open();
                Console.WriteLine("Connected.");

                Console.Write("Connecting to target...");


                var targetConnection = target.Database.GetDbConnection();
                targetConnection.Open();
                if (targetConnection.State == ConnectionState.Open)
                {
                    Console.WriteLine("Connected.");

                    Console.WriteLine();
                    Console.Write($"Before importing can continue, all records in the database {targetConnection.Database} will be deleted. \nContinue (y/N) ? ");
                    var answer = Console.ReadLine();
                    Console.WriteLine();

                    if (answer == "y")
                    {
                        DeleteAllRecords(targetConnection, target);
                        ImportUsers(source, targetConnection);
                    }
                    else
                    {
                        Console.WriteLine("Import cancelled by user.");
                    }
                }
                else
                {
                    Console.WriteLine("Failed.");
                }
            }
            Console.WriteLine("Disconnected from source.");
        }

        //public void DisableConstraints(DbConnection target)
        //{
        //    var sourceCommand = target.CreateCommand();
        //    sourceCommand.CommandText = $"USE {target.Database}; EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'";
        //    sourceCommand.ExecuteNonQuery();
        //}

        //public void EnableConstraints(DbConnection target)
        //{
        //    var sourceCommand = target.CreateCommand();
        //    sourceCommand.CommandText = $"USE {target.Database}; EXEC sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT all'";
        //    sourceCommand.ExecuteNonQuery();
        //}

        public void DeleteAllRecords(DbConnection target, ApplicationDbContext targetContext)
        {
            var d = targetContext.GetTableName<AffiliateAd>();

            Console.Write("Deleting all record in database...");

            var sourceCommand = target.CreateCommand();
            sourceCommand.CommandText = @$"
USE {target.Database};

EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all';

DELETE FROM [dbo].[AspNetRoleClaims];
DELETE FROM [dbo].[AspNetRoles];
DELETE FROM [dbo].[AspNetUserClaims];
DELETE FROM [dbo].[AspNetUserLogins];
DELETE FROM [dbo].[AspNetUserRoles];
DELETE FROM [dbo].[AspNetUsers];
DELETE FROM [dbo].[AspNetUserTokens];
DELETE FROM [dbo].[{targetContext.GetTableName<AffiliateAd>()}];
DELETE FROM [dbo].[{targetContext.GetTableName<BannerType>()}];
DELETE FROM [dbo].[{targetContext.GetTableName<FAQCategory>()}];
DELETE FROM [dbo].[{targetContext.GetTableName<FAQItem>()}];
DELETE FROM [dbo].[{targetContext.GetTableName<MarketConfiguration>()}];
DELETE FROM [dbo].[{targetContext.GetTableName<MarketDate>()}];
DELETE FROM [dbo].[{targetContext.GetTableName<MarketInvoiceLine>()}];
DELETE FROM [dbo].[{targetContext.GetTableName<MarketInvoiceReminder>()}];
DELETE FROM [dbo].[{targetContext.GetTableName<MarketInvoice>()}];
DELETE FROM [dbo].[{targetContext.GetTableName<MarketRevision>()}];
DELETE FROM [dbo].[{targetContext.GetTableName<MarketRevisionWithTheme>()}];
DELETE FROM [dbo].[{targetContext.GetTableName<Market>()}];
DELETE FROM [dbo].[{targetContext.GetTableName<MarketTheme>()}];
DELETE FROM [dbo].[{targetContext.GetTableName<MarketWithTheme>()}];
DELETE FROM [dbo].[{targetContext.GetTableName<NewsArticle>()}];
DELETE FROM [dbo].[{targetContext.GetTableName<Province>()}];

EXEC sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT all';
";
            int records = sourceCommand.ExecuteNonQuery();


            Console.WriteLine($"done ({records})");
        }

        public void ImportUsers(DbConnection source, DbConnection target)
        {
            //var sourceCommand = source.CreateCommand();
            //sourceCommand.CommandText = "SELECT * FROM AspNetUsers";
            //using (var reader = await sourceCommand.ExecuteReaderAsync())
            //{
            //    while (reader.Read())
            //    {
            //        var id = reader["Id"].ToString();
            //        var email = reader["Email"].ToString();
            //        //var passwordHash = reader["PasswordHash"].ToString();
            //        //var securityStamp = reader["SecurityStamp"].ToString();
            //        //var concurrencyStamp = reader["ConcurrencyStamp"].ToString();
            //        //var phoneNumber = reader["PhoneNumber"].ToString();
            //        //var phoneNumberConfirmed = reader["PhoneNumberConfirmed"].ToString();
            //        //var twoFactorEnabled = reader["TwoFactorEnabled"].ToString();
            //        //var lockoutEnd = reader["LockoutEnd"].ToString();
            //        //var lockoutEnabled = reader["LockoutEnabled"].ToString();
            //        //var accessFailedCount = reader["AccessFailedCount"].ToString();
            //        //var userName = reader["UserName"].ToString();
            //        //var normalizedUserName = reader["NormalizedUserName"].ToString();
            //        //var normalizedEmail = reader["NormalizedEmail"].ToString();
            //        //var emailConfirmed = reader["EmailConfirmed"].ToString();
            //        //var firstName = reader["FirstName"].ToString();
            //        //var lastName = reader["LastName"].ToString();
            //        //var street = reader["Street"].ToString();
            //        //var houseNumber = reader["HouseNumber"].ToString();
            //        //var postalCode = reader["PostalCode"].ToString();
            //        //var city = reader["City"].ToString();
            //        //var country = reader["Country"].ToString();
            //        //var birthDate = reader["BirthDate"].ToString();
            //    }
            //}
        }
    }
}

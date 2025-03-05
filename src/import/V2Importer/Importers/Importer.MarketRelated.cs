using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Domain.Affiliates;
using Rommelmarkten.Api.Domain.Markets;
using System.Data;
using System.Data.Common;

namespace V2Importer.Importers
{

    public static class DynamicDictionaryExtensions
    {

        public static void AddField<T> (this Dictionary<string, dynamic?> dictionary, string fieldName, DataRow row)
        {
            dictionary.Add(fieldName, row.Field<T>(fieldName));
        }

    }

    public static class DateTimeHelpers
    {

        public static DateTime? GetDateAsUtc(DateTime? datetime)
        {
            if (datetime.HasValue)
            {
                var dateTimeOffset = new DateTimeOffset(datetime.Value);
                return dateTimeOffset.UtcDateTime;
            }
            return null;
        }
    }


    public partial class Importer
    {

        private DateTime? GetLastModifiedDate(DateTime? dateLastAdminUpdate, DateTime? dateLastUserUpdate)
        {
            if(dateLastAdminUpdate != null && dateLastUserUpdate != null)
            {
                return dateLastAdminUpdate >= dateLastUserUpdate ? dateLastAdminUpdate : dateLastUserUpdate;
            }
            else if(dateLastAdminUpdate != null)
            {
                return dateLastAdminUpdate;
            }
            else if (dateLastUserUpdate != null)
            {
                return dateLastUserUpdate;
            }
            else
            {
                return null;
            }
        }
        private string? GetLastModifierId(DateTime? dateLastAdminUpdate, DateTime? dateLastUserUpdate, string? userId)
        {
            if (dateLastAdminUpdate != null && dateLastUserUpdate != null)
            {
                return dateLastAdminUpdate >= dateLastUserUpdate ? alexAdminCurrentUserService.UserId : userId;
            }
            else if (dateLastAdminUpdate != null)
            {
                return alexAdminCurrentUserService.UserId;
            }
            else if (dateLastUserUpdate != null)
            {
                return userId;
            }
            else
            {
                return null;
            }
        }

        private async Task ImportMarkets(DbConnection source)
        {
            Console.Write("Importing Markets...");
            ConsoleHelpers.ShowCount(0, 0);


            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = "SELECT * FROM RMs";

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
                parms.AddField<long>("RMid", row);
                parms.AddField<long?>("RMTemp_Original_RMid", row);
                parms.AddField<string?>("Province_Code", row);
                parms.AddField<string?>("Title", row);
                parms.AddField<string?>("Description", row);
                parms.AddField<decimal>("EntryPrice", row);
                parms.AddField<decimal>("StandPrice", row);
                parms.AddField<string?>("Hall", row);
                parms.AddField<string>("Address", row);
                parms.AddField<string>("PostalCode", row);
                parms.AddField<string>("City", row);
                parms.AddField<string>("Country", row);
                parms.AddField<string?>("CoordLatitude", row);
                parms.AddField<string?>("CoordLongitude", row);
                parms.AddField<DateTime>("DateCreated", row);
                parms.AddField<DateTime?>("DateLastAdminUpdate", row);
                parms.AddField<DateTime?>("DateLastUserUpdate", row);
                parms.AddField<string>("Image", row);
                parms.AddField<string>("ImageThumb", row);
                parms.AddField<string>("OrganizerName", row);
                parms.AddField<bool>("ShowOrganizerName", row);
                parms.AddField<string>("OrganizerPhone", row);
                parms.AddField<bool>("ShowOrganizerPhone", row);
                parms.AddField<string>("OrganizerEmail", row);
                parms.AddField<bool>("ShowOrganizerEmail", row);
                parms.AddField<string?>("OrganizerURL", row);
                parms.AddField<string?>("OrganizerContactNotes", row);
                parms.AddField<bool>("IsActive", row);
                parms.AddField<bool>("IsSuspended", row);
                parms.AddField<long?>("BannerType_RMBannerTypeID", row);
                parms.AddField<long>("Category_RMCategoryID", row);
                parms.AddField<long>("MainTheme_RMThemeId", row);
                parms.AddField<string>("User_Id", row);

                if (parms["RMTemp_Original_RMid"] == null)
                {
                    ((ImportCurrentUserService)currentUserService).UserId = parms["User_Id"]!;
                    ((ImporterDatetime)dateTimeService).Now = parms["DateCreated"]!;

                    var entity = new Market
                    {
                        Id = LongToGuid(parms["RMid"]),
                        BannerTypeId = parms["BannerType_RMBannerTypeID"] != null ? LongToGuid(parms["BannerType_RMBannerTypeID"]) : null,
                        ConfigurationId = LongToGuid(parms["Category_RMCategoryID"])!,
                        //Created = dateTimeService.Now,
                        //CreatedBy = currentUserService.UserId,
                        DateLastAdminUpdate = DateTimeHelpers.GetDateAsUtc(parms["DateLastAdminUpdate"]),
                        DateLastUserUpdate = DateTimeHelpers.GetDateAsUtc(parms["DateLastUserUpdate"]),
                        Description = parms["Description"]!,
                        Image = new MarketImage
                        {
                            ImageThumbUrl = parms["ImageThumb"]!,
                            ImageUrl = parms["Image"]!,
                        },
                        IsActive = parms["IsActive"]!,
                        IsSuspended = parms["IsSuspended"]!,
                        LastModified = GetLastModifiedDate(
                                DateTimeHelpers.GetDateAsUtc(parms["DateLastUserUpdate"]),
                                DateTimeHelpers.GetDateAsUtc(parms["DateLastAdminUpdate"])),
                        LastModifiedBy = GetLastModifierId(
                                DateTimeHelpers.GetDateAsUtc(parms["DateLastUserUpdate"]),
                                DateTimeHelpers.GetDateAsUtc(parms["DateLastAdminUpdate"]),
                                parms["User_Id"]!),
                        Location = new MarketLocation
                        {
                            Address = parms["Address"]!,
                            City = parms["City"]!,
                            CoordLatitude = parms["CoordLatitude"] != null ? double.Parse(parms["CoordLatitude"]) : null,
                            CoordLongitude = parms["CoordLongitude"] != null ? double.Parse(parms["CoordLongitude"]) : null,
                            Country = parms["Country"]!,
                            Hall = parms["Hall"],
                            PostalCode = parms["PostalCode"]!,
                        },
                        Organizer = new Organizer
                        {
                            ContactNotes = parms["OrganizerContactNotes"],
                            Email = parms["OrganizerEmail"]!,
                            Name = parms["OrganizerName"]!,
                            Phone = parms["OrganizerPhone"]!,
                            ShowEmail = parms["ShowOrganizerEmail"],
                            ShowName = parms["ShowOrganizerName"],
                            ShowPhone = parms["ShowOrganizerPhone"],
                            URL = parms["OrganizerURL"],
                        },
                        Pricing = new MarketPricing
                        {
                            EntryPrice = parms["EntryPrice"]!,
                            StandPrice = parms["StandPrice"]!,
                        },
                        ProvinceId = parms["Province_Code"] != null ? StringToGuid(parms["Province_Code"]) : null,
                        Title = parms["Title"] ?? "",
                    };

                    ((ImportCurrentUserService)currentUserService).UserId = default;
                    ((ImporterDatetime)dateTimeService).Now = default;


                    await marketRepository.InsertAsync(entity);

                }
                else
                {
                    //revision
                    ((ImportCurrentUserService)currentUserService).UserId = parms["User_Id"]!;
                    ((ImporterDatetime)dateTimeService).Now = parms["DateCreated"]!;

                    var entity = new MarketRevision
                    {
                        Id = LongToGuid(parms["RMid"]),
                        RevisedMarketId = LongToGuid(parms["RMTemp_Original_RMid"]),
                        Created = dateTimeService.Now,
                        CreatedBy = currentUserService.UserId,
                        Description = parms["Description"]!,
                        Image = new MarketImage
                        {
                            ImageThumbUrl = parms["ImageThumb"]!,
                            ImageUrl = parms["Image"]!,
                        },
                        LastModified = GetLastModifiedDate(
                                DateTimeHelpers.GetDateAsUtc(parms["DateLastUserUpdate"]),
                                DateTimeHelpers.GetDateAsUtc(parms["DateLastAdminUpdate"])),
                        LastModifiedBy = GetLastModifierId(
                                DateTimeHelpers.GetDateAsUtc(parms["DateLastUserUpdate"]),
                                DateTimeHelpers.GetDateAsUtc(parms["DateLastAdminUpdate"]),
                                parms["User_Id"]!),
                        Location = new MarketLocation
                        {
                            Address = parms["Address"]!,
                            City = parms["City"]!,
                            CoordLatitude = parms["CoordLatitude"] != null ? double.Parse(parms["CoordLatitude"]) : null,
                            CoordLongitude = parms["CoordLongitude"] != null ? double.Parse(parms["CoordLongitude"]) : null,
                            Country = parms["Country"]!,
                            Hall = parms["Hall"],
                            PostalCode = parms["PostalCode"]!,
                        },
                        Organizer = new Organizer
                        {
                            ContactNotes = parms["OrganizerContactNotes"],
                            Email = parms["OrganizerEmail"]!,
                            Name = parms["OrganizerName"]!,
                            Phone = parms["OrganizerPhone"]!,
                            ShowEmail = parms["ShowOrganizerEmail"],
                            ShowName = parms["ShowOrganizerName"],
                            ShowPhone = parms["ShowOrganizerPhone"],
                            URL = parms["OrganizerURL"],
                        },
                        Pricing = new MarketPricing
                        {
                            EntryPrice = parms["EntryPrice"]!,
                            StandPrice = parms["StandPrice"]!,
                        },
                        Title = parms["Title"] ?? "",
                    };

                    ((ImportCurrentUserService)currentUserService).UserId = default;
                    ((ImporterDatetime)dateTimeService).Now = default;


                    await marketRevisionRepository.InsertAsync(entity);
                }

                count++;

                ConsoleHelpers.ShowCount(count, totalCount, jumpback: true);
            }

            Console.WriteLine();

        }

        private async Task ImportMarketDates(DbConnection source)
        {
            Console.Write("Importing MarketDates...");
            ConsoleHelpers.ShowCount(0, 0);


            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = @"
SELECT * FROM RMDates 
WHERE RM_RMId IN (SELECT RMid FROM RMs WHERE RMTemp_Original_RMid IS NULL)
";

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
                parms.AddField<long>("RMDateId", row);
                parms.AddField<long>("RM_RMid", row);
                parms.AddField<DateTime>("Date", row);
                parms.AddField<short>("StartHour", row);
                parms.AddField<short>("StartMinutes", row);
                parms.AddField<short>("StopHour", row);
                parms.AddField<short>("StopMinutes", row);
                parms.AddField<bool>("IsActive", row);

                var entity = new MarketDate
                {
                    Id = LongToGuid(parms["RMDateId"]),
                    ParentMarketId = LongToGuid(parms["RM_RMid"]),
                    Date = DateOnly.FromDateTime(DateTimeHelpers.GetDateAsUtc(parms["Date"])),
                    StartHour = parms["StartHour"]!,
                    StartMinutes = parms["StartMinutes"]!,
                    StopHour = parms["StopHour"]!,
                    StopMinutes = parms["StopMinutes"]!,
                    IsActive = parms["IsActive"]!,
                };

                await marketDateRepository.InsertAsync(entity);

                count++;

                ConsoleHelpers.ShowCount(count, totalCount, jumpback: true);
            }

            Console.WriteLine();

        }

        private string CreateInvoiceNumber(long count, DateTime dateTime)
        {
            return $"RM-{dateTime:yyyyMMdd}-{count}";
        }
        private PaymentStatus CreateInvoiceStatus(bool? isPaid, decimal bannerPrice, decimal daysPrice, string comments)
        {
            decimal total = bannerPrice + daysPrice;
            if (isPaid.HasValue)
            {
                if (total == 0)
                {
                    return PaymentStatus.Free;
                }
                else
                {
                    return PaymentStatus.Paid;
                }
            }
            else
            {
                if (total == 0)
                {
                    return PaymentStatus.Free;
                }
                else
                {
                    return PaymentStatus.NotPaid;
                }
            }
        }
        private IEnumerable<MarketInvoiceLine> CreateInvoiceLines(Guid invoiceId, decimal bannerPrice, decimal daysPrice, int days, string? bannerDescription)
        {
            if (bannerPrice + daysPrice > 0)
            {
                if (bannerPrice > 0)
                {
                    yield return new MarketInvoiceLine
                    {
                        Id = Guid.NewGuid(),
                        ParentInvoiceId = invoiceId,
                        Amount = bannerPrice,
                        Subject = bannerDescription ?? "Banner toevoegen",
                    };
                }
                if (daysPrice > 0)
                {
                    yield return new MarketInvoiceLine
                    {
                        Id = Guid.NewGuid(),
                        ParentInvoiceId = invoiceId,
                        Amount = bannerPrice,
                        Subject = $"Advertentie weergeven voor {days} {(days > 1 ? "dagen" : "dag")}",
                    };
                }
            }
        }


        private async Task ImportMarketInvoices(DbConnection source)
        {
            Console.Write("Importing MarketInvoices...");
            ConsoleHelpers.ShowCount(0, 0);

            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = @"
SELECT rs.RMSaleId,
      rs.Old_CatId,
      rs.DaysPrice,
      rs.BannerTypePrice,
      rs.Old_AlgemeenTotaal,
      rs.Comments,
      rs.IsPaid,
      rs.RM_RMid,
	  rm.DateCreated,
	  bt.Name as BannerName,
	  bt.Description as BannerDescription,
	  (SELECT COUNT(*) FROM RMDates rd WHERE rm.RMid = rd.RM_RMid) as DaysCount
  FROM RMSales rs
  JOIN RMs rm ON rm.RMid = rs.RM_RMid
  LEFT OUTER JOIN RMBannerTypes bt ON rm.BannerType_RMBannerTypeID = bt.RMBannerTypeID
";

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
                parms.AddField<long>("RMSaleId", row);
                parms.AddField<long>("RM_RMid", row);
                parms.AddField<long?>("Old_CatId", row);
                parms.AddField<decimal>("DaysPrice", row);
                parms.AddField<decimal>("BannerTypePrice", row);
                parms.AddField<decimal?>("Old_AlgemeenTotaal", row);
                parms.AddField<string>("Comments", row);
                parms.AddField<bool>("IsPaid", row);

                parms.AddField<DateTime>("DateCreated", row);
                parms.AddField<string?>("BannerName", row);
                parms.AddField<string?>("BannerDescription", row);
                parms.AddField<int>("DaysCount", row);

                var entity = new MarketInvoice
                {
                    Id = LongToGuid(parms["RMSaleId"]),
                    MarketId = LongToGuid(parms["RM_RMid"]),
                    InvoiceNumber = CreateInvoiceNumber(count, DateTimeHelpers.GetDateAsUtc(parms["DateCreated"])),
                    Status = CreateInvoiceStatus(parms["IsPaid"], parms["BannerTypePrice"], parms["DaysPrice"], parms["Comments"]),
                    StatusChanged = DateTimeHelpers.GetDateAsUtc(parms["DateCreated"]),
                };

                IEnumerable<MarketInvoiceLine> lines = CreateInvoiceLines(
                    entity.Id,
                    parms["BannerTypePrice"],
                    parms["DaysPrice"],
                    parms["DaysCount"],
                    parms["BannerDescription"]
                );
                entity.InvoiceLines = lines.ToList();
                
                foreach(var line in entity.InvoiceLines)
                {
                    target.Entry(line).State = EntityState.Added;
                }

                await marketInvoiceRepository.InsertAsync(entity);

                count++;

                ConsoleHelpers.ShowCount(count, totalCount, jumpback: true);
            }

            Console.WriteLine();
        }



        private async Task ImportMarketInvoiceReminders(DbConnection source)
        {
            Console.Write("Importing MarketInvoiceReminders...");
            ConsoleHelpers.ShowCount(0, 0);

            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = @"SELECT * FROM RMPaymentReminders";

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
                parms.AddField<long>("RMPaymentReminderId", row);
                parms.AddField<long>("RMSale_RMSaleId", row);
                parms.AddField<DateTime>("SendDate", row);

                var entity = new MarketInvoiceReminder
                {
                    Id = LongToGuid(parms["RMPaymentReminderId"]),
                    ParentInvoiceId = LongToGuid(parms["RMSale_RMSaleId"]),
                    SentDate = DateTimeHelpers.GetDateAsUtc(parms["SendDate"]),
                };

                await marketInvoiceReminderRepository.InsertAsync(entity);

                count++;

                ConsoleHelpers.ShowCount(count, totalCount, jumpback: true);
            }

            Console.WriteLine();
        }


        private async Task ImportMarketAndRevisionWithTheme(DbConnection source)
        {
            Console.Write("Importing related Themes...");
            ConsoleHelpers.ShowCount(0, 0);

            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = @"
SELECT rwt.RMThemeId,
       rwt.RMId,
	   rm.RMTemp_Original_RMid as OriginalRMId,
	   rm.MainTheme_RMThemeId as MainThemeId
  FROM RMsWithThemes rwt
  JOIN RMs rm ON rm.RMid = rwt.RMId
";

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
                parms.AddField<long>("RMThemeId", row);
                parms.AddField<long>("RMId", row);
                parms.AddField<long?>("OriginalRMId", row);
                parms.AddField<long>("MainThemeId", row);

                if (parms["OriginalRMId"] == null)
                {
                    var entity = new MarketWithTheme
                    {
                        IsDefault = (parms["RMThemeId"] == parms["MainThemeId"]),
                        ThemeId = LongToGuid(parms["RMThemeId"]),
                        MarketId = LongToGuid(parms["RMId"]),
                    };
                    await marketWithThemeRepository.InsertAsync(entity);
                }
                else
                {
                    var entity = new MarketRevisionWithTheme
                    {
                        IsDefault = (parms["RMThemeId"] == parms["MainThemeId"]),
                        ThemeId = LongToGuid(parms["RMThemeId"]),
                        MarketRevisionId = LongToGuid(parms["RMId"]),
                    };
                    await marketRevisionWithThemeRepository.InsertAsync(entity);
                }

                count++;

                ConsoleHelpers.ShowCount(count, totalCount, jumpback: true);
            }

            Console.WriteLine();
        }
    }
}

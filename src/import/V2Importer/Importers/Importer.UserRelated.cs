using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Features.Affiliates.Domain;
using Rommelmarkten.Api.Features.Users.Domain;
using System.Data;
using System.Data.Common;

namespace V2Importer.Importers
{

    public partial class Importer
    {
       
        private async Task ImportUsers(DbConnection source)
        {
            Console.Write("Importing Users...");
            ConsoleHelpers.ShowCount(0, 0);
            

            var sourceUsersCommand = source.CreateCommand();
            sourceUsersCommand.CommandText = "SELECT * FROM AspNetUsers";

            DbDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sourceUsersCommand;
            DataTable usersTable = new DataTable();
            adapter.Fill(usersTable);

            int totalCount = usersTable.Rows.Count;
            long count = 0;

            foreach (DataRow row in usersTable.Rows)
            {
                //prepare parameters
                var parms = new Dictionary<string, dynamic?>();

                //collect parameters from source record
                parms.Add("Id", row.Field<string>("Id"));
                parms.Add("IsBlocked", row.Field<bool>("IsBlocked"));
                parms.Add("SendMail", row.Field<bool>("SendMail"));
                parms.Add("ActivationDate", row.Field<DateTime?>("ActivationDate"));
                parms.Add("LastActivityDate", row.Field<DateTime?>("LastActivityDate"));
                parms.Add("LastPasswordResetDate", row.Field<DateTime?>("LastPasswordResetDate"));
                parms.Add("PasswordResetCount", row.Field<int>("PasswordResetCount"));
                parms.Add("Email", row.Field<string?>("Email"));
                parms.Add("EmailConfirmed", row.Field<bool>("EmailConfirmed"));
                parms.Add("PasswordHash", row.Field<string?>("PasswordHash"));
                parms.Add("SecurityStamp", row.Field<string?>("SecurityStamp"));
                parms.Add("PhoneNumber", row.Field<string?>("PhoneNumber"));
                parms.Add("PhoneNumberConfirmed", row.Field<bool>("PhoneNumberConfirmed"));
                parms.Add("TwoFactorEnabled", row.Field<bool>("TwoFactorEnabled"));
                parms.Add("LockoutEndDateUtc", row.Field<DateTime?>("LockoutEndDateUtc"));
                parms.Add("LockoutEnabled", row.Field<bool>("LockoutEnabled"));
                parms.Add("AccessFailedCount", row.Field<int>("AccessFailedCount"));
                parms.Add("UserName", row.Field<string>("UserName"));

                //get related RMUSer
                var sourceRMUserCommand = source.CreateCommand();
                sourceRMUserCommand.CommandText = "SELECT * FROM RMUsers WHERE Id=@Id";
                sourceRMUserCommand.Parameters.Add(new SqlParameter("Id", parms["Id"]));
                var rmUserReader = await sourceRMUserCommand.ExecuteReaderAsync();
                if (rmUserReader.Read())
                {
                    parms.Add("RMUser_Name", rmUserReader.GetValue<string>("Name"));
                    parms.Add("RMUser_Address", rmUserReader.GetValue<string?>("Address"));
                    parms.Add("RMUser_PostalCode", rmUserReader.GetValue<string?>("PostalCode"));
                    parms.Add("RMUser_City", rmUserReader.GetValue<string?>("City"));
                    parms.Add("RMUser_Country", rmUserReader.GetValue<string?>("Country"));
                    parms.Add("RMUser_VAT", rmUserReader.GetValue<string?>("VAT"));
                    parms.Add("RMUser_ActivationRemindersSent", rmUserReader.GetValue<int?>("ActivationRemindersSent"));
                    parms.Add("RMUser_LastActivationMailSendDate", rmUserReader.GetValue<DateTime?>("LastActivationMailSendDate"));
                }
                else
                {
                    throw new Exception("RMUser not found for user with id " + parms["Id"]);
                }

                //construct insert for Identity records
                string insertSql = $@"
                    INSERT INTO AspNetUsers (
                     Id
                    ,FirstName
                    ,LastName
                    ,UserName
                    ,NormalizedUserName
                    ,Email
                    ,NormalizedEmail
                    ,EmailConfirmed
                    ,PasswordHash
                    ,SecurityStamp
                    ,ConcurrencyStamp
                    ,PhoneNumber
                    ,PhoneNumberConfirmed
                    ,TwoFactorEnabled
                    ,LockoutEnd
                    ,LockoutEnabled
                    ,AccessFailedCount
                    ) 
                    VALUES 
                    (
                    @Id,
                    @FirstName,
                    @LastName,
                    @UserName,
                    @NormalizedUserName,
                    @Email,
                    @NormalizedEmail,
                    @EmailConfirmed,
                    @PasswordHash,
                    @SecurityStamp,
                    @ConcurrencyStamp,
                    @PhoneNumber,
                    @PhoneNumberConfirmed,
                    @TwoFactorEnabled,
                    @LockoutEnd,
                    @LockoutEnabled,
                    @AccessFailedCount
                    )
                    ";

                target.Database.ExecuteSqlRaw(
                    insertSql,
                    [
                        new SqlParameter("Id", parms["Id"] ?? DBNull.Value),
                        new SqlParameter("FirstName", DBNull.Value),
                        new SqlParameter("LastName", DBNull.Value),
                        new SqlParameter("UserName", parms["UserName"] ?? DBNull.Value),
                        new SqlParameter("NormalizedUserName", Normalize(parms["UserName"]) ?? DBNull.Value), //mimics default identity normalization
                        new SqlParameter("Email", parms["Email"] ?? DBNull.Value),
                        new SqlParameter("NormalizedEmail",Normalize(parms["Email"]) ?? DBNull.Value), //mimics default identity normalization
                        new SqlParameter("EmailConfirmed", parms["EmailConfirmed"] ?? DBNull.Value),
                        new SqlParameter("PasswordHash", parms["PasswordHash"] ?? DBNull.Value),
                        new SqlParameter("SecurityStamp", parms["SecurityStamp"] ?? DBNull.Value),
                        new SqlParameter("ConcurrencyStamp", Guid.NewGuid().ToString("N")), //mimics default identity concurrency stamp
                        new SqlParameter("PhoneNumber", parms["PhoneNumber"] ?? DBNull.Value),
                        new SqlParameter("PhoneNumberConfirmed", parms["PhoneNumberConfirmed"] ?? DBNull.Value),
                        new SqlParameter("TwoFactorEnabled", parms["TwoFactorEnabled"] ?? DBNull.Value),
                        new SqlParameter("LockoutEnd", parms["LockoutEndDateUtc"] ?? DBNull.Value),
                        new SqlParameter("LockoutEnabled", parms["LockoutEnabled"] ?? DBNull.Value),
                        new SqlParameter("AccessFailedCount", parms["AccessFailedCount"] ?? DBNull.Value),
                    ]
                );
               
                var userProfile = new UserProfile
                {
                    OwnedBy = parms["Id"]!,
                    Consented = true,
                    IsBanned = parms["IsBlocked"],
                    ActivationDate = parms["ActivationDate"],
                    LastActivityDate = parms["LastActivityDate"],
                    LastPasswordResetDate = parms["LastPasswordResetDate"],

                    Name = parms["RMUser_Name"] ?? "",
                    Address = parms["RMUser_Address"],
                    PostalCode = parms["RMUser_PostalCode"],
                    City = parms["RMUser_City"],
                    Country = parms["RMUser_Country"],
                    ActivationRemindersSent = parms["RMUser_ActivationRemindersSent"] ?? 0,
                    LastActivationMailSendDate = parms["RMUser_LastActivationMailSendDate"],
                    VAT = parms["RMUser_VAT"],
                };

                await profileRepository.InsertAsync(userProfile);

                count++;

                ConsoleHelpers.ShowCount(count, totalCount, jumpback: true);
            }

            Console.WriteLine();

        }

        private async Task ImportRoles(DbConnection source)
        {
            Console.Write("Importing Roles...");
            ConsoleHelpers.ShowCount(0, 0);

            var sourceRolesCommand = source.CreateCommand();
            sourceRolesCommand.CommandText = "SELECT * FROM AspNetRoles";

            DbDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sourceRolesCommand;
            DataTable rolesTable = new DataTable();
            adapter.Fill(rolesTable);

            int totalCount = rolesTable.Rows.Count;
            long count = 0;

            foreach (DataRow row in rolesTable.Rows)
            {
                //prepare parameters
                var parms = new Dictionary<string, dynamic?>();

                //collect parameters from source record
                parms.Add("Id", row.Field<string>("Id"));
                parms.Add("Name", row.Field<string>("Name"));

                if(parms["Id"] == "_CMSADMIN")  //skip this role
                {
                    continue;
                }

                //construct insert for Identity records
                string insertSql = $@"
                    INSERT INTO AspNetRoles (
                     Id
                    ,Name
                    ,NormalizedName
                    ,ConcurrencyStamp
                    ) 
                    VALUES 
                    (
                    @Id,
                    @Name,
                    @NormalizedName,
                    @ConcurrencyStamp
                    ) 
                    ";

                target.Database.ExecuteSqlRaw(
                    insertSql,
                    [
                        new SqlParameter("Id", parms["Id"] ?? DBNull.Value),
                        new SqlParameter("Name", parms["Name"] ?? DBNull.Value),
                        new SqlParameter("NormalizedName", Normalize(parms["Name"]) ?? DBNull.Value), //mimics default identity normalization
                        new SqlParameter("ConcurrencyStamp", Guid.NewGuid().ToString("N")), //mimics default identity concurrency stamp
                    ]
                );

                //add new claims to old role
                var roleClaimsParms = new Dictionary<string, dynamic?>();

                //collect parameters from source record
                roleClaimsParms.Add("RoleId", row.Field<string>("Id"));

                if (roleClaimsParms["RoleId"] == "ADMIN")
                {
                    roleClaimsParms.Add("ClaimType", "IsAdmin");
                    roleClaimsParms.Add("ClaimValue", "true");
                }
                else if(roleClaimsParms["RoleId"] == "USER")
                {
                    roleClaimsParms.Add("ClaimType", "IsUser");
                    roleClaimsParms.Add("ClaimValue", "true");
                }

                string insertRoleClaimSql = $@"
                    INSERT INTO AspNetRoleClaims (
                    RoleId
                    ,ClaimType
                    ,ClaimValue
                    ) 
                    VALUES 
                    (
                    @RoleId,
                    @ClaimType,
                    @ClaimValue
                    )
                    ";

                target.Database.ExecuteSqlRaw(
                    insertRoleClaimSql,
                    [
                        new SqlParameter("RoleId", roleClaimsParms["RoleId"] ?? DBNull.Value),
                        new SqlParameter("ClaimType", roleClaimsParms["ClaimType"] ?? DBNull.Value),
                        new SqlParameter("ClaimValue", roleClaimsParms["ClaimValue"] ?? DBNull.Value),
                    ]
                );

                count++;

                ConsoleHelpers.ShowCount(count, totalCount, jumpback: true);
            }

            Console.WriteLine();

            await Task.CompletedTask;
        }

        private async Task ImportUserRoles(DbConnection source)
        {
            Console.Write("Importing UserRoles...");
            ConsoleHelpers.ShowCount(0, 0);

            var sourceRolesCommand = source.CreateCommand();
            sourceRolesCommand.CommandText = "SELECT * FROM AspNetUserRoles";

            DbDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sourceRolesCommand;
            DataTable rolesTable = new DataTable();
            adapter.Fill(rolesTable);

            int totalCount = rolesTable.Rows.Count;
            long count = 0;

            foreach (DataRow row in rolesTable.Rows)
            {
                //prepare parameters
                var parms = new Dictionary<string, dynamic?>();

                //collect parameters from source record
                parms.Add("UserId", row.Field<string>("UserId"));
                parms.Add("RoleId", row.Field<string>("RoleId"));

                if (parms["RoleId"] == "_CMSADMIN")  //skip this role
                {
                    continue;
                }

                //construct insert for Identity records
                string insertSql = $@"
                    INSERT INTO AspNetUserRoles (
                     UserId
                    ,RoleId
                    ) 
                    VALUES 
                    (
                    @UserId,
                    @RoleId
                    )
                    ";

                target.Database.ExecuteSqlRaw(
                    insertSql,
                    [
                        new SqlParameter("UserId", parms["UserId"] ?? DBNull.Value),
                        new SqlParameter("RoleId", parms["RoleId"] ?? DBNull.Value),
                    ]
                );

                count++;

                ConsoleHelpers.ShowCount(count, totalCount, jumpback: true);
            }

            Console.WriteLine();

            await Task.CompletedTask;
        }

        private async Task ImportAffiliateAds(DbConnection source)
        {
            Console.Write("Importing AffiliateAds...");
            ConsoleHelpers.ShowCount(0, 0);


            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = "SELECT * FROM RMAffiliateBanners";

            DbDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sourceCommand;
            DataTable sourceTable = new DataTable();
            adapter.Fill(sourceTable);

            int totalCount = sourceTable.Rows.Count;
            long count = 0;


            ((ImportCurrentUserService)currentUserService).UserId = alexAdminCurrentUserService.UserId;
            ((ImporterDatetime)dateTimeService).Now = DateTime.UtcNow;

            foreach (DataRow row in sourceTable.Rows)
            {
                //prepare parameters
                var parms = new Dictionary<string, dynamic?>();

                //collect parameters from source record
                parms.Add("Id", row.Field<long>("Id"));
                parms.Add("ImageUrl", row.Field<string>("ImageUrl"));
                parms.Add("AffiliateName", row.Field<string>("AffiliateName"));
                parms.Add("AffiliateURL", row.Field<string>("AffiliateURL"));
                parms.Add("Order", row.Field<int>("Order"));
                parms.Add("IsActive", row.Field<bool>("IsActive"));



                var entity = new AffiliateAd
                {
                    Id = LongToGuid(parms["Id"]),
                    AdContent = null,
                    AffiliateName = parms["AffiliateName"]!,
                    AffiliateURL = parms["AffiliateURL"]!,
                    IsActive = parms["IsActive"]!,
                    Order = parms["Order"]!,
                    ImageUrl = parms["ImageUrl"]!,
                    LastModified = null,
                    LastModifiedBy = null,
                };

                await affiliateAdRepository.InsertAsync(entity);


                count++;

                ConsoleHelpers.ShowCount(count, totalCount, jumpback: true);
            }

            ((ImportCurrentUserService)currentUserService).UserId = default;
            ((ImporterDatetime)dateTimeService).Now = default;

            Console.WriteLine();

        }
    }
}

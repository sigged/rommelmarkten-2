using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Users;
using Rommelmarkten.Api.Infrastructure.Persistence;
using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace V2Importer.Importers
{

    public class UserImporter
    {
        private readonly ApplicationDbContext target;
        private readonly IEntityRepository<UserProfile> profileRepository;

        public UserImporter(ApplicationDbContext target, IEntityRepository<UserProfile> profileRepository)
        {
            this.target = target;
            this.profileRepository = profileRepository;
        }
        public async Task Import(DbConnection source)
        {
            
            //await ImportUsers(source);
            await ImportRoles(source);
            await ImportUserRoles(source);
        }
        private string? Normalize(string input)
        {
            return input?.Normalize(NormalizationForm.FormKD).ToLowerInvariant();
        }

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
                        new SqlParameter("NormalizedEmail", parms["Email"] ?? DBNull.Value),
                        new SqlParameter("EmailConfirmed", Normalize(parms["EmailConfirmed"]) ?? DBNull.Value), //mimics default identity normalization
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
                    UserId = parms["Id"]!,
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
    }
}

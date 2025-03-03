using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Infrastructure.Persistence;
using System.Data;
using System.Data.Common;

namespace V2Importer.Importers
{

    public static class DbDataReaderExtensions
    {

        public static T GetValue<T>(this DbDataReader reader, string name)
        {
            if (reader[name] == DBNull.Value)
            {
                return default!;
            }
            else
            {
                return reader.GetFieldValue<T>(name);
            }
        }
    }

    public class UserImporter
    {
        private readonly ApplicationDbContext target;

        public UserImporter(ApplicationDbContext target)
        {
            this.target = target;
        }
        public async Task Import(DbConnection source)
        {
            
            await ImportUsers(source);
            //await ImportRoles(source);
            //await ImportUserRoles(source);
        }

        private async Task ImportUsers(DbConnection source)
        {
            Console.Write("Importing Users...");

            var sourceUsersCommand = source.CreateCommand();
            sourceUsersCommand.CommandText = "SELECT * FROM AspNetUsers";

            DbDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sourceUsersCommand;
            DataTable usersTable = new DataTable();
            adapter.Fill(usersTable);

            long count = 0;

            foreach (DataRow row in usersTable.Rows)
            {
                //prepare parameters
                var parms = new Dictionary<string, dynamic?> {
                        { "Id", null },
                        { "IsBlocked", null },
                        { "SendMail", null }, //deprecated in v2
                        { "ActivationDate", null },
                        { "LastActivityDate", null },
                        { "LastPasswordResetDate", null },
                        { "PasswordResetCount", null },
                        { "Email", null },
                        { "EmailConfirmed", null },
                        { "PasswordHash", null },
                        { "SecurityStamp", null },
                        { "PhoneNumber", null },
                        { "PhoneNumberConfirmed", null },
                        { "TwoFactorEnabled", null },
                        { "LockoutEndDateUtc", null },
                        { "LockoutEnabled", null },
                        { "AccessFailedCount", null },
                        { "UserName", null },
                    };

                //collect parameters from source record
                parms["Id"] = row.Field<string>("Id");
                parms["IsBlocked"] = row.Field<bool>("IsBlocked");
                parms["SendMail"] = row.Field<bool>("SendMail");
                parms["ActivationDate"] = row.Field<DateTime?>("ActivationDate");
                parms["LastActivityDate"] = row.Field<DateTime?>("LastActivityDate");
                parms["LastPasswordResetDate"] = row.Field<DateTime?>("LastPasswordResetDate");
                parms["PasswordResetCount"] = row.Field<int>("PasswordResetCount");
                parms["Email"] = row.Field<string?>("Email");
                parms["EmailConfirmed"] = row.Field<bool>("EmailConfirmed");
                parms["PasswordHash"] = row.Field<string?>("PasswordHash");
                parms["SecurityStamp"] = row.Field<string?>("SecurityStamp");
                parms["PhoneNumber"] = row.Field<string?>("PhoneNumber");
                parms["PhoneNumberConfirmed"] = row.Field<bool>("PhoneNumberConfirmed");
                parms["TwoFactorEnabled"] = row.Field<bool>("TwoFactorEnabled");
                parms["LockoutEndDateUtc"] = row.Field<DateTime?>("LockoutEndDateUtc");
                parms["LockoutEnabled"] = row.Field<bool>("LockoutEnabled");
                parms["AccessFailedCount"] = row.Field<int>("AccessFailedCount");
                parms["UserName"] = row.Field<string>("UserName");

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

                count++;
            }


            Console.WriteLine($"{count} inserted");

            //using (var reader = await sourceUsersCommand.ExecuteReaderAsync())
            //{
            //    while (reader.Read())
            //    {






            //        //transform parameters


            //        //                    //construct command
            //        //                    string insertSql = $@"
            //        //INSERT INTO AspNetUsers ({string.Join(", ", parms.Keys)})
            //        //VALUES ({string.Join(", ", parms.Keys.Select(k => "@" + k))})
            //        //";

            //        //target.Database.ExecuteSqlRaw(
            //        //    insertSql,
            //        //    [.. parms.Keys.Select(key => parms[key]!)]
            //        //);

            //    }
            //}
        }

        private async Task ImportRoles(DbConnection source)
        {
            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = "SELECT * FROM AspNetRoles";
            using (var reader = await sourceCommand.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    //prepare parameters
                    var parms = new Dictionary<string, object?> {
                        { "Id", null },
                        { "Name", null },
                        { "NormalizedName", null },
                        { "ConcurrencyStamp", null },
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


                    //construct command
                    string insertSql = $@"
INSERT INTO AspNetRoles ({string.Join(", ", parms.Keys)})
VALUES ({string.Join(", ", parms.Keys.Select(k => "@" + k))})
";

                    target.Database.ExecuteSqlRaw(
                        insertSql,
                        [.. parms.Keys.Select(key => parms[key]!)]
                    );

                }
            }
        }

        private async Task ImportUserRoles(DbConnection source)
        {
            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = "SELECT * FROM AspNetUserRoles";
            using (var reader = await sourceCommand.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    //prepare parameters
                    var parms = new Dictionary<string, object?> {
                        { "UserId", null },
                        { "RoleId", null },
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


                    //construct command
                    string insertSql = $@"
INSERT INTO AspNetUserRoles ({string.Join(", ", parms.Keys)})
VALUES ({string.Join(", ", parms.Keys.Select(k => "@" + k))})
";

                    target.Database.ExecuteSqlRaw(
                        insertSql,
                        [.. parms.Keys.Select(key => parms[key]!)]
                    );

                }
            }
        }
    }
}

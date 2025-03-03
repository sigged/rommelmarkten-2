using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Infrastructure.Persistence;
using System.Data.Common;

namespace V2Importer.Importers
{

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
            await ImportRoles(source);
            await ImportUserRoles(source);
        }

        private async Task ImportUsers(DbConnection source)
        {
            var sourceCommand = source.CreateCommand();
            sourceCommand.CommandText = "SELECT * FROM AspNetUsers";
            using (var reader = await sourceCommand.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    //prepare parameters
                    var parms = new Dictionary<string, object?> {
                        { "Id", null },
                        { "Email", null },
                        { "PasswordHash", null },
                        { "SecurityStamp", null },
                        { "ConcurrencyStamp", null },
                        { "PhoneNumber", null },
                        { "PhoneNumberConfirmed", null },
                        { "TwoFactorEnabled", null },
                        { "LockoutEnd", null },
                        { "LockoutEnabled", null },
                        { "AccessFailedCount", null },
                        { "UserName", null },
                        { "NormalizedUserName", null },
                        { "NormalizedEmail", null },
                        { "EmailConfirmed", null },
                        { "FirstName", null },
                        { "LastName", null },
                        { "Street", null },
                        { "HouseNumber", null },
                        { "PostalCode", null },
                        { "City", null },
                        { "Country", null },
                        { "BirthDate", null },
                    };

                    //collect parameters from source record
                    for(int i = 0; i < reader.FieldCount; i++)
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
INSERT INTO AspNetUsers ({string.Join(", ", parms.Keys)})
VALUES ({string.Join(", ", parms.Keys.Select(k => "@" + k))})
";

                    target.Database.ExecuteSqlRaw(
                        insertSql,
                        [.. parms.Keys.Select(key => parms[key]!)]
                    );

                }
            }
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

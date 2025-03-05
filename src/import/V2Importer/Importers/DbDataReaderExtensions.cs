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
}

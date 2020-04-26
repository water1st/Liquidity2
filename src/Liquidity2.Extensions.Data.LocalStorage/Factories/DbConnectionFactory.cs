using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace Liquidity2.Extensions.Data.LocalStorage
{
    internal class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly LocalStorageOptions options;

        public DbConnectionFactory(LocalStorageOptions options)
        {
            this.options = options;
        }

        public IDbConnection Create()
        {
            try
            {
                var args = AnalyzingConnectionString(options.ConnectionString);
                var filePath = args["data source"];
                var dirPath = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);

                if (!File.Exists(filePath))
                    SQLiteConnection.CreateFile(filePath);

                return new SQLiteConnection(options.ConnectionString);
            }
            catch
            {
                throw;
            }
        }

        private IDictionary<string, string> AnalyzingConnectionString(string connectionString)
        {
            var result = new Dictionary<string, string>();
            var args = connectionString.Split(';');
            foreach (var arg in args)
            {
                var datas = arg.Split('=');
                var key = datas[0].Trim().ToLower();
                var value = datas[1].Trim();
                result.Add(key, value);
            }

            return result;
        }
    }
}

using System.Data;
using System.Data.SQLite;

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
                return new SQLiteConnection(options.ConnectionString);
            }
            catch
            {
                throw;
            }
        }
    }
}

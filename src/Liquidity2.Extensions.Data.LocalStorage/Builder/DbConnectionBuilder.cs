namespace Liquidity2.Extensions.Data.LocalStorage
{
    internal class DbConnectionBuilder : IDbConnectionBuilder
    {
        private readonly LocalStorageOptions options;

        public DbConnectionBuilder(LocalStorageOptions options)
        {
            this.options = options;
        }

        public void UseSQLite(string connectionString)
        {
            options.ConnectionString = connectionString;
        }
    }
}

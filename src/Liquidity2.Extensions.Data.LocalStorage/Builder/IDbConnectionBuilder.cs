namespace Liquidity2.Extensions.Data.LocalStorage
{
    public interface IDbConnectionBuilder
    {
        void UseSQLite(string connectionString);
    }
}

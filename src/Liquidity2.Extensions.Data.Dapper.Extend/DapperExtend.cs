using System;
using System.Data;
using System.Threading.Tasks;

namespace Dapper
{
    public static class DapperExtend
    {
        public static int ExecuteWithTransaction(this IDbConnection dbConnection, string sql, object persistentObject = null, int? timeout = null)
        {
            if (dbConnection is null)
            {
                throw new ArgumentNullException(nameof(dbConnection));
            }

            int result = 0;
            using (dbConnection)
            {
                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        result = dbConnection.Execute(sql, persistentObject, transaction, timeout);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                if (dbConnection.State != ConnectionState.Closed)
                    dbConnection.Close();

                return result;
            }
        }

        public static async Task<int> ExecuteWithTransactionAsync(this IDbConnection dbConnection, string sql, object persistentObject = null, int? timeout = null)
        {
            if (dbConnection is null)
            {
                throw new ArgumentNullException(nameof(dbConnection));
            }

            int result = 0;
            using (dbConnection)
            {
                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        result = await dbConnection.ExecuteAsync(sql, persistentObject, transaction, timeout);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                if (dbConnection.State != ConnectionState.Closed)
                    dbConnection.Close();

                return result;
            }
        }
    }
}

using Dapper;
using Liquidity2.Data.Client.Market.Errors;
using Liquidity2.Extensions.Data.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liquidity2.Data.Client.LocalStorage.Errors
{
    public class ErrorRepository : IErrorRepository
    {
        private readonly IDbConnectionFactory factory;

        public ErrorRepository(IDbConnectionFactory factory)
        {
            this.factory = factory;
            Initialize();
        }

        private void Initialize()
        {
            const string SQL = @"CREATE TABLE IF NOT EXISTS 'liquidity2_errors' (
            'Id' TEXT(36) NOT NULL,
            'ErrorCode' INTEGER NOT NULL,
            'Symbol' TEXT NOT NULL,
            'Operation' INTEGER NOT NULL,
            'ErrorMessage' TEXT NOT NULL,
            'CreateTimestamp' INTEGER NOT NULL,
            PRIMARY KEY ('Id')
            );";

            var connect = factory.Create();
            connect.Execute(SQL);
        }

        public async Task<bool> Exist(string id)
        {
            const string SQL = "SELECT Id FROM liquidity2_errors WHERE Id = @id";
            var connection = factory.Create();
            var result = await connection.QueryFirstOrDefaultAsync<ErrorPersistentObject>(SQL, new { id });
            return result != null;
        }

        public async Task<bool> Exist(ErrorPersistentObject aggregateRoot)
        {
            if (aggregateRoot == null)
                return false;

            var id = aggregateRoot.Id;
            return await Exist(id);
        }

        public async Task<IEnumerable<ErrorPersistentObject>> GetLastMonthErrors()
        {
            var createTimestamp = DateTimeOffset.UtcNow.AddMonths(-1).ToUnixTimeSeconds();
            const string SQL = "SELECT * FROM liquidity2_errors WHERE CreateTimestamp >= @createTimestamp";
            var connection = factory.Create();
            var persistentObjects = await connection.QueryAsync<ErrorPersistentObject>(SQL, new { createTimestamp });
            return persistentObjects;
        }

        public async Task<IEnumerable<ErrorPersistentObject>> GetAll()
        {
            const string SQL = "SELECT * FROM Error";
            var connection = factory.Create();
            var persistentObjects = await connection.QueryAsync<ErrorPersistentObject>(SQL);
            return persistentObjects;
        }

        public Task Add(ErrorPersistentObject persistentObject)
        {
            const string SQL = "INSERT OR REPLACE INTO liquidity2_errors (Id,ErrorCode,Symbol,Operation,ErrorMessage,CreateTimestamp) VALUES (@Id,@ErrorCode,@Symbol,@Operation,@ErrorMessage,@CreateTimestamp)";
            var connection = factory.Create();
            return connection.ExecuteWithTransactionAsync(SQL, persistentObject);
        }

        public Task Update(ErrorPersistentObject persistentObject)
        {
            const string SQL = "UPDATE liquidity2_errors SET Id = @Id, ErrorCode = @ErrorCode, Symbol = @Symbol, Operation = @Operation, ErrorMessage = @ErrorMessage, CreateTimestamp = @CreateTimestamp WHERE Id = @Id";
            var connection = factory.Create();
            return connection.ExecuteWithTransactionAsync(SQL, persistentObject);
        }

        public Task Delete(string id)
        {
            const string SQL = "DELETE FROM liquidity2_errors WHERE Id = @id";
            var connection = factory.Create();
            return connection.ExecuteWithTransactionAsync(SQL, new { id });
        }

        public Task Delete(ErrorPersistentObject persistentObject)
        {
            return Delete(persistentObject.Id);
        }

        public Task RemoveBeforeTime(DateTime dateTime)
        {
            var createTimestamp = new DateTimeOffset(dateTime).ToUnixTimeSeconds();
            const string SQL = "DELETE FROM liquidity2_errors WHERE CreateTimestamp <= @createTimestamp";
            var connection = factory.Create();
            return connection.ExecuteWithTransactionAsync(SQL, new { createTimestamp });
        }

        public async Task<IEnumerable<ErrorPersistentObject>> GetByErrorCode(int errorCode)
        {
            const string SQL = "SELECT * FROM liquidity2_errors WHERE ErrorCode = @errorCode";
            var connection = factory.Create();
            var persistentObjects = await connection.QueryAsync<ErrorPersistentObject>(SQL, new { errorCode });
            return persistentObjects;
        }

        public async Task<IEnumerable<ErrorPersistentObject>> GetByExpression(Func<ErrorPersistentObject, bool> predicate)
        {
            var data = await GetAll();
            return data.Where(predicate);
        }

        public async Task<ErrorPersistentObject> GetById(string id)
        {
            const string SQL = "SELECT * FROM liquidity2_errors WHERE Id = @id";
            var connection = factory.Create();
            var persistentObject = await connection.QueryFirstOrDefaultAsync<ErrorPersistentObject>(SQL, new { id });
            return persistentObject;
        }
    }
}

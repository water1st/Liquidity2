using Dapper;
using Liquidity2.Extensions.Data.LocalStorage;
using Liquidity2.Extensions.WindowPostions.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.WindowPostions
{
    public class WindowPostionClient : IWindowPostionClient
    {
        private readonly IDbConnectionFactory factory;

        public WindowPostionClient(IDbConnectionFactory factory)
        {
            this.factory = factory;
            Initialize();
        }

        private void Initialize()
        {
            const string SQL = @"CREATE TABLE IF NOT EXISTS 'liquidity2_extensions_window_postions' (
                                      'Id' TEXT NOT NULL,
                                      'TypeFullName' TEXT NOT NULL,
                                      'Left' REAL NOT NULL,
                                      'Top' REAL NOT NULL,
                                      'Height' REAL NOT NULL,
                                      'Width' REAL NOT NULL,
                                      'CreateTimeUtc' INTEGER NOT NULL,
                                      PRIMARY KEY ('Id'));";

            var connect = factory.Create();
            connect.Execute(SQL);
        }


        public async Task<IEnumerable<WindowPostionPersistentObject>> GetByType(string typeName)
        {
            const string SQL = "SELECT * FROM liquidity2_extensions_window_postions WHERE TypeFullName = @typeName ORDER BY CreateTimeUtc ASC";

            var connect = factory.Create();
            var result = await connect.QueryAsync<WindowPostionPersistentObject>(SQL, new { typeName });
            return result;
        }

        public async Task<WindowPostionPersistentObject> GetById(string id)
        {
            const string SQL = "SELECT * FROM liquidity2_extensions_window_postions WHERE Id = @id";

            var connect = factory.Create();
            var result = await connect.QueryFirstOrDefaultAsync<WindowPostionPersistentObject>(SQL, new { id });
            return result;
        }

        public async Task<IEnumerable<WindowPostionPersistentObject>> GetAll()
        {
            const string SQL = "SELECT * FROM liquidity2_extensions_window_postions";

            var connect = factory.Create();
            var result = await connect.QueryAsync<WindowPostionPersistentObject>(SQL);
            return result;
        }

        public async Task Add(WindowPostionPersistentObject persistentObject)
        {
            persistentObject.CreateTimeUtc = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            const string SQL = "INSERT INTO liquidity2_extensions_window_postions (Id,TypeFullName,Left,Top,Height,Width,CreateTimeUtc) VALUES (@Id,@TypeFullName,@Left,@Top,@Height,@Width,@CreateTimeUtc);";

            var connect = factory.Create();
            await connect.ExecuteWithTransactionAsync(SQL, persistentObject);
        }

        public async Task Update(WindowPostionPersistentObject persistentObject)
        {
            const string SQL = "UPDATE liquidity2_extensions_window_postions SET Left = @Left, Top = @Top, Height = @Height, Width = @Width  WHERE Id = @Id;";

            var connect = factory.Create();
            await connect.ExecuteWithTransactionAsync(SQL, persistentObject);
        }

        public async Task Delete(string id)
        {
            const string SQL = "DELETE FROM liquidity2_extensions_window_postions WHERE Id = @id";

            var connect = factory.Create();
            await connect.ExecuteWithTransactionAsync(SQL, new { id });
        }

        public async Task Delete(WindowPostionPersistentObject persistentObject)
        {
            await Delete(persistentObject.Id);
        }
    }
}

using Liquidity2.Data.Client.LocalStorage.Errors;
using Liquidity2.Data.Client.Market.Errors;
using System;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LocalStorageExtensions
    {
        public static void AddLocalStorageDAL(this IServiceCollection services)
        {
            //添加本地存储
            services.AddLocalStorage(storageBuilder =>
            {
                string fileDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BITTO");
                if (!Directory.Exists(fileDirectory))
                    Directory.CreateDirectory(fileDirectory);
                storageBuilder.UseSQLite($"Data Source={Path.Combine(fileDirectory, "liquidity_local_storage.db")};Version=3");
            });
            services.AddSingleton<IErrorRepository, ErrorRepository>();
        }
    }
}
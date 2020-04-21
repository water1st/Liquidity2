using Liquidity2.Extensions.Data.CRUD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liquidity2.Data.Client.Market.Errors
{
    public interface IErrorRepository : IRepository<ErrorPersistentObject, string>
    {
        Task RemoveBeforeTime(DateTime dateTime);
        Task<IEnumerable<ErrorPersistentObject>> GetByErrorCode(int errorCode);
        Task<IEnumerable<ErrorPersistentObject>> GetLastMonthErrors();
    }
}

using Liquidity2.Data.Client.DTO;
using Liquidity2.Data.Client.Market.Errors.Events;
using Liquidity2.Extensions.EventBus;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liquidity2.Service.Errors
{
    public interface IErrorService : IEventHandler<TransactionServiceErrorEvent>
    {
        Task<IEnumerable<Error>> GetErrors();
    }
}

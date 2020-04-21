using Liquidity2.Data.Client.DTO;
using Liquidity2.Data.Client.Market.Errors;
using Liquidity2.Data.Client.Market.Errors.Events;

namespace Liquidity2.Service.Errors
{
    public interface IErrorMapper
    {
        ErrorPersistentObject Map(Error error);

        Error Map(ErrorPersistentObject error);

        ErrorUpdateToUIEvent MapToErrorUpdateEvent(Error error);
    }
}

using Liquidity2.Data.Client.DTO;
using Liquidity2.Data.Client.Market.Errors;
using Liquidity2.Data.Client.Market.Errors.Events;
using Liquidity2.Extensions.EventBus;
using Liquidity2.Extensions.Lifecycle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Service.Errors
{
    public class ErrorService : LocalStorageStageObject, IErrorService
    {
        private readonly IErrorMapper mapper;
        private readonly IErrorRepository repository;
        private readonly IEventBus bus;

        public ErrorService(IErrorMapper mapper, IErrorRepository repository, IEventBus bus)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.bus = bus;

            this.bus.Subscribe(this);
        }

        public async Task<IEnumerable<Error>> GetErrors()
        {
            var errors = await repository.GetLastMonthErrors();
            var result = errors.Select(x => mapper.Map(x));
            return result;
        }

        public async Task Handle(TransactionServiceErrorEvent @event, CancellationToken token)
        {
            var error = new Error { ErrorCode = @event.Code, Symbol = @event.Symbol, Operation = @event.Operation, ErrorMessage = @event.ErrorMessage };
            await repository.Add(mapper.Map(error));
            var updateEvent = mapper.MapToErrorUpdateEvent(error);
            await bus.Publish(updateEvent, token);
        }

        protected override async Task OnStart(CancellationToken token)
        {
            var time = DateTime.UtcNow.AddMonths(-1);
            await repository.RemoveBeforeTime(time);
        }
    }
}

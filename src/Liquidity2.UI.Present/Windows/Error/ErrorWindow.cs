using Liquidity2.Data.Client.Market.Errors.Events;
using Liquidity2.Extensions.EventBus;
using Liquidity2.Service.Errors;
using Liquidity2.UI.Core;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Liquidity2.Extensions.EventBus.EventObserver;

namespace Liquidity2.UI.Present.Windows.Error
{
    public class ErrorWindow : WindowBase, IEventHandler<ErrorUpdateToUIEvent>, IEventObserver
    {
        private readonly IErrorService _errorService;

        public ErrorViewModel ViewModel { get; set; }

        public ErrorWindow(IWindowCommonBehavior windowCommonBehavior, IErrorService errorService) : base(windowCommonBehavior)
        {
            _errorService = errorService;

            ViewModel = new ErrorViewModel();
            LoadData();
        }

        private void LoadData()
        {
            ViewModel.ErrorDatas.Clear();

            _ = Dispatcher.InvokeAsync(async () =>
              {
                  var errors = await _errorService.GetErrors();
                  foreach (var item in errors)
                  {
                      ViewModel.ErrorDatas.Insert(0, new ErrorData(item.CreateTime.ToLocalTime(), item.Symbol, item.Operation, item.ErrorCode, item.ErrorMessage));
                  }
              });
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        public async Task Handle(ErrorUpdateToUIEvent @event, CancellationToken token)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                ViewModel.ErrorDatas.Insert(0, new ErrorData(@event.CreateTime, @event.Symbol, @event.Operation, @event.ErrorCode, @event.ErrorMessage));
            });
        }

        public void Subscribe(IEventBusRegistrator registrator)
        {
            registrator.Register(this);
        }
    }
}

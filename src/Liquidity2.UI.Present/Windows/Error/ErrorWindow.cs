using Liquidity2.Extensions.EventBus;
using Liquidity2.UI.Core;
using System.ComponentModel;

namespace Liquidity2.UI.Present.Windows.Error
{
    public class ErrorWindow : WindowBase
    //IEventHandler<ErrorUpdateToUIEvent>
    {
        //private ITransactionErrorService _errorService;
        private IEventBus _bus;

        public ErrorViewModel ViewModel { get; set; }

        public ErrorWindow(IWindowCommonBehavior windowCommonBehavior, IEventBus bus/*, ITransactionErrorService errorService*/) : base(windowCommonBehavior)
        {
            //_errorService = errorService;
            _bus = bus;

            //_bus.RegisterEventHandler(this);
            ViewModel = new ErrorViewModel();
            InsertError();
        }

        private void InsertError()
        {
            //var errors = _errorService.GetErrors();

            //ViewModel.ErrorDatas.Clear();

            //foreach (var item in errors.Result)
            //{
            //    ViewModel.ErrorDatas.Insert(0, new ErrorData(item.CreateTime.ToLocalTime(), item.Symbol, item.Operation, item.ErrorCode, item.ErrorMessage));
            //}
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        //public async Task Handle(ErrorUpdateToUIEvent @event)
        //{
        //    await Dispatcher.InvokeAsync(() =>
        //    {
        //        ViewModel.ErrorDatas.Insert(0, new ErrorData(@event.ErrorDTO.CreateTime, @event.ErrorDTO.Symbol, @event.ErrorDTO.Operation, @event.ErrorDTO.ErrorCode, @event.ErrorDTO.ErrorMessage));
        //    });
        //}
    }
}

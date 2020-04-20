using Liquidity2.Extensions.EventBus;
using Liquidity2.UI.Core;
using Liquidity2.UI.Present.Windows.Entrust.EventHandler;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Liquidity2.UI.Present.Windows.Entrust
{
    public class EntrustWindow : WindowBase, /*IEventHandler<OrderingQueryToUIEvent>, IEventHandler<HistoryOrderQueryToUIEvent>,*/
       IOrderStatusEventHandler
    {
        private readonly IEventBus _bus;
        private readonly IEntrustDataMapper _mapper;
        //private readonly IOrderService _tradeService;
        //private readonly IAccountService _accountService;

        public ICommand CancelOrderCommand { get; set; }
        public EntrustViewModel EntrustVM { get; set; }

        public EntrustWindow(IEventBus bus, IWindowCommonBehavior windowCommonBehavior, IEntrustDataMapper mapper/*, IOrderService tradeService, IAccountService accountService*/) : base(windowCommonBehavior)
        {
            _bus = bus;
            _mapper = mapper;
            //_tradeService = tradeService;
            //_accountService = accountService;
            EntrustVM = new EntrustViewModel();
            EntrustVM.OnSelectedIndexChange += EntrustVM_OnSelectedIndexChange;
            Subscribe(_bus);
            InitializeCommand();
            InsertData();
        }

        private void EntrustVM_OnSelectedIndexChange()
        {
            InsertData();
        }

        private void Subscribe(IEventBus eventBus)
        {
            //eventBus.RegisterEventHandler<OrderingQueryToUIEvent>(this);
            //eventBus.RegisterEventHandler<HistoryOrderQueryToUIEvent>(this);
            //eventBus.RegisterEventHandler<PlaceOrderSuccessToUIEvent>(this);
            //eventBus.RegisterEventHandler<CancelOrderSuccessToUIEvent>(this);
            //eventBus.RegisterEventHandler<CompletelyTransactedToUIEvent>(this);
            //eventBus.RegisterEventHandler<PlaceOrderFailedToUIEvent>(this);
        }

        private void InsertData()
        {
            //_tradeService.GetOrder(1, 200);
        }

        private void InitializeCommand()
        {
            CancelOrderCommand = new CustomRoutedCommand(nameof(CancelOrderCommand), typeof(EntrustWindow), this);
            this.AddCommandBinding(CancelOrderCommand, CancelOrderCommandcb_CanExecute, CancelOrderCommandcb_Executed);
        }

        private void CancelOrderCommandcb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is CancelParameter parameter)
            {
                //_tradeService.CancelOrder(parameter.OrderId, parameter.Symbol);

                var index = EntrustVM.EntrustListData.ToList().FindIndex(x => x.Order_id == parameter.OrderId);
                if (index != -1)
                {
                    EntrustVM.EntrustListData.RemoveAt(index);
                }
                e.Handled = true;
            }
        }

        private void CancelOrderCommandcb_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        //当前委托数据传入
        //public async Task Handle(OrderingQueryToUIEvent @event)
        //{
        //    await Dispatcher.InvokeAsync(() =>
        //    {
        //        EntrustVM.EntrustListData.Clear();
        //        foreach (var data in @event.OrderDatas)
        //        {
        //            var entrustData = _mapper.MapToEntrustData(data);
        //            EntrustVM.EntrustListData.Insert(EntrustVM.EntrustListData.Count, entrustData);
        //        }
        //    });
        //}

        /// <summary>
        /// 历史委托数据传入
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        //public async Task Handle(HistoryOrderQueryToUIEvent @event)
        //{
        //    await Dispatcher.InvokeAsync(() =>
        //    {
        //        EntrustVM.HistoryEntrustListData.Clear();
        //        foreach (var data in @event.HistoryOrderDatas)
        //        {
        //            var entrustData = _mapper.MapToHistoryEntrustData(data);
        //            EntrustVM.HistoryEntrustListData.Insert(EntrustVM.HistoryEntrustListData.Count, entrustData);
        //        }

        //        EntrustVM.BillDetialListData.Clear();
        //        var temp = (from x in @event.HistoryOrderDatas where (x.Status == OrderStatus.CompletelyTransacted || x.Status == OrderStatus.IncompleteTransacted) select x);

        //        foreach (var item in temp)
        //        {
        //            var data = _mapper.MapToHistoryEntrustData(item);
        //            EntrustVM.BillDetialListData.Insert(EntrustVM.BillDetialListData.Count, data);
        //        }

        //    });
        //}

        /// <summary>
        /// 下单成功
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        //public async Task Handle(PlaceOrderSuccessToUIEvent @event)
        //{
        //    await Dispatcher.InvokeAsync(() =>
        //    {
        //        var index = EntrustVM.EntrustListData.ToList().FindIndex(x => x.Order_id == @event.OrderData.Order_id);
        //        var entrustData = _mapper.MapToEntrustData(@event.OrderData);
        //        if (index == -1)
        //        {
        //            EntrustVM.EntrustListData.Insert(0, entrustData);
        //        }
        //        else
        //        {
        //            EntrustVM.EntrustListData[index] = entrustData;
        //        }
        //        _accountService.GetWallets();
        //    });
        //}

        /// <summary>
        /// 撤单成功
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        //public async Task Handle(CancelOrderSuccessToUIEvent @event)
        //{
        //    await Dispatcher.InvokeAsync(() =>
        //    {
        //        var entrustData = _mapper.MapToHistoryEntrustData(@event.OrderData);

        //        var historyIndex = EntrustVM.HistoryEntrustListData.ToList().FindIndex(x => x.Order_Id == @event.OrderData.Order_id);

        //        if (historyIndex == -1)
        //        {
        //            EntrustVM.HistoryEntrustListData.Insert(0, entrustData);
        //        }
        //        else
        //        {
        //            EntrustVM.HistoryEntrustListData[historyIndex] = entrustData;
        //        }

        //        var index = EntrustVM.EntrustListData.ToList().FindIndex(x => x.Order_id == @event.OrderData.Order_id);
        //        if (index != -1)
        //        {
        //            EntrustVM.EntrustListData.RemoveAt(index);
        //        }
        //        _accountService.GetWallets();
        //    });
        //}

        /// <summary>
        /// 完成成交
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        //public async Task Handle(CompletelyTransactedToUIEvent @event)
        //{
        //    await Dispatcher.InvokeAsync(() =>
        //    {
        //        var entrustData = _mapper.MapToHistoryEntrustData(@event.HistoryOrderData);

        //        var historyIndex = EntrustVM.HistoryEntrustListData.ToList().FindIndex(x => x.Order_Id == @event.HistoryOrderData.Order_id);

        //        if (historyIndex == -1)
        //        {
        //            EntrustVM.HistoryEntrustListData.Insert(0, entrustData);
        //        }
        //        else
        //        {
        //            EntrustVM.HistoryEntrustListData[historyIndex] = entrustData;
        //        }

        //        var index = EntrustVM.EntrustListData.ToList().FindIndex(x => x.Order_id == @event.HistoryOrderData.Order_id);
        //        if (index != -1)
        //        {
        //            EntrustVM.EntrustListData.RemoveAt(index);
        //        }
        //        _accountService.GetWallets();
        //    });
        //}

        /// <summary>
        /// 下单失败
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        //public async Task Handle(PlaceOrderFailedToUIEvent @event)
        //{
        //    await Dispatcher.InvokeAsync(() =>
        //    {
        //        var index = EntrustVM.HistoryEntrustListData.ToList().FindIndex(x => x.Order_Id == @event.OrderData.Order_id);
        //        if (index == -1)
        //        {
        //            var entrustData = _mapper.MapToHistoryEntrustData(@event.OrderData);
        //            EntrustVM.HistoryEntrustListData.Insert(0, entrustData);
        //        }
        //    });
        //}
    }
}

using Liquidity2.Extensions.EventBus;
using Liquidity2.UI.Windows.TOS.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liquidity2.UI.Windows.TOS.EventHandlers
{
    public interface IPrecisionChangeEventHandler:IEventHandler<PrecisionChangeSubscribeEvent>,IEventHandler<PrecisionChangeUnsubscribeEvent>
    {
    }
}

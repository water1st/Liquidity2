using Liquidity2.Extensions.EventBus;
using System.Collections.Generic;

namespace Liquidity2.UI.Services.SelfSelect.Events
{
    public class GetMarkSymbolResponseEvent:Event
    {
        public GetMarkSymbolResponseEvent(IEnumerable<string> markSymbols)
        {
            MarkSymbols = markSymbols;
        }

        public IEnumerable<string> MarkSymbols { get; }
    }
}

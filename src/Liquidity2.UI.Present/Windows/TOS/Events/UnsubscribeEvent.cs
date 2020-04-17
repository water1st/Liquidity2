using Liquidity2.Extensions.EventBus;

namespace Liquidity2.UI.Windows.TOS.Events
{
    public class UnsubscribeEvent:Event
    {
        public UnsubscribeEvent(string group)
        {
            Group = group;
        }

        public string Group { get; }
    }
}

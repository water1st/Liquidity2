using System.Threading.Tasks;
using System.Windows.Threading;

namespace Liquidity2.Extensions.Blocker.WPFBlocker
{
    internal class Blocker : IBlocker
    {
        public void Block(Task task)
        {
            var frame = new DispatcherFrame();
            task.ContinueWith(_ =>
            {
                frame.Continue = false;
            });
            Dispatcher.PushFrame(frame);
        }

        public TResult Block<TResult>(Task<TResult> task)
        {
            TResult result = default;
            var frame = new DispatcherFrame();
            task.ContinueWith(t =>
            {
                result = t.Result;
                frame.Continue = false;
            });
            Dispatcher.PushFrame(frame);

            return result;
        }
    }
}

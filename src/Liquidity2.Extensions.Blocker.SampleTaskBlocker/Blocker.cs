using System.Threading.Tasks;

namespace Liquidity2.Extensions.Blocker.SampleTaskBlocker
{
    internal class Blocker : IBlocker
    {
        public void Block(Task task)
        {
            var t = new Task(async () =>
            {
                await task;
            });

            t.Start();
            Task.WaitAll(t);
        }

        public TResult Block<TResult>(Task<TResult> task)
        {
            TResult result = default;

            var t = new Task(async () =>
            {
                result = await task;
            });

            t.Start();
            Task.WaitAll(t);

            return result;
        }
    }
}

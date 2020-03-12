using System;

namespace Liquidity2.Utilities
{
    public sealed class Disposabler : IDisposable
    {
        private readonly Action dispose;
        private bool disposed = false;

        public Disposabler(Action dispose)
        {
            this.dispose = dispose;
        }

        public void Dispose()
        {
            if (!disposed && dispose != null)
            {
                dispose();
                disposed = true;
            }
        }
    }
}

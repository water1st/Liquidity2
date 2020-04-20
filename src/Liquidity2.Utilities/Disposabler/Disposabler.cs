using System;

namespace Liquidity2.Utilities
{
    public sealed class Disposabler : IDisposable
    {
        private readonly Action _dispose;
        private bool _disposed = false;

        public Disposabler(Action dispose)
        {
            this._dispose = dispose;
        }

        public void Dispose()
        {
            if (!_disposed && _dispose != null)
            {
                _dispose.Invoke();
                _disposed = true;
            }
        }
    }
}

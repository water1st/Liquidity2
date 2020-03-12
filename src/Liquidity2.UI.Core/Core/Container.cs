using System;

namespace Liquidity2.UI.Core
{
    public static class Container
    {
        private static IServiceProvider _insecure;
        private static readonly object _myLock = new object();
        public static IServiceProvider Insecure
        {
            get
            {
                return _insecure;
            }
            set
            {
                lock (_myLock)
                {
                    if (_insecure == null)
                    {
                        _insecure = value;
                    }
                }
            }
        }
    }
}

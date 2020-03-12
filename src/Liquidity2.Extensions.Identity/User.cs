namespace Liquidity2.Extensions.Identity
{
    public class User
    {
        private User() { }
        private static User _current;
        private readonly static object myLock = new object();
        public static User Current
        {
            get
            {
                lock (myLock)
                {
                    if (_current == null)
                    {
                        _current = new User();
                    }
                }
                return _current;
            }
        }

        public string Name { get; set; }
    }
}

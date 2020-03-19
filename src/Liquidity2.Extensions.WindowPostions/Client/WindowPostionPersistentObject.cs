namespace Liquidity2.Extensions.WindowPostions.Client
{
    public class WindowPostionPersistentObject
    {
        public string Id { get; set; }
        public string TypeFullName { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public long CreateTimeUtc { get; set; }
    }
}

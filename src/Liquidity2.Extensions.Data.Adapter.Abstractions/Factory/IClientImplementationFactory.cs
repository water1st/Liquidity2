namespace Liquidity2.Extensions.Data.Adapter
{
    public interface IClientImplementationFactory
    {
        TClient Create<TClient>(string name) where TClient : class;
    }
}

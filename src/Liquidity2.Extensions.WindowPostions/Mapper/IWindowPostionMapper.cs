using Liquidity2.Extensions.WindowPostions.Client;

namespace Liquidity2.Extensions.WindowPostions
{
    public interface IWindowPostionMapper
    {
        WindowPostion Map(WindowPostionPersistentObject persistentObject);
        WindowPostionPersistentObject Map(WindowPostion postion);
    }
}

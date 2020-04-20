using Liquidity2.UI.Components.Model;

namespace Liquidity2.UI.Components.Interface
{
    public interface IHasData { }

    public interface IHasData<TData> : IHasData where TData : DataBase
    {
        TData GetData();
    }
}

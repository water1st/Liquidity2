namespace Liquidity2.UI.Components.Interface
{
    public interface IRender
    {
        /// <summary>
        /// 渲染阶段
        /// </summary>
        int Stage { get; }

        string Name { get; }

        void Render(IHasData data);
    }
}

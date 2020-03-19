using Liquidity2.UI.Core.Options;

namespace Liquidity2.UI.Core.Builder
{
    internal class UICoreBuilder : IUICoreBuilder
    {
        private readonly UIOptions _options;

        public UICoreBuilder(UIOptions options) => _options = options;

        public void UseTemplate(string name) => _options.Template = name;
    }
}

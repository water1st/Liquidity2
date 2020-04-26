using System.Collections.Generic;

namespace Liquidity2.UI.Core
{
    public interface IResourceLoader
    {
        void Load(string path);
        void Load(IEnumerable<string> paths);
    }
}

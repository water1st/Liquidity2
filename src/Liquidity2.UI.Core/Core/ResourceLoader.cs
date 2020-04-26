using System;
using System.Collections.Generic;
using System.Windows;

namespace Liquidity2.UI.Core
{
    internal class ResourceLoader : IResourceLoader
    {
        public void Load(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                Uri uri = new Uri(path, UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = uri });
            }
            else
            {
                throw new TemplateNotFoundException($"{path} not exists");
            }
        }

        public void Load(IEnumerable<string> paths)
        {
            foreach (var path in paths)
            {
                Load(path);
            }
        }
    }
}

using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Liquidity2.UI.Components
{
    public class WindowOptions
    {
        private readonly ConcurrentDictionary<string, IList<string>> styleFiles;
        private readonly ConcurrentDictionary<string, IList<string>> templateFiles;

        public WindowOptions()
        {
            styleFiles = new ConcurrentDictionary<string, IList<string>>();
            templateFiles = new ConcurrentDictionary<string, IList<string>>();
        }

        public string Template { get; set; }

        public IList<string> StyleFiles => styleFiles.GetOrAdd(Template, new List<string>());

        public IList<string> TemplateFiles => templateFiles.GetOrAdd(Template, new List<string>());

        public void AddStyleFile(string templateName, string path)
        {
            var list = styleFiles.GetOrAdd(templateName, new List<string>());
            list.Add(path);
        }

        public void AddTemplateFile(string templateName, string path)
        {
            var list = templateFiles.GetOrAdd(templateName, new List<string>());
            list.Add(path);
        }
    }
}

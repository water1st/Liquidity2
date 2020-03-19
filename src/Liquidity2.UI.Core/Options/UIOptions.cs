using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Liquidity2.UI.Core.Options
{
    public class UIOptions
    {
        private readonly ConcurrentDictionary<string, IList<string>> templateFiles;

        public UIOptions()
        {
            templateFiles = new ConcurrentDictionary<string, IList<string>>();
        }

        public string Template { get; set; }

        public IList<string> TemplateFiles
        {
            get
            {
                return templateFiles.GetOrAdd(Template, new List<string>());
            }
        }

        public void AddTemplateFile(string templateName, string path)
        {
            var list = templateFiles.GetOrAdd(templateName, new List<string>());
            list.Add(path);
        }
    }
}

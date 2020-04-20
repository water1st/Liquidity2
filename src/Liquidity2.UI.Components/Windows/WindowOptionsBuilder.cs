namespace Liquidity2.UI.Components
{
    public class WindowOptionsBuilder : IWindowOptionsBuilder
    {
        private readonly WindowOptions options;

        public WindowOptionsBuilder(WindowOptions options)
        {
            this.options = options;
        }

        public void AddStyleFile(string templateName, string path)
        {
            options.AddStyleFile(templateName, path);
        }

        public void AddTemplateFile(string templateName, string path)
        {
            options.AddTemplateFile(templateName, path);
        }

        public void UseTemplate(string templateName)
        {
            options.Template = templateName;
        }

    }
}

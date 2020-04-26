namespace Liquidity2.UI.Components
{
    public interface IWindowOptionsBuilder
    {
        void UseTemplate(string templateName);
        void AddTemplateFile(string templateName, string path);
        void AddStyleFile(string templateName, string path);
    }
}

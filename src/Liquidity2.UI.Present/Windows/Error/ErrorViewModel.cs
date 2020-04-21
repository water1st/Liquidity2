using System.Collections.ObjectModel;

namespace Liquidity2.UI.Present.Windows.Error
{
    public class ErrorViewModel
    {
        public ObservableCollection<ErrorData> ErrorDatas { get; set; }

        public ErrorViewModel()
        {
            ErrorDatas = new ObservableCollection<ErrorData>();
        }
    }
}

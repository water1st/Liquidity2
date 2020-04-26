using System.ComponentModel;
using System.Windows;

namespace Liquidity2.UI.Components.UsersControl
{
    internal class SearchTextboxViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Visibility _textBoxVisibility;
        private Visibility _littleSearchTextGridVis;
        private string _searchtext;

        public string SearchText
        {
            get { return _searchtext; }
            set
            {
                _searchtext = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchText)));
            }
        }

        public Visibility TextBoxVisbility
        {
            get { return _textBoxVisibility; }
            set
            {
                _textBoxVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextBoxVisbility)));
            }
        }

        public Visibility LittleSearchTextGridVis
        {
            get { return _littleSearchTextGridVis; }
            set
            {
                _littleSearchTextGridVis = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LittleSearchTextGridVis)));
            }
        }

        public SearchTextboxViewModel()
        {
            LittleSearchTextGridVis = Visibility.Visible;
        }
    }
}

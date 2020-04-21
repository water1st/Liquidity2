using Liquidity2.UI.Windows.SelfSelect.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Liquidity2.UI.Windows.SelfSelect
{
    public class SelfSelectViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<SelfSelectData> NowSearched { get; set; } = new ObservableCollection<SelfSelectData>();

        public ObservableCollection<SelfSelectData> NowDataSource { get; set; } = new ObservableCollection<SelfSelectData>();

        public ObservableCollection<SelfSelectData> USDTs { get; set; } = new ObservableCollection<SelfSelectData>();

        public ObservableCollection<SelfSelectData> HUSDTs { get; set; } = new ObservableCollection<SelfSelectData>();

        public ObservableCollection<SelfSelectData> BTCs { get; set; } = new ObservableCollection<SelfSelectData>();

        public ObservableCollection<SelfSelectData> ETHs { get; set; } = new ObservableCollection<SelfSelectData>();

        public ObservableCollection<SelfSelectData> SelfSelectDatas { get; set; } = new ObservableCollection<SelfSelectData>();

        public ObservableCollection<string> SelfSelectDatasString { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<SelfSelectData> AllMarkSymbols { get; set; } = new ObservableCollection<SelfSelectData>();

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                SearchTextChange?.Invoke(searchText);
            }
        }

        private string group;
        public string Group
        {
            get => group;
            set
            {
                group = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Group)));
            }
        }

        public ICommand SelfSelectdataSourceSetCmd { get; } = new CustomRoutedCommand(nameof(SelfSelectdataSourceSetCmd), typeof(SelfSelectWindow));

        public ICommand USDTdataSourceSetCmd { get; } = new CustomRoutedCommand(nameof(USDTdataSourceSetCmd), typeof(SelfSelectWindow));

        public ICommand HUSDTdataSourceSetCmd { get; } = new CustomRoutedCommand(nameof(HUSDTdataSourceSetCmd), typeof(SelfSelectWindow));

        public ICommand BTCdataSourceSetCmd { get; } = new CustomRoutedCommand(nameof(BTCdataSourceSetCmd), typeof(SelfSelectWindow));

        public ICommand ETHdataSourceSetCmd { get; } = new CustomRoutedCommand(nameof(ETHdataSourceSetCmd), typeof(SelfSelectWindow));

        public ICommand StarButtonClickCmd { get; set; } = new CustomRoutedCommand(nameof(StarButtonClickCmd), typeof(SelfSelectWindow));

        public ICommand GridViewItemClickCmd { get; set; } = new CustomRoutedCommand(nameof(GridViewItemClickCmd), typeof(SelfSelectWindow));

        public event Action<string> SearchTextChange;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

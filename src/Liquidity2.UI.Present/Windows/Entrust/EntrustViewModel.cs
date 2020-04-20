using System;
using System.Collections.ObjectModel;

namespace Liquidity2.UI.Present.Windows.Entrust
{
    public class EntrustViewModel
    {
        public ObservableCollection<EntrustData> EntrustListData { get; set; }

        public ObservableCollection<HistoryEntrustData> HistoryEntrustListData { get; set; }

        public ObservableCollection<HistoryEntrustData> BillDetialListData { get; set; }

        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                if (selectedIndex != value)
                {
                    OnSelectedIndexChange?.Invoke();
                }
                selectedIndex = value;
            }
        }

        public event Action OnSelectedIndexChange;

        public EntrustViewModel()
        {
            EntrustListData = new ObservableCollection<EntrustData>();
            HistoryEntrustListData = new ObservableCollection<HistoryEntrustData>();
            BillDetialListData = new ObservableCollection<HistoryEntrustData>();
        }
    }
}

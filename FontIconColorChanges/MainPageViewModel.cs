using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FontIconColorChanges
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ICommand RefreshListCommand { get; set; }

        private ObservableCollection<MyModelItem> _itemsToDisplay;
        public ObservableCollection<MyModelItem> ItemsToDisplay
        {
            get => _itemsToDisplay;
            set
            {
                if (value != _itemsToDisplay)
                {
                    _itemsToDisplay = value;
                    OnPropertyChanged("ItemsToDisplay");
                }
            }
        }

        public MainPageViewModel()
        {
            ItemsToDisplay = new ObservableCollection<MyModelItem>();
            RefreshListCommand = new Command(OnRefreshListTapped);
            PopulateMenuItemsFizzBuzzIsh();
        }

        private async void OnRefreshListTapped(object obj)
        {
            ItemsToDisplay.Clear();
            await Task.Delay(1000);
            PopulateMenuItemsFizzBuzzIsh();
        }

        private void PopulateMenuItemsFizzBuzzIsh()
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(PopulateMenuItemsFizzBuzzIsh)}");

            for (int i = 1; i < 43; i++)
            {
                var newItem = new MyModelItem
                {
                    ItemText = $"Menu item {i}",
                    StartImageName = MaterialFont.AccountOutline
                };

                if (i % 5 == 0)
                    newItem.IsNew = true;

                if (i % 3 == 0)
                    newItem.StartImageName = MaterialFont.ClipboardTextOutline;

                if (i % 3 == 0 && i % 5 == 0)
                {
                    newItem.StartImageName = MaterialFont.VideoOutline;
                    newItem.IsNew = i == 0 ? true : false;
                }

                ItemsToDisplay.Add(newItem);
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }
}

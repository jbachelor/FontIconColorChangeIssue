using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FontIconColorChanges
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MyModelItem> _itemsToDisplay;
        public ObservableCollection<MyModelItem> ItemsToDisplay
        {
            get => _itemsToDisplay;
            set
            {
                if(value != _itemsToDisplay)
                {
                    _itemsToDisplay = value;
                    OnPropertyChanged("ItemsToDisplay");
                }
            }
        }

        public MainPageViewModel()
        {
            PopulateMenuItemsFizzBuzzIsh();
        }

        private void PopulateMenuItemsFizzBuzzIsh()
        {
            ItemsToDisplay = new ObservableCollection<MyModelItem>();

            for (int i = 0; i < 29; i++)
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

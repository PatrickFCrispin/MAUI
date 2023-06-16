using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyFirstAppMAUI.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private string _title;
        private string _description;
        private string _fileName;

        public string Title
        {
            get => _title;
            protected set { SetProperty(ref _title, value); }
        }

        public string Description
        {
            get => _description;
            set { SetProperty(ref _description, value); }
        }

        public string FileName
        {
            get => _fileName;
            set { SetProperty(ref _fileName, value); }
        }

        public Command LoadItemsCommand { get; protected set; }
        public Command SaveCommand { get; protected set; }

        protected static async Task GoToRouteAsync(string root)
        {
            await Shell.Current.GoToAsync(root);
        }

        protected bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(Description);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value)) { return false; }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null) { return; }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
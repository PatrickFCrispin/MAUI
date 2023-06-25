namespace MyFirstAppMAUI.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private string _appName;
        private string _appVersion;
        private string _info;
        private string _moreInfoUrl;
        private Image _image;

        public string AppName
        {
            get => _appName;
            private set { SetProperty(ref _appName, value); }
        }

        public string AppVersion
        {
            get => _appVersion;
            private set { SetProperty(ref _appVersion, value); }
        }

        public string Info
        {
            get => _info;
            private set { SetProperty(ref _info, value); }
        }

        public string MoreInfoUrl
        {
            get => _moreInfoUrl;
            private set { SetProperty(ref _moreInfoUrl, value); }
        }

        public Image Image
        {
            get => _image;
            private set { SetProperty(ref _image, value); }
        }

        public AboutViewModel()
        {
            Title = "Sobre";
            AppName = AppInfo.Name;
            AppVersion = AppInfo.VersionString;
            Info = "Este App foi desenvolvido em .NET MAUI.";
            MoreInfoUrl = "https://aka.ms/maui";
            Image = new Image { Source = "dotnet_bot.png" };
        }
    }
}
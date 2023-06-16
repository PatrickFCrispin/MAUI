namespace MyFirstAppMAUI.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string AppName { get; private set; }
        public string AppVersion { get; private set; }
        public string Info { get; private set; }
        public string MoreInfoUrl { get; private set; }
        public Image Image { get; private set; }

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
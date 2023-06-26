namespace MyFirstAppMAUI.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string AppName { get; }
        public string AppVersion { get; }
        public string Info { get; }
        public string MoreInfoUrl { get; }
        public Image Image { get; }

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
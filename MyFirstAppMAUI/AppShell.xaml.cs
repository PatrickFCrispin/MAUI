using MyFirstAppMAUI.Views;

namespace MyFirstAppMAUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(AllNotesPage), typeof(AllNotesPage));
		Routing.RegisterRoute(nameof(AddNotePage), typeof(AddNotePage));
		Routing.RegisterRoute(nameof(EditNotePage), typeof(EditNotePage));
		Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
	}
}
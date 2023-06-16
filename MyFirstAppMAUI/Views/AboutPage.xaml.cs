using MyFirstAppMAUI.ViewModels;

namespace MyFirstAppMAUI.Views;

public partial class AboutPage : ContentPage
{
	private readonly AboutViewModel _viewModel;

	public AboutPage()
	{
		InitializeComponent();

		BindingContext = _viewModel = new AboutViewModel();
	}

	private async void MoreInfo_ClickedAsync(object sender, EventArgs e)
	{
        await Launcher.Default.OpenAsync(_viewModel.MoreInfoUrl);
    }
}
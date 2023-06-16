using MyFirstAppMAUI.ViewModels;

namespace MyFirstAppMAUI.Views;

public partial class AllNotesPage : ContentPage
{
	private readonly AllNotesViewModel _viewModel;

	public AllNotesPage()
	{
		InitializeComponent();

		BindingContext = _viewModel = new AllNotesViewModel();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		_viewModel.LoadItemsCommand.Execute(null);
	}
}
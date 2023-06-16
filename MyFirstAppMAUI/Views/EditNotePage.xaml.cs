using MyFirstAppMAUI.ViewModels;

namespace MyFirstAppMAUI.Views;

public partial class EditNotePage : ContentPage
{
	private readonly EditNoteViewModel _viewModel;

	public EditNotePage()
	{
		InitializeComponent();

		BindingContext = _viewModel = new EditNoteViewModel();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		_viewModel.LoadItemsCommand.Execute(null);
	}
}
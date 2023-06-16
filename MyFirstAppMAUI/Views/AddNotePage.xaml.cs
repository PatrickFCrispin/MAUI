using MyFirstAppMAUI.ViewModels;

namespace MyFirstAppMAUI.Views;

public partial class AddNotePage : ContentPage
{
    public AddNotePage()
	{
		InitializeComponent();

        BindingContext = new AddNoteViewModel();
    }
}
using MyFirstAppMAUI.Views;

namespace MyFirstAppMAUI.ViewModels
{
    public class AddNoteViewModel : BaseViewModel
    {
        public Command CancelCommand { get; }

        public AddNoteViewModel()
        {
            Title = "Nova nota";
            CancelCommand = new Command(async () => await GoToRouteAsync($"//{nameof(AllNotesPage)}"));
            SaveCommand = new Command(async () => await OnSaveAsync(), ValidateSave);
            // Invokes a delegate
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        private async Task OnSaveAsync()
        {
            var randomFileName = $"{Path.GetRandomFileName()}.notes.txt";
            var filePath = Path.Combine(FileSystem.AppDataDirectory, randomFileName);
            try
            {
                File.WriteAllText(filePath, Description);

                await Shell.Current.DisplayAlert("SUCESSO", Messages.AddedSuccessfully, "OK");
                await GoToRouteAsync($"//{nameof(AllNotesPage)}");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ERRO", ex.Message, "OK");
            }
        }

        private static class Messages
        {
            public const string AddedSuccessfully = "Nota criada e adicionada com sucesso!";
        }
    }
}
using MyFirstAppMAUI.Models;
using MyFirstAppMAUI.Views;

namespace MyFirstAppMAUI.ViewModels
{
    [QueryProperty(nameof(FileName), nameof(FileName))]
    public class EditNoteViewModel : BaseViewModel
    {
        public Command RemoveCommand { get; }

        public EditNoteViewModel()
        {
            Title = "Edição";
            LoadItemsCommand = new Command(async () => await GetItemAsync(FileName));
            RemoveCommand = new Command(async () => await OnRemoveAsync());
            SaveCommand = new Command(async () => await OnSaveAsync(), ValidateSave);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        private async Task GetItemAsync(string fileName)
        {
            try
            {
                var note = Directory.EnumerateFiles(FileSystem.AppDataDirectory, "*.notes.txt")
                    .Select(note => new Note()
                    {
                        Filename = note,
                        Description = File.ReadAllText(note),
                        CreatedAt = File.GetCreationTime(note),
                    })
                    .FirstOrDefault(note => note.Filename == fileName);

                if (note is null)
                {
                    await Shell.Current.DisplayAlert("ERRO", Messages.NoteNotFound, "OK");
                    await GoToRouteAsync($"//{nameof(AllNotesPage)}");
                    return;
                }

                Description = note.Description;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ERRO", ex.Message, "OK");
            }
        }

        private async Task OnRemoveAsync()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, FileName);
            try
            {
                var accepted = await Shell.Current.DisplayAlert("INFO", Messages.AsksIfWantToRemoveNote, "OK", "Cancelar");
                if (!accepted) { return; }

                File.Delete(filePath);

                await Shell.Current.DisplayAlert("SUCESSO", Messages.NoteSuccessfullyRemoved, "OK");
                await GoToRouteAsync($"//{nameof(AllNotesPage)}");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ERRO", ex.Message, "OK");
            }
        }

        private async Task OnSaveAsync()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, FileName);
            try
            {
                var createdAt = File.GetCreationTime(filePath);

                File.WriteAllText(filePath, Description);
                File.SetCreationTime(filePath, createdAt);

                await Shell.Current.DisplayAlert("SUCESSO", Messages.NoteSuccessfullyUpdated, "OK");
                await GoToRouteAsync($"//{nameof(AllNotesPage)}");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ERRO", ex.Message, "OK");
            }
        }

        private static class Messages
        {
            public const string NoteNotFound = "Nota não encontrada.";
            public const string AsksIfWantToRemoveNote = "Tem certeza que deseja remover esta nota?";
            public const string NoteSuccessfullyRemoved = "Nota removida com sucesso!";
            public const string NoteSuccessfullyUpdated = "Nota atualizada com sucesso!";
        }
    }
}
using MyFirstAppMAUI.Models;
using MyFirstAppMAUI.Views;
using System.Collections.ObjectModel;

namespace MyFirstAppMAUI.ViewModels
{
    public class AllNotesViewModel : BaseViewModel
    {
        private bool _isListEmpty;
        private string _listEmptyMessage;

        public bool IsListEmpty
        {
            get => _isListEmpty;
            private set { SetProperty(ref _isListEmpty, value); }
        }

        public string ListEmptyMessage
        {
            get => _listEmptyMessage;
            private set { SetProperty(ref _listEmptyMessage, value); }
        }

        public ObservableCollection<Note> Items { get; private set; }
        public Command AddItemCommand { get; }
        public Command<Note> ItemTappedCommand { get; }

        public AllNotesViewModel()
        {
            Title = "Notas";
            Items = new ObservableCollection<Note>();
            LoadItemsCommand = new Command(async () => await LoadNotesAsync());
            AddItemCommand = new Command(async () => await GoToRouteAsync($"{nameof(AddNotePage)}"));
            ItemTappedCommand = new Command<Note>(async (note) => await GoToRouteAsync($"{nameof(EditNotePage)}?{nameof(FileName)}={note.Filename}"));
        }

        private async Task LoadNotesAsync()
        {
            try
            {
                var notes = Directory.EnumerateFiles(FileSystem.AppDataDirectory, "*.notes.txt")
                    .Select(note => new Note
                    {
                        Filename = note,
                        Description = File.ReadAllText(note),
                        CreatedAt = File.GetCreationTime(note),
                    })
                    .OrderBy(note => note.CreatedAt);

                if (!notes.Any())
                {
                    IsListEmpty = true;
                    ListEmptyMessage = Messages.NoItemsToShow;
                    return;
                }

                HandleSuccessfulLoadingOf(notes);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ERRO", ex.Message, "OK");
            }
        }

        private void HandleSuccessfulLoadingOf(IOrderedEnumerable<Note> notes)
        {
            IsListEmpty = false;
            Items.Clear();
            foreach (var note in notes)
            {
                Items.Add(note);
            }
        }

        private static class Messages
        {
            public const string NoItemsToShow = "Nenhuma nota cadastrada.";
        }
    }
}
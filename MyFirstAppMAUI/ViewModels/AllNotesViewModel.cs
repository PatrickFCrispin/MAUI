﻿using MyFirstAppMAUI.Models;
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

        public ObservableCollection<Note> Items { get; }
        public Command<Note> ItemTappedCommand { get; }
        public Command AddItemCommand { get; }

        public AllNotesViewModel()
        {
            Title = "Notas";
            LoadItemsCommand = new Command(async () => await LoadNotesAsync());
            Items = new ObservableCollection<Note>();
            ItemTappedCommand = new Command<Note>(async (note) => await GoToRouteAsync($"{nameof(EditNotePage)}?{nameof(FileName)}={note.Filename}"));
            AddItemCommand = new Command(async () => await GoToRouteAsync($"{nameof(AddNotePage)}"));
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
                    ListEmptyMessage = Messages.ListEmpty;
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
            public const string ListEmpty = "Nenhuma nota cadastrada.";
        }
    }
}
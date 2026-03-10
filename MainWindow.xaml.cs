using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Text.Json;
using System.IO;

namespace RaidPlanner
{
    public partial class MainWindow : Window
    {
        private readonly string notesFilePath = "notes.json";

        public ObservableCollection<Note> NotesList { get; set; } = new ObservableCollection<Note>();

        public MainWindow()
        {
            InitializeComponent();

            NotesListBox.ItemsSource = NotesList;
            LoadNotesFromFile();

            this.Closing += MainWindow_Closing;
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            AddNoteWindow addNoteWindow = new AddNoteWindow();
            bool? result = addNoteWindow.ShowDialog();

            if (result == true)
            {
                NotesList.Add(new Note { Title = addNoteWindow.NoteTitle, Content = addNoteWindow.NoteContent });
            }
        }

        private void DeleteNoteButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotesListBox.SelectedItem is Note selectedNote)
            {
                var result = MessageBox.Show($"Are you sure you want to delete \"{selectedNote.Title}\"?",
                                             "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    NotesList.Remove(selectedNote);
                    NoteContentTextBox.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Please select a note to delete.", "No Note Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditNoteButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotesListBox.SelectedItem is Note selectedNote)
            {
                AddNoteWindow window = new AddNoteWindow(selectedNote.Title, selectedNote.Content);

                bool? result = window.ShowDialog();

                if (result == true)
                {
                    selectedNote.Title = window.NoteTitle;
                    selectedNote.Content = window.NoteContent;

                    NotesListBox.Items.Refresh();
                    NoteContentTextBox.Text = selectedNote.Content;
                }
            }
            else
            {
                MessageBox.Show("Select a note to edit.");
            }
        }

        private void NotesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NotesListBox.SelectedItem is Note selectedNote)
                NoteContentTextBox.Text = selectedNote.Content;
            else
                NoteContentTextBox.Text = "";
        }

        private void LoadNotesFromFile()
        {
            try
            {
                if (File.Exists(notesFilePath))
                {
                    string json = File.ReadAllText(notesFilePath);
                    var notes = JsonSerializer.Deserialize<ObservableCollection<Note>>(json);
                    if (notes != null)
                    {
                        NotesList = notes;
                        NotesListBox.ItemsSource = NotesList;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading notes: " + ex.Message);
            }
        }

        private void SaveNotesToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(NotesList);
                File.WriteAllText(notesFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving notes: " + ex.Message);
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveNotesToFile();
        }
    }

    // Prosta klasa Note
    public class Note
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
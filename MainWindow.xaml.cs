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
        private string plansFile = "plans.json";

        public ObservableCollection<Note> NotesList { get; set; } = new ObservableCollection<Note>();

        public MainWindow()
        {
            InitializeComponent();

            LoadPlans();
            PlansListBox.ItemsSource = plans;

            LoadWishlists();
            WishlistListBox.ItemsSource = wishlists;

            NotesListBox.ItemsSource = NotesList;
            LoadNotesFromFile();

            LoadHistory();
            HistoryListBox.ItemsSource = history;

            this.Closing += MainWindow_Closing;
        }

        private ObservableCollection<RaidPlan> plans = new ObservableCollection<RaidPlan>();

        private void AddPlanButton_Click(object sender, RoutedEventArgs e)
        {
            string map = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter map name:",
                "New Raid Plan",
                "");

            if (string.IsNullOrWhiteSpace(map))
                return;

            string objective = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter objective:",
                "Raid Plan",
                "");

            string gear = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter gear:",
                "Raid Plan",
                "");

            string notes = Microsoft.VisualBasic.Interaction.InputBox(
                "Additional notes:",
                "Raid Plan",
                "");

            string wishlist = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter wishlist name to use:",
                "Raid Plan",
                "");

            plans.Add(new RaidPlan
            {
                MapName = map,
                Objective = objective,
                Gear = gear,
                Notes = notes,
                WishlistName = wishlist
            });
        }

        private void DeletePlanButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlansListBox.SelectedItem is RaidPlan selected)
            {
                plans.Remove(selected);
            }
        }

        private void PlansListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlansListBox.SelectedItem is RaidPlan selected)
            {
                ObjectiveText.Text = selected.Objective;
                GearText.Text = selected.Gear;
                NotesText.Text = selected.Notes;
                WishlistText.Text = selected.WishlistName;
            }
        }

        private void EditPlanButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlansListBox.SelectedItem is RaidPlan selected)
            {
                string map = Microsoft.VisualBasic.Interaction.InputBox(
                    "Edit map name:",
                    "Edit Plan",
                    selected.MapName);

                string objective = Microsoft.VisualBasic.Interaction.InputBox(
                    "Edit objective:",
                    "Edit Plan",
                    selected.Objective);

                string gear = Microsoft.VisualBasic.Interaction.InputBox(
                    "Edit gear:",
                    "Edit Plan",
                    selected.Gear);

                string notes = Microsoft.VisualBasic.Interaction.InputBox(
                    "Edit notes:",
                    "Edit Plan",
                    selected.Notes);

                string wishlist = Microsoft.VisualBasic.Interaction.InputBox(
                    "Edit wishlist:",
                    "Edit Plan",
                    selected.WishlistName);

                selected.MapName = map;
                selected.Objective = objective;
                selected.Gear = gear;
                selected.Notes = notes;
                selected.WishlistName = wishlist;

                PlansListBox.Items.Refresh();
            }
        }

        private void SavePlans()
        {
            try
            {
                string json = JsonSerializer.Serialize(plans);
                File.WriteAllText(plansFile, json);
            }
            catch
            {
                MessageBox.Show("Error saving plans.");
            }
        }

        private void LoadPlans()
        {
            try
            {
                if (File.Exists(plansFile))
                {
                    string json = File.ReadAllText(plansFile);

                    var loaded = JsonSerializer.Deserialize<ObservableCollection<RaidPlan>>(json);

                    if (loaded != null)
                        plans = loaded;
                }
            }
            catch
            {
                MessageBox.Show("Error loading plans.");
            }
        }


        private ObservableCollection<Wishlist> wishlists = new ObservableCollection<Wishlist>();
        private string wishlistFile = "wishlists.json";

        private void AddWishlistButton_Click(object sender, RoutedEventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter wishlist name:",
                "New Wishlist",
                "");

            if (!string.IsNullOrWhiteSpace(name))
            {
                wishlists.Add(new Wishlist { Name = name });
            }
        }

        private void DeleteWishlistButton_Click(object sender, RoutedEventArgs e)
        {
            if (WishlistListBox.SelectedItem is Wishlist selected)
            {
                wishlists.Remove(selected);
            }
            else
            {
                MessageBox.Show("Select a wishlist first.");
            }
        }

        private void WishlistListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WishlistListBox.SelectedItem is Wishlist selected)
            {
                WishlistItemsListBox.ItemsSource = selected.Items;
            }
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (WishlistListBox.SelectedItem is Wishlist selected)
            {
                string item = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter item name:",
                    "Add Item",
                    "");

                selected.Items.Add(new WishlistItem
                {
                    Name = item,
                    IsFound = false
                });
            }
            else
            {
                MessageBox.Show("Select a wishlist first.");
            }
        }

        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (WishlistListBox.SelectedItem is Wishlist selected &&
                WishlistItemsListBox.SelectedItem is WishlistItem item)
            {
                selected.Items.Remove(item);
            }
        }

        private void SaveWishlists()
        {
            try
            {
                string json = JsonSerializer.Serialize(wishlists);
                File.WriteAllText(wishlistFile, json);
            }
            catch
            {
                MessageBox.Show("Error saving wishlists.");
            }
        }

        private void LoadWishlists()
        {
            try
            {
                if (File.Exists(wishlistFile))
                {
                    string json = File.ReadAllText(wishlistFile);

                    var loaded = JsonSerializer.Deserialize<ObservableCollection<Wishlist>>(json);

                    if (loaded != null)
                        wishlists = loaded;
                }
            }
            catch
            {
                MessageBox.Show("Error loading wishlists.");
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            SaveWishlists();
            SavePlans();
            SaveHistory();
            base.OnClosed(e);
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

        private ObservableCollection<HistoryEntry> history = new ObservableCollection<HistoryEntry>();
        private string historyFile = "history.json";

        private void AddHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            string map = Microsoft.VisualBasic.Interaction.InputBox(
                "Map name:",
                "History",
                "");

            string wishlist = Microsoft.VisualBasic.Interaction.InputBox(
                "Wishlist used:",
                "History",
                "");

            string items = Microsoft.VisualBasic.Interaction.InputBox(
                "What did you find:",
                "History",
                "");

            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            history.Add(new HistoryEntry
            {
                MapName = map,
                WishlistName = wishlist,
                FoundItems = items,
                Date = date
            });
        }

        private void DeleteHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (HistoryListBox.SelectedItem is HistoryEntry selected)
            {
                history.Remove(selected);
            }
        }

        private void SaveHistory()
        {
            try
            {
                string json = JsonSerializer.Serialize(history);
                File.WriteAllText(historyFile, json);
            }
            catch
            {
                MessageBox.Show("Error saving history.");
            }
        }

        private void LoadHistory()
        {
            try
            {
                if (File.Exists(historyFile))
                {
                    string json = File.ReadAllText(historyFile);

                    var loaded = JsonSerializer.Deserialize<ObservableCollection<HistoryEntry>>(json);

                    if (loaded != null)
                        history = loaded;
                }
            }
            catch
            {
                MessageBox.Show("Error loading history.");
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




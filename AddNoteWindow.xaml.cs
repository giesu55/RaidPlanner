using System.Windows;

namespace RaidPlanner
{
    public partial class AddNoteWindow : Window
    {
        public string NoteTitle { get; private set; }
        public string NoteContent { get; private set; }

        public AddNoteWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            NoteTitle = TitleTextBox.Text;
            NoteContent = ContentTextBox.Text;

            if (string.IsNullOrWhiteSpace(NoteTitle))
            {
                MessageBox.Show("Title cannot be empty!");
                return;
            }

            this.DialogResult = true;
        }
    }
}
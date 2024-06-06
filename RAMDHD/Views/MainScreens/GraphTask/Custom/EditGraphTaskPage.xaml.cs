using RAMDHD.DataAccess;
using RAMDHD.Database;
using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.MainScreens.Mindfulness;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using System.Windows.Input;

namespace RAMDHD.Views.MainScreens.GraphTask.Custom
{
    public partial class EditGraphTaskPage : ContentPage
    {
        private int activePage;
        private int placeholderId;
        //private GraphTaskViewModel viewModel;
        private static string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Database\\ramdhd.db");
        private DatabaseDataAccess _databaseAccess = new DatabaseDataAccess(new DatabaseConnection(dbPath));

        public EditGraphTaskPage(string placeholderIdString)
        {
            ActivePage = 4;
            InitializeComponent();
            BindingContext = this;
            if (int.TryParse(placeholderIdString, out placeholderId))
            {
                // Fetch and load the graph task data
                LoadGraphTaskDataAsync(placeholderId);
            }
            else
            {
                // Handle the case where the ID is not valid
                DisplayAlert("Error", "Invalid Graph Task ID", "OK");
            }
        }
        public int ActivePage
        {
            get => activePage;
            set
            {
                if (activePage != value)
                {
                    activePage = value;
                    OnPropertyChanged(nameof(ActivePage));
                }
            }
        }
        public ICommand NavigateCommand => new Command<string>(async (page) =>
        {
            switch (page)
            {
                case "Attention":
                    await this.Navigation.PushAsync(new AttentionHomePage());
                    break;
                case "Organization":
                    await this.Navigation.PushAsync(new OrganizationHomePage());
                    break;
                case "People":
                    await this.Navigation.PushAsync(new PeopleHomePage());
                    break;
                case "Mindfulness":
                    await this.Navigation.PushAsync(new MindfulnessHomePage());
                    break;
                case "GraphTasks":
                    await this.Navigation.PushAsync(new ProcrastinationHomePage());
                    break;
            }
        });
        private async Task LoadGraphTaskDataAsync(int id)
        {
            var graphTask = await _databaseAccess.GetGraphTaskByIdAsync(id);
            if (graphTask != null)
            {
                TitleEntry.Text = graphTask.Title;
                Activity1Entry.Text = graphTask.Activity1;
                Activity2Entry.Text = graphTask.Activity2;
                Activity3Entry.Text = graphTask.Activity3;
                Activity4Entry.Text = graphTask.Activity4;

                switch (graphTask.ProcrastinationActivity)
                {
                    case 1:
                        Activity1Radio.IsChecked = true;
                        break;
                    case 2:
                        Activity2Radio.IsChecked = true;
                        break;
                    case 3:
                        Activity3Radio.IsChecked = true;
                        break;
                    default:
                        break;
                }
            }
        }
        private async void OnSaveButtonClickedAsync(object sender, EventArgs e)
        {
            int procrastinationTask = 0;
            if (Activity1Radio.IsChecked) procrastinationTask = 1;
            else if (Activity2Radio.IsChecked) procrastinationTask = 2;
            else if (Activity3Radio.IsChecked) procrastinationTask = 3;

            // Save the graph task details to your data source or print to console
            Console.WriteLine($"Task: {TitleEntry.Text}, Procrastination Task: {procrastinationTask}");

            // Create a new GraphTask object
            var graphTask = new Models.GraphTask
            {
                Title = !string.IsNullOrWhiteSpace(TitleEntry.Text) ? TitleEntry.Text : "default",
                Activity1 = !string.IsNullOrWhiteSpace(Activity1Entry.Text) ? Activity1Entry.Text : "default",
                Activity2 = !string.IsNullOrWhiteSpace(Activity2Entry.Text) ? Activity2Entry.Text : "default",
                Activity3 = !string.IsNullOrWhiteSpace(Activity3Entry.Text) ? Activity3Entry.Text : "default",
                Activity4 = !string.IsNullOrWhiteSpace(Activity4Entry.Text) ? Activity4Entry.Text : "default",
                ProcrastinationActivity = int.IsEvenInteger(procrastinationTask) ? procrastinationTask : 0,
            };

            // Assume _database is an instance of your DatabaseConnection with a proper DB path.
            await _databaseAccess.InsertOrUpdateGraphTaskByIdAsync(graphTask, placeholderId);

            // Show success message
            await DisplayAlert("Success", $"Graph Task {TitleEntry.Text} saved successfully", "OK");

            // Navigate to Graph Task Menu
            await this.Navigation.PushAsync(new GraphTaskMenu());
        }
        private async void OnCancelButtonClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

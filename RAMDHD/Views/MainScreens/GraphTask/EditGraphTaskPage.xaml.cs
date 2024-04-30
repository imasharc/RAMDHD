using RAMDHD.DataAccess;
using RAMDHD.Database;
using RAMDHD.ViewModels.GraphTask;
using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.MainScreens.Entertainment;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMDHD.Views.MainScreens.GraphTask
{
    public partial class EditGraphTaskPage : ContentPage
    {
        private int placeholderId;
        //private GraphTaskViewModel viewModel;
        private static string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Database\\ramdhd.db");
        private DatabaseDataAccess _databaseAccess = new DatabaseDataAccess(new DatabaseConnection(dbPath));

        public EditGraphTaskPage(string placeholderIdString)
        {
            InitializeComponent();
            if (int.TryParse(placeholderIdString, out placeholderId))
            {
                // placeholderId now holds the ID passed from the previous page
            }
            else
            {
                // Handle the case where the ID is not valid
            }
        }

        public EditGraphTaskPage()
        {
            InitializeComponent();

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
                Title = TitleEntry.Text,
                Activity1 = Activity1Entry.Text,
                Activity2 = Activity2Entry.Text,
                Activity3 = Activity3Entry.Text,
                Activity4 = Activity4Entry.Text,
                ProcrastinationActivity = procrastinationTask
            };

            // Assume _database is an instance of your DatabaseConnection with a proper DB path.
            await _databaseAccess.InsertOrUpdateGraphTaskByIdAsync(graphTask, placeholderId);

            // Show success message
            await DisplayAlert("Success", $"Graph Task {placeholderId} saved successfully", "OK");

            // Retrieve and log all graph tasks
            var allGraphTasks = await _databaseAccess.GetAllGraphTasksAsync();
            foreach (var task in allGraphTasks)
            {
                Console.WriteLine($"ID: {task.Id}, Title: {task.Title}, Procrastination Task Index: {task.ProcrastinationActivity}");
            }

            // Navigate to Graph Task Menu
            await this.Navigation.PushAsync(new GraphTaskMenu());
        }
        private async void OnCancelButtonClickedAsync(object sender, EventArgs e)
        {
            Console.WriteLine("OnCancelButtonClicked");

            // Pop the current page off the stack
            await Navigation.PopAsync();
        }
        private async void OnAttentionClicked(object sender, EventArgs e)
        {
            Console.WriteLine("OnAttentionClicked");
            // Navigate to AttentionHomePage
            await this.Navigation.PushAsync(new AttentionHomePage());
        }
        private async void OnOrganizationClicked(object sender, EventArgs e)
        {
            Console.WriteLine("OnOrganizationClicked");
            // Navigate to OrganizationHomePage
            await this.Navigation.PushAsync(new OrganizationHomePage());
        }
        private async void OnPeopleClicked(object sender, EventArgs e)
        {
            Console.WriteLine("OnAttentionClicked");
            // Navigate to PeopleHomePage
            await this.Navigation.PushAsync(new PeopleHomePage());
        }
        private async void OnEntertainmentClicked(object sender, EventArgs e)
        {
            Console.WriteLine("OnEntertainmentClicked");
            // Navigate to EntertainmentHomePage
            await this.Navigation.PushAsync(new EntertainmentHomePage());
        }
        private async void OnGraphTasksClicked(object sender, EventArgs e)
        {
            Console.WriteLine("GraphTasks");
            await this.Navigation.PushAsync(new ProcrastinationHomePage());
        }
    }
}

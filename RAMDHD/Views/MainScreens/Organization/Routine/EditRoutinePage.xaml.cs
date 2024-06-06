using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.MainScreens.Mindfulness;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.People;
using RAMDHD.DataAccess;
using RAMDHD.Database;
using System.Windows.Input;

namespace RAMDHD.Views.MainScreens.Organization.Routine;

public partial class EditRoutinePage : ContentPage
{
    private int activePage;
    private int placeholderId;
    private static string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Database\\ramdhd.db");
    private DatabaseDataAccess _databaseAccess = new DatabaseDataAccess(new DatabaseConnection(dbPath));
    public EditRoutinePage(string placeholderIdString)
    {
        ActivePage = 1;
        InitializeComponent();
        BindingContext = this;
        if (int.TryParse(placeholderIdString, out placeholderId))
        {
            // Fetch and load the graph task data
            LoadRoutineDataAsync(placeholderId);
        }
        else
        {
            // Handle the case where the ID is not valid
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
    private async Task LoadRoutineDataAsync(int id)
    {
        var routine = await _databaseAccess.GetRoutineByIdAsync(id);
        if (routine != null)
        {
            HeadlineEntry.Text = routine.Title;
            DescriptionEntry.Text = routine.Description;
            Step1Entry.Text = routine.Step1;
            Step2Entry.Text = routine.Step2;
            Step3Entry.Text = routine.Step3;
            Step4Entry.Text = routine.Step4;

        }
    }
    private async void OnSaveButtonClickedAsync(object sender, EventArgs e)
    {
        // Create a new Routine object
        var routine = new Models.Routine
        {
            Title = !string.IsNullOrWhiteSpace(HeadlineEntry.Text) ? HeadlineEntry.Text : "default",
            Description = !string.IsNullOrWhiteSpace(DescriptionEntry.Text) ? DescriptionEntry.Text : "default",
            Step1 = !string.IsNullOrWhiteSpace(Step1Entry.Text) ? Step1Entry.Text : "default",
            Step2 = !string.IsNullOrWhiteSpace(Step2Entry.Text) ? Step2Entry.Text : "default",
            Step3 = !string.IsNullOrWhiteSpace(Step3Entry.Text) ? Step3Entry.Text : "default",
            Step4 = !string.IsNullOrWhiteSpace(Step4Entry.Text) ? Step4Entry.Text : "default",

        };

        // Assume _database is an instance of your DatabaseConnection with a proper DB path.
        await _databaseAccess.InsertOrUpdateRoutineByIdAsync(routine, placeholderId);

        // Show success message
        await DisplayAlert("Success", $"Routine {placeholderId} saved successfully", "OK");

        // Navigate to Graph Task Menu
        await this.Navigation.PushAsync(new RoutineMenuPage());
    }
    private async void OnCancelButtonClickedAsync(object sender, EventArgs e)
    {
        // Pop the current page off the stack
        await Navigation.PopAsync();
    }
}
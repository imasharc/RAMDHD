using RAMDHD.DataAccess;
using RAMDHD.Database;
using RAMDHD.Views.MainScreens.Mindfulness;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using System.Windows.Input;

namespace RAMDHD.Views.MainScreens.Attention;

public partial class NoteTemplatePage : ContentPage
{
    private int activePage;
    private static string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Database\\ramdhd.db");
    private DatabaseDataAccess _databaseAccess = new DatabaseDataAccess(new DatabaseConnection(dbPath));
    public NoteTemplatePage(string id)
	{
        InitializeComponent();
        ActivePage = 0;
        _databaseAccess = new DatabaseDataAccess(new DatabaseConnection(Path.Combine(FileSystem.AppDataDirectory, "Database\\ramdhd.db")));
        LoadNoteData(int.Parse(id));
        BindingContext = this;
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
    private async void LoadNoteData(int id)
    {
        var note = await _databaseAccess.GetNoteByIdAsync(id);
        if (note != null)
        {
            // Assign the values from graphTask to your UI elements
            HeadlineLabel.Text = note.Headline;
            DescriptionLabel.Text = note.Description;

            // Set the titles for your Labels
            this.Title = note.Headline;
        }
        else
        {
            // Handle the case where no task was found, maybe navigate back or show an error
            await DisplayAlert("Error", "Note not found.", "OK");
            await Navigation.PopAsync();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}
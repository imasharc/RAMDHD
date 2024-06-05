using RAMDHD.DataAccess;
using RAMDHD.Database;
using RAMDHD.Views.MainScreens.Mindfulness;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using System.Windows.Input;
using System.ComponentModel;

namespace RAMDHD.Views.MainScreens.Attention;

public partial class EditNotePage : ContentPage, INotifyPropertyChanged
{
    private int placeholderId;
    private int activePage;
    private static string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Database\\ramdhd.db");
    private DatabaseDataAccess _databaseAccess = new DatabaseDataAccess(new DatabaseConnection(dbPath));

    public EditNotePage(string placeholderIdString)
	{
        InitializeComponent();
        if (int.TryParse(placeholderIdString, out placeholderId))
        {
            ActivePage = 0;
            BindingContext = this;
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

    private async void OnSaveButtonClickedAsync(object sender, EventArgs e)
    {
        // Save the graph task details to your data source or print to console
        Console.WriteLine($"Note: {HeadlineEntry.Text}, Description: {DescriptionEntry.Text}");

        // Create a new GraphTask object
        var note = new Models.Note
        {
            Headline = !string.IsNullOrWhiteSpace(HeadlineEntry.Text) ? HeadlineEntry.Text : "default",
            Description = !string.IsNullOrWhiteSpace(DescriptionEntry.Text) ? DescriptionEntry.Text : "default",
        };

        // Assume _database is an instance of your DatabaseConnection with a proper DB path.
        await _databaseAccess.InsertOrUpdateNoteByIdAsync(note, placeholderId);

        // Show success message
        await DisplayAlert("Success", $"Note {HeadlineEntry.Text} saved successfully", "OK");

        await this.Navigation.PushAsync(new NotesMenuPage());
    }
    private async void OnCancelButtonClickedAsync(object sender, EventArgs e)
    {
        Console.WriteLine("OnCancelButtonClicked");
        await Navigation.PopAsync();
    }
}
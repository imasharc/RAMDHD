using RAMDHD.DataAccess;
using RAMDHD.Database;
using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.MainScreens.Mindfulness;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.People;
using System.Windows.Input;

namespace RAMDHD.Views.MainScreens.Organization.Routine;

public partial class RoutineTemplatePage : ContentPage
{
    private int activePage;
    private static string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Database\\ramdhd.db");
    private DatabaseDataAccess _databaseAccess = new DatabaseDataAccess(new DatabaseConnection(dbPath));
    public RoutineTemplatePage(string id)
	{
        ActivePage = 1;
        InitializeComponent();
        BindingContext = this;
        LoadRoutineData(int.Parse(id));
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
    private async void LoadRoutineData(int id)
    {
        var routine = await _databaseAccess.GetRoutineByIdAsync(id);
        Console.WriteLine($"Routine: {routine.Title}, Description: {routine.Description}, Step1: {routine.Step1}, Step2: {routine.Step2}, Step3: {routine.Step3}, Step4: {routine.Step4}");

        if (routine != null)
        {
            // Assign the values from graphTask to your UI elements
            TitleLabel.Text = routine.Title;
            DescriptionLabel.Text = routine.Description;
            Step1Label.Text = routine.Step1;
            Step2Label.Text = routine.Step2;
            Step3Label.Text = routine.Step3;
            Step4Label.Text = routine.Step4;


            // Set the titles for your Labels
            this.Title = routine.Title;
        }
        else
        {
            // Handle the case where no task was found, maybe navigate back or show an error
            await DisplayAlert("Error", "Note not found.", "OK");
            await Navigation.PopAsync();
        }
    }
    private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (Step1CheckBox.IsChecked && Step2CheckBox.IsChecked && Step3CheckBox.IsChecked && Step4CheckBox.IsChecked)
        {
            DisplayAlert("Congratulations!", "You have completed all the routine steps!", "OK");
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
    private async void OnBackToMenuButtonClickedAsync(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
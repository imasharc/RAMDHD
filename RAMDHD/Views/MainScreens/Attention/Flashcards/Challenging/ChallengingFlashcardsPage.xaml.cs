using RAMDHD.Views.MainScreens.Attention.Flashcards.Challenging.Sample;
using RAMDHD.Views.MainScreens.Mindfulness;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using RAMDHD.Views.MainScreens.Attention.Flashcards.Challenging.Custom;
using System.Windows.Input;

namespace RAMDHD.Views.MainScreens.Attention.Flashcards.Challenging;

public partial class ChallengingFlashcardsPage : ContentPage
{
    private int activePage;
    public ChallengingFlashcardsPage()
	{
        ActivePage = 0;
        InitializeComponent();
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
    private async void OnSampleChallengesClicked(object sender, EventArgs e)
    {
        await this.Navigation.PushAsync(new SampleChallengesFlashcardsPage());
    }
    private async void OnCustomChallengesClicked(object sender, EventArgs e)
    {
        await this.Navigation.PushAsync(new CustomChallengesFlashcardsPage());
    }
}
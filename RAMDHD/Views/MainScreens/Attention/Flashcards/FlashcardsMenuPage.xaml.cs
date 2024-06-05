using RAMDHD.Views.MainScreens.Attention.Flashcards.Challenging;
using RAMDHD.Views.MainScreens.Attention.Flashcards.Educational;
using RAMDHD.Views.MainScreens.Mindfulness;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using System.Windows.Input;

namespace RAMDHD.Views.MainScreens.Attention.Flashcards;

public partial class FlashcardsMenuPage : ContentPage
{
    private int activePage;
    public FlashcardsMenuPage()
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
    private async void OnEducationalFlashcardsClicked(object sender, EventArgs e)
    {
        await this.Navigation.PushAsync(new EducationalFlashcardsPage());
    }
    private async void OnChallengingFlashcardsClicked(object sender, EventArgs e)
    {
        await this.Navigation.PushAsync(new ChallengingFlashcardsPage());
    }
}
using RAMDHD.Database.Flashcards;
using RAMDHD.Models;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using RAMDHD.Views.MainScreens.Attention;
using System.Windows.Input;

namespace RAMDHD.Views.MainScreens.Mindfulness.Affirmations;

public partial class AffirmationsPage : ContentPage
{
    private int activePage;
    private List<Flashcard> flashcards;
    private Random random = new Random();

    public AffirmationsPage()
    {
        ActivePage = 3;
        InitializeComponent();
        BindingContext = this;
        LoadFlashcards();
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
    private void LoadFlashcards()
    {
        flashcards = AffirmationsRepository.GetFlashcards();
        if (flashcards.Count > 0)
        {
            DisplayFlashcard(flashcards[0]);
        }
    }

    private void DisplayFlashcard(Flashcard flashcard)
    {
        Flashcard1Front.Text = flashcard.Question;
    }

    private async void OnRandomButtonClicked(object sender, EventArgs e)
    {
        var randomIndex = random.Next(flashcards.Count);
        DisplayFlashcard(flashcards[randomIndex]);
    }
}
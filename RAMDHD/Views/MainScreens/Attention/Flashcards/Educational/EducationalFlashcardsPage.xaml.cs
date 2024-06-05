using RAMDHD.Database.Flashcards;
using RAMDHD.Models;
using RAMDHD.Views.MainScreens.Mindfulness;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using System.Windows.Input;

namespace RAMDHD.Views.MainScreens.Attention.Flashcards.Educational;

public partial class EducationalFlashcardsPage : ContentPage
{
    private int activePage;
    private bool isFlashcardFlipped = false;
    private List<Flashcard> flashcards;
    private Random random = new Random();
    private const uint rotationDuration = 130; // Adjusted rotation duration

    public EducationalFlashcardsPage()
    {
        ActivePage = 0;
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
        flashcards = EducationalFlashcardsRepository.GetFlashcards();
        if (flashcards.Count > 0)
        {
            DisplayFlashcard(flashcards[0]);
        }
    }

    private void DisplayFlashcard(Flashcard flashcard)
    {
        Flashcard1Front.Text = flashcard.Question;
        Flashcard1Back.Text = flashcard.Answer;
    }

    private async void OnFlashcard1Tapped(object sender, EventArgs e)
    {
        if (isFlashcardFlipped)
        {
            await FlipToFront(Flashcard1, Flashcard1Front, Flashcard1Back);
        }
        else
        {
            await FlipToBack(Flashcard1, Flashcard1Front, Flashcard1Back);
        }
        isFlashcardFlipped = !isFlashcardFlipped;
    }

    private async Task FlipToFront(View flashcard, View front, View back)
    {
        Flashcard1.HasShadow = false; // Remove shadow during flip
        await flashcard.RotateYTo(90, rotationDuration);
        back.IsVisible = false;
        flashcard.BackgroundColor = Colors.White; // Change to front color

        front.IsVisible = true;
        flashcard.RotationY = -90;
        await flashcard.RotateYTo(0, rotationDuration);
        Flashcard1.HasShadow = true; // Restore shadow after flip
    }

    private async Task FlipToBack(View flashcard, View front, View back)
    {
        Flashcard1.HasShadow = false; // Remove shadow during flip
        await flashcard.RotateYTo(90, rotationDuration);
        front.IsVisible = false;
        flashcard.BackgroundColor = Colors.LightGreen; // Change to front color

        back.IsVisible = true;
        flashcard.RotationY = -90;
        await flashcard.RotateYTo(0, rotationDuration);
        Flashcard1.HasShadow = true; // Restore shadow after flip
    }

    private async void OnRandomButtonClicked(object sender, EventArgs e)
    {
        if (isFlashcardFlipped)
        {
            await FlipToFront(Flashcard1, Flashcard1Front, Flashcard1Back);
            isFlashcardFlipped = false;
        }

        var randomIndex = random.Next(flashcards.Count);
        DisplayFlashcard(flashcards[randomIndex]);
    }
}
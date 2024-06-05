using RAMDHD.Views.MainScreens.Attention.Flashcards;
using RAMDHD.Views.MainScreens.Mindfulness;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using System.Windows.Input;
using System.ComponentModel;

namespace RAMDHD.Views.MainScreens.Attention
{
    public partial class AttentionHomePage : ContentPage, INotifyPropertyChanged
    {
        private int activePage;
        public AttentionHomePage()
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

        private async void OnTimerClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TimerPage());
        }
        private async void OnNotesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotesMenuPage());
        }
        private async void OnFlashcardsClicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new FlashcardsMenuPage());
        } 
    }
}

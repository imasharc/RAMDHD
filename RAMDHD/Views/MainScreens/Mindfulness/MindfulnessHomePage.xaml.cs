using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.Mindfulness.Affirmations;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using System.Windows.Input;

namespace RAMDHD.Views.MainScreens.Mindfulness
{
    public partial class MindfulnessHomePage : ContentPage
    {
        private int activePage;
        public MindfulnessHomePage()
        {
            ActivePage = 3;
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
        private async void OnAffirmationsClicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new AffirmationsPage());
        }
    }
}

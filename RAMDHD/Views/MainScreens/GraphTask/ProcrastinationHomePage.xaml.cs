using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.MainScreens.Mindfulness;
using RAMDHD.Views.MainScreens.GraphTask.Examples;
using RAMDHD.Views.MainScreens.GraphTask.Custom;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using System.Windows.Input;

namespace RAMDHD.Views.MainScreens.GraphTask
{
    public partial class ProcrastinationHomePage : ContentPage
    {
        private int activePage;
        public ProcrastinationHomePage()
        {
            ActivePage = 4;
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
        private async void BuyingBreadGraphExample(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new BuyingBreadGraphTask());
        }
        private async void OnSkipIntroductionTapped(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new GraphTaskMenu());
        }
    }
}

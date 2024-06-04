using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.MainScreens.Mindfulness;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.Organization.Routine;
using RAMDHD.Views.MainScreens.People;
using System.Windows.Input;
using System.ComponentModel;

namespace RAMDHD.Views.MainScreens.Organization
{
    public partial class OrganizationHomePage : ContentPage, INotifyPropertyChanged
    {
        private int activePage;
        public OrganizationHomePage()
        {
            ActivePage = 1;
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
        private async void OnRoutineClicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new RoutineMenuPage());
        }
    }
}

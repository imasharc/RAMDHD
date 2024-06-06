using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.MainScreens.Mindfulness;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.Organization;
using FeatureNotSupportedException = Xamarin.Essentials.FeatureNotSupportedException;
using System.Windows.Input;

namespace RAMDHD.Views.MainScreens.People.Forum;

public partial class ForumPage : ContentPage
{
    private int activePage;
    public ForumPage()
	{
        ActivePage = 2;
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
    private void OnPlaceholder1Tapped(object sender, EventArgs e)
    {
        Launcher("https://www.facebook.com/groups/moznazwariowac");
    }

    private void OnPlaceholder2Tapped(object sender, EventArgs e)
    {
        Launcher("https://www.reddit.com/r/ADHD/");
    }

    private void OnPlaceholder3Tapped(object sender, EventArgs e)
    {
        Launcher("https://www.additudemag.com/");
    }

    private async void Launcher(string uri)
    {
        try
        {
            await Xamarin.Essentials.Launcher.OpenAsync(uri);
        }
        catch (ArgumentNullException)
        {
            await DisplayAlert("Error", "Invalid link", "OK");
        }
        catch (FeatureNotSupportedException)
        {
            await DisplayAlert("Error", "Website launcher is not supported on this device.", "OK");
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "An error occurred while trying to open the website.", "OK");
        }
    }
}
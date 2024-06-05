using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.MainScreens.Mindfulness;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.Organization;
using PhoneDialer = Xamarin.Essentials.PhoneDialer;
using FeatureNotSupportedException = Xamarin.Essentials.FeatureNotSupportedException;
using System.Windows.Input;

namespace RAMDHD.Views.MainScreens.People.Helpline;

public partial class HelplinePage : ContentPage
{
    private int activePage;
    public HelplinePage()
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
    private void OnPlaceholder1Tapped(object sender, EventArgs e)
    {
        DialPhoneNumber("800702222");
    }

    private void OnPlaceholder2Tapped(object sender, EventArgs e)
    {
        DialPhoneNumber("116123");
    }

    private void OnPlaceholder3Tapped(object sender, EventArgs e)
    {
        DialPhoneNumber("225949100");
    }

    private void OnPlaceholder4Tapped(object sender, EventArgs e)
    {
        DialPhoneNumber("222904442");
    }

    private void OnPlaceholder5Tapped(object sender, EventArgs e)
    {
        DialPhoneNumber("800100100");
    }
    private void DialPhoneNumber(string phoneNumber)
    {
        try
        {
            PhoneDialer.Open(phoneNumber);
        }
        catch (ArgumentNullException)
        {
            DisplayAlert("Error", "Invalid phone number.", "OK");
        }
        catch (FeatureNotSupportedException)
        {
            DisplayAlert("Error", "Phone dialer is not supported on this device.", "OK");
        }
        catch (Exception)
        {
            DisplayAlert("Error", "An error occurred while trying to make the call.", "OK");
        }
    }
}
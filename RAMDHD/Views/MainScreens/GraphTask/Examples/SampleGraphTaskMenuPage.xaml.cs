using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.MainScreens.Entertainment;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;

namespace RAMDHD.Views.MainScreens.GraphTask.Examples;

public partial class SampleGraphTaskMenuPage : ContentPage
{
	public SampleGraphTaskMenuPage()
	{
		InitializeComponent();
	}
    private async void NavigateToProcrastinationPage(object sender, EventArgs e)
    {
        // Navigate to BuyingBreadGraphExample
        await this.Navigation.PushAsync(new ProcrastinationHomePage());
    }
    private async void BuyingBreadGraphExample(object sender, EventArgs e)
    {
        // Navigate to BuyingBreadGraphExample
        await this.Navigation.PushAsync(new BuyingBreadGraphTask());
    }
    private async void ReadingBookGraphExample(object sender, EventArgs e)
    {
        // Navigate to ReadingBookGraphExample
        await this.Navigation.PushAsync(new ReadingBookGraphTask());
    }
    private async void NavigateToEditGraphTaskPage(object sender, EventArgs e)
    {
        // Navigate to GraphTaskMenu
        await this.Navigation.PushAsync(new EditGraphTaskPage());
    }
    private async void OnAttentionClicked(object sender, EventArgs e)
    {
        Console.WriteLine("OnAttentionClicked");
        // Navigate to AttentionHomePage
        await this.Navigation.PushAsync(new AttentionHomePage());
    }
    private async void OnOrganizationClicked(object sender, EventArgs e)
    {
        Console.WriteLine("OnOrganizationClicked");
        // Navigate to OrganizationHomePage
        await this.Navigation.PushAsync(new OrganizationHomePage());
    }
    private async void OnPeopleClicked(object sender, EventArgs e)
    {
        Console.WriteLine("OnAttentionClicked");
        // Navigate to PeopleHomePage
        await this.Navigation.PushAsync(new PeopleHomePage());
    }
    private async void OnEntertainmentClicked(object sender, EventArgs e)
    {
        Console.WriteLine("OnEntertainmentClicked");
        // Navigate to EntertainmentHomePage
        await this.Navigation.PushAsync(new EntertainmentHomePage());
    }
    private async void OnGraphTasksClicked(object sender, EventArgs e)
    {
        Console.WriteLine("GraphTasks");
        await this.Navigation.PushAsync(new ProcrastinationHomePage());
    }
}
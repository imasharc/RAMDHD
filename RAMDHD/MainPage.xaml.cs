namespace RAMDHD;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

    public async void OnTimerClicked(object sender, EventArgs e)
    {
        Console.WriteLine("Timer clicked."); // Debug line
                                             // Navigate to the StopwatchPage
        await Navigation.PushAsync(new TimerPage());
    }


    private void OnCalendarClicked(object sender, EventArgs e)
    {
        Console.WriteLine("Calendar clicked");
    }
}


namespace RAMDHD;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

        // Set the MainPage to a new NavigationPage with the MainPage as its root
        MainPage = new NavigationPage(new TitleScreen());
    }
}

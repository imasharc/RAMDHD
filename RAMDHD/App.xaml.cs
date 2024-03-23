namespace RAMDHD;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        // Set the MainPage to the AppShell which defines your app's navigation structure
        MainPage = new AppShell();
    }
}

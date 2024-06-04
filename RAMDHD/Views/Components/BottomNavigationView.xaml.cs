using System.Windows.Input;

namespace RAMDHD.Views.Components;

public partial class BottomNavigationView : ContentView
{
    public static readonly BindableProperty ActivePageProperty = BindableProperty.Create(
            nameof(ActivePage),
            typeof(int),
            typeof(BottomNavigationView),
            0);

    public static readonly BindableProperty NavigateCommandProperty = BindableProperty.Create(
        nameof(NavigateCommand),
        typeof(ICommand),
        typeof(BottomNavigationView));

    public int ActivePage
    {
        get => (int)GetValue(ActivePageProperty);
        set => SetValue(ActivePageProperty, value);
    }

    public ICommand NavigateCommand
    {
        get => (ICommand)GetValue(NavigateCommandProperty);
        set => SetValue(NavigateCommandProperty, value);
    }
    public BottomNavigationView()
	{
		InitializeComponent();
	}
}
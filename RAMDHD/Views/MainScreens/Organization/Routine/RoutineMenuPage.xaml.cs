using RAMDHD.DataAccess;
using RAMDHD.Database;
using RAMDHD.ViewModels.Routine;
using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.MainScreens.Mindfulness;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.People;
using System.Windows.Input;

namespace RAMDHD.Views.MainScreens.Organization.Routine;

public partial class RoutineMenuPage : ContentPage
{
    private int activePage;
    private static string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Database\\ramdhd.db");
    private DatabaseDataAccess _databaseAccess = new DatabaseDataAccess(new DatabaseConnection(dbPath));
    public RoutineMenuPage()
	{
        ActivePage = 0;
        InitializeComponent();
        BindingContext = this;
        LoadAndDisplayNotes();
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
    private async void OnPlaceholderTapped(object sender, EventArgs e)
    {
        if (sender is Label label && label.GestureRecognizers.FirstOrDefault() is TapGestureRecognizer tapGesture)
        {
            string id = tapGesture.CommandParameter.ToString();
            Console.WriteLine($"Label {label.Text} with ID {id} tapped");
            if (label.Text == "Add new routine")
            {
                Console.WriteLine($"Image with ID {id} tapped");
                // The label indicates that a new graph task should be added.
                // Navigate to the EditGraphTaskPage to add a new task.
                await Navigation.PushAsync(new EditNotePage(id));
            }
            else
            {
                // The label indicates an existing graph task.
                // Navigate to the GraphTaskTemplatePage to view the task.
                // Pass the ID as a parameter to the page.
                await Navigation.PushAsync(new RoutineTemplatePage(id));
                Console.WriteLine($"Title {label.Text} with ID {id} tapped");

            }
        }
    }
    private async void EditRoutine(object sender, EventArgs e)
    {
        if (sender is Image image && image.GestureRecognizers.FirstOrDefault() is TapGestureRecognizer tapGesture)
        {
            string id = tapGesture.CommandParameter.ToString();
            Console.WriteLine($"Image with ID {id} tapped");
            // Navigate to an edit page or perform action related to the image tap
            await Navigation.PushAsync(new EditRoutinePage(id));
        }
    }
    private async void LoadAndDisplayNotes()
    {
        var routines = await _databaseAccess.GetAllRoutinesAsync();
        foreach (var routine in routines)
        {
            Console.WriteLine($"LOADED routine with id {routine.Id}, title {routine.Title}");

            DisplayNote(routine);
        }
    }
    private void DisplayNote(Models.Routine routine)
    {
        // If the task title is null or empty, set the default title.
        string title = string.IsNullOrEmpty(routine.Title) ? "Add new routine" : routine.Title;
        Console.WriteLine($"routine {routine.Id}, Title {title}");


        var viewModel = new RoutineViewModel
        {
            Title = title,
            ImageSource = "icons8_edit_96.png" // Make sure this image exists in your resources
        };

        Frame placeholder = FindPlaceholderById(routine.Id);
        if (placeholder != null)
        {
            // Ensuring the Frame has a HorizontalStackLayout to hold the content
            var stackLayout = placeholder.Content as HorizontalStackLayout ?? new HorizontalStackLayout();
            placeholder.Content = stackLayout;

            // Applying the ViewModel to the Frame's BindingContext
            stackLayout.BindingContext = viewModel;

            // Optionally clear existing children if needed
            stackLayout.Children.Clear();

            // Add the newly bound image and label to the StackLayout
            stackLayout.Children.Add(CreateLabel(viewModel, routine.Id));
            stackLayout.Children.Add(CreateImage(viewModel, routine.Id));
        }
    }
    private Label CreateLabel(RoutineViewModel viewModel, int commandParameter)
    {
        var label = new Label
        {
            FontFamily = "ReemKufiFunRegular",
            //Text = string.IsNullOrWhiteSpace(viewModel.Title) ? "Add new graph task" : viewModel.Title,
            Text = "Add new routine",
            FontSize = 13,
            WidthRequest = 250,
            HorizontalTextAlignment = TextAlignment.Start,
            VerticalTextAlignment = TextAlignment.Center,
            VerticalOptions = LayoutOptions.Center,
            Margin = new Thickness(40, 0, 0, 0),
            TextColor = Color.FromRgb(0, 0, 0)
        };
        label.SetBinding(Label.TextProperty, "Title");

        var labelTapGestureRecognizer = new TapGestureRecognizer();
        labelTapGestureRecognizer.Tapped += OnPlaceholderTapped; // assuming this is a method in your code
        labelTapGestureRecognizer.CommandParameter = commandParameter; // or whatever parameter you want to pass
        label.GestureRecognizers.Add(labelTapGestureRecognizer);

        return label;
    }
    private Image CreateImage(RoutineViewModel viewModel, int commandParameter)
    {
        var image = new Image
        {
            Source = "icons8_edit_96.png",
            HeightRequest = 25, // Adjust the size as necessary
            WidthRequest = 25,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            Aspect = Aspect.AspectFit
        };
        image.SetBinding(Image.SourceProperty, "ImageSource");

        var imageTapGestureRecognizer = new TapGestureRecognizer();
        imageTapGestureRecognizer.Tapped += EditRoutine; // assuming this is a method in your code
        imageTapGestureRecognizer.CommandParameter = commandParameter; // or whatever parameter you want to pass
        image.GestureRecognizers.Add(imageTapGestureRecognizer);

        return image;
    }
    private Frame FindPlaceholderById(int id)
    {
        // This method should return the correct placeholder based on the task ID.
        // This example assumes placeholders are named consistently with task IDs.
        return this.FindByName<Frame>($"placeholder{id}");
    }
}
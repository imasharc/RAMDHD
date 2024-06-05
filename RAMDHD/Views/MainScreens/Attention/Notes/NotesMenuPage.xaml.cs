using RAMDHD.DataAccess;
using RAMDHD.Database;
using RAMDHD.ViewModels.Note;
using RAMDHD.Views.MainScreens.Mindfulness;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using System.Windows.Input;

namespace RAMDHD.Views.MainScreens.Attention;

public partial class NotesMenuPage : ContentPage
{
    private static string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Database\\ramdhd.db");
    private DatabaseDataAccess _databaseAccess = new DatabaseDataAccess(new DatabaseConnection(dbPath));
    private int activePage;
    public NotesMenuPage()
	{
        ActivePage = 0;
        InitializeComponent();
        BindingContext = this; // Set the BindingContext to the current instance
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

            await Navigation.PushAsync(new NoteTemplatePage(id));

        }
    }

    private async void EditNote(object sender, EventArgs e)
    {
        if (sender is Image image && image.GestureRecognizers.FirstOrDefault() is TapGestureRecognizer tapGesture)
        {
            string id = tapGesture.CommandParameter.ToString();
            Console.WriteLine($"note with ID {id} EDITED");
            await Navigation.PushAsync(new EditNotePage(id));
        }
    }
    private async void DeleteNote(object sender, EventArgs e)
    {
        if (sender is Image image && image.GestureRecognizers.FirstOrDefault() is TapGestureRecognizer tapGesture)
        {
            int.TryParse(tapGesture.CommandParameter.ToString(), out int id);

            bool answer = await Application.Current.MainPage.DisplayAlert(
            "Confirm Delete",
            "Are you sure you want to delete this note?",
            "Yes",
            "No"
        );

            if (answer)
            {
                // Perform the delete action
                await _databaseAccess.DeleteNoteByIdAsync(id);

                Console.WriteLine($"note with ID {id} DELETED");
                await DisplayAlert("Success", $"Note deleted successfully", "OK");
                // Insert the new GraphTaskMenu page before the current page and then pop the current one
                // Creates the illusion of page refreshing
                Navigation.InsertPageBefore(new NotesMenuPage(), this);
                await Navigation.PopAsync();
            }
        }
    }
    private async void LoadAndDisplayNotes()
    {
        var notes = await _databaseAccess.GetAllNotesAsync();
        foreach (var note in notes)
        {
            Console.WriteLine($"LOADED note with id {note.Id}, Headline {note.Headline}");

            DisplayNote(note);
        }
    }

    private void DisplayNote(Models.Note note)
    {
        // If the task title is null or empty, set the default title.
        string headline = string.IsNullOrEmpty(note.Headline) ? "Add new note" : note.Headline;
        Console.WriteLine($"note {note.Id}, Headline {headline}");


        var viewModel = new NoteViewModel
        {
            Headline = headline,
            EditIcon = "edit_pencil.svg",
            DeleteIcon = "trash.svg"
        };

        Frame placeholder = FindPlaceholderById(note.Id);
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
            stackLayout.Children.Add(CreateLabel(viewModel, note.Id));
            stackLayout.Children.Add(CreateEditIcon(viewModel, note.Id));
            stackLayout.Children.Add(CreateDeleteIcon(viewModel, note.Id));

        }
    }

    private Label CreateLabel(NoteViewModel viewModel, int commandParameter)
        {
            var label = new Label
            {
                FontFamily = "ReemKufiFunRegular",
                //Text = string.IsNullOrWhiteSpace(viewModel.Title) ? "Add new graph task" : viewModel.Title,
                Text = "Add new note",
                FontSize = 13,
                WidthRequest = 220,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(40, 0, 0, 0),
                TextColor = Color.FromRgb(0, 0, 0)
            };
            label.SetBinding(Label.TextProperty, "Headline");

            var labelTapGestureRecognizer = new TapGestureRecognizer();
            labelTapGestureRecognizer.Tapped += OnPlaceholderTapped; // assuming this is a method in your code
            labelTapGestureRecognizer.CommandParameter = commandParameter; // or whatever parameter you want to pass
            label.GestureRecognizers.Add(labelTapGestureRecognizer);
            
            return label;
        }

        private Image CreateEditIcon(NoteViewModel viewModel, int commandParameter)
        {
            var image = new Image
            {
                Source = "edit_pencil.svg",
                HeightRequest = 30, // Adjust the size as necessary
                WidthRequest = 30,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 0, 0),
                Aspect = Aspect.AspectFit
            };
            image.SetBinding(Image.SourceProperty, "EditIcon");

            var imageTapGestureRecognizer = new TapGestureRecognizer();
            imageTapGestureRecognizer.Tapped += EditNote; // assuming this is a method in your code
            imageTapGestureRecognizer.CommandParameter = commandParameter; // or whatever parameter you want to pass
            image.GestureRecognizers.Add(imageTapGestureRecognizer);

            return image;
        }
    private Image CreateDeleteIcon(NoteViewModel viewModel, int commandParameter)
    {
        var image = new Image
        {
            Source = "trash.svg",
            HeightRequest = 30, // Adjust the size as necessary
            WidthRequest = 30,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            Margin = new Thickness(20, 0, 0, 0),
            Aspect = Aspect.AspectFit
        };
        image.SetBinding(Image.SourceProperty, "DeleteIcon");

        var imageTapGestureRecognizer = new TapGestureRecognizer();
        imageTapGestureRecognizer.Tapped += DeleteNote; // assuming this is a method in your code
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
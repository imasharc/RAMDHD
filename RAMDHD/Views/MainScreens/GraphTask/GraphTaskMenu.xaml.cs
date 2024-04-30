using Microsoft.Maui.Graphics.Text;
using Microsoft.Maui.Layouts;
using RAMDHD.DataAccess;
using RAMDHD.Database;
using RAMDHD.ViewModels.GraphTask;
using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.MainScreens.Entertainment;
using RAMDHD.Views.MainScreens.GraphTask.Examples;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMDHD.Views.MainScreens.GraphTask
{
    public partial class GraphTaskMenu : ContentPage
    {
        private static string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Database\\ramdhd.db");
        private DatabaseDataAccess _databaseAccess = new DatabaseDataAccess(new DatabaseConnection(dbPath));
        private double nextYPosition = 0.5; // Start at some Y position after the predefined buttons.

        public GraphTaskMenu()
        {
            InitializeComponent();
            //BindingContext = new GraphTaskMenuViewModel();
            LoadAndDisplayGraphTasks();

        }

        private async void OnPlaceholderTapped(object sender, EventArgs e)
        {
            if (sender is Label label && label.GestureRecognizers.FirstOrDefault() is TapGestureRecognizer tapGesture)
            {
                string id = tapGesture.CommandParameter.ToString();
                Console.WriteLine($"Label {label.Text} with ID {id} tapped");
                if (label.Text == "Add new graph task")
                {
                    Console.WriteLine($"Image with ID {id} tapped");
                    // The label indicates that a new graph task should be added.
                    // Navigate to the EditGraphTaskPage to add a new task.
                    await Navigation.PushAsync(new EditGraphTaskPage(id));
                }
                else
                {
                    // The label indicates an existing graph task.
                    // Navigate to the GraphTaskTemplatePage to view the task.
                    // Pass the ID as a parameter to the page.
                    await Navigation.PushAsync(new GraphTaskTemplatePage(id));
                }
            }
        }

        private async void EditGraphTask(object sender, EventArgs e)
        {
            if (sender is Image image && image.GestureRecognizers.FirstOrDefault() is TapGestureRecognizer tapGesture)
            {
                string id = tapGesture.CommandParameter.ToString();
                Console.WriteLine($"Image with ID {id} tapped");
                // Navigate to an edit page or perform action related to the image tap
                await Navigation.PushAsync(new EditGraphTaskPage(id));
            }
        }

        private async void LoadAndDisplayGraphTasks()
        {
            var graphTasks = await _databaseAccess.GetAllGraphTasksAsync();
            foreach (var task in graphTasks)
            {
                Console.WriteLine($"LOADED task with id {task.Id}, Title {task.Title}");

                DisplayGraphTask(task);
            }
        }

        private void DisplayGraphTask(Models.GraphTask task)
        {
            // If the task title is null or empty, set the default title.
            string title = string.IsNullOrEmpty(task.Title) ? "Add new graph task" : task.Title;
            Console.WriteLine($"task {task.Id}, Title {title}");


            var viewModel = new GraphTaskViewModel
            {
                Title = title,
                ImageSource = "icons8_edit_96.png" // Make sure this image exists in your resources
            };

            Frame placeholder = FindPlaceholderById(task.Id);
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
                stackLayout.Children.Add(CreateLabel(viewModel, task.Id));
                stackLayout.Children.Add(CreateImage(viewModel, task.Id));
            }
        }

        private Label CreateLabel(GraphTaskViewModel viewModel, int commandParameter)
        {
            var label = new Label
            {
                FontFamily = "ReemKufiFunRegular",
                //Text = string.IsNullOrWhiteSpace(viewModel.Title) ? "Add new graph task" : viewModel.Title,
                Text = "Add new graph task",
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

        private Image CreateImage(GraphTaskViewModel viewModel, int commandParameter)
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
            imageTapGestureRecognizer.Tapped += EditGraphTask; // assuming this is a method in your code
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

        private void NavigateToGraphTaskPage(Models.GraphTask task)
        {
            // Navigation logic to the detailed graph task page
            Console.WriteLine($"Navigating to detailed page for {task.Title}");
        }
        private async void NavigateToSampleGraphTaskPage(object sender, EventArgs e)
        {
            // Navigate to BuyingBreadGraphExample
            await this.Navigation.PushAsync(new SampleGraphTaskMenuPage());
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
}

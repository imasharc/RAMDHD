﻿using Android.Graphics.Fonts;
using RAMDHD.DataAccess;
using RAMDHD.Database;
using RAMDHD.ViewModels.GraphTask;
using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.MainScreens.Entertainment;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RAMDHD.Views.MainScreens.GraphTask
{
    public partial class GraphTaskTemplatePage : ContentPage
    {
        private static string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Database\\ramdhd.db");
        private DatabaseDataAccess _databaseAccess = new DatabaseDataAccess(new DatabaseConnection(dbPath));
        public GraphTaskTemplatePage()
        {
            InitializeComponent();
        }
        public GraphTaskTemplatePage(string id)
        {
            InitializeComponent();
            _databaseAccess = new DatabaseDataAccess(new DatabaseConnection(Path.Combine(FileSystem.AppDataDirectory, "Database\\ramdhd.db")));
            LoadGraphTaskData(int.Parse(id));
        }

        private async void LoadGraphTaskData(int id)
        {
            var graphTask = await _databaseAccess.GetGraphTaskByIdAsync(id);
            if (graphTask != null)
            {
                // Assign the values from graphTask to your UI elements
                Activity1Label.Text = "1. " + graphTask.Activity1;
                Activity2Label.Text = "2. " + graphTask.Activity2;
                Activity3Label.Text = "3. " + graphTask.Activity3;
                Activity4Label.Text = "4. " + graphTask.Activity4;

                // Set the titles for your Labels
                this.Title = graphTask.Title;

                // Setting the procrastination activity to orange
                switch (graphTask.ProcrastinationActivity)
                {
                    case 1:
                        Activity1.BackgroundColor = Colors.Orange;
                        Activity1Label.TextColor = Colors.Brown;
                        Activity1Label.FontFamily = "ReemKufiFunBold";
                        break;
                    case 2:
                        Activity2.BackgroundColor = Colors.Orange;
                        Activity2Label.TextColor = Colors.Brown;
                        Activity2Label.FontFamily = "ReemKufiFunBold";
                        break;
                    case 3:
                        Activity3.BackgroundColor = Colors.Orange;
                        Activity3Label.TextColor = Colors.Brown;
                        Activity3Label.FontFamily = "ReemKufiFunBold";
                        break;
                }
            }
            else
            {
                // Handle the case where no task was found, maybe navigate back or show an error
                await DisplayAlert("Error", "Graph task not found.", "OK");
                await Navigation.PopAsync();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        public ICommand NavigateToEditActivityCommand => new Command<string>(activityProperty =>
        {
            //var editPage = new EditActivityPage(activityProperty, viewModel);
            //Navigation.PushAsync(editPage);
        });
        private async void ActivityTapped(object sender, EventArgs e)
        {
            var frame = sender as Frame;
            if (frame != null)
            {
                var activityName = frame.GestureRecognizers.OfType<TapGestureRecognizer>().FirstOrDefault()?.CommandParameter as string;
                if (activityName != null)
                {
                    await Navigation.PushAsync(new EditGraphTaskPage(activityName));
                }
            }
        }
        private async void NavigateToGraphTaskMenu(object sender, EventArgs e)
        {
            Console.WriteLine("GraphTaskMenu");
            await this.Navigation.PushAsync(new GraphTaskMenu());
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

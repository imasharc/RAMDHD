using System;
using Microsoft.Maui.Controls;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.WelcomeScreens;
using RAMDHD.Views.MainScreens.People;
using RAMDHD.Views.MainScreens.Entertainment;

namespace RAMDHD.Views.MainScreens
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnAttentionClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Attention");
            await this.Navigation.PushAsync(new AttentionHomePage());

        }

        private async void OnOrganizationClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Organization");
            await this.Navigation.PushAsync(new OrganizationHomePage());
        }

        private async void OnPeopleClicked(object sender, EventArgs e)
        {
            Console.WriteLine("People");
            await this.Navigation.PushAsync(new PeopleHomePage());
        }

        private async void OnEntertainmentClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Entertainment");
            await this.Navigation.PushAsync(new EntertainmentHomePage());
        }

        private void OnGraphTasksClicked(object sender, EventArgs e)
        {
            Console.WriteLine("GraphTasks");
        }
    }
}

using System;
using Microsoft.Maui.Controls;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.WelcomeScreens;

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

        private void OnPeopleClicked(object sender, EventArgs e)
        {
            Console.WriteLine("People");
        }

        private void OnEntertainmentClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Entertainment");
        }

        private void OnGraphTasksClicked(object sender, EventArgs e)
        {
            Console.WriteLine("GraphTasks");
        }
    }
}

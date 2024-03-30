using RAMDHD.Views.MainScreens.Entertainment;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMDHD.Views.MainScreens.Attention
{
    public partial class AttentionHomePage : ContentPage
    {
        public AttentionHomePage()
        {
            InitializeComponent();

        }
        private void OnGraphTasksClicked(object sender, EventArgs e)
        {
            Console.WriteLine("GraphTasks");
        }
        private async void OnTimerClicked(object sender, EventArgs e)
        {
            Console.WriteLine("OnTimerClicked");
            // Navigate to the Timer page
            await Navigation.PushAsync(new TimerPage());
        }

        private async void OnOrganizationClicked(object sender, EventArgs e)
        {
            // Insert the new page before the current one
            Navigation.InsertPageBefore(new OrganizationHomePage(), this);

            // Pop the current page off the stack
            await Navigation.PopAsync();
        }
        private async void OnPeopleClicked(object sender, EventArgs e)
        {
            Console.WriteLine("OnPeopleClicked");
            // Insert the new page before the current one
            Navigation.InsertPageBefore(new PeopleHomePage(), this);

            // Pop the current page off the stack
            await Navigation.PopAsync();
        }
        private async void OnEntertainmentClicked(object sender, EventArgs e)
        {
            Console.WriteLine("OnEntertainmentClicked");
            // Insert the new page before the current one
            Navigation.InsertPageBefore(new EntertainmentHomePage(), this);

            // Pop the current page off the stack
            await Navigation.PopAsync();
        }
        private async void OnHomeImageTapped(object sender, EventArgs e)
        {
            Console.WriteLine("OnHomeImageTapped");
            // Navigate to the home page
            await Navigation.PopAsync();

            //// Navigate to the home page using Shell navigation
            //await Shell.Current.GoToAsync("//HomePage");
        }
    }
}

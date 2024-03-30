using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.MainScreens.Organization;
using RAMDHD.Views.MainScreens.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMDHD.Views.MainScreens.Entertainment
{
    public partial class EntertainmentHomePage : ContentPage
    {
        public EntertainmentHomePage()
        {
            InitializeComponent();
        }
        private void OnGraphTasksClicked(object sender, EventArgs e)
        {
            Console.WriteLine("OnGraphTasksClicked");
        }
        private async void OnAttentionClicked(object sender, EventArgs e)
        {
            Console.WriteLine("OnAttentionClicked");
            // Pop the current page off the stack
            // Insert the new page before the current one
            Navigation.InsertPageBefore(new AttentionHomePage(), this);

            // Pop the current page off the stack
            await Navigation.PopAsync();
        }
        private async void OnOrganizationClicked(object sender, EventArgs e)
        {
            Console.WriteLine("OrganizationHomePage");
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

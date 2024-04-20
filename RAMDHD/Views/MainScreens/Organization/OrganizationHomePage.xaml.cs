using RAMDHD.Views.MainScreens.Attention;
using RAMDHD.Views.MainScreens.Entertainment;
using RAMDHD.Views.MainScreens.GraphTask;
using RAMDHD.Views.MainScreens.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMDHD.Views.MainScreens.Organization
{
    public partial class OrganizationHomePage : ContentPage
    {
        public OrganizationHomePage()
        {
            InitializeComponent();
        }
        private async void OnHomeImageTapped(object sender, EventArgs e)
        {
            Console.WriteLine("OnHomeImageTapped");
            // Navigate to HomePage
            await this.Navigation.PushAsync(new HomePage());
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
    }
}

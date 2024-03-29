using RAMDHD.Views.MainScreens.Attention;
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
        private void OnGraphTasksClicked(object sender, EventArgs e)
        {
            Console.WriteLine("OnGraphTasksClicked");
        }
        private async void OnAttentionClicked(object sender, EventArgs e)
        {
            Console.WriteLine("OnAttentionClicked");
            // Insert the new page before the current one
            Navigation.InsertPageBefore(new AttentionHomePage(), this);

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

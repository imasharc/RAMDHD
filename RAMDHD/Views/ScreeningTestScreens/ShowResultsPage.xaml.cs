using Microsoft.Maui.Controls;
using RAMDHD.Views.MainScreens;
using RAMDHD.Views.MainScreens.Attention;
using System;

namespace RAMDHD.Views.ScreeningTestScreens
{
    public partial class ShowResultsPage : ContentPage
    {
        public ShowResultsPage(string result)
        {
            InitializeComponent();
            ResultLabel.Text = $"Your ADHD Type: {result}";

            // Subscribe to the Clicked event of the button
            StartUsingAppButton.Clicked += StartUsingAppButton_Clicked;
        }

        private async void StartUsingAppButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to the HomePage
            await Navigation.PushAsync(new AttentionHomePage());
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Unsubscribe from the Clicked event to prevent memory leaks
            StartUsingAppButton.Clicked -= StartUsingAppButton_Clicked;
        }
    }
}

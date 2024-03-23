using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace RAMDHD.Views.WelcomeScreens
{
    public partial class TitlePage : ContentPage
    {
        private bool isShaking = true; // A flag to control the shaking animation

        public TitlePage()
        {
            InitializeComponent(); // This is necessary.

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                // Stop the shaking animation when tapped
                isShaking = false;

                // Pop animation
                await BrainImage.ScaleTo(3, 250, Easing.SpringIn); // Scale up the image
                                                                   // Use the built-in navigation mechanism if inside a NavigationPage or Shell.
                await this.Navigation.PushAsync(new IntroductionPage());
                await BrainImage.ScaleTo(1.0, 250, Easing.SpringOut); // Scale back to original size


            };

            // Assuming you have an image with x:Name="BrainImage" in your TitleScreen.xaml
            BrainImage.GestureRecognizers.Add(tapGestureRecognizer);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            isShaking = true; // Ensure the animation starts when the page appears
            ShakeBrainImage(); // Start the animation when the page appears
        }

        private async void ShakeBrainImage()
        {
            const int shakeDuration = 50;
            while (isShaking)
            {
                await BrainImage.RotateTo(-5, shakeDuration);  // Rotate slightly to the left
                await BrainImage.RotateTo(5, shakeDuration);   // Rotate slightly to the right
                await BrainImage.RotateTo(-3, shakeDuration);  // Rotate back a bit to the left
                await BrainImage.RotateTo(3, shakeDuration);   // Rotate back a bit to the right
                await BrainImage.RotateTo(0, shakeDuration);   // Return to the original position

                if (!isShaking) break; // Check the flag before starting another loop
                await Task.Delay(2000); // Wait for 2 seconds before shaking again
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            isShaking = false; // Ensure the animation stops when the page disappears
        }
    }
}

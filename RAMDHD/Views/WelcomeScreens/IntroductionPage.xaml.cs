using RAMDHD.Views.MainScreens;
using RAMDHD.Views.MainScreens.Attention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMDHD.Views.WelcomeScreens
{
    public partial class IntroductionPage
    {
        private bool isShaking = true; // A flag to control the shaking animation

        public IntroductionPage()
        {
            InitializeComponent();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                // Stop the shaking animation when tapped
                isShaking = false;

                // Pop animation
                await BrainImage.ScaleTo(3, 250, Easing.SpringIn); // Scale up the image
                                                                   // Use the built-in navigation mechanism if inside a NavigationPage or Shell.
                await this.Navigation.PushAsync(new ScreeningTestScreens.ScreeningTestPage());
                await BrainImage.ScaleTo(1.0, 250, Easing.SpringOut); // Scale back to original size


            };

            // Assuming you have an image with x:Name="BrainImage" in your TitleScreen.xaml
            BrainImage.GestureRecognizers.Add(tapGestureRecognizer);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            isShaking = true; // Ensure the animation starts when the page appears
            ShakeBrainImage(); // Start the animation when the page appears

            // Reset the properties of your images before starting animations
            ResetImage(HiddenImage1);
            ResetImage(HiddenImage2);
            ResetImage(HiddenImage3);

            // Slide the ADHD Test button into view from the right
            SlideInButton(AdhdTestButton);

            // Start the pop-in animation for the other images
            await PopInImage(HiddenImage1, -100, -140);

            //await Task.Delay(200);
            await PopInImage(HiddenImage2, 0, -150);
            await PopInImage(HiddenImage3, 100, -140);

        }

        private void ResetImage(View image)
        {
            image.Scale = 0; // Scaled down (initially invisible)
            image.Opacity = 0; // Fully transparent (initially invisible)
            image.TranslationX = 0; // Horizontal position reset
            image.TranslationY = 0; // Vertical position reset
        }


        private async Task PopInImage(View image, double finalX, double finalY)
        {
            image.Scale = 0; // Start from a scaled down state1.5
            image.Opacity = 0; // Start fully transparent

            // Animate scaling up and fading in
            await image.ScaleTo(1.0, 750, Easing.BounceIn);
            await image.FadeTo(1, 500, Easing.SinIn);

            // Animate the image moving to the top left corner above the brain image
            var moveImage = image.TranslateTo(finalX, finalY, 1000, Easing.SpringOut);

            // Wait for the move animation to complete
            await moveImage;
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
                await Task.Delay(1000); // Wait for 2 seconds before shaking again
            }
        }

        private async Task SlideInButton(Button button)
        {
            await button.TranslateTo(0, 0, 3000, Easing.CubicOut);
        }

        public async void OnAdhdTestButtonClicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new ScreeningTestScreens.ScreeningTestPage());
        }
        private async void OnSkipTestTapped(object sender, EventArgs e)
        {
            // Navigate to HomePage
            await this.Navigation.PushAsync(new AttentionHomePage());
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            isShaking = false; // Ensure the animation stops when the page disappears
        }
    }
}

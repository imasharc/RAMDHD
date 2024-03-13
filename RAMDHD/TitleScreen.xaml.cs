using System;
using Microsoft.Maui.Controls;

namespace RAMDHD
{
    public partial class TitleScreen : ContentPage
    {
        public TitleScreen()
        {
            InitializeComponent(); // This is necessary.

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                // Use the built-in navigation mechanism if inside a NavigationPage or Shell.
                await this.Navigation.PushAsync(new MainPage());
            };

            // Assuming you have an image with x:Name="BrainImage" in your TitleScreen.xaml
            BrainImage.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}

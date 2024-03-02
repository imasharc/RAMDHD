using Microsoft.Maui.Controls;

namespace RAMDHD
{
    public partial class CustomImageButton : ContentView
    {
        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(CustomImageButton), default(ImageSource));

        public static readonly BindableProperty ButtonTextProperty =
            BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(CustomImageButton), default(string));

        public event EventHandler Tapped;

        public CustomImageButton()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void OnFrameTapped(object sender, EventArgs e)
        {
            // Invoke any event handlers attached to our custom event
            Tapped?.Invoke(this, e);
        }

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }
    }
}

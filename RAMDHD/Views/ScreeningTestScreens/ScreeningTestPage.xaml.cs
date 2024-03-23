using Microsoft.Maui.Controls;
using System;

namespace RAMDHD.Views.ScreeningTestScreens
{
    public partial class ScreeningTestPage : ContentPage
    {
        private int _selectedScore = -1; // -1 indicates no selection

        public ScreeningTestPage()
        {
            InitializeComponent();
        }

        private void OnAnswerSelected(object sender, CheckedChangedEventArgs e)
        {
            if (sender is RadioButton radioButton && e.Value)
            {
                _selectedScore = Convert.ToInt32(radioButton.Value);
            }
        }

        private void OnShowResultsClicked(object sender, EventArgs e)
        {
            // For now, just log the selected score to the console
            Console.WriteLine($"Selected Score: {_selectedScore}");

            // In a real application, you might navigate to a results page or calculate and display the results here.
        }
    }
}

using Microsoft.Maui.Controls;
using RAMDHD.ViewModels.ScreeningTest;
using System;

namespace RAMDHD.Views.ScreeningTestScreens
{
    public partial class ScreeningTestPage : ContentPage
    {
        private ScreeningTestViewModel _viewModel;
        public ScreeningTestPage()
        {
            InitializeComponent();
            _viewModel = new ScreeningTestViewModel();
            this.BindingContext = _viewModel;

            // Subscribe to the message sent from the ViewModel to navigate to the results page
            MessagingCenter.Subscribe<ScreeningTestViewModel, string>(this, "NavigateToShowResultsPage", async (sender, result) =>
            {
                // Assume ShowResultsPage is the page you want to navigate to, and it has a constructor that accepts a result string
                await Navigation.PushAsync(new ShowResultsPage(result));
            });
        }

        private void OnAnswerChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value && sender is RadioButton radioButton)
            {
                var viewModel = (ViewModels.ScreeningTest.ScreeningTestViewModel)BindingContext;
                var selectedAnswerIndex = viewModel.CurrentQuestion.Answers.IndexOf(radioButton.Content.ToString());
                viewModel.CurrentQuestion.SelectedAnswerScore = selectedAnswerIndex;
                // Assuming the Content of the RadioButton is the answer text
                string answerText = radioButton.Value.ToString();

                // Call a method on the ViewModel to handle the selection
                _viewModel.SelectAnswer(answerText);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Unsubscribe from the MessagingCenter to prevent memory leaks
            MessagingCenter.Unsubscribe<ScreeningTestViewModel, string>(this, "NavigateToShowResultsPage");
        }
    }
}

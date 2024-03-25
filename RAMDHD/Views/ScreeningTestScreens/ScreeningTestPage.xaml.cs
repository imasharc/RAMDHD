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
        }

        private void OnAnswerChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value && sender is RadioButton radioButton)
            {
                var viewModel = (ViewModels.ScreeningTest.ScreeningTestViewModel)BindingContext;
                var selectedAnswerIndex = viewModel.CurrentQuestion.Answers.IndexOf(radioButton.Content.ToString());
                viewModel.CurrentQuestion.SelectedAnswerScore = selectedAnswerIndex;
            }
        }
    }

}

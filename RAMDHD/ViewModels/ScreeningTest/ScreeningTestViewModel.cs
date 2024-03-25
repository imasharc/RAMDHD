using RAMDHD.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using RAMDHD.Models.ScreeningTest;

namespace RAMDHD.ViewModels.ScreeningTest
{
    public class ScreeningTestViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Question> _questions;
        private int _currentQuestionIndex;

        public ObservableCollection<Question> Questions
        {
            get => _questions;
            set
            {
                _questions = value;
                OnPropertyChanged(nameof(Questions));
            }
        }

        public Question CurrentQuestion => Questions[_currentQuestionIndex];

        public bool CanMoveNext => _currentQuestionIndex < Questions.Count - 1;

        public ICommand NextQuestionCommand { get; }

        public ScreeningTestViewModel()
        {
            _questions = new ObservableCollection<Question>
            {
                new Question
                {
                    Text = "How often do you have trouble nailing down the final details of a project once you've completed the difficult parts?",
                    Answers = new List<string> { "Never", "Rarely", "Sometimes", "Often", "Very Often" }
                },
                new Question
                {
                    Text = "How often do you fidget or shake your hands or feet when you have to sit for a long time?",
                    Answers = new List<string> { "Never", "Rarely", "Sometimes", "Often", "Very Often" }
                },
                new Question
                {
                    Text = "How often do you find yourself talking too much when you are in a social situation?",
                    Answers = new List<string> { "Never", "Rarely", "Sometimes", "Often", "Very Often" }
                }
                // Continue adding other questions as necessary
            };

            NextQuestionCommand = new Command(MoveToNextQuestion, () => CanMoveNext);
        }

        private void MoveToNextQuestion()
        {
            if (CanMoveNext)
            {
                _currentQuestionIndex++;
                OnPropertyChanged(nameof(CurrentQuestion));
                OnPropertyChanged(nameof(CanMoveNext));
                ((Command)NextQuestionCommand).ChangeCanExecute();
            }
            else
            {
                // Handle the logic when the last question is answered,
                // such as calculating the score or navigating to a results page.
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

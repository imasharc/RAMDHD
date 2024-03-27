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

        // Add commands for moving to the next question and showing results
        public ICommand NextQuestionCommand { get; private set; }
        public ICommand ShowResultsCommand { get; private set; }
        public Question CurrentQuestion => Questions[_currentQuestionIndex];

        public bool CanMoveNext => _currentQuestionIndex < Questions.Count - 1;
        public bool IsChecked { get; set; }

        // Add a property to change the text of the button based on the current question
        public string NextButtonText => CanMoveNext ? "Next Question" : "Show Results";

        private int Type1Score = 0;
        private int Type2Score = 0;

        public ScreeningTestViewModel()
        {
            _questions = new ObservableCollection<Question>
            {
                new Question
                {
                    Text = "1. How often do you have trouble nailing down the final details of a project once you've completed the difficult parts?",
                    Answers = new List<string> { "Never", "Rarely", "Sometimes", "Often", "Very Often" }
                },
                new Question
                {
                    Text = "2. How often do you fidget or shake your hands or feet when you have to sit for a long time?",
                    Answers = new List<string> { "Never", "Rarely", "Sometimes", "Often", "Very Often" }
                },
                new Question
                {
                    Text = "3. How often do you find yourself talking too much when you are in a social situation?",
                    Answers = new List<string> { "Never", "Rarely", "Sometimes", "Often", "Very Often" }
                },
                new Question
                {
                    Text = "4. How often do you put things in the wrong place or have difficulty finding things at home or at work?",
                    Answers = new List<string> { "Never", "Rarely", "Sometimes", "Often", "Very Often" }
                },
                new Question
                {
                    Text = "5. How often do you have difficulty concentrating on what people are saying to you, even when they are speaking to you directly?",
                    Answers = new List<string> { "Never", "Rarely", "Sometimes", "Often", "Very Often" }
                },
                new Question
                {
                    Text = "6. How often are you distracted by activity or noise around you?",
                    Answers = new List<string> { "Never", "Rarely", "Sometimes", "Often", "Very Often" }
                }
            };

            // Initialize the commands
            NextQuestionCommand = new Command(MoveToNextQuestion, () => CanMoveNext);
            ShowResultsCommand = new Command(ShowResults, AllQuestionsAnswered);

        }

        public void SelectAnswer(string answer)
        {
            
            if (CurrentQuestion != null)
            {
                // Find the answer in the current question's Answers list
                int answerIndex = CurrentQuestion.Answers.IndexOf(answer);
                Console.WriteLine("answerIndex: " + answerIndex + ", answerText: " + answer);

                if (answerIndex != -1)
                {
                    // Set the score based on the answer index
                    // Here, I'm assuming the score is the same as the index
                    CurrentQuestion.SelectedAnswerScore = GetScoreFromAnswer(answer);
                    Console.WriteLine("answerIndex: " + answerIndex + ", selectedAnswerScore: " + CurrentQuestion.SelectedAnswerScore);

                    // Notify that the current question has been updated
                    OnPropertyChanged(nameof(CurrentQuestion));
                    IsChecked = true;

                    Console.WriteLine("_currentQuestionIndex: " + _currentQuestionIndex);

                }
            }
        }

        private bool AllQuestionsAnswered()
        {
            // Here you check if all questions have been answered, e.g.:
            return Questions.All(q => q.SelectedAnswerScore >= 0);
        }

        private void MoveToNextQuestion()
        {
            if (CurrentQuestion.SelectedAnswerIndex == -1)
            {
                IsChecked = false;
                Console.WriteLine("CurrentQuestion.SelectedAnswerScore: " + CurrentQuestion.SelectedAnswerScore);
                Console.WriteLine("IsChecked: " + IsChecked);
                
            }
            if (CurrentQuestion != null)
            {
                // Calculate and add the score to the appropriate type before moving to the next question
                if (_currentQuestionIndex < 3) // Assuming the first 3 questions are Type1
                {
                    Type1Score += CurrentQuestion.SelectedAnswerScore;
                }
                else // The next set of questions are Type2
                {
                    Type2Score += CurrentQuestion.SelectedAnswerScore;
                }

            }

            if (CanMoveNext)
            {
                _currentQuestionIndex++;
                OnPropertyChanged(nameof(CurrentQuestion));
                OnPropertyChanged(nameof(CanMoveNext));
                OnPropertyChanged(nameof(NextButtonText));
                ((Command)ShowResultsCommand).ChangeCanExecute();
                ShowResults();
            }
            else
            {
                // Calculate the final result on the last question
                ShowResults();
            }
        }

        private int GetScoreFromAnswer(string answer)
        {
            var scores = new Dictionary<string, int>
            {
                { "Never", 0 },
                { "Rarely", 1 },
                { "Sometimes", 2 },
                { "Often", 3 },
                { "Very Often", 4 }
            };
            return scores.TryGetValue(answer, out int score) ? score : 0;
        }

        private void ShowResults()
        {
            
            Console.WriteLine("Not all questions have been answered.");
            Console.WriteLine("_currentQuestionIndex: " + _currentQuestionIndex);
            
            if (_currentQuestionIndex == 5)
            {
                int totalScore = Type1Score + Type2Score;

                string result = "None";
                if (Type1Score >= 8 && totalScore >= 12)
                {
                    result = "Type1";
                }
                else if (Type2Score >= 8 && totalScore >= 12)
                {
                    result = "Type2";
                }
                else if (Type1Score >= 8 && Type2Score >= 8)
                {
                    result = "Type3";
                }

                // Output the result to the console
                Console.WriteLine($"Final ADHD Type: {result} with total score of: {totalScore}");

                // If you want to display the result in the UI, you might set a property here that you bind to a Label in the XAML.
                MessagingCenter.Send(this, "NavigateToShowResultsPage", result);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMDHD.Models.ScreeningTest
{
    public class Question : INotifyPropertyChanged
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public int SelectedAnswerScore { get; set; } = 0;
        public int SelectedAnswerIndex { get; set; } = -1; // Default to -1 indicating no selection

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }
        }

        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using System;

namespace RAMDHD
{
    public partial class TimerPage : ContentPage
    {
        private TimeSpan _timeSpan;
        private IDispatcherTimer _timer;
        private int _hours = 0;
        private int _minutes = 0;

        public TimerPage()
        {
            InitializeComponent();

            _timer = Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _timeSpan = _timeSpan.Subtract(TimeSpan.FromSeconds(1));
            TimeLabel.Text = _timeSpan.ToString(@"hh\:mm\:ss");

            if (_timeSpan <= TimeSpan.Zero)
            {
                _timer.Stop();
                TimeLabel.Text = "00:00:00";
                DisplayAlert("Time's Up!", "The countdown has finished.", "OK");
            }
        }

        private void IncreaseHours(object sender, EventArgs e)
        {
            _hours = (_hours + 1) % 24;
            UpdateTimeLabels();
        }

        private void DecreaseHours(object sender, EventArgs e)
        {
            _hours = (_hours - 1 + 24) % 24;
            UpdateTimeLabels();
        }

        private void IncreaseMinutes(object sender, EventArgs e)
        {
            _minutes = (_minutes + 1) % 60;
            UpdateTimeLabels();
        }

        private void DecreaseMinutes(object sender, EventArgs e)
        {
            _minutes = (_minutes - 1 + 60) % 60;
            UpdateTimeLabels();
        }

        private void UpdateTimeLabels()
        {
            HourLabel.Text = _hours.ToString("00");
            MinuteLabel.Text = _minutes.ToString("00");
        }

        private void OnStartClicked(object sender, EventArgs e)
        {
            _timeSpan = new TimeSpan(_hours, _minutes, 0);
            _timer.Start();
        }

        private void OnStopClicked(object sender, EventArgs e)
        {
            _timer.Stop();
            TimeLabel.Text = "00:00:00";
        }
    }
}

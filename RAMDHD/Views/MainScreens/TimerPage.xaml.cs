﻿using System;
using System.Timers;
using Microsoft.Maui.Controls;

namespace RAMDHD.Views.MainScreens
{
    public partial class TimerPage : ContentPage
    {
        private System.Timers.Timer _timer;
        private int _hours;
        private int _minutes;
        private int _seconds;

        public TimerPage()
        {
            InitializeComponent();
            InitializePickers();
            InitializeTimer();
        }

        private void InitializePickers()
        {
            // Populate HoursPicker
            for (int i = 0; i < 24; i++)
            {
                HoursPicker.Items.Add(i.ToString("00"));
            }

            // Populate MinutesPicker
            for (int i = 0; i < 60; i++)
            {
                MinutesPicker.Items.Add(i.ToString("00"));
            }
        }

        private void InitializeTimer()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTimerElapsed;
        }

        // Event handler for HoursPicker
        private void HoursPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            _hours = HoursPicker.SelectedIndex;
            UpdateTimeLabel();
        }

        // Event handler for MinutesPicker
        private void MinutesPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            _minutes = MinutesPicker.SelectedIndex;
            UpdateTimeLabel();
        }

        // Event handler for Start button
        private void OnStartClicked(object sender, EventArgs e)
        {
            _seconds = (_hours * 3600) + (_minutes * 60);
            _timer.Start();
            StartButton.BackgroundColor = Colors.Gray;
            StopButton.BackgroundColor = Color.FromArgb("#DF4C20"); // Original color
            ResetButton.BackgroundColor = Color.FromArgb("#364955"); // Original color
            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;
        }

        // Event handler for Stop button
        private void OnStopClicked(object sender, EventArgs e)
        {
            _timer.Stop();
            StopButton.BackgroundColor = Colors.Gray;
            StartButton.BackgroundColor = Color.FromArgb("#00C6AE"); // Original color
            ResetButton.BackgroundColor = Color.FromArgb("#364955"); // Original color
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
        }

        // Event handler for Reset button
        private void OnResetClicked(object sender, EventArgs e)
        {
            _timer.Stop();
            _hours = 0;
            _minutes = 0;
            _seconds = 0;
            HoursPicker.SelectedIndex = 0;
            MinutesPicker.SelectedIndex = 0;
            MainThread.BeginInvokeOnMainThread(UpdateTimeLabel);
            ResetButton.BackgroundColor = Colors.Gray;
            StartButton.BackgroundColor = Color.FromArgb("#00C6AE"); // Original color
            StopButton.BackgroundColor = Color.FromArgb("#DF4C20"); // Original color
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_seconds > 0)
            {
                _seconds--;
                MainThread.BeginInvokeOnMainThread(UpdateTimeLabel);
            }
            else
            {
                _timer.Stop();
                // Optionally, alert the user that the timer has finished.
                // Ensure that any UI updates are posted back to the main thread.
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    // Update UI to show timer has finished
                    UpdateTimeLabel();
                    StartButton.IsEnabled = true;
                    StopButton.IsEnabled = false;
                    // Consider showing an alert to the user here
                });
            }
        }

        private void UpdateTimeLabel()
        {
            // Assuming TimeLabel is on the main thread
            var ts = TimeSpan.FromSeconds(_seconds);
            TimeLabel.Text = ts.ToString(@"hh\:mm\:ss");
        }

    }
}

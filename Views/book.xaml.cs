using Microsoft.Maui.Controls;
using System;

namespace facultyAvailabilityScheduler.Views
{
    public partial class book : ContentPage
    {
        public book()
        {
            InitializeComponent();

            // Populate the FacultyPicker with faculty names (Replace with actual data source)
            FacultyPicker.ItemsSource = new string[] { "Faculty A", "Faculty B", "Faculty C" };

            // Set initial date and time values for DatePicker and TimePicker
            DatePicker.MinimumDate = DateTime.Today;
            DatePicker.MaximumDate = DateTime.Today.AddDays(7);
            TimePicker.Time = new TimeSpan(10, 0, 0); // Set default time to 10:00 AM
        }

        private void OnScheduleButtonClicked(object sender, EventArgs e)
        {
            // Get the selected faculty and the chosen date and time
            string selectedFaculty = FacultyPicker.SelectedItem?.ToString();
            DateTime selectedDate = DatePicker.Date;
            TimeSpan selectedTime = TimePicker.Time;

            // Handle scheduling logic here, for example, show confirmation or make API request
            string confirmationMessage = $"Faculty: {selectedFaculty}\nDate: {selectedDate.ToString("D")}\nTime: {selectedTime.ToString(@"hh\:mm")}";
            DisplayAlert("Meeting Scheduled", confirmationMessage, "OK");
        }
    }
}

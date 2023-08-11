using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace facultyAvailabilityScheduler.Views
{
    public partial class book : ContentPage
    {
        public ObservableCollection<string> previousAppointmentsList { get; } = new ObservableCollection<string>();

        public book()
        {
            InitializeComponent();

                        BindingContext = this;


            // Populate the FacultyPicker with faculty names (Replace with actual data source)
            FacultyPicker.ItemsSource = new string[] { "Faculty A", "Faculty B", "Faculty C" };

            // Set initial date and time values for DatePicker and TimePicker
            DatePicker.MinimumDate = DateTime.Today;
            DatePicker.MaximumDate = DateTime.Today.AddDays(7);
            TimePicker.Time = new TimeSpan(10, 0, 0); // Set default time to 10:00 AM


            // Populate the previous appointments list with sample data
           // previousAppointmentsList.Add("Faculty A - 2023-08-10 10:00 AM");
           // previousAppointmentsList.Add("Faculty B - 2023-08-11 02:00 PM");

           // previousAppointmentsListView.ItemsSource = previousAppointmentsList;
        }

        private void OnScheduleButtonClicked(object sender, EventArgs e)
        {
            // Get the selected faculty and the chosen date and time
            string selectedFaculty = FacultyPicker.SelectedItem?.ToString();
            DateTime selectedDate = DatePicker.Date;
            DateTime selectedDateTime = selectedDate.Add(TimePicker.Time); // Combine DatePicker.Date with TimePicker.Time

            if (selectedFaculty != null)
            {
                string facultyText = selectedFaculty.Trim(); // Remove leading/trailing spaces
                if (!string.IsNullOrEmpty(facultyText))
                {
                    // Handle scheduling logic here
                    string appointmentDetails = $"{facultyText} - {selectedDateTime:MMMM dd, yyyy hh:mm tt}";

                    previousAppointmentsList.Add(appointmentDetails);

                    string confirmationMessage = $"Faculty: {facultyText}\nDate and Time: {selectedDateTime:MMMM dd, yyyy hh:mm tt}";
                    DisplayAlert("Meeting Scheduled", confirmationMessage, "OK");
                }
                else
                {
                    DisplayAlert("Error", "Please select a valid faculty before scheduling.", "OK");
                }
            }
            else
            {
                DisplayAlert("Error", "Please select a faculty before scheduling.", "OK");
            }
        }





    }
}

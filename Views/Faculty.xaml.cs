using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace facultyAvailabilityScheduler.Views
{
    public partial class Faculty : ContentPage
    {
        private string userName;

        private ObservableCollection<string> availabilityList = new ObservableCollection<string>();

        public Faculty()
        {
            InitializeComponent();
            availabilityListView.ItemsSource = availabilityList;
            BindingContext = this; // Set the BindingContext to the current page to access its properties
        }

        // Property to hold the username from the global variable
        public string UserName
        {
            get => userName;
            private set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged(nameof(UserName)); // Notify the UI that the property has changed
                }
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Retrieve the username asynchronously
            UserName = await GlobalData.GetUserName();
        }

        private void OnAddAvailabilityClicked(object sender, EventArgs e)
        {
            // Get the selected date from the DatePicker
            DateTime selectedDate = datePicker.Date;

            // Get the selected start time from the Start TimePicker
            TimeSpan selectedStartTime = startTimePicker.Time;

            // Get the selected end time from the End TimePicker
            TimeSpan selectedEndTime = endTimePicker.Time;

            // Combine the selected date and start time into a single DateTime object
            DateTime selectedStartDateTime = selectedDate.Date + selectedStartTime;

            // Combine the selected date and end time into a single DateTime object
            DateTime selectedEndDateTime = selectedDate.Date + selectedEndTime;

            // Add the selected time slot to the availability list
            availabilityList.Add($"{selectedStartDateTime.ToString("g")} to {selectedEndDateTime.ToString("g")}");
        }
    }
}

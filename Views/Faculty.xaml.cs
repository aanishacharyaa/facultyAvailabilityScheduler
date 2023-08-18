using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text;
using Microsoft.Maui.Controls;

namespace facultyAvailabilityScheduler.Views
{
    public class AppointmentSlot
    {
        public DateTime date { get; set; }
        public bool available { get; set; }
        public string _id { get; set; } // You might need to adjust the data type of _id
    }

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

            await LoadAvailableAppointments();

        }

        private async Task LoadAvailableAppointments()
        {
            try
            {
                // Replace with your API endpoint URL
                string apiUrl = "https://chat.crazytech.eu.org/api/faculty_appointments";

                using (HttpClient client = new HttpClient())
                {
                    // Attach the authentication token if available
                    string token = await GlobalData.GetAccessToken();
                    if (!string.IsNullOrEmpty(token))
                    {
                        client.DefaultRequestHeaders.Add("x-auth-token", token);
                    }

                    // Send the GET request to fetch available appointments
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string responseContent = await response.Content.ReadAsStringAsync();

                        // Deserialize the response content into a list of AppointmentSlot objects
                        List<AppointmentSlot> availableAppointments = JsonSerializer.Deserialize<List<AppointmentSlot>>(responseContent);

                        // Update the availabilityList to display the fetched appointments
                        availabilityList.Clear();
                        foreach (AppointmentSlot appointment in availableAppointments)
                        {
                            availabilityList.Add($"{appointment.date.ToString("g")} - {appointment.available}");
                        }
                    }
                    else
                    {
                        // Handle error response
                        await DisplayAlert("Error", "Failed to fetch available appointments.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async void OnAddAvailabilityClicked(object sender, EventArgs e)
        {
            try
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

                // Create the payload for the API request
                var payload = new { date = selectedStartDateTime }; // Assuming the API expects the "date" field

                // Serialize the payload
                string jsonPayload = JsonSerializer.Serialize(payload);

                // Replace with your API endpoint URL
                string apiUrl = "https://chat.crazytech.eu.org/api/faculty_add_availability";

                using (HttpClient client = new HttpClient())
                {
                    // Attach the authentication token if available
                    string token = await GlobalData.GetAccessToken();
                    if (!string.IsNullOrEmpty(token))
                    {
                        client.DefaultRequestHeaders.Add("x-auth-token", token);
                    }

                    // Prepare the HTTP request content
                    HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    // Send the POST request to the server
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Update the UI or handle success
                        availabilityList.Add($"{selectedStartDateTime.ToString("g")} to {selectedEndDateTime.ToString("g")}");
                      //  availabilityList.Add($"{selectedStartDateTime:MMMM dd, yyyy hh:mm tt} to {selectedEndTime:hh:mm tt}");

                    }
                    else
                    {
                        // Handle error response
                        await DisplayAlert("Error", "Failed to add availability. Please try again.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

    }
}

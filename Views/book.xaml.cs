using facultyAvailabilityScheduler.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

 
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


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
            GetPreviousAppointments();

        }

        private async void OnScheduleButtonClicked(object sender, EventArgs e)
        {
            string selectedFaculty = FacultyPicker.SelectedItem?.ToString();
            DateTime selectedDate = DatePicker.Date;
            DateTime selectedDateTime = selectedDate.Add(TimePicker.Time);

            if (!string.IsNullOrEmpty(selectedFaculty))
            {
                try
                {
                    string token = await GlobalData.GetAccessToken(); // Await the asynchronous method to get the token

                    if (string.IsNullOrEmpty(token))
                    {
                        await DisplayAlert("Error", "Authentication token not available.", "OK");
                        return;
                    }

                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Add("x-auth-token", $"{token}");

                        var appointmentData = new
                        {
                            selectedFaculty,
                            selectedDateTime
                        };

                        var json = JsonSerializer.Serialize(appointmentData);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        var response = await httpClient.PostAsync("https://chat.crazytech.eu.org/api/schedule-appointmentments", content);

                        if (response.IsSuccessStatusCode)
                        {
                            // Successfully scheduled appointment
                            string appointmentDetails = $"{selectedFaculty} - {selectedDateTime:MMMM dd, yyyy hh:mm tt}";
                            previousAppointmentsList.Add(appointmentDetails);

                            string confirmationMessage = $"Faculty: {selectedFaculty}\nDate and Time: {selectedDateTime:MMMM dd, yyyy hh:mm tt}";
                            await DisplayAlert("Meeting Scheduled", confirmationMessage, "OK");
                        }
                        else
                        {
                            // Handle error response
                            var errorMessage = await response.Content.ReadAsStringAsync();
                            await DisplayAlert("Error", errorMessage, "OK");
                        }
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please select a valid faculty before scheduling.", "OK");
            }
        }

        private async Task GetPreviousAppointments()
        {
            try
            {
                string token = await GlobalData.GetAccessToken();

                if (string.IsNullOrEmpty(token))
                {
                    await DisplayAlert("Error", "Authentication token not available.", "OK");
                    return;
                }

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("x-auth-token", token);

                    var response = await httpClient.GetAsync("https://chat.crazytech.eu.org/api/user-appointments");

                    if (response.IsSuccessStatusCode)
                    {
                        var appointmentsJson = await response.Content.ReadAsStringAsync();
                        var appointments = JsonSerializer.Deserialize<List<Appointment>>(appointmentsJson);

                        foreach (var appointment in appointments)
                        {
                            string appointmentDetails = $"{appointment.faculty} - {appointment.date:MMMM dd, yyyy hh:mm tt}";
                            previousAppointmentsList.Add(appointmentDetails);
                        }
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        await DisplayAlert("Error", errorMessage, "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }


    }
}

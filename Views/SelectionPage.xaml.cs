using System;
using Microsoft.Maui.Controls;

namespace facultyAvailabilityScheduler.Views
{
    public partial class SelectionPage : ContentPage
    {
        public SelectionPage()
        {
            InitializeComponent();
        }

        private async void OnStudentLoginClicked(object sender, EventArgs e)
        {
            // Logic for handling student login
            await Navigation.PushAsync(new LoginPage("student"));
        }

        private async void OnFacultyLoginClicked(object sender, EventArgs e)
        {
            // Logic for handling faculty login
            await Navigation.PushAsync(new LoginPage("faculty"));
        }
    }
}

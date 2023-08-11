using System;
using System.Text.Json;
using System.Text;
using Microsoft.Maui.Controls;
using System.Data;

namespace facultyAvailabilityScheduler.Views
{
 
    public partial class LoginPage : ContentPage
    {
        private readonly string role;
        public LoginPage(string role)
        {
            InitializeComponent();

            this.role = role;

            // Customize the UI based on the role (student or faculty)
            if (role == "student")
            {
                // Set the label text and other UI elements for the student role
                LoginLabel.Text = "Student Login";
                // Other customizations specific to students
            }
            else if (role == "faculty")
            {
                // Set the label text and other UI elements for the faculty role
                LoginLabel.Text = "Faculty Login";
                // Other customizations specific to faculty
            }

            loginButton.Clicked += OnLoginButtonClicked;


        }


        // Add login logic here

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            // Create the payload to send in the POST request
            var payload = new { password = password };
            string jsonPayload = JsonSerializer.Serialize(payload);


            string apiUrl = "https://chat.crazytech.eu.org/api/signin";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Prepare the HTTP request content
                    HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    // Send the POST request to the server
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);



                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response);
                        // Read the response content as a string
                        string responseContent = await response.Content.ReadAsStringAsync();

                        using (JsonDocument doc = JsonDocument.Parse(responseContent))
                        {
                            // Access the token and other properties
                            if (doc.RootElement.TryGetProperty("token", out JsonElement tokenElement))
                            {
                                string token = tokenElement.GetString();
                                Console.WriteLine($"Token: {token}");
                                await GlobalData.SetAccessToken(token);

                                if (doc.RootElement.TryGetProperty("_id", out JsonElement idElement))
                                {
                                    string id = idElement.GetString();
                                    Console.WriteLine($"User ID: {id}");
                                    await GlobalData.SetUserId(id);
                                }

                                if (doc.RootElement.TryGetProperty("name", out JsonElement nameElement))
                                {
                                    string name = nameElement.GetString();
                                    Console.WriteLine($"Name: {name}");
                                    await GlobalData.SetUserName(name);
                                }

                                if (doc.RootElement.TryGetProperty("email", out JsonElement emailElement))
                                {
                                    string email = emailElement.GetString();
                                    Console.WriteLine($"Email: {email}");
                                    await GlobalData.SetUserEmail(email);
                                }

                                // Optionally, you can save the token locally for later use
                                // For example, you can use Xamarin.Essentials to save it securely
                                //   await SecureStorage.SetAsync("AccessToken", token);


                            ;


                                if (role == "student")
                                {
                                    await Navigation.PushAsync(new book());
                                }
                                else if (role == "faculty")
                                {
                                    // Navigate to a different page for faculty (customize this line)
                                    await Navigation.PushAsync(new Faculty());
                                }

                                // Handle any other logic or navigation based on the successful login
                            }
                            else
                            {
                                // Handle the case when the "token" property is missing in the response
                                await DisplayAlert("Error", "Invalid response format.", "OK");
                            }



                            // Handle any other logic or navigation based on the successful login
                        }
                    }
                    else
                    {
                        // Handle the unsuccessful response (if needed)
                        // For example, show an error message to the user
                        await DisplayAlert("Error", "Login failed. Please try again.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the HTTP request
                // For example, show an error message to the user
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}



using Newtonsoft.Json;
using PreECommerceApp.Classes;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace PreECommerceApp.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            Padding = Device.OnPlatform(
                new Thickness(10, 20, 10, 10),
                new Thickness(10),
                new Thickness(10));

            loginButton.Clicked += LoginButton_Clicked;

        }

        private async void LoginButton_Clicked(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(userEntry.Text))
            {
                await DisplayAlert("Error", "You must enter your email", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(passwordEntry.Text))
            {
                await DisplayAlert("Error", "You must enter your password", "Accept");
                return;
            }

            Login();
        }

        private async void Login()
        {
            waitActivityIndicator.IsRunning = true;

            var loginRequest = new LoginRequest
            {
                Email = userEntry.Text,
                Password = passwordEntry.Text,
            };

            var result = string.Empty;
            try
            {

                var jsonRequest = JsonConvert.SerializeObject(loginRequest);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://zulu-software.com");
                var url = "/ECommerce/api/Users/Login";
                var response = await client.PostAsync(url, httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    waitActivityIndicator.IsRunning = false;
                    await DisplayAlert("Error", "Wrong user or password ", "Accept");
                    passwordEntry.Text = string.Empty;
                    passwordEntry.Focus();
                    return;
                }

                result = await response.Content.ReadAsStringAsync();



            }
            catch (Exception ex )
            {
                waitActivityIndicator.IsRunning = false;
                await DisplayAlert("Error", ex.Message, "Accept");
               return;
            }

            var userResponse = JsonConvert.DeserializeObject<UserResponse>(result);
            userResponse.Password = passwordEntry.Text; 
            waitActivityIndicator.IsRunning = false;
            await Navigation.PushAsync(new MainPage(userResponse));

        }
    }
}

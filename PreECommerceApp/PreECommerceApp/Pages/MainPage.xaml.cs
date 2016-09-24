using PreECommerceApp.Classes;
using Xamarin.Forms;

namespace PreECommerceApp.Pages
{
    public partial class MainPage : ContentPage
    {
        private UserResponse userResponse;
        public MainPage(UserResponse userResponse)
        {
            InitializeComponent();
            Padding = Device.OnPlatform(
            new Thickness(10, 20, 10, 10),
            new Thickness(10),
            new Thickness(10));

        this.userResponse = userResponse;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            userNameLabel.Text = userResponse.FullName;
            photoImage.Source = userResponse.PhotoFullPath;
            photoImage.HeightRequest = 280;
            photoImage.WidthRequest = 280;
        }
    }


}
 
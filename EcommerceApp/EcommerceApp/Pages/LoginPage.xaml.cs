
using EcommerceApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcommerceApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }
    }
}
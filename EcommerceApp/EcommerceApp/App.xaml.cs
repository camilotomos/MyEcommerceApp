using EcommerceApp.Pages;
using EcommerceApp.PlatformSpecifics;
using Xamarin.Forms;

namespace EcommerceApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SingUpPage());

            var setupTheme = DependencyService.Get<ISetupTheme>();
            setupTheme.SetStatusBarColor((Color)Current.Resources["StatusBar"]);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

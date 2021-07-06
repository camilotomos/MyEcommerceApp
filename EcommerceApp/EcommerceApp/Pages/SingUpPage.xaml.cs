using EcommerceApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcommerceApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingUpPage : ContentPage
    {
        public SingUpPage()
        {
            InitializeComponent();
            BindingContext = new SignUpViewModel(Navigation);

        }

    }
}
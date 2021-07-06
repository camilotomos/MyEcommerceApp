using Acr.UserDialogs;
using EcommerceApp.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EcommerceApp.ViewModels
{
    public class SignUpViewModel :INotifyPropertyChanged
    {
        #region Private Attributes
        private String _fullName;
        private String _email;
        private String _password;
        private String _confirmationPassword;
        private INavigation _navigation;

        #endregion

        #region Constructor
        public SignUpViewModel(INavigation navigation)
        {
            _navigation = navigation;
            ContinueCommand = new Command (async()=> await Continue());
            LoginCommand = new Command(async () => await Login());
        }
         

        #endregion

        #region Public Properties

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ContinueCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public String FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }
        public String Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public String Password
        {
            get { return _password; }
            set {
                _password = value;
                NotifyPropertyChanged();
            }
        }
        public String ConfirmationPassword
        {
            get { return _confirmationPassword; }
            set { 
                _confirmationPassword = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Methods
        private async Task Continue()
        {
            //SE VERIFICA QUE EL PASS Y LA CONFIRMACION SEAN IGUALES
            if (Password != ConfirmationPassword)
            {
                Password = String.Empty;
                ConfirmationPassword = String.Empty;
                await UserDialogs.Instance.AlertAsync("Password Mismatch", "Please Check Your Password", "OK");
            }
            else
            {      
                //SI SON IGUALES CREO EL REGISTRO DE LOS DATOS
                var register = new {
                    Name = FullName,
                    Email = Email,
                    Password = Password
                };
                //SE CREA UN JSON PARA ENVIAR LOS DATOS 
                var json = JsonConvert.SerializeObject(register);
                //SE CREA LA CONEXION HTTP Y EL CONTENIDO
                var httpClient = new HttpClient();
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //SE ESPERA LA RESPUESTA DEL SERVIDOR
                var response = await httpClient.PostAsync("https://ecommercebackendapi.azurewebsites.net/api/Accounts/Register", content);

                //VERIFICAMOS EL ESTADO DE LA RESPUESTA DEL SERVIDOR
                if (!response.IsSuccessStatusCode)
                {
                  await UserDialogs.Instance.AlertAsync("Algo Salio Mal...", "Please try again later", "OK");
                }
                else
                {
                    //SI EL USUARIO QUEDO BIEN CREADO ME MUESTRA MENSAJE EN PANTALLA
                    await UserDialogs.Instance.AlertAsync("SINCWEB SAY..", "Your acccount has been created", "OK");
                    //AL DAR OK, ME ENVIA A LA PAGINA DE LOGIN
                    await _navigation.PushModalAsync(new LoginPage());
                }

            }
            
        }
        private async Task Login()
        {
            await _navigation.PushAsync(new LoginPage());
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

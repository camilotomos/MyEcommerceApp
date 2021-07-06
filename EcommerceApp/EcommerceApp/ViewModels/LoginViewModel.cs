using Acr.UserDialogs;
using EcommerceApp.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EcommerceApp.ViewModels
{
    public class LoginViewModel
    {

        #region Private Attributes

        private INavigation _navigation;
        private String _email;
        private String _password;


        #endregion

        #region Constructor
        
        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;
            SubmitCommand = new Command(async() => await Login());
        }

        
        #endregion

        #region Public Properties
        public String Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public String Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public ICommand SubmitCommand { get; set; }

        #endregion

        #region Methods
        private async Task Login()
        {
         
            var logueado = new
            {
                Email = Email,
                Password = Password
            };
            //SE CREA UN JSON PARA ENVIAR LOS DATOS 
            var json = JsonConvert.SerializeObject(logueado);
            //SE CREA LA CONEXION HTTP Y EL CONTENIDO
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //SE ESPERA LA RESPUESTA DEL SERVIDOR
            var response = await httpClient.PostAsync("https://ecommercebackendapi.azurewebsites.net/api/Accounts/Login", content);

            //VERIFICAMOS EL ESTADO DE LA RESPUESTA DEL SERVIDOR
            if (!response.IsSuccessStatusCode)
            {
                await UserDialogs.Instance.AlertAsync("Algo Salio Mal...", "Please try again later", "OK");
            }
            else
            {
                //SI EL USUARIO SE LOGUEO BIEN CREADO ME MUESTRA MENSAJE EN PANTALLA
                await UserDialogs.Instance.AlertAsync("WELCOME TE HAS LOGUEADO", "SINCWEB SAY..",  "OK");
                //AL DAR OK, ME ENVIA A LA PAGINA DE LOGUEADO
                await _navigation.PushModalAsync(new LogueadoPage());
            }
        }

        #endregion
    }
}

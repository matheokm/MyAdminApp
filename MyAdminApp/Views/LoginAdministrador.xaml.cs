using MyAdminApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyAdminApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginAdministrador : ContentPage
    {
        public LoginAdministrador()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            UsuariosViewModel vm = (BindingContext as UsuariosViewModel);
            await vm.ObtenerTipos();
        }
    }
}
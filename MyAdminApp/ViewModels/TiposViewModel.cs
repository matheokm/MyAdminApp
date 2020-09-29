using Acr.UserDialogs;
using MyAdminApp.Models;
using MyAdminApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyAdminApp.ViewModels
{
    class TiposViewModel:BaseViewModel
    {
        private Tipos tipos;
        public string controlador = "/api/TiposApi";

        public string Tipo
        {
            get => tipos.Tipo;
            set { tipos.Tipo = value; OnPropertyChanged(); }
        }

        public ICommand ComandoAgregar { private set; get; }
        public ICommand ComandoActualizar { private set; get; }
        public ICommand ComandoEliminar { private set; get; }

        //Constructor
        public TiposViewModel(Tipos r = null)
        {
            tipos = r ?? new Tipos();
            ComandoAgregar = new Command(async () => await Agregar());
            ComandoActualizar = new Command(async () => await Actualizar());
            ComandoEliminar = new Command(async () => await Eliminar());

        }


        public async Task Agregar()
        {
            bool ok = await WebApiService.AgregarItem(controlador, tipos);
            if (ok)
                await UserDialogs.Instance.AlertAsync("NUevo Tipo registrado!", "Correcto", "OK");
            else
                await UserDialogs.Instance.AlertAsync("Tipo NO registrado!", "Error", "OK");

        }

        public async Task Actualizar()
        {
            bool ok = await WebApiService.ActualizarItem(controlador, tipos, tipos.idTipo);
            if (ok)
                await UserDialogs.Instance.AlertAsync("Tipo actualizado!", "Correcto", "OK");
            else
                await UserDialogs.Instance.AlertAsync("Tipo NO actualizado!", "Error", "OK");

        }

        public async Task Eliminar()
        {
            bool confirmar = await UserDialogs.Instance.ConfirmAsync("¿Estás seguro de eliminar?", "Confirme", "Si", "No");
            if (confirmar)
            {
                bool ok = await WebApiService.EliminarItem(controlador, tipos.idTipo);
                if (ok)
                    await UserDialogs.Instance.AlertAsync("Tipo eliminado!", "Correcto", "OK");
                else
                    await UserDialogs.Instance.AlertAsync("Tipo NO eliminado!", "Error", "OK");
            }
        }
    }
}

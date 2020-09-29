using Acr.UserDialogs;
using MyAdminApp.Models;
using MyAdminApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyAdminApp.ViewModels
{
    class UsuariosViewModel:BaseViewModel
    {
        private Usuarios usuarios;
        private string controlador = "/api/ApiUsuarios";

        public String Usuario 
        { 
            get=> usuarios.Usuario;
            set {usuarios.Usuario =value; OnPropertyChanged(); } 
        }
        public String Password 
        {
            get => usuarios.Password;
            set { usuarios.Password = value; OnPropertyChanged(); }
        }

        //Lista de Tipos
        private ObservableCollection<Tipos> tipos;
        public ObservableCollection<Tipos> Tipos 
        {
            get => tipos;
            set { tipos = value; OnPropertyChanged(); }
        }
        //Seleccion de Tipo> idTipo
        private Tipos seleccionTipo;
        public Tipos SeleccionTipo
        { 
            get=> seleccionTipo;
            set
            {
                seleccionTipo = value;
                usuarios.idTipo = seleccionTipo.idTipo;
                OnPropertyChanged();
            } 
        }
        
        public ICommand ComandoAgregar { private set; get; }
        public ICommand ComandoActualizar { private set; get; }
        public ICommand ComandoEliminar { private set; get; }

        //Constructor
        public UsuariosViewModel(Usuarios u = null)
        {
            usuarios = u ?? new Usuarios();
            ComandoAgregar = new Command(async () => await Agregar());
            ComandoActualizar = new Command(async () => await Actualizar());
            ComandoEliminar = new Command(async () => await Eliminar());

        }

        public async Task ObtenerTipos()
        {
            List<Tipos> tiposDis = await WebApiService.ObtenerItems<Tipos>("api/ApiTipos");
            Tipos = new ObservableCollection<Tipos>(tiposDis);

            if (null != usuarios.idTipo)
            {
                SeleccionTipo = Tipos.FirstOrDefault(x => x.idTipo == usuarios.idTipo);
            }
                
        }

        public async Task Agregar()
        {        
            bool ok = await WebApiService.AgregarItem(controlador, usuarios);
            if (ok)
                await UserDialogs.Instance.AlertAsync("Usuario registrado!", "Correcto", "OK");
            else
                await UserDialogs.Instance.AlertAsync("Usuario NO registrado!", "Error", "OK");

        }

        public async Task Actualizar()
        {
            bool ok = await WebApiService.ActualizarItem(controlador, usuarios, usuarios.idUsuario);
            if (ok)
                await UserDialogs.Instance.AlertAsync("Usuario actualizado!", "Correcto", "OK");
            else
                await UserDialogs.Instance.AlertAsync("Usuario NO actualizado!", "Error", "OK");

        }

        public async Task Eliminar()
        {
            bool confirmar = await UserDialogs.Instance.ConfirmAsync("¿Estás seguro de eliminar?", "Confirme", "Si", "No");
            if (confirmar)
            {
                bool ok = await WebApiService.EliminarItem(controlador, usuarios.idUsuario);
                if (ok)
                    await UserDialogs.Instance.AlertAsync("Usuario eliminado!", "Correcto", "OK");
                else
                    await UserDialogs.Instance.AlertAsync("Usuario NO eliminado!", "Error", "OK");
            }
        }
    }
}

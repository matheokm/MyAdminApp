using Acr.UserDialogs;
using MyAdminApp.Models;
using MyAdminApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyAdminApp.ViewModels
{
    class ResgistrosViewModel:BaseViewModel
    {
        private Registros registros;
        private string controlador = "/api/ApiRegistros";

        public DateTime Fecha 
        { 
            get=>registros.Fecha;
            set { registros.Fecha = value; OnPropertyChanged(); } 
        }
        
        public string Descripcion
        {
            get => registros.Descripcion;
            set { registros.Descripcion = value; OnPropertyChanged(); }
        }
        
        public int TipoDocumento
        {
            get => registros.TipoDocumento;
            set { registros.TipoDocumento = value; OnPropertyChanged(); }
        }
        
        public int NroDocumento
        {
            get => registros.NroDocumento;
            set { registros.NroDocumento = value; OnPropertyChanged(); }
        }
        
        public double Valor
        {
            get => registros.Valor;
            set { registros.Valor = value; OnPropertyChanged(); }
        }
        
        public string Observaciones
        {
            get => registros.Observaciones;
            set { registros.Observaciones = value; OnPropertyChanged(); }
        }

        //Lista de Usuarios
        private ObservableCollection<Usuarios> usuario;
        public ObservableCollection<Usuarios> Usuario
        {
            get => usuario;
            set { usuario = value; OnPropertyChanged(); }
        }

        //Seleccion de Usuario
        private Usuarios seleccionUsuario;
        public Usuarios SeleccionUsuario
        {
            get => seleccionUsuario;
            set
            {
                seleccionUsuario = value;
                registros.IdUsuario = seleccionUsuario.idUsuario;
                OnPropertyChanged();
            }
        }

        public ICommand ComandoAgregar { private set; get; }
        public ICommand ComandoActualizar { private set; get; }
        public ICommand ComandoEliminar { private set; get; }

        //Constructor
        public ResgistrosViewModel(Registros r = null)
        {
            registros = r ?? new Registros();
            ComandoAgregar = new Command(async () => await Agregar());
            ComandoActualizar = new Command(async () => await Actualizar());
            ComandoEliminar = new Command(async () => await Eliminar());

        }

        public async Task ObtenerUsuarios()
        {
            List<Usuarios> UsuariosDis = await WebApiService.ObtenerItems<Usuarios>("api/ApiUsuarios");
            Usuario = new ObservableCollection<Usuarios>(UsuariosDis);

            if (null != registros.IdUsuario)
            {
                SeleccionUsuario = Usuario.FirstOrDefault(x => x.idUsuario == registros.IdUsuario);
            }

        }

        public async Task Agregar()
        {
            bool ok = await WebApiService.AgregarItem(controlador, registros);
            if (ok)
                await UserDialogs.Instance.AlertAsync("Documento registrado!", "Correcto", "OK");
            else
                await UserDialogs.Instance.AlertAsync("Documento NO registrado!", "Error", "OK");

        }

        public async Task Actualizar()
        {
            bool ok = await WebApiService.ActualizarItem(controlador, registros, registros.IdRegistro);
            if (ok)
                await UserDialogs.Instance.AlertAsync("Documento actualizado!", "Correcto", "OK");
            else
                await UserDialogs.Instance.AlertAsync("Documento NO actualizado!", "Error", "OK");

        }

        public async Task Eliminar()
        {
            bool confirmar = await UserDialogs.Instance.ConfirmAsync("¿Estás seguro de eliminar?", "Confirme", "Si", "No");
            if (confirmar)
            {
                bool ok = await WebApiService.EliminarItem(controlador, registros.IdRegistro);
                if (ok)
                    await UserDialogs.Instance.AlertAsync("Documento eliminado!", "Correcto", "OK");
                else
                    await UserDialogs.Instance.AlertAsync("Documento NO eliminado!", "Error", "OK");
            }
        }
    }
}


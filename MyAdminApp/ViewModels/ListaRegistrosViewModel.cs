using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MyAdminApp.Models;
using MyAdminApp.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace MyAdminApp.ViewModels
{
    class ListaRegistrosViewModel : BaseViewModel
    {
        private string controlador = "/api/apiProductos";

        private ObservableCollection<ResgistrosViewModel> registros;

        public ObservableCollection<ResgistrosViewModel> Registros
        {
            get => registros;
            set { registros = value; OnPropertyChanged(); }
        }

        private ResgistrosViewModel registroSeleccionado;

        public ResgistrosViewModel RegistroSeleccionado
        {
            get => registroSeleccionado;
            set { registroSeleccionado = value; OnPropertyChanged(); }
        }

        private bool actualizando;

        public bool Actualizando
        {
            get => actualizando;
            set { actualizando = value; OnPropertyChanged(); }
        }

        public ICommand ComandoVerDetalles { private set; get; }
        public ICommand ComandoAltaRegistro { private set; get; }
        public ICommand ComandoCargarDatos { private set; get; }

        public INavigation Navegacion { get; set; }

        public ListaRegistrosViewModel(INavigation n)
        {
            Navegacion = n;

            ComandoVerDetalles = new Command<Type>(async (pagina) => await VerDetalles(pagina));
            ComandoAltaRegistro = new Command<Type>(async (pagina) => await AltaRegistro(pagina));

            ComandoCargarDatos = new Command(async () => await CargarDatos());
        }

        public async Task CargarDatos()
        {
            Actualizando = true;
            List<Registros> registros = await WebApiService.ObtenerItems<Registros>(controlador);
            List<ResgistrosViewModel> registrosVM = new List<ResgistrosViewModel>();
            foreach (Registros r in registros)
            {
                registrosVM.Add(new ResgistrosViewModel(r));
            }
            Registros = new ObservableCollection<ResgistrosViewModel>(registrosVM);
            Actualizando = false;
        }

        async Task VerDetalles(Type p)
        {
            if (RegistroSeleccionado != null)
            {
                Page pagina = (Page)Activator.CreateInstance(p);
                pagina.BindingContext = registroSeleccionado;
                await Navegacion.PushAsync(pagina);
            }
        }

        async Task AltaRegistro(Type p)
        {
            Page pagina = (Page)Activator.CreateInstance(p);
            pagina.BindingContext = new ResgistrosViewModel();
            await Navegacion.PushAsync(pagina);

        }

    }
}

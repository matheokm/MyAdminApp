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
    public partial class HistorialDeRegistros : ContentPage
    {
        ListaRegistrosViewModel rvm;
        public HistorialDeRegistros()
        {
            InitializeComponent();
            rvm = new ListaRegistrosViewModel(this.Navigation);
            BindingContext = rvm;
        }
        protected async override void OnAppearing()
        {
            await rvm.CargarDatos();

            base.OnAppearing();
        }
    }
}
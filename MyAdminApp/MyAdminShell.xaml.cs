using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyAdminApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAdminShell : Shell
    {
        public MyAdminShell()
        {
            InitializeComponent();
        }
    }
}
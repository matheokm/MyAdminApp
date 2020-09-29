using System;
using System.Collections.Generic;
using System.Text;

namespace MyAdminApp.Models
{
    class Registros
    {
        public int IdRegistro { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public int TipoDocumento { get; set; }
        public int NroDocumento { get; set; }
        public double Valor { get; set; }
        public string Observaciones { get; set; }
        public int IdUsuario { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class CarritoDetalle
    {
        public int IdArticulo { get; set; }
        public string Nombre { get; set; }
        public string UrlImagen { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public float PrecioUnitario { get; set; }
        public float PrecioTotal { get; set; }
    }
}

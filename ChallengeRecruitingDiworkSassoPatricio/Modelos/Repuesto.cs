using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Repuesto
    {
        public Repuesto()
        {

        }

        public Repuesto(long id, string nombre, decimal precio)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }
}

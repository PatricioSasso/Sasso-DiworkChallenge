using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Presupuesto
    {
        public long Id;

        public Presupuesto(long id, decimal total, Cliente cliente, Vehiculo vehiculo)
        {
            Id = id;
            Total = total;
            Cliente = cliente;
            Vehiculo = vehiculo;
        }

        public decimal Total { get; set; }
        public Cliente Cliente { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }
}

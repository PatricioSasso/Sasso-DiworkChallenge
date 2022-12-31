using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Desperfecto
    {
        public Desperfecto(long id, string descripcion, decimal costoManoObra, int tiempoTrabajoEstimado)
        {
            Id = id;
            Descripcion = descripcion;
            CostoManoObra = costoManoObra;
            TiempoTrabajoEstimado = tiempoTrabajoEstimado;
            Repuestos = new List<Repuesto>();
        }

        public long Id { get; set; }
        public string Descripcion { get; set; }
        public decimal CostoManoObra { get; set; }
        public int TiempoTrabajoEstimado { get; set; }
        public List<Repuesto> Repuestos { get; set; }
    }
}

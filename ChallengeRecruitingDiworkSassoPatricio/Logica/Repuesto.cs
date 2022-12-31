using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Repuesto
    {
        public static List<Modelos.Repuesto> ObtenerTodos()
        {
            return Persistencia.Repuesto.ObtenerTodos();
        }

        public static void Agregar(Modelos.Desperfecto desperfecto, Modelos.Repuesto repuesto)
        {
            desperfecto.Repuestos.Add(repuesto);
        }

        public static void Quitar(Modelos.Desperfecto desperfecto, Modelos.Repuesto repuesto)
        {
            if (desperfecto.Repuestos.Exists(d => d.Id == desperfecto.Id))
            {
                desperfecto.Repuestos.Remove(repuesto);
            }
        }

        public static string ObtenerRepuestoMasUtilizado(string marca, string modelo)
        {
            return Persistencia.Repuesto.ObtenerRepuestoMasUtilizadoPorMarcaOModelo(marca, modelo);
        }
    }
}

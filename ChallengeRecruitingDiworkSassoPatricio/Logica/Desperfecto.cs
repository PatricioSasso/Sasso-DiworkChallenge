using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public static class Desperfecto
    {
        public static List<Modelos.Desperfecto> ObtenerTodos()
        {
            return Persistencia.Desperfecto.ObtenerTodos();
        }

        public static void Agregar(Modelos.Vehiculo vehiculo, Modelos.Desperfecto desperfecto)
        {
            vehiculo.Desperfectos.Add(desperfecto);
        }

        public static void Quitar(Modelos.Vehiculo vehiculo, Modelos.Desperfecto desperfecto)
        {
            if (vehiculo.Desperfectos.Exists(d => d.Id == desperfecto.Id))
            {
                vehiculo.Desperfectos.Remove(desperfecto);
            }
        }

    }
}

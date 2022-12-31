using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public static class Desperfecto
    {
        public static List<Modelos.Desperfecto> ObtenerTodos()
        {
            List<Modelos.Desperfecto> desperfectos = new List<Modelos.Desperfecto>();

            var dtr = DatabaseAccess.executeStoredProcedure("DesperfectoLoad");

            while (dtr.Read())
            {
                desperfectos.Add(new Modelos.Desperfecto(id: Convert.ToInt64(dtr["Id"]),
                                                         descripcion: Convert.ToString(dtr["Descripcion"]),
                                                         costoManoObra: Convert.ToDecimal(dtr["ManoObra"]),
                                                         tiempoTrabajoEstimado: Convert.ToInt32(dtr["TiempoTrabajo"])));
            }

            return desperfectos;
        }

    }
}

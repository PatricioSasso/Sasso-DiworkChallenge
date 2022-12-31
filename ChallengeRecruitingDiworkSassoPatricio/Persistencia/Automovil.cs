using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos;

namespace Persistencia
{
    public class Automovil
    {
        public static long Guardar(short tipo, short cantPuertas, long idVehiculo)
        {
            long id = 0;

            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {nameof(tipo), tipo },
                {nameof(cantPuertas), cantPuertas },
                {nameof(idVehiculo), idVehiculo }
            };

            var dtr = DatabaseAccess.executeStoredProcedure("AutomovilAdd", parametros);

            if(dtr.Read())
            {
                id = Convert.ToInt64(dtr["id"]);
            }

            return id;
        }

        public static Modelos.Automovil Obtener(long id)
        {
            Modelos.Automovil vehiculo = new Modelos.Automovil();

            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                { nameof(id), id },
            };

            var dtr = DatabaseAccess.executeStoredProcedure("VehiculoPorIdLoad", parametros);

            if (dtr.Read())
            {
                vehiculo = new Modelos.Automovil(Convert.ToInt64(dtr["Id"]),
                                                 Convert.ToString(dtr["Marca"]),
                                                 Convert.ToString(dtr["Modelo"]),
                                                 Convert.ToString(dtr["Patente"]));
            }

            return vehiculo;
        }
    }
}

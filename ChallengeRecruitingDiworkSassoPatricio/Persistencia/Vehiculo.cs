using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class Vehiculo
    {
        public static long Guardar(string marca, string modelo, string patente)
        {
            long id = 0;

            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {nameof(marca), marca },
                {nameof(modelo), modelo},
                {nameof(patente), patente}
            };

            var dtr = DatabaseAccess.executeStoredProcedure("VehiculoAdd", parametros);

            if (dtr.Read())
            {
                id = Convert.ToInt64(dtr["id"]);
            }

            return id;
        }
    }
}

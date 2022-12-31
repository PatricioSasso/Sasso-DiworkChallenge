using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class Moto
    {
        public static long Guardar(string cilindrada, long idVehiculo)
        {
            long id = 0;

            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                {nameof(cilindrada), cilindrada },
                {nameof(idVehiculo), idVehiculo }
            };

            var dtr = DatabaseAccess.executeStoredProcedure("MotoAdd", parametros);

            if (dtr.Read())
            {
                id = Convert.ToInt64(dtr["id"]);
            }

            return id;
        }

    }
}

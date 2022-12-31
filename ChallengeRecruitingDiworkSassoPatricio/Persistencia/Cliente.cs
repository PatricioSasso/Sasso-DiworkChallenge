using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public static class Cliente
    {
        public static long Guardar(string nombre, string apellido, string email)
        {
            long id = 0;

            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                { nameof(nombre), nombre  },
                { nameof(apellido), apellido },
                { nameof(email), email }
            };

            var dtr = DatabaseAccess.executeStoredProcedure("ClienteAdd", parametros);

            if(dtr.Read())
            {
                id = Convert.ToInt32(dtr["id"]);
            }

            return id;
        }

        public static Modelos.Cliente Obtener(long id)
        {
            Modelos.Cliente cliente = new Modelos.Cliente();

            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                { nameof(id), id },
            };
            
            var dtr = DatabaseAccess.executeStoredProcedure("ClientePorIdLoad", parametros);

            if(dtr.Read())
            {
                cliente = new Modelos.Cliente(Convert.ToString(dtr["Nombre"]),
                                              Convert.ToString(dtr["Apellido"]),
                                              Convert.ToString(dtr["Email"]));
            }

            return cliente;
        }
    }
}

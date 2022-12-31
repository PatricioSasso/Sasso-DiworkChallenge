using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public static class Repuesto
    {
        public static List<Modelos.Repuesto> ObtenerTodos()
        {
            List<Modelos.Repuesto> repuestos = new List<Modelos.Repuesto>();

            var dtr = DatabaseAccess.executeStoredProcedure("RepuestoLoad");

            while (dtr.Read())
            {
                repuestos.Add(new Modelos.Repuesto(id: Convert.ToInt64(dtr["Id"]),
                                                 nombre: Convert.ToString(dtr["Nombre"]),
                                                 precio: Convert.ToDecimal(dtr["Precio"])));
            }

            return repuestos;
        }

        public static string ObtenerRepuestoMasUtilizadoPorMarcaOModelo(string marca, string modelo)
        {
            string repuesto = "";

            Dictionary<string, object> parametros = new Dictionary<string, object>()
            {
                { nameof(marca), marca },
                { nameof(modelo), modelo }
            };

            var dtr = DatabaseAccess.executeStoredProcedure("RepuestoMasUtilizadoPorMarcaOModelo", parametros);

            if(dtr.Read())
            {
                repuesto = "Nombre : " + Convert.ToString(dtr["Nombre"]) + " |  Cantidad : " + Convert.ToInt64(dtr["Cantidad"]);
            }

            return repuesto;
        }
    }
}

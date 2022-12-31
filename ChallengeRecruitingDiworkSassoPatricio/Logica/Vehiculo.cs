using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia;

namespace Logica
{
    public static class Vehiculo
    {
        public static long Guardar(string marca, string modelo, string patente)
        {
            return Persistencia.Vehiculo.Guardar(marca, modelo, patente);
        }
    }
}

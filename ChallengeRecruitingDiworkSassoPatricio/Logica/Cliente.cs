using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public static class Cliente
    {
        public static long Guardar(string nombre, string apellido, string email)
        {
            return Persistencia.Cliente.Guardar(nombre, apellido, email);
        }
    }
}

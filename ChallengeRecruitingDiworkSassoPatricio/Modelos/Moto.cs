using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Moto : Vehiculo
    {
        public Moto(long id, string marca, string modelo, string patente, string cilindrada) : base(id, marca, modelo, patente)
        {
            Id = id;
            Marca = marca;
            Modelo = modelo;
            Patente = patente;
            Cilindrada = cilindrada;
        }

        public string Cilindrada { get; set; }
    }
}

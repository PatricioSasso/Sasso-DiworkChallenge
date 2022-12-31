using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public abstract class Vehiculo
    {
        public Vehiculo()
        {

        }

        protected Vehiculo(long id, string marca, string modelo, string patente)
        {
            Id = id;
            Marca = marca;
            Modelo = modelo;
            Patente = patente;
            Id = id;
            Marca = marca;
            Modelo = modelo;
            Patente = patente;
            Desperfectos = new List<Desperfecto>();
        }

        public long Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Patente { get; set; }
        public List<Desperfecto> Desperfectos { get; set; }
    }
}
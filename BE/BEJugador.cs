using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEJugador
    {
        public BEJugador()
        {


        }

        public int Codigo { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int DNI { get; set; }

        public string Mail { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Localidad { get; set; }

        public override string ToString()
        {
            return this.Codigo + " " + this.Nombre + " " + this.Apellido;
        }
    }
}

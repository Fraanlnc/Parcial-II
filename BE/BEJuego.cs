using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEJuego
    {
        public BEJuego()
        {

        }

        public int Codigo { get; set; }
        public string Nombre { get; set; }

        public override string ToString()
        {
            return this.Codigo + " " + this.Nombre;
        }
    }
}

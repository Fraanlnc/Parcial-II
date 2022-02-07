using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEPartidas
    {
        public BEPartidas()
        {

        }

       // public int Codigo { get; set; }

        public BEJuego Juego { get; set; }
        public BEJugador Jugador { get; set; }
        
        public int Ganadas { get; set; }
        public int Perdidas { get; set; }
        public int Empates { get; set; }

        public override string ToString()
        {
            return this.Juego.Codigo + " " + this.Jugador.Codigo; 
        }

    }
}

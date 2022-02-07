using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using BE;

namespace BLL
{
    public class BLLJugador
    {
        public BLLJugador()
        {
            oMPPJugador = new MPPJugador();
        }
        MPPJugador oMPPJugador;
        public void Registrar(BEJugador jugador)
        {
            oMPPJugador.AgregarXML(jugador);
        }

        public List<BEJugador> BuscarXML (BEJugador jugador)
        {
            return oMPPJugador.BuscarXML(jugador);
        }

        public List<BEJugador> ListarTodo()
        {
            return oMPPJugador.LeerXML();
        }

        public void Modificar(BEJugador jugador)
        {
            oMPPJugador.ModificarXML(jugador);
        }

        public void Eliminar(BEJugador jugador)
        {
            oMPPJugador.EliminarXML(jugador);
        }

    }
}

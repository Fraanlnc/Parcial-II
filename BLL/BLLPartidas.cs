using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using BE;

namespace BLL
{
    public class BLLPartidas
    {
        public BLLPartidas()
        {
            oMPPPartidas = new MPPPartidas();
        }

        MPPPartidas oMPPPartidas;
        public void Registrar(BEPartidas obj)
        {
            oMPPPartidas.AgregarXML(obj);
        }

        public List<BEPartidas> BuscarXML(BEJugador jugador, BEJuego juego)
        {
            return oMPPPartidas.BuscarXML(jugador,juego);
        }

        public List<BEPartidas> ListarTodo()
        {
            return oMPPPartidas.LeerXML();
        }

        public void ModificarXML(BEPartidas obj)
        {
            oMPPPartidas.ModificarXML(obj);
        }

        public void EliminarXML(BEPartidas obj)
        {
            oMPPPartidas.EliminarXML(obj);
        }
    }
}

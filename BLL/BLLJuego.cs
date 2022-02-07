using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;

namespace BLL
{
    public class BLLJuego
    {

        public BLLJuego()
        {
            oMPPJuego = new MPPJuego();
        }
        MPPJuego oMPPJuego;

        public List<BEJuego> ListarTodo()
        {
            return oMPPJuego.LeerXML();
        }
    }
}

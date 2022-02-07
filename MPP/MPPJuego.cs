using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using BE;

namespace MPP
{
    public class MPPJuego
    {

        

        public List<BEJuego> LeerXML()
        { 
            var consulta =
                from Juegos in XElement.Load("Juegos.XML").Elements("Juego")

                select new BEJuego
                {
                    Codigo = Convert.ToInt32(Convert.ToString(Juegos.Attribute("Codigo").Value).Trim()),
                    Nombre = Convert.ToString(Juegos.Element("Nombre").Value).Trim(),
                };//Fin de consulta.

            List<BEJuego> ListaJuegos = consulta.ToList<BEJuego>();


            return ListaJuegos;
        }
    }
}

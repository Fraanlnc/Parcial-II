using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using BE;
using System.Xml.XPath;

namespace MPP
{
    public class MPPPartidas
    {

        public void AgregarXML(BEPartidas partida)
        {
            try
            {
                //Se intenta abrir el documento
                XDocument xmlDoc1 = XDocument.Load("Partidas.XML");
            }
            catch
            {
                //Si no existe se crea
                XmlTextWriter xmlDoc = new XmlTextWriter("Partidas.XML", System.Text.Encoding.UTF8);

                xmlDoc.Formatting = Formatting.Indented;

                xmlDoc.WriteStartDocument(true);
                xmlDoc.WriteStartElement("Partidas");
                xmlDoc.Close();
            }
            XDocument xmlDoc2 = XDocument.Load("Partidas.XML");
            
            xmlDoc2.Element("Partidas").Add(new XElement("Partida",
                                            //new XAttribute("Codigo", Convert.ToString(partida.Codigo).Trim()),
                                            new XAttribute("Codigo_Juego", Convert.ToString(partida.Juego.Codigo).Trim()),
                                            new XAttribute("Codigo_Jugador", Convert.ToString(partida.Jugador.Codigo).Trim()),
                                            new XElement("Ganadas", Convert.ToString(partida.Ganadas).Trim()),
                                            new XElement("Perdidas", Convert.ToString(partida.Perdidas).Trim()),
                                            new XElement("Empates", Convert.ToString(partida.Empates).Trim())));
            //luego el metodo save guarda lo ingresado en el XML
            xmlDoc2.Save("Partidas.XML");
        }
        public List<BEPartidas> LeerXML()
        {
            //Primero cargo una lista de codigos de los juegos
            var consultaJuegos =
                from Partidas in XElement.Load("Partidas.XML").Elements("Partida")

                select new BEJuego
                {
                    Codigo = Convert.ToInt32(Convert.ToString(Partidas.Attribute("Codigo_Juego").Value).Trim()),
                };

            List<BEJuego> listacodigosjuegos = consultaJuegos.ToList<BEJuego>();

            //Cargo una lista de codigos de jugadores
            var consultaJugadores =
               from Partidas in XElement.Load("Partidas.XML").Elements("Partida")

               select new BEJugador
               {
                   Codigo = Convert.ToInt32(Convert.ToString(Partidas.Attribute("Codigo_Jugador").Value).Trim()),
               };

            List<BEJugador> listacodigosjugadores = consultaJugadores.ToList<BEJugador>();

            


            //leer partidas
            var consulta =
                from Partidas in XElement.Load("Partidas.XML").Elements("Partida")

                select new BEPartidas
                {
                    //Codigo = Convert.ToInt32(Convert.ToString(Partidas.Attribute("Codigo").Value).Trim()),
                    Ganadas = Convert.ToInt32(Convert.ToString(Partidas.Element("Ganadas").Value).Trim()),
                    Perdidas = Convert.ToInt32(Convert.ToString(Partidas.Element("Perdidas").Value).Trim()),
                    Empates = Convert.ToInt32(Convert.ToString(Partidas.Element("Empates").Value).Trim()),

                };//Fin de consulta.

            List<BEPartidas> listapartidas = consulta.ToList<BEPartidas>();


            int i = 0;
            foreach (BEJuego p in listacodigosjuegos)
            {
                //Asigno los codigos de los juegos 
                listapartidas[i].Juego = p;
                i++;
            }
            i = 0;
            foreach (BEJugador p in listacodigosjugadores)
            {
                //Asigno los codigos de los juegos 
                listapartidas[i].Jugador = p;
                i++;
            }

            return listapartidas;
        }

        public List<BEPartidas> BuscarXML(BEJugador jugador, BEJuego juego)
        {
            //primero guardo el juego
            var consultajuego =
               from Partidas in XElement.Load("Partidas.XML").Elements("Partida")
               where (string)Partidas.Attribute("Codigo_Jugador") == jugador.Codigo.ToString() && (string)Partidas.Attribute("Codigo_Juego") == juego.Codigo.ToString()
               select new BEJuego
               {
                   Codigo = Convert.ToInt32(Convert.ToString(Partidas.Attribute("Codigo_Juego").Value).Trim()),

                   


               };//Fin de consulta.

            List<BEJuego> listajuego = consultajuego.ToList<BEJuego>();

            //luego guardo el jugador
            
            var consultaJugador =
               from Partidas in XElement.Load("Partidas.XML").Elements("Partida")
               where (string)Partidas.Attribute("Codigo_Jugador") == jugador.Codigo.ToString() && (string)Partidas.Attribute("Codigo_Juego") == juego.Codigo.ToString()
               select new BEJugador
               {
                   Codigo = Convert.ToInt32(Convert.ToString(Partidas.Attribute("Codigo_Jugador").Value).Trim()),




               };//Fin de consulta.

            List<BEJugador> listajugador = consultaJugador.ToList<BEJugador>();


            //por ultimo traigo el historial
            var consulta =
               from Partidas in XElement.Load("Partidas.XML").Elements("Partida")
               where (string)Partidas.Attribute("Codigo_Jugador") == jugador.Codigo.ToString() && (string)Partidas.Attribute("Codigo_Juego")== juego.Codigo.ToString()
               select new BEPartidas
               {
                   //Codigo = Convert.ToInt32(Convert.ToString(Partidas.Attribute("Codigo").Value).Trim()),
                   
                   Ganadas = Convert.ToInt32(Convert.ToString(Partidas.Element("Ganadas").Value).Trim()),
                   Perdidas = Convert.ToInt32(Convert.ToString(Partidas.Element("Perdidas").Value).Trim()),
                   Empates = Convert.ToInt32(Convert.ToString(Partidas.Element("Empates").Value).Trim()),


               };//Fin de consulta.

            List<BEPartidas> listapartidas = consulta.ToList<BEPartidas>();

            int i = 0;
            if (listapartidas.Count > 0)
            {
                foreach (BEPartidas p in listapartidas)
                {
                    //asigno los codigos de jugador y juego al elemento partida
                    p.Juego = listajuego[i];
                    p.Jugador = listajugador[i];
                }

            }


            return listapartidas;
        }

        public void ModificarXML(BEPartidas obj)
        {
            //XDocument xmlDoc1 = XDocument.Load("Partidas.XML");

            var xdoc = XDocument.Load("Partidas.XML");
            //este funciona para un elemento solo xdoc.XPathSelectElement("/Partidas/Partida/Empates").Value = obj.Empates.ToString();

            //funciona elementos fijos xdoc.XPathSelectElement("/Partidas/Partida[@Codigo_Juego='1'][@Codigo_Jugador='1']/Empates").Value = obj.Empates.ToString();
            
            //Modificaciones del historial de partidas
            xdoc.XPathSelectElement("/Partidas/Partida[@Codigo_Juego='" + obj.Juego.Codigo.ToString() + "'][@Codigo_Jugador='" + obj.Jugador.Codigo.ToString() + "']/Ganadas").Value = obj.Ganadas.ToString();
            xdoc.XPathSelectElement("/Partidas/Partida[@Codigo_Juego='" + obj.Juego.Codigo.ToString() + "'][@Codigo_Jugador='" + obj.Jugador.Codigo.ToString() + "']/Perdidas").Value = obj.Perdidas.ToString();
            xdoc.XPathSelectElement("/Partidas/Partida[@Codigo_Juego='"+ obj.Juego.Codigo.ToString() +"'][@Codigo_Jugador='"+obj.Jugador.Codigo.ToString()+"']/Empates").Value = obj.Empates.ToString();

            
            xdoc.Save("Partidas.XML");
        }

        public void EliminarXML(BEPartidas obj)
        {
            var xdoc = XDocument.Load("Partidas.XML");

            xdoc.XPathSelectElement("/Partidas/Partida[@Codigo_Jugador='" + obj.Jugador.Codigo.ToString() + "']").Remove();



            //XmlDocument xdoc = new XmlDocument();
            // xdoc.Load("Partidas.XML");
            /* foreach (XmlNode xNode in xdoc.SelectNodes("Partidas/Partida"))
                 if (xNode.SelectSingleNode("Codigo_Jugador").InnerText == obj.Jugador.Codigo.ToString()) xNode.ParentNode.RemoveChild(xNode);
             */












            /*

            var nodes = xdoc.Elements().Where(x => x.Element("Codigo_Juego") == a && x.Attribute("Codigo_Jugador") == obj.Jugador.Codigo.ToString()).ToList();

            foreach (var node in nodes)
                node.Remove();

           




            //calculo cuantos registros voy a eliminar
            List<BEPartidas> listapartidas = new List<BEPartidas>();
            listapartidas = this.BuscarXML(obj.Jugador, obj.Juego);
            int i = 0;

            

            foreach (XElement p in xdoc.Descendants("Partida"))
            {
                //xdoc.XPathSelectElement("/Partidas/Partida[@Codigo_Jugador='" + obj.Jugador.Codigo.ToString() + "']").Remove();
                xdoc.XPathSelectElement("/Partidas/Partida[@Codigo_Juego='" + obj.Juego.Codigo.ToString() + "'][@Codigo_Jugador='" + obj.Jugador.Codigo.ToString() + "']").Remove();
                i++;
                if (i == listapartidas.Count) break;
            }
            // funciona pero deja jugador / xdoc.XPathSelectElement("/Jugadores/Jugador[@Codigo='" + obj.Codigo.ToString() + "']").RemoveAll();
            //Elimina el jugador seleccionado
            // funca oK xdoc.XPathSelectElement("/Partidas/Partida[@Codigo_Jugador='" + obj.Jugador.Codigo.ToString() + "']").Remove();
            
            */

            //se guardan los cambios
            //comentado para no guardar cambios
            xdoc.Save("Partidas.XML");

        }
    }
}

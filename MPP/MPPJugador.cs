using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using BE;

namespace MPP
{
    public class MPPJugador
    {

        public void AgregarXML(BEJugador jugador)
        {
            try
            {
                //Se intenta abrir el documento
                XDocument xmlDoc1 = XDocument.Load("Jugadores.XML");
            }
            catch
            {
                //Si no existe se crea
                XmlTextWriter xmlDoc = new XmlTextWriter("Jugadores.XML", System.Text.Encoding.UTF8);

                xmlDoc.Formatting = Formatting.Indented;

                xmlDoc.WriteStartDocument(true);
                xmlDoc.WriteStartElement("Jugadores");
                xmlDoc.Close();
            }

            XDocument xmlDoc2 = XDocument.Load("Jugadores.XML");
            //Carga de sancion
            xmlDoc2.Element("Jugadores").Add(new XElement("Jugador",
                                            new XAttribute("Codigo", Convert.ToString(jugador.Codigo).Trim()),
                                            new XElement("Nombre", jugador.Nombre.Trim()),
                                            new XElement("Apellido", jugador.Apellido.Trim()),
                                            new XElement("DNI", Convert.ToString(jugador.DNI).Trim()),
                                            new XElement("Mail", jugador.Mail.Trim()),
                                            new XElement("FechaNacimiento", Convert.ToString(jugador.FechaNacimiento.ToShortDateString()).Trim()),
                                            new XElement("Localidad", jugador.Localidad.Trim())));

            //luego el metodo save guarda lo ingresado en el XML
            xmlDoc2.Save("Jugadores.XML");


        }

        public List<BEJugador> LeerXML()
        {
            var consulta =
                from Jugadores in XElement.Load("Jugadores.XML").Elements("Jugador")

                select new BEJugador
                {
                    Codigo = Convert.ToInt32(Convert.ToString(Jugadores.Attribute("Codigo").Value).Trim()),
                    Nombre = Convert.ToString(Jugadores.Element("Nombre").Value).Trim(),
                    Apellido = Convert.ToString(Jugadores.Element("Apellido").Value).Trim(),
                    DNI = Convert.ToInt32(Convert.ToString(Jugadores.Element("DNI").Value).Trim()),
                    Mail = Convert.ToString(Jugadores.Element("Mail").Value).Trim(),
                    FechaNacimiento = Convert.ToDateTime(Convert.ToString(Jugadores.Element("FechaNacimiento").Value).Trim()),
                    Localidad = Convert.ToString(Jugadores.Element("Localidad").Value).Trim()
                };//Fin de consulta.

            List<BEJugador> ListaJugadores = consulta.ToList<BEJugador>();


            return ListaJugadores;
        }

        public List<BEJugador> BuscarXML(BEJugador jugador)
        {


            var consulta =
               from Jugadores in XElement.Load("Jugadores.XML").Elements("Jugador")
               where (string)Jugadores.Element("DNI") == jugador.DNI.ToString()
               select new BEJugador
               {
                   Codigo = Convert.ToInt32(Convert.ToString(Jugadores.Attribute("Codigo").Value).Trim()),
                   Nombre = Convert.ToString(Jugadores.Element("Nombre").Value).Trim(),
                   Apellido = Convert.ToString(Jugadores.Element("Apellido").Value).Trim(),
                   DNI = Convert.ToInt32(Convert.ToString(Jugadores.Element("DNI").Value).Trim()),
                   Mail = Convert.ToString(Jugadores.Element("Mail").Value).Trim(),
                   FechaNacimiento = Convert.ToDateTime(Convert.ToString(Jugadores.Element("FechaNacimiento").Value).Trim()),
                   Localidad = Convert.ToString(Jugadores.Element("Localidad").Value).Trim()
                   
               };//Fin de consulta.

            List<BEJugador> ListaJugador = consulta.ToList<BEJugador>();
        
            return ListaJugador;
        }

        public void EliminarXML(BEJugador obj)
        {
            var xdoc = XDocument.Load("Jugadores.XML");

            // funciona pero deja jugador / xdoc.XPathSelectElement("/Jugadores/Jugador[@Codigo='" + obj.Codigo.ToString() + "']").RemoveAll();
            //Elimina el jugador seleccionado
            xdoc.XPathSelectElement("/Jugadores/Jugador[@Codigo='" + obj.Codigo.ToString() + "']").Remove();
            //se guardan los cambios
            
           xdoc.Save("Jugadores.XML");

        }
        public void ModificarXML(BEJugador obj)
        {
           

            var xdoc = XDocument.Load("Jugadores.XML");
            
            xdoc.XPathSelectElement("/Jugadores/Jugador[@Codigo='" + obj.Codigo.ToString() + "']/Nombre").Value = obj.Nombre.ToString();
            xdoc.XPathSelectElement("/Jugadores/Jugador[@Codigo='" + obj.Codigo.ToString() + "']/Apellido").Value = obj.Apellido.ToString();
            xdoc.XPathSelectElement("/Jugadores/Jugador[@Codigo='" + obj.Codigo.ToString() + "']/DNI").Value = obj.DNI.ToString();
            xdoc.XPathSelectElement("/Jugadores/Jugador[@Codigo='" + obj.Codigo.ToString() + "']/Mail").Value = obj.Mail.ToString();
            xdoc.XPathSelectElement("/Jugadores/Jugador[@Codigo='" + obj.Codigo.ToString() + "']/FechaNacimiento").Value = obj.FechaNacimiento.ToShortDateString().ToString();
            xdoc.XPathSelectElement("/Jugadores/Jugador[@Codigo='" + obj.Codigo.ToString() + "']/Localidad").Value = obj.Localidad.ToString();

            xdoc.Save("Jugadores.XML");
        }
    }
}

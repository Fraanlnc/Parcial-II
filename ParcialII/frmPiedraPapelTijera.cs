using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using BE;
using BLL;

namespace ParcialII
{
    public partial class frmPiedraPapelTijera : Form
    {
        public frmPiedraPapelTijera()
        {
            InitializeComponent();
            oBEJugador = new BEJugador();
            oBLLJugador = new BLLJugador();
            oBEPartida = new BEPartidas();
            oBLLPartida = new BLLPartidas();
            oBEJuego = new BEJuego();
            oBLLJuego = new BLLJuego();
            PC = new BEJugador();
        }
        BEJugador oBEJugador;
        BLLJugador oBLLJugador;
        BEPartidas oBEPartida;
        BLLPartidas oBLLPartida;
        BEJuego oBEJuego;
        BLLJuego oBLLJuego;
        BEJugador PC;


        private void frmPiedraPapelTijera_Load(object sender, EventArgs e)
        {
            try
            {



                PC.Codigo = 99;
                PC.Nombre = "PC";

                //Leo XML de juegos
                List<BEJuego> listajuegos = new List<BEJuego>();
                listajuegos = oBLLJuego.ListarTodo();
                //Busco y guardo el juego
                oBEJuego = listajuegos.Find(x => x.Nombre == "Piedra, papel o tijera");

           

                radioButton1.Checked = true;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = false;

                //cargo los combo box con los jugadores
                comboBox1.DataSource = oBLLJugador.ListarTodo();
                comboBox2.DataSource = oBLLJugador.ListarTodo();
                comboBox3.DataSource = oBLLJugador.ListarTodo();

              


            }
            catch (XmlException ex) { MessageBox.Show(ex.Message);}
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = false;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
               if (radioButton1.Checked == true)
               {
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox1.SelectedItem;
                    jugador2 = (BEJugador)comboBox2.SelectedItem;
                    if (jugador1.Codigo == jugador2.Codigo)
                    {
                        MessageBox.Show("No se puede elegir el mismo jugador");
                    }
                    else
                    {
                        button1.Enabled = false;
                        groupBox1.Enabled = true;
                        groupBox2.Text = comboBox1.SelectedItem.ToString();
                        groupBox3.Text = comboBox2.SelectedItem.ToString();

                    }
                      

               }

               if (radioButton2.Checked == true)
               {
                    groupBox1.Enabled = true;
                    groupBox2.Text = comboBox3.SelectedItem.ToString();
                    groupBox3.Text = "PC";
                    var random = new Random();
                    //Genero un numero aleatorio para que la pc elija
                    var aux = random.Next(-2, 2);
                    if (aux > 0) radioButton8.Checked = true;
                    if (aux==0) radioButton7.Checked = true;
                    if (aux < 0) radioButton6.Checked = true;

                   


               }


            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            groupBox1.Enabled = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;
            radioButton7.Checked = false;
            radioButton8.Checked = false;
            radioButton1.Checked = true;
            comboBox3.Enabled = false;
            comboBox2.Enabled = true;
            comboBox1.Enabled = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
           

                //validar si jugador vs jugador o jugador vs pc
                if (radioButton1.Checked == true)
                {
                    //jugador vs Jugador
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox1.SelectedItem;
                    jugador2 = (BEJugador)comboBox2.SelectedItem;

                    
                        //empates
                     if ((radioButton3.Checked == true && radioButton8.Checked == true) || (radioButton4.Checked == true && radioButton7.Checked == true) || (radioButton5.Checked == true && radioButton6.Checked == true)) 
                     { 
                            MessageBox.Show("Empate");

                        //Leer xml, buscar partidas hechas por este jugador en este juego
                        //actualizar empates, perdidas y ganadas
                        
                        //JUGADOR 1
                         List<BEPartidas> lista = new List<BEPartidas>();
                         lista= oBLLPartida.BuscarXML(jugador1, oBEJuego);
                        BEPartidas aux = new BEPartidas();
                        aux = lista.Find(x => x.Jugador.Codigo == jugador1.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        //chequear que haya encontrado una partida
                        if (aux != null)
                        {//Guardo los datos del jugador 1 y del juego completos
                            aux.Jugador = jugador1;
                            aux.Juego = oBEJuego;
                            //Sumo el empate
                            aux.Empates++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux);
                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador1;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Empates++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }


                        //JUGADOR 2
                        List<BEPartidas> lista2 = new List<BEPartidas>();
                        lista2 = oBLLPartida.BuscarXML(jugador2, oBEJuego);
                        BEPartidas aux2 = new BEPartidas();
                        aux2 = lista2.Find(x => x.Jugador.Codigo == jugador2.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        //chequear que haya encontrado una partida
                        if (aux2 != null)
                        {//Guardo los datos del jugador 2 y del juego completos
                            aux2.Jugador = jugador2;
                            aux2.Juego = oBEJuego;
                            //Sumo el empate
                            aux2.Empates++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux2);
                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 2 y del juego completos}
                            
                            nuevo.Jugador = jugador2;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Empates++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }







                     }
                   

                        //piedra - tijera
                        if (radioButton3.Checked == true && radioButton6.Checked == true) 
                        {
                            MessageBox.Show("Gana " + jugador1.ToString());
                        //Jugador 1
                        #region "Jugador 1 GANA"
                        List<BEPartidas> lista = new List<BEPartidas>();
                            lista = oBLLPartida.BuscarXML(jugador1, oBEJuego);
                            BEPartidas aux = new BEPartidas();
                            aux = lista.Find(x => x.Jugador.Codigo == jugador1.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                                 if (aux != null)
                                 {
                                    //Guardo los datos del jugador 1 y del juego completos
                                    aux.Jugador = jugador1;
                                    aux.Juego = oBEJuego;
                                    //Sumo el empate
                                    aux.Ganadas++;
                                    //guardo modificacion
                                    oBLLPartida.ModificarXML(aux);



                                 }
                                 else
                                 {
                                        BEPartidas nuevo = new BEPartidas();
                                        //Guardo los datos del jugador 1 y del juego completos
                                        nuevo.Jugador = jugador1;
                                        nuevo.Juego = oBEJuego;
                                        //Sumo el empate
                                        nuevo.Ganadas++;
                                        //Registro una partida nueva
                                        oBLLPartida.Registrar(nuevo);

                                 }
                        #endregion

                        //JUGADOR 2
                        #region "JUGADOR 2 PIERDE"
                        lista = oBLLPartida.BuscarXML(jugador2, oBEJuego);
                        aux = lista.Find(x => x.Jugador.Codigo == jugador2.Codigo && x.Juego.Codigo == oBEJuego.Codigo);

                        //jugador2 Pierde
                        if (aux != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            aux.Jugador = jugador2;
                            aux.Juego = oBEJuego;
                            //Sumo el empate
                            aux.Perdidas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador2;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Perdidas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }

                        #endregion


                        }


                    //piedra - papel
                    if (radioButton3.Checked == true && radioButton7.Checked == true) 
                    {
                        MessageBox.Show("Gana " + jugador2.ToString());

                        #region "JUGADOR 2 GANA"
                        List<BEPartidas> lista = new List<BEPartidas>();
                        lista = oBLLPartida.BuscarXML(jugador2, oBEJuego);
                        BEPartidas aux = new BEPartidas();
                        aux = lista.Find(x => x.Jugador.Codigo == jugador2.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (aux != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            aux.Jugador = jugador2;
                            aux.Juego = oBEJuego;
                            //Sumo el empate
                            aux.Ganadas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador2;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Ganadas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }

                        #endregion

                        #region "JUGADOR 1 PIERDE"
                        lista = oBLLPartida.BuscarXML(jugador1, oBEJuego);
                        aux = lista.Find(x => x.Jugador.Codigo == jugador1.Codigo && x.Juego.Codigo == oBEJuego.Codigo);

                        //jugador2 Pierde
                        if (aux != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            aux.Jugador = jugador1;
                            aux.Juego = oBEJuego;
                            //Sumo el empate
                            aux.Perdidas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador1;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Perdidas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion

                    }



                    //papel - piedra
                    if (radioButton4.Checked == true && radioButton8.Checked == true)
                    { 
                        MessageBox.Show("Gana " + jugador1.ToString());

                        #region "JUGADOR 1 GANA"
                        List<BEPartidas> lista = new List<BEPartidas>();
                        lista = oBLLPartida.BuscarXML(jugador1, oBEJuego);
                        BEPartidas aux = new BEPartidas();
                        aux = lista.Find(x => x.Jugador.Codigo == jugador1.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (aux != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            aux.Jugador = jugador1;
                            aux.Juego = oBEJuego;
                            //Sumo el empate
                            aux.Ganadas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador1;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Ganadas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion

                        #region "JUGADOR 2 PIERDE"
                        lista = oBLLPartida.BuscarXML(jugador2, oBEJuego);
                        aux = lista.Find(x => x.Jugador.Codigo == jugador2.Codigo && x.Juego.Codigo == oBEJuego.Codigo);

                        //jugador2 Pierde
                        if (aux != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            aux.Jugador = jugador2;
                            aux.Juego = oBEJuego;
                            //Sumo el empate
                            aux.Perdidas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador2;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Perdidas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion
                    }


                    //papel-tijera
                    if (radioButton4.Checked == true && radioButton6.Checked == true) 
                    { 
                        MessageBox.Show("Gana " + jugador2.ToString());
                        #region "JUGADOR 2 GANA"
                        List<BEPartidas> lista = new List<BEPartidas>();
                        lista = oBLLPartida.BuscarXML(jugador2, oBEJuego);
                        BEPartidas aux = new BEPartidas();
                        aux = lista.Find(x => x.Jugador.Codigo == jugador2.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (aux != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            aux.Jugador = jugador2;
                            aux.Juego = oBEJuego;
                            //Sumo el empate
                            aux.Ganadas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador2;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Ganadas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion

                        #region "JUGADOR 1 PIERDE"
                        lista = oBLLPartida.BuscarXML(jugador1, oBEJuego);
                        aux = lista.Find(x => x.Jugador.Codigo == jugador1.Codigo && x.Juego.Codigo == oBEJuego.Codigo);

                        //jugador2 Pierde
                        if (aux != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            aux.Jugador = jugador1;
                            aux.Juego = oBEJuego;
                            //Sumo el empate
                            aux.Perdidas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador1;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Perdidas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion
                    }
                    //tijera - piedra
                    if (radioButton5.Checked == true && radioButton8.Checked == true) 
                    { 
                        MessageBox.Show("Gana " + jugador2.ToString());

                        #region "JUGADOR 2 GANA"
                        List<BEPartidas> lista = new List<BEPartidas>();
                        lista = oBLLPartida.BuscarXML(jugador2, oBEJuego);
                        BEPartidas aux = new BEPartidas();
                        aux = lista.Find(x => x.Jugador.Codigo == jugador2.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (aux != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            aux.Jugador = jugador2;
                            aux.Juego = oBEJuego;
                            //Sumo el empate
                            aux.Ganadas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador2;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Ganadas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion

                        #region "JUGADOR 1 PIERDE"
                        lista = oBLLPartida.BuscarXML(jugador1, oBEJuego);
                        aux = lista.Find(x => x.Jugador.Codigo == jugador1.Codigo && x.Juego.Codigo == oBEJuego.Codigo);

                        //jugador2 Pierde
                        if (aux != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            aux.Jugador = jugador1;
                            aux.Juego = oBEJuego;
                            //Sumo el empate
                            aux.Perdidas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador1;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Perdidas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion
                    }
                    //tijera - papel
                    if (radioButton5.Checked == true && radioButton7.Checked == true) 
                    { 
                        MessageBox.Show("Gana " + jugador1.ToString());


                        #region "JUGADOR 1 GANA"
                        List<BEPartidas> lista = new List<BEPartidas>();
                        lista = oBLLPartida.BuscarXML(jugador1, oBEJuego);
                        BEPartidas aux = new BEPartidas();
                        aux = lista.Find(x => x.Jugador.Codigo == jugador1.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (aux != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            aux.Jugador = jugador1;
                            aux.Juego = oBEJuego;
                            //Sumo el empate
                            aux.Ganadas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador1;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Ganadas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion

                        #region "JUGADOR 2 PIERDE"
                        lista = oBLLPartida.BuscarXML(jugador2, oBEJuego);
                        aux = lista.Find(x => x.Jugador.Codigo == jugador2.Codigo && x.Juego.Codigo == oBEJuego.Codigo);

                        //jugador2 Pierde
                        if (aux != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            aux.Jugador = jugador2;
                            aux.Juego = oBEJuego;
                            //Sumo el empate
                            aux.Perdidas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador2;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Perdidas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion

                    }

                }



                
                if (radioButton2.Checked == true)
                {
                    //jugador vs pc
                    BEJugador jugador3 = new BEJugador();
                    jugador3 = (BEJugador)comboBox3.SelectedItem;
                    //empates
                    if ((radioButton3.Checked == true && radioButton8.Checked == true) || (radioButton4.Checked == true && radioButton7.Checked == true) || (radioButton5.Checked == true && radioButton6.Checked == true))
                    {
                        MessageBox.Show("Empate");
                        #region "Jugador 3 EMPATE vs pc"
                        List<BEPartidas> lista = new List<BEPartidas>();
                        lista = oBLLPartida.BuscarXML(jugador3, oBEJuego);
                        BEPartidas aux2 = new BEPartidas();
                        aux2 = lista.Find(x => x.Jugador.Codigo == jugador3.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (aux2 != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            aux2.Jugador = jugador3;
                            aux2.Juego = oBEJuego;
                            //Sumo el empate
                            aux2.Empates++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux2);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador3;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Empates++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion

                        #region "PC EMPATA"

                        lista = oBLLPartida.BuscarXML(PC, oBEJuego);
                        BEPartidas partida = new BEPartidas();
                        partida = lista.Find(x => x.Jugador.Codigo == PC.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (partida != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            partida.Jugador = PC;
                            partida.Juego = oBEJuego;
                            //Sumo el empate
                            partida.Empates++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(partida);

                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = PC;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Empates++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion

                    }

                    //piedra - tijera
                    if (radioButton3.Checked == true && radioButton6.Checked == true) 
                    {
                        MessageBox.Show("Gana " + jugador3.ToString());
                        #region "Jugador 3 GANA vs pc"
                        List<BEPartidas> lista = new List<BEPartidas>();
                        lista = oBLLPartida.BuscarXML(jugador3, oBEJuego);
                        BEPartidas aux2 = new BEPartidas();
                        aux2 = lista.Find(x => x.Jugador.Codigo == jugador3.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (aux2 != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            aux2.Jugador = jugador3;
                            aux2.Juego = oBEJuego;
                            //Sumo el empate
                            aux2.Ganadas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux2);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador3;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Ganadas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion

                        #region "PC PIERDE"
                       
                        lista = oBLLPartida.BuscarXML(PC, oBEJuego);
                        BEPartidas partida = new BEPartidas();
                        partida = lista.Find(x => x.Jugador.Codigo == PC.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (partida != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            partida.Jugador = PC;
                            partida.Juego = oBEJuego;
                            //Sumo el empate
                            partida.Perdidas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(partida);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = PC;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Perdidas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion
                    }
                    //piedra - papel
                    if (radioButton3.Checked == true && radioButton7.Checked == true) 
                    {
                        MessageBox.Show("Gana la PC");
                        #region "JUGADOR 3 PIERDE vs pc"
                        List<BEPartidas> lista = new List<BEPartidas>();
                        lista = oBLLPartida.BuscarXML(jugador3, oBEJuego);
                        BEPartidas aux2 = new BEPartidas();
                        aux2 = lista.Find(x => x.Jugador.Codigo == jugador3.Codigo && x.Juego.Codigo == oBEJuego.Codigo);

                        //jugador2 Pierde
                        if (aux2 != null)
                        {
                            
                            aux2.Jugador = jugador3;
                            aux2.Juego = oBEJuego;
                            //Sumo
                            aux2.Perdidas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux2);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador  y del juego completos
                            nuevo.Jugador = jugador3;
                            nuevo.Juego = oBEJuego;
                            
                            nuevo.Perdidas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion

                        #region "PC GANA"

                        lista = oBLLPartida.BuscarXML(PC, oBEJuego);
                        BEPartidas partida = new BEPartidas();
                        partida = lista.Find(x => x.Jugador.Codigo == PC.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (partida != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            partida.Jugador = PC;
                            partida.Juego = oBEJuego;
                            //Sumo el empate
                            partida.Ganadas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(partida);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = PC;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Ganadas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion
                    }
                    //papel - piedra
                    if (radioButton4.Checked == true && radioButton8.Checked == true) 
                    { 
                        MessageBox.Show("Gana " + jugador3.ToString());
                        #region "Jugador 3 GANA vs pc"
                        List<BEPartidas> lista = new List<BEPartidas>();
                        lista = oBLLPartida.BuscarXML(jugador3, oBEJuego);
                        BEPartidas aux2 = new BEPartidas();
                        aux2 = lista.Find(x => x.Jugador.Codigo == jugador3.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (aux2 != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            aux2.Jugador = jugador3;
                            aux2.Juego = oBEJuego;
                            //Sumo el empate
                            aux2.Ganadas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux2);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador3;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Ganadas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion

                        #region "PC PIERDE"

                        lista = oBLLPartida.BuscarXML(PC, oBEJuego);
                        BEPartidas partida = new BEPartidas();
                        partida = lista.Find(x => x.Jugador.Codigo == PC.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (partida != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            partida.Jugador = PC;
                            partida.Juego = oBEJuego;
                            //Sumo el empate
                            partida.Perdidas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(partida);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = PC;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Perdidas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion
                    }
                    //papel-tijera
                    if (radioButton4.Checked == true && radioButton6.Checked == true) 
                    {
                        MessageBox.Show("Gana la PC");
                        #region "JUGADOR 3 PIERDE vs pc"
                        List<BEPartidas> lista = new List<BEPartidas>();
                        lista = oBLLPartida.BuscarXML(jugador3, oBEJuego);
                        BEPartidas aux2 = new BEPartidas();
                        aux2 = lista.Find(x => x.Jugador.Codigo == jugador3.Codigo && x.Juego.Codigo == oBEJuego.Codigo);

                        //jugador2 Pierde
                        if (aux2 != null)
                        {

                            aux2.Jugador = jugador3;
                            aux2.Juego = oBEJuego;
                            //Sumo
                            aux2.Perdidas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux2);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador  y del juego completos
                            nuevo.Jugador = jugador3;
                            nuevo.Juego = oBEJuego;

                            nuevo.Perdidas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion

                        #region "PC GANA"

                        lista = oBLLPartida.BuscarXML(PC, oBEJuego);
                        BEPartidas partida = new BEPartidas();
                        partida = lista.Find(x => x.Jugador.Codigo == PC.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (partida != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            partida.Jugador = PC;
                            partida.Juego = oBEJuego;
                            //Sumo el empate
                            partida.Ganadas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(partida);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = PC;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Ganadas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion
                    }
                    //tijera - piedra
                    if (radioButton5.Checked == true && radioButton8.Checked == true) 
                    { 
                        MessageBox.Show("Gana la PC");
                        #region "JUGADOR 3 PIERDE vs pc"
                        List<BEPartidas> lista = new List<BEPartidas>();
                        lista = oBLLPartida.BuscarXML(jugador3, oBEJuego);
                        BEPartidas aux2 = new BEPartidas();
                        aux2 = lista.Find(x => x.Jugador.Codigo == jugador3.Codigo && x.Juego.Codigo == oBEJuego.Codigo);

                        //jugador2 Pierde
                        if (aux2 != null)
                        {

                            aux2.Jugador = jugador3;
                            aux2.Juego = oBEJuego;
                            //Sumo
                            aux2.Perdidas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux2);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador  y del juego completos
                            nuevo.Jugador = jugador3;
                            nuevo.Juego = oBEJuego;

                            nuevo.Perdidas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion

                        #region "PC GANA"

                        lista = oBLLPartida.BuscarXML(PC, oBEJuego);
                        BEPartidas partida = new BEPartidas();
                        partida = lista.Find(x => x.Jugador.Codigo == PC.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (partida != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            partida.Jugador = PC;
                            partida.Juego = oBEJuego;
                            //Sumo el empate
                            partida.Ganadas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(partida);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = PC;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Ganadas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion
                    }
                    //tijera - papel
                    if (radioButton5.Checked == true && radioButton7.Checked == true) 
                    {
                        MessageBox.Show("Gana " + jugador3.ToString());
                        #region "Jugador 3 GANA vs pc"
                        List<BEPartidas> lista = new List<BEPartidas>();
                        lista = oBLLPartida.BuscarXML(jugador3, oBEJuego);
                        BEPartidas aux2 = new BEPartidas();
                        aux2 = lista.Find(x => x.Jugador.Codigo == jugador3.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (aux2 != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            aux2.Jugador = jugador3;
                            aux2.Juego = oBEJuego;
                            //Sumo el empate
                            aux2.Ganadas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(aux2);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = jugador3;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Ganadas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion

                        #region "PC PIERDE"

                        lista = oBLLPartida.BuscarXML(PC, oBEJuego);
                        BEPartidas partida = new BEPartidas();
                        partida = lista.Find(x => x.Jugador.Codigo == PC.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                        if (partida != null)
                        {
                            //Guardo los datos del jugador 1 y del juego completos
                            partida.Jugador = PC;
                            partida.Juego = oBEJuego;
                            //Sumo el empate
                            partida.Perdidas++;
                            //guardo modificacion
                            oBLLPartida.ModificarXML(partida);



                        }
                        else
                        {
                            BEPartidas nuevo = new BEPartidas();
                            //Guardo los datos del jugador 1 y del juego completos
                            nuevo.Jugador = PC;
                            nuevo.Juego = oBEJuego;
                            //Sumo el empate
                            nuevo.Perdidas++;
                            //Registro una partida nueva
                            oBLLPartida.Registrar(nuevo);

                        }
                        #endregion
                    }

                    //Cambio la opcion de la PC luego de jugar
                    var random = new Random();
                    //Genero un numero aleatorio para que la pc elija
                    var aux = random.Next(-2, 2);
                    if (aux > 0) radioButton8.Checked = true;
                    if (aux == 0) radioButton7.Checked = true;
                    if (aux < 0) radioButton6.Checked = true;

                }

            }
            catch (XmlException ex) { MessageBox.Show(ex.Message);}
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

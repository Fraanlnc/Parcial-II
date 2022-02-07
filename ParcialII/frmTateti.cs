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
    public partial class frmTateti : Form
    {
        public frmTateti()
        {
            InitializeComponent();
            oBEJugador = new BEJugador();
            oBEJuego = new BEJuego();
            oBLLJuego = new BLLJuego();
            oBLLJugador = new BLLJugador();
            oBEPartida = new BEPartidas();
            oBLLPartida = new BLLPartidas();
            x = "X";
            o = "O";
            PC = new BEJugador();

        }
        BEJugador oBEJugador;
        BEJuego oBEJuego;
        BLLJugador oBLLJugador;
        BLLJuego oBLLJuego;
        BEPartidas oBEPartida;
        BLLPartidas oBLLPartida;
        BEJugador PC;
        string x;
        string o;

        private void frmTateti_Load(object sender, EventArgs e)
        {
            try
            {
                List<BEJuego> listajuegos = new List<BEJuego>();
                listajuegos = oBLLJuego.ListarTodo();
                oBEJuego = listajuegos.Find(x => x.Codigo == 2);

                PC.Codigo = 99;
                PC.Nombre = "PC";
                //solo se habilitara el volver a jugar una vez terminada la partida
                button10.Enabled = false;
                groupBox1.Enabled = false;
                comboBox1.DataSource = oBLLJugador.ListarTodo();
                comboBox2.DataSource = oBLLJugador.ListarTodo();
                comboBox3.DataSource = oBLLJugador.ListarTodo();

            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


        }

        private void button111_Click(object sender, EventArgs e)
        {
            try
            {
                button111.Enabled = false;
                button10.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                groupBox1.Enabled = true;
                

                if (radioButton1.Checked == true)
                {
                    //JUGADOR VS JUGADOR
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox1.SelectedItem;
                    jugador2 = (BEJugador)comboBox2.SelectedItem;
                    label5.Text = jugador1.ToString();
                }
                else
                {
                    //jugador vs pc

                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox3.SelectedItem;
                    jugador2 = PC;
                    label5.Text = jugador1.ToString();
                }

            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        void CalcularGanador()
        {
            
          
            if (radioButton1.Checked == true)
            {
                //jugador vs jugador
                BEJugador jugador1 = (BEJugador)comboBox1.SelectedItem;
                BEJugador jugador2 = (BEJugador)comboBox2.SelectedItem;

                //si empatan
                if (button1.Enabled == false && button2.Enabled == false && button3.Enabled == false && button4.Enabled == false
                    && button5.Enabled == false && button6.Enabled == false && button7.Enabled == false && button8.Enabled == false && button9.Enabled == false)
                {
                    //empate
                    MessageBox.Show("Empate!");
                    groupBox1.Enabled = false;
                    button111.Enabled = true;


                    #region "JUGADOR 2 EMPATA"
                    //JUGADOR 2
                    //chequear que tenga partidas existentes
                    List<BEPartidas> lista = new List<BEPartidas>();
                    lista = oBLLPartida.BuscarXML(jugador2, oBEJuego);
                    BEPartidas aux = new BEPartidas();
                    aux = lista.Find(x => x.Jugador.Codigo == jugador2.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                    //chequear que haya encontrado una partida
                    if (aux != null)
                    {//Guardo los datos del jugador 2 y del juego completos
                        aux.Jugador = jugador2;
                        aux.Juego = oBEJuego;

                        aux.Empates++;
                        //guardo modificacion
                        oBLLPartida.ModificarXML(aux);
                    }
                    else
                    {
                        BEPartidas nuevo = new BEPartidas();
                        //Guardo los datos del jugador 1 y del juego completos
                        nuevo.Jugador = jugador2;
                        nuevo.Juego = oBEJuego;

                        nuevo.Empates++;
                        //Registro una partida nueva
                        oBLLPartida.Registrar(nuevo);

                    }
                    #endregion

                    #region "JUGADOR 1 EMPATA"
                    lista = oBLLPartida.BuscarXML(jugador1, oBEJuego);
                    aux = lista.Find(x => x.Jugador.Codigo == jugador1.Codigo && x.Juego.Codigo == oBEJuego.Codigo);


                    if (aux != null)
                    {
                        //Guardo los datos del jugador 1 y del juego completos
                        aux.Jugador = jugador1;
                        aux.Juego = oBEJuego;

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

                        nuevo.Empates++;
                        //Registro una partida nueva
                        oBLLPartida.Registrar(nuevo);

                    }

                    #endregion

                    button10.Enabled = true;
                }

                //ganador X horizontales, verticales y diagonales
                if (button1.Text == x && button2.Text == x && button3.Text == x || button4.Text == x && button5.Text == x && button6.Text == x ||
                    button7.Text == x && button8.Text == x && button9.Text == x || button1.Text == x && button4.Text == x && button7.Text == x ||
                    button2.Text == x && button5.Text == x && button8.Text == x || button3.Text == x && button6.Text == x && button9.Text == x ||
                    button1.Text == x && button5.Text == x && button9.Text == x || button3.Text == x && button5.Text == x && button7.Text == x)
                {
                    MessageBox.Show("Ganador: " + jugador1.ToString());
                    groupBox1.Enabled = false;
                    //button111.Enabled = true;

                    
                    #region "JUGADOR 1 GANA"
                    //JUGADOR 1
                    //chequear que tenga partidas existentes
                    List<BEPartidas> lista = new List<BEPartidas>();
                    lista = oBLLPartida.BuscarXML(jugador1, oBEJuego);
                    BEPartidas aux = new BEPartidas();
                    aux = lista.Find(x => x.Jugador.Codigo == jugador1.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                    //chequear que haya encontrado una partida
                    if (aux != null)
                    {//Guardo los datos del jugador 1 y del juego completos
                        aux.Jugador = jugador1;
                        aux.Juego = oBEJuego;
                       
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
                        
                        nuevo.Ganadas++;
                        //Registro una partida nueva
                        oBLLPartida.Registrar(nuevo);

                    }
                    #endregion

                    #region "JUGADOR 2 PIERDE"
                    lista = oBLLPartida.BuscarXML(jugador2, oBEJuego);
                    aux = lista.Find(x => x.Jugador.Codigo == jugador2.Codigo && x.Juego.Codigo == oBEJuego.Codigo);

                   
                    if (aux != null)
                    {
                        //Guardo los datos del jugador 2 y del juego completos
                        aux.Jugador = jugador2;
                        aux.Juego = oBEJuego;
                        
                        aux.Perdidas++;
                        //guardo modificacion
                        oBLLPartida.ModificarXML(aux);



                    }
                    else
                    {
                        BEPartidas nuevo = new BEPartidas();
                        //Guardo los datos del jugador 2 y del juego completos
                        nuevo.Jugador = jugador2;
                        nuevo.Juego = oBEJuego;
                        
                        nuevo.Perdidas++;
                        //Registro una partida nueva
                        oBLLPartida.Registrar(nuevo);

                    }

                    #endregion

                    button10.Enabled = true;

                }
               


                //ganador O horizontales, verticales y diagonales
                if (button1.Text == o && button2.Text == o && button3.Text == o || button4.Text == o && button5.Text == o && button6.Text == o ||
                    button7.Text == o && button8.Text == o && button9.Text == o || button1.Text == o && button4.Text == o && button7.Text == o ||
                    button2.Text == o && button5.Text == o && button8.Text == o || button3.Text == o && button6.Text == o && button9.Text == o ||
                    button1.Text == o && button5.Text == o && button9.Text == o || button3.Text == o && button5.Text == o && button7.Text == o)
                {
                    MessageBox.Show("Ganador: " + jugador2.ToString());
                    groupBox1.Enabled = false;
                    button111.Enabled = true;


                    #region "JUGADOR 2 GANA"
                    //JUGADOR 2
                    //chequear que tenga partidas existentes
                    List<BEPartidas> lista = new List<BEPartidas>();
                    lista = oBLLPartida.BuscarXML(jugador2, oBEJuego);
                    BEPartidas aux = new BEPartidas();
                    aux = lista.Find(x => x.Jugador.Codigo == jugador2.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                    //chequear que haya encontrado una partida
                    if (aux != null)
                    {//Guardo los datos del jugador 2 y del juego completos
                        aux.Jugador = jugador2;
                        aux.Juego = oBEJuego;

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

                        nuevo.Ganadas++;
                        //Registro una partida nueva
                        oBLLPartida.Registrar(nuevo);

                    }
                    #endregion

                    #region "JUGADOR 1 PIERDE"
                    lista = oBLLPartida.BuscarXML(jugador1, oBEJuego);
                    aux = lista.Find(x => x.Jugador.Codigo == jugador1.Codigo && x.Juego.Codigo == oBEJuego.Codigo);


                    if (aux != null)
                    {
                        //Guardo los datos del jugador 1 y del juego completos
                        aux.Jugador = jugador1;
                        aux.Juego = oBEJuego;

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

                        nuevo.Perdidas++;
                        //Registro una partida nueva
                        oBLLPartida.Registrar(nuevo);

                    }

                    #endregion

                    button10.Enabled = true;

                }

               


            }
            else
            {
                //jugador vs pc

                BEJugador jugador1 = (BEJugador)comboBox3.SelectedItem;
                BEJugador jugador2 = PC;

                //si empatan
                if (button1.Enabled == false && button2.Enabled == false && button3.Enabled == false && button4.Enabled == false
                    && button5.Enabled == false && button6.Enabled == false && button7.Enabled == false && button8.Enabled == false && button9.Enabled == false)
                {
                    //empate
                    MessageBox.Show("Empate!");
                    groupBox1.Enabled = false;
                    button111.Enabled = true;


                    #region "JUGADOR 2 EMPATA"
                    //JUGADOR 2
                    //chequear que tenga partidas existentes
                    List<BEPartidas> lista = new List<BEPartidas>();
                    lista = oBLLPartida.BuscarXML(jugador2, oBEJuego);
                    BEPartidas aux = new BEPartidas();
                    aux = lista.Find(x => x.Jugador.Codigo == jugador2.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                    //chequear que haya encontrado una partida
                    if (aux != null)
                    {//Guardo los datos del jugador 2 y del juego completos
                        aux.Jugador = jugador2;
                        aux.Juego = oBEJuego;

                        aux.Empates++;
                        //guardo modificacion
                        oBLLPartida.ModificarXML(aux);
                    }
                    else
                    {
                        BEPartidas nuevo = new BEPartidas();
                        //Guardo los datos del jugador 1 y del juego completos
                        nuevo.Jugador = jugador2;
                        nuevo.Juego = oBEJuego;

                        nuevo.Empates++;
                        //Registro una partida nueva
                        oBLLPartida.Registrar(nuevo);

                    }
                    #endregion

                    #region "JUGADOR 1 EMPATA"
                    lista = oBLLPartida.BuscarXML(jugador1, oBEJuego);
                    aux = lista.Find(x => x.Jugador.Codigo == jugador1.Codigo && x.Juego.Codigo == oBEJuego.Codigo);


                    if (aux != null)
                    {
                        //Guardo los datos del jugador 1 y del juego completos
                        aux.Jugador = jugador1;
                        aux.Juego = oBEJuego;

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

                        nuevo.Empates++;
                        //Registro una partida nueva
                        oBLLPartida.Registrar(nuevo);

                    }

                    #endregion

                    button10.Enabled = true;

                }


                //ganador X horizontales, verticales y diagonales
                if (button1.Text == x && button2.Text == x && button3.Text == x || button4.Text == x && button5.Text == x && button6.Text == x ||
                    button7.Text == x && button8.Text == x && button9.Text == x || button1.Text == x && button4.Text == x && button7.Text == x ||
                    button2.Text == x && button5.Text == x && button8.Text == x || button3.Text == x && button6.Text == x && button9.Text == x ||
                    button1.Text == x && button5.Text == x && button9.Text == x || button3.Text == x && button5.Text == x && button7.Text == x)
                {
                    MessageBox.Show("Ganador: " + jugador1.ToString());
                    groupBox1.Enabled = false;
                    button111.Enabled = true;


                    #region "JUGADOR 1 GANA"
                    //JUGADOR 1
                    //chequear que tenga partidas existentes
                    List<BEPartidas> lista = new List<BEPartidas>();
                    lista = oBLLPartida.BuscarXML(jugador1, oBEJuego);
                    BEPartidas aux = new BEPartidas();
                    aux = lista.Find(x => x.Jugador.Codigo == jugador1.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                    //chequear que haya encontrado una partida
                    if (aux != null)
                    {//Guardo los datos del jugador 1 y del juego completos
                        aux.Jugador = jugador1;
                        aux.Juego = oBEJuego;

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

                        nuevo.Ganadas++;
                        //Registro una partida nueva
                        oBLLPartida.Registrar(nuevo);

                    }
                    #endregion

                    #region "JUGADOR PC"
                    lista = oBLLPartida.BuscarXML(jugador2, oBEJuego);
                    aux = lista.Find(x => x.Jugador.Codigo == jugador2.Codigo && x.Juego.Codigo == oBEJuego.Codigo);


                    if (aux != null)
                    {
                        //Guardo los datos del jugador 2 y del juego completos
                        aux.Jugador = jugador2;
                        aux.Juego = oBEJuego;

                        aux.Perdidas++;
                        //guardo modificacion
                        oBLLPartida.ModificarXML(aux);



                    }
                    else
                    {
                        BEPartidas nuevo = new BEPartidas();
                        //Guardo los datos del jugador 2 y del juego completos
                        nuevo.Jugador = jugador2;
                        nuevo.Juego = oBEJuego;

                        nuevo.Perdidas++;
                        //Registro una partida nueva
                        oBLLPartida.Registrar(nuevo);

                    }

                    #endregion

                    button10.Enabled = true;

                }

                //ganador O horizontales, verticales y diagonales
                if (button1.Text == o && button2.Text == o && button3.Text == o || button4.Text == o && button5.Text == o && button6.Text == o ||
                    button7.Text == o && button8.Text == o && button9.Text == o || button1.Text == o && button4.Text == o && button7.Text == o ||
                    button2.Text == o && button5.Text == o && button8.Text == o || button3.Text == o && button6.Text == o && button9.Text == o ||
                    button1.Text == o && button5.Text == o && button9.Text == o || button3.Text == o && button5.Text == o && button7.Text == o)
                {
                    MessageBox.Show("Ganador: " + jugador2.ToString());
                    groupBox1.Enabled = false;
                    button111.Enabled = true;


                    #region "JUGADOR PC GANA"
                    //JUGADOR 2
                    //chequear que tenga partidas existentes
                    List<BEPartidas> lista = new List<BEPartidas>();
                    lista = oBLLPartida.BuscarXML(jugador2, oBEJuego);
                    BEPartidas aux = new BEPartidas();
                    aux = lista.Find(x => x.Jugador.Codigo == jugador2.Codigo && x.Juego.Codigo == oBEJuego.Codigo);
                    //chequear que haya encontrado una partida
                    if (aux != null)
                    {//Guardo los datos del jugador 2 y del juego completos
                        aux.Jugador = jugador2;
                        aux.Juego = oBEJuego;

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

                        nuevo.Ganadas++;
                        //Registro una partida nueva
                        oBLLPartida.Registrar(nuevo);

                    }
                    #endregion

                    #region "JUGADOR 1 PIERDE"
                    lista = oBLLPartida.BuscarXML(jugador1, oBEJuego);
                    aux = lista.Find(x => x.Jugador.Codigo == jugador1.Codigo && x.Juego.Codigo == oBEJuego.Codigo);


                    if (aux != null)
                    {
                        //Guardo los datos del jugador 1 y del juego completos
                        aux.Jugador = jugador1;
                        aux.Juego = oBEJuego;

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

                        nuevo.Perdidas++;
                        //Registro una partida nueva
                        oBLLPartida.Registrar(nuevo);

                    }

                    #endregion

                    button10.Enabled = true;

                }

              

            }
           
           

           
         
        }

     
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    //jugador vs jugador
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox1.SelectedItem;
                    jugador2 = (BEJugador)comboBox2.SelectedItem;

                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button1.Text = x;
                        label5.Text = jugador2.ToString();

                    }
                    else
                    {
                        //si es el turno del jugador1
                        button1.Text = o;
                        label5.Text = jugador1.ToString();
                    }
                    
                    button1.Enabled = false;
                    CalcularGanador();
                }
                else
                {
                    //jugador vs PC
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox3.SelectedItem;
                    jugador2 =PC;

               

                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button1.Text = x;
                        label5.Text = jugador2.ToString();
                        
                        button1.Enabled = false;
                        CalcularGanador();
                    }
                    else
                    {
                        JugadaPC(rdm.Next(1, 9));
                        label5.Text = jugador1.ToString();
                        CalcularGanador();
                    }

                   

                }
              
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    //jugador vs jugador
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox1.SelectedItem;
                    jugador2 = (BEJugador)comboBox2.SelectedItem;

                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button2.Text = x;
                        label5.Text = jugador2.ToString();

                    }
                    else
                    {
                        //si es el turno del jugador1
                        button2.Text = o;
                        label5.Text = jugador1.ToString();
                    }
                   
                    button2.Enabled = false;
                    CalcularGanador();
                }
                else
                {
                    //jugador vs PC
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox3.SelectedItem;
                    jugador2 = PC;



                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button2.Text = x;
                        label5.Text = jugador2.ToString();
                        
                        button2.Enabled = false;
                        CalcularGanador();

                    }
                    else
                    {
                        JugadaPC(rdm.Next(1, 9));
                        label5.Text = jugador1.ToString();
                        CalcularGanador();
                    }



                }

            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    //jugador vs jugador
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox1.SelectedItem;
                    jugador2 = (BEJugador)comboBox2.SelectedItem;

                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button3.Text = x;
                        label5.Text = jugador2.ToString();

                    }
                    else
                    {
                        //si es el turno del jugador1
                        button3.Text = o;
                        label5.Text = jugador1.ToString();
                    }
                 
                    button3.Enabled = false;
                    CalcularGanador();
                }
                else
                {
                    //jugador vs PC
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox3.SelectedItem;
                    jugador2 = PC;



                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button3.Text = x;
                        label5.Text = jugador2.ToString();
                      
                        button3.Enabled = false;
                        CalcularGanador();

                    }
                    else
                    {
                        JugadaPC(rdm.Next(1, 9));
                        label5.Text = jugador1.ToString();
                        CalcularGanador();
                    }



                }

            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    //jugador vs jugador
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox1.SelectedItem;
                    jugador2 = (BEJugador)comboBox2.SelectedItem;

                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button4.Text = x;
                        label5.Text = jugador2.ToString();

                    }
                    else
                    {
                        //si es el turno del jugador1
                        button4.Text = o;
                        label5.Text = jugador1.ToString();
                    }
                   
                    button4.Enabled = false;
                    CalcularGanador();
                }
                else
                {
                    //jugador vs PC
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox3.SelectedItem;
                    jugador2 = PC;



                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button4.Text = x;
                        label5.Text = jugador2.ToString();
                       
                        button4.Enabled = false;
                        CalcularGanador();

                    }
                    else
                    {
                        JugadaPC(rdm.Next(1, 9));
                        label5.Text = jugador1.ToString();
                        CalcularGanador();
                    }



                }

            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

            private void button5_Click(object sender, EventArgs e)
            {
            try
            {
                if (radioButton1.Checked == true)
                {
                    //jugador vs jugador
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox1.SelectedItem;
                    jugador2 = (BEJugador)comboBox2.SelectedItem;

                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button5.Text = x;
                        label5.Text = jugador2.ToString();

                    }
                    else
                    {
                        //si es el turno del jugador1
                        button5.Text = o;
                        label5.Text = jugador1.ToString();
                    }
                  
                    button5.Enabled = false;
                    CalcularGanador();
                }
                else
                {
                    //jugador vs PC
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox3.SelectedItem;
                    jugador2 = PC;



                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button5.Text = x;
                        label5.Text = jugador2.ToString();
              
                        button5.Enabled = false;
                        CalcularGanador();

                    }
                    else
                    {
                        JugadaPC(rdm.Next(1, 9));
                        label5.Text = jugador1.ToString();
                        CalcularGanador();
                    }



                }

            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    //jugador vs jugador
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox1.SelectedItem;
                    jugador2 = (BEJugador)comboBox2.SelectedItem;

                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button6.Text = x;
                        label5.Text = jugador2.ToString();

                    }
                    else
                    {
                        //si es el turno del jugador1
                        button6.Text = o;
                        label5.Text = jugador1.ToString();
                    }
                  
                    button6.Enabled = false;
                    CalcularGanador();
                }
                else
                {
                    //jugador vs PC
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox3.SelectedItem;
                    jugador2 = PC;



                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button6.Text = x;
                        label5.Text = jugador2.ToString();
                       
                        button6.Enabled = false;
                        CalcularGanador();

                    }
                    else
                    {
                        JugadaPC(rdm.Next(1, 9));
                        label5.Text = jugador1.ToString();
                        CalcularGanador();
                    }



                }

            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    //jugador vs jugador
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox1.SelectedItem;
                    jugador2 = (BEJugador)comboBox2.SelectedItem;

                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button7.Text = x;
                        label5.Text = jugador2.ToString();

                    }
                    else
                    {
                        //si es el turno del jugador1
                        button7.Text = o;
                        label5.Text = jugador1.ToString();
                    }
                 
                    button7.Enabled = false;
                    CalcularGanador();
                }
                else
                {
                    //jugador vs PC
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox3.SelectedItem;
                    jugador2 = PC;



                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button7.Text = x;
                        label5.Text = jugador2.ToString();
                        
                        button7.Enabled = false;
                        CalcularGanador();

                    }
                    else
                    {
                        JugadaPC(rdm.Next(1, 9));
                        label5.Text = jugador1.ToString();
                        CalcularGanador();
                    }



                }

            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    //jugador vs jugador
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox1.SelectedItem;
                    jugador2 = (BEJugador)comboBox2.SelectedItem;

                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button8.Text = x;
                        label5.Text = jugador2.ToString();

                    }
                    else
                    {
                        //si es el turno del jugador1
                        button8.Text = o;
                        label5.Text = jugador1.ToString();
                    }
                   
                    button8.Enabled = false;
                    CalcularGanador();
                }
                else
                {
                    //jugador vs PC
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox3.SelectedItem;
                    jugador2 = PC;



                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button8.Text = x;
                        label5.Text = jugador2.ToString();
                     
                        button8.Enabled = false;
                        CalcularGanador();

                    }
                    else
                    {
                        JugadaPC(rdm.Next(1, 9));
                        label5.Text = jugador1.ToString();
                        CalcularGanador();
                    }



                }

            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    //jugador vs jugador
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox1.SelectedItem;
                    jugador2 = (BEJugador)comboBox2.SelectedItem;

                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button9.Text = x;
                        label5.Text = jugador2.ToString();

                    }
                    else
                    {
                        //si es el turno del jugador1
                        button9.Text = o;
                        label5.Text = jugador1.ToString();
                    }
                  
                    button9.Enabled = false;
                    CalcularGanador();
                }
                else
                {
                    //jugador vs PC
                    BEJugador jugador1 = new BEJugador();
                    BEJugador jugador2 = new BEJugador();
                    jugador1 = (BEJugador)comboBox3.SelectedItem;
                    jugador2 = PC;



                    if (label5.Text == jugador1.ToString())
                    {
                        //si es el turno del jugador1
                        button9.Text = x;
                        label5.Text = jugador2.ToString();
                        
                        button9.Enabled = false;
                        CalcularGanador();

                    }
                    else
                    {
                        JugadaPC(rdm.Next(1, 9));
                        label5.Text = jugador1.ToString();
                        CalcularGanador();
                    }



                }

            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        Random rdm = new Random();
       
        void JugadaPC(int random)
        {
            //int random=rdm.Next(1, 9);
            if (random == 1)
            {
                if (button1.Enabled == true)
                {
                    button1.Text = o;
                    button1.Enabled = false;
                }
                else
                {
                    JugadaPC(rdm.Next(1, 9));
                }
            }

            if (random == 2)
            {
                if (button2.Enabled == true)
                {
                    button2.Text = o;
                    button2.Enabled = false;
                }
                else
                {
                    JugadaPC(rdm.Next(1, 9));
                }
            }

            if (random == 3)
            {
                if (button3.Enabled == true)
                {
                    button3.Text = o;
                    button3.Enabled = false;
                }
                else
                {
                    JugadaPC(rdm.Next(1, 9));
                }
            }

            if (random == 4)
            {
                if (button4.Enabled == true)
                {
                    button4.Text = o;
                    button4.Enabled = false;
                }
                else
                {
                    JugadaPC(rdm.Next(1, 9));
                }
            }

            if (random == 5)
            {
                if (button5.Enabled == true)
                {
                    button5.Text = o;
                    button5.Enabled = false;
                }
                else
                {
                    JugadaPC(rdm.Next(1, 9));
                }
            }

            if (random == 6)
            {
                if (button6.Enabled == true)
                {
                    button6.Text = o;
                    button6.Enabled = false;
                }
                else
                {
                    JugadaPC(rdm.Next(1, 9));
                }
            }

            if (random == 7)
            {
                if (button7.Enabled == true)
                {
                    button7.Text = o;
                    button7.Enabled = false;
                }
                else
                {
                    JugadaPC(rdm.Next(1, 9));
                }
            }

            if (random == 8)
            {
                if (button8.Enabled == true)
                {
                    button8.Text = o;
                    button8.Enabled = false;
                }
                else
                {
                    JugadaPC(rdm.Next(1, 9));
                }
            }

            if (random == 9)
            {
                if (button9.Enabled == true)
                {
                    button9.Text = o;
                    button9.Enabled = false;
                }
                else
                {
                    JugadaPC(rdm.Next(1, 9));
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                button111.Enabled = true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                groupBox1.Enabled = false;

                label5.Text = "";
                button1.Enabled = true;
                button1.Text = "";
                button2.Enabled = true;
                button2.Text = "";
                button3.Enabled = true;
                button3.Text = "";
                button4.Enabled = true;
                button4.Text = "";
                button5.Enabled = true;
                button5.Text = "";
                button6.Enabled = true;
                button6.Text = "";
                button7.Enabled = true;
                button7.Text = "";
                button8.Enabled = true;
                button8.Text = "";
                button9.Enabled = true;
                button9.Text = "";


            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using BE;
using BLL;

namespace ParcialII
{
    public partial class frmJugadores : Form
    {
        public frmJugadores()
        {
            InitializeComponent();
            oBEJugador = new BEJugador();
            oBLLJugador = new BLLJugador();
            oBLLPartida = new BLLPartidas();
            oBLLJuego = new BLLJuego();
        }
        BEJugador oBEJugador;
        BLLJugador oBLLJugador;
        BLLPartidas oBLLPartida;
        BLLJuego oBLLJuego;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length > 0)
                {
                    //El codigo solo admite numeros
                    bool codigo = Regex.IsMatch(textBox1.Text, "^([0-9]+$)");
                    if (codigo == false)
                    {
                        //codigo mal ingresado
                        MessageBox.Show("El codigo solo admite numeros");
                    }
                    else
                    {
                        //codigo bien ingresado, chequeo nombre:

                        if (textBox2.Text.Length > 0)
                        {
                            //el nombre admite letras y espacios
                            bool nombre = Regex.IsMatch(textBox2.Text, "^([\\sa-zA-Z]+$)");
                            if (nombre == false)
                            {
                                //nombre mal ingresado
                                MessageBox.Show("El nombre solo admite letras y espacios");
                            }
                            else
                            {
                                if (textBox3.Text.Length > 0)
                                {
                                    //el apellido admite letras y espacios
                                    bool apellido = Regex.IsMatch(textBox3.Text, "^([\\sa-zA-Z]+$)");
                                    if (apellido == false)
                                    {
                                        //apellido mal ingresado
                                        MessageBox.Show("El apellido solo admite letras y espacios");
                                    }
                                    else
                                    {
                                        if (textBox4.Text.Length > 0)
                                        {
                                            //el dni solo admite numeros y hasta 8 digitos
                                            bool dni = Regex.IsMatch(textBox4.Text, "^[0-9]{8}$");
                                            if (dni == false)
                                            {
                                                //dni mal ingresado
                                                MessageBox.Show("El DNI solo admite numeros, hasta 8 digitos");
                                            }
                                            else
                                            {
                                                if (textBox5.Text.Length > 0)
                                                {
                                                    //formato de mail ---@----.com/.ar
                                                    bool mail = Regex.IsMatch(textBox5.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                                                    if (mail == false)
                                                    {
                                                        MessageBox.Show("El mail ingresado no es correcto");

                                                    }
                                                    else
                                                    {
                                                        if (textBox6.Text.Length > 0)
                                                        {
                                                            //validar fecha de nac? formato
                                                            bool fechanac = Regex.IsMatch(textBox6.Text, "^([0][1-9]|[12][0-9]|3[01])(\\/|-)([0][1-9]|[1][0-2])\\2(\\d{4})$");
                                                            if (fechanac == false)
                                                            {
                                                                MessageBox.Show("El formato de fecha ingresado no es correcto");
                                                            }
                                                            else
                                                            {
                                                                if (textBox7.Text.Length > 0)
                                                                {
                                                                    bool localidad = Regex.IsMatch(textBox3.Text, "^([a-zA-Z\\s]+$)");

                                                                    if (localidad == false)
                                                                    {
                                                                        MessageBox.Show("La localidad solo admite letras");
                                                                    }
                                                                    else
                                                                    {
                                                                        //validaciones hechas
                                                                        oBEJugador.Codigo = Convert.ToInt32(textBox1.Text);
                                                                        oBEJugador.Nombre = textBox2.Text;
                                                                        oBEJugador.Apellido = textBox3.Text;
                                                                        oBEJugador.DNI = Convert.ToInt32(textBox4.Text);
                                                                        oBEJugador.Mail = textBox5.Text;
                                                                        oBEJugador.FechaNacimiento = Convert.ToDateTime(textBox6.Text);
                                                                        oBEJugador.Localidad = textBox7.Text;



                                                                        //Si la lista tiene elementos es porque el jugador ya existe
                                                                        if (oBLLJugador.BuscarXML(oBEJugador).Count > 0)
                                                                        {
                                                                            MessageBox.Show("El DNI de jugador ingresado ya existe");
                                                                        }
                                                                        else
                                                                        {
                                                                            oBLLJugador.Registrar(oBEJugador);

                                                                            Limpiar();
                                                                        }
                                                                    }
                                                                }
                                                                else { MessageBox.Show("Complete el campo Localidad"); }

                                                            }

                                                        }
                                                        else { MessageBox.Show("Complete el campo Fecha de nacimiento"); }

                                                    }
                                                }
                                                else { MessageBox.Show("Complete el campo Mail"); }
                                            }
                                        }
                                        else { MessageBox.Show("Complete el campo DNI"); }
                                    }
                                }
                                else { MessageBox.Show("Complete el campo Apellido"); }
                            }
                        }
                        else { MessageBox.Show("Complete el campo nombre"); }
                    }
                }
                else { MessageBox.Show("Complete el campo Codigo");}

              
               
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message);}
            catch (Exception ex) { MessageBox.Show(ex.Message);}
            
        }

        void Limpiar()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";
            textBox6.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
               dataGridView1.DataSource=  oBLLJugador.ListarTodo();
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message);}
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void frmJugadores_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message);}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                BEJugador aux = new BEJugador();
                aux = dataGridView1.SelectedRows[0].DataBoundItem as BEJugador;
                textBox1.Text = aux.Codigo.ToString();
                textBox2.Text = aux.Nombre;
                textBox3.Text = aux.Apellido;
                textBox4.Text = aux.DNI.ToString();
                textBox5.Text = aux.Mail;
                textBox7.Text = aux.Localidad;
                textBox6.Text = aux.FechaNacimiento.ToString();
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length > 0)
                {
                    //El codigo solo admite numeros
                    bool codigo = Regex.IsMatch(textBox1.Text, "^([0-9]+$)");
                    if (codigo == false)
                    {
                        //codigo mal ingresado
                        MessageBox.Show("El codigo solo admite numeros");
                    }
                    else
                    {
                        //codigo bien ingresado, chequeo nombre:

                        if (textBox2.Text.Length > 0)
                        {
                            //el nombre admite letras y espacios
                            bool nombre = Regex.IsMatch(textBox2.Text, "^([\\sa-zA-Z]+$)");
                            if (nombre == false)
                            {
                                //nombre mal ingresado
                                MessageBox.Show("El nombre solo admite letras y espacios");
                            }
                            else
                            {
                                if (textBox3.Text.Length > 0)
                                {
                                    //el apellido admite letras y espacios
                                    bool apellido = Regex.IsMatch(textBox3.Text, "^([\\sa-zA-Z]+$)");
                                    if (apellido == false)
                                    {
                                        //apellido mal ingresado
                                        MessageBox.Show("El apellido solo admite letras y espacios");
                                    }
                                    else
                                    {
                                        if (textBox4.Text.Length > 0)
                                        {
                                            //el dni solo admite numeros y hasta 8 digitos
                                            bool dni = Regex.IsMatch(textBox4.Text, "^[0-9]{8}$");
                                            if (dni == false)
                                            {
                                                //dni mal ingresado
                                                MessageBox.Show("El DNI solo admite numeros, hasta 8 digitos");
                                            }
                                            else
                                            {
                                                if (textBox5.Text.Length > 0)
                                                {
                                                    //formato de mail ---@----.com/.ar
                                                    bool mail = Regex.IsMatch(textBox5.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                                                    if (mail == false)
                                                    {
                                                        MessageBox.Show("El mail ingresado no es correcto");

                                                    }
                                                    else
                                                    {
                                                        if (textBox6.Text.Length > 0)
                                                        {
                                                            //validar fecha de nac? formato
                                                            bool fechanac = Regex.IsMatch(textBox6.Text, "^([0][1-9]|[12][0-9]|3[01])(\\/|-)([0][1-9]|[1][0-2])\\2(\\d{4})$");
                                                            if (fechanac == false)
                                                            {
                                                                MessageBox.Show("El formato de fecha ingresado no es correcto");
                                                            }
                                                            else
                                                            {
                                                                if (textBox7.Text.Length > 0)
                                                                {
                                                                    bool localidad = Regex.IsMatch(textBox3.Text, "^([a-zA-Z\\s]+$)");

                                                                    if (localidad == false)
                                                                    {
                                                                        MessageBox.Show("La localidad solo admite letras y espacios en blanco");
                                                                    }
                                                                    else
                                                                    {
                                                                        //validaciones hechas
                                                                        BEJugador aux = new BEJugador();
                                                                        aux.Codigo = Convert.ToInt32(textBox1.Text);
                                                                        aux.Nombre = textBox2.Text;
                                                                        aux.Apellido = textBox3.Text;
                                                                        aux.DNI = Convert.ToInt32(textBox4.Text);
                                                                        aux.Mail = textBox5.Text;
                                                                        aux.FechaNacimiento = Convert.ToDateTime(textBox6.Text);
                                                                        aux.Localidad = textBox7.Text;

                                                                      



                                                                        //Si la lista tiene elementos es porque el jugador ya existe
                                                                        if (oBLLJugador.BuscarXML(aux).Count > 0)
                                                                        {
                                                                            MessageBox.Show("El DNI de jugador ingresado ya existe");
                                                                        }
                                                                        else
                                                                        {
                                                                            oBLLJugador.Modificar(aux);
                                                                            

                                                                            Limpiar();
                                                                        }
                                                                    }
                                                                }
                                                                else { MessageBox.Show("Complete el campo Localidad"); }

                                                            }

                                                        }
                                                        else { MessageBox.Show("Complete el campo Fecha de nacimiento"); }

                                                    }
                                                }
                                                else { MessageBox.Show("Complete el campo Mail"); }
                                            }
                                        }
                                        else { MessageBox.Show("Complete el campo DNI"); }
                                    }
                                }
                                else { MessageBox.Show("Complete el campo Apellido"); }
                            }
                        }
                        else { MessageBox.Show("Complete el campo nombre"); }
                    }
                }
                else { MessageBox.Show("Complete el campo Codigo"); }





               

            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                BEJugador aux = new BEJugador();
                aux = dataGridView1.SelectedRows[0].DataBoundItem as BEJugador;
                //al eliminar el jugador, se elimina el historial de partidas
                oBLLJugador.Eliminar(aux);
                BEPartidas partida = new BEPartidas();
                partida.Jugador = aux;
                //Eliminar historial juego 1 (piedra papel tijera)
                List<BEJuego> listajuegos = new List<BEJuego>();
                BEJuego oBEJuego = new BEJuego();
                listajuegos = oBLLJuego.ListarTodo();
                oBEJuego = listajuegos.Find(x => x.Codigo == 1);
                List<BEPartidas> listapartidas = new List<BEPartidas>();
                List<BEPartidas> listapartidasdeljugador = new List<BEPartidas>();
                listapartidas = oBLLPartida.ListarTodo();
                foreach (BEPartidas p in listapartidas)
                {
                    if (p.Jugador.Codigo == aux.Codigo)
                    {
                        listapartidasdeljugador.Add(p);
                    }

                }
                int i = 1;

                int cantidad = listapartidasdeljugador.Count;
                
                for (i = 1; i <= cantidad; i++)
                {
                    
                    oBLLPartida.EliminarXML(partida);
                }
               

            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}

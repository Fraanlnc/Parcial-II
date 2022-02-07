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
    public partial class frmInforme : Form
    {
        public frmInforme()
        {
            InitializeComponent();
            oBEJuego = new BEJuego();
            oBLLJuego = new BLLJuego();
            oBEJugador = new BEJugador();
            oBLLJugador = new BLLJugador();
            oBLLPartidas = new BLLPartidas();
        }
        BEJugador oBEJugador;
        BLLJugador oBLLJugador;
        BEJuego oBEJuego;
        BLLJuego oBLLJuego;
        BLLPartidas oBLLPartidas;

        private void frmInforme_Load(object sender, EventArgs e)
        {
            try
            {
                #region "JUEGO MAS JUGADO"

                List<BEPartidas> listapartidas = new List<BEPartidas>();
                //me traigo todas las partidas hechas
                listapartidas = oBLLPartidas.ListarTodo();

                BEPartidas partidasjuego1 = new BEPartidas();
                BEPartidas partidasjuego2 = new BEPartidas();
                //piedra papel o tijera
                //partidasjuego1.Juego.Codigo = 1;
                //tateti
                //partidasjuego2.Juego.Codigo = 2;

                foreach (BEPartidas p in listapartidas)
                {
                    if (p.Juego.Codigo == 1)
                    {
                        partidasjuego1.Juego=p.Juego;
                        //se acumulan las partidas ganadas empatadas y perdidas por juego
                        partidasjuego1.Ganadas = partidasjuego1.Ganadas + p.Ganadas;
                        partidasjuego1.Empates = partidasjuego1.Empates + p.Empates;
                        partidasjuego1.Perdidas = partidasjuego1.Perdidas + p.Perdidas;
                    }

                    if (p.Juego.Codigo == 2)
                    {
                        partidasjuego2.Juego = p.Juego;
                        //se acumulan las partidas ganadas empatadas y perdidas por juego
                        partidasjuego2.Ganadas = partidasjuego2.Ganadas + p.Ganadas;
                        partidasjuego2.Empates = partidasjuego2.Empates + p.Empates;
                        partidasjuego2.Perdidas = partidasjuego2.Perdidas + p.Perdidas;
                    }

                }

                int sumadorjuego1,sumadorjuego2 = 0;
                //sumo todas las partidas jugadas
                sumadorjuego1 = partidasjuego1.Ganadas + partidasjuego1.Empates + partidasjuego1.Perdidas;
                sumadorjuego2 = partidasjuego2.Ganadas + partidasjuego2.Empates + partidasjuego2.Perdidas;

                if (sumadorjuego1 > sumadorjuego2)
                {
                    MessageBox.Show("El juego 1 es el mas jugado");
                    label8.Text = "PIEDRA PAPEL O TIJERA";
                }
                else
                {

                    
                    
                        MessageBox.Show("El juego 2 es el mas jugado");
                    label8.Text = "TATETI";
                }

                #endregion


                #region "JUEGO 1 GANADAS"
                //juego 1 ganadas
                
                DataSet DS = new DataSet();
                DS.ReadXml("Partidas.XML");
                
                DataSet juego2 = new DataSet();
                
               
                DataSet juego1 = new DataSet("JUEGO1");
                DataTable Tabla1 = juego1.Tables.Add("Juego 1");

                DataTable dtjuego1 = new DataTable();
                DataTable dtjuego2 = new DataTable();

                dtjuego1 = DS.Tables[0].Clone();
                dtjuego2 = DS.Tables[0].Clone();


                foreach (DataRow p in DS.Tables[0].Rows)
                {

                    if ((string)p["Codigo_Juego"] == 1.ToString())
                    {
                        
                        dtjuego1.ImportRow(p);
                    }
                    else
                    {
                        dtjuego2.ImportRow(p);
                    }
                          

                }

                chart1.DataSource = dtjuego1;
                
                    chart1.Series[0].XValueMember = "Codigo_Jugador";
                    chart1.Series[0].YValueMembers = "Ganadas";
                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                    chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                    chart1.DataBind();

                //juego 2 ganadas

                chart5.DataSource = dtjuego2;

                chart5.Series[0].XValueMember = "Codigo_Jugador";
                chart5.Series[0].YValueMembers = "Ganadas";
                chart5.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                chart5.ChartAreas[0].Area3DStyle.Enable3D = true;
                chart5.DataBind();

                #endregion

                #region "JUEGO 1 Perdidas"
                //juego 1 perdidas

                DataSet DSperdidas = new DataSet();
                DSperdidas.ReadXml("Partidas.XML");

                DataSet juego2perdidas = new DataSet();


                DataSet juego1perdidas = new DataSet("JUEGO1");
                DataTable Tabla1perdidas = juego1perdidas.Tables.Add("Juego 1");

                DataTable dtjuego1perdidas = new DataTable();
                DataTable dtjuego2perdidas = new DataTable();

                dtjuego1perdidas = DSperdidas.Tables[0].Clone();
                dtjuego2perdidas = DSperdidas.Tables[0].Clone();


                foreach (DataRow p in DSperdidas.Tables[0].Rows)
                {

                    if ((string)p["Codigo_Juego"] == 1.ToString())
                    {

                        dtjuego1perdidas.ImportRow(p);
                    }
                    else
                    {
                        dtjuego2perdidas.ImportRow(p);
                    }


                }

                chart2.DataSource = dtjuego1perdidas;

                chart2.Series[0].XValueMember = "Codigo_Jugador";
                chart2.Series[0].YValueMembers = "Perdidas";
                chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                chart2.ChartAreas[0].Area3DStyle.Enable3D = true;
                chart2.DataBind();

                //juego 2 perdidas
                chart4.DataSource = dtjuego2perdidas;
                chart4.Series[0].XValueMember = "Codigo_Jugador";
                chart4.Series[0].YValueMembers = "Perdidas";
                chart4.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                chart4.ChartAreas[0].Area3DStyle.Enable3D = true;
                chart4.DataBind();




                #endregion

                #region "JUEGO 1 empates"
                //juego 1 empates

                DataSet DSempates= new DataSet();
                DSempates.ReadXml("Partidas.XML");

                DataSet juego2empates = new DataSet();


                DataSet juego1empates = new DataSet("JUEGO1");
                DataTable Tabla1empates= juego1empates.Tables.Add("Juego 1");

                DataTable dtjuego1empates = new DataTable();
                DataTable dtjuego2empates= new DataTable();

                dtjuego1empates = DSempates.Tables[0].Clone();
                dtjuego2empates = DSempates.Tables[0].Clone();


                foreach (DataRow p in DSempates.Tables[0].Rows)
                {

                    if ((string)p["Codigo_Juego"] == 1.ToString())
                    {

                        dtjuego1empates.ImportRow(p);
                    }
                    else
                    {
                        dtjuego2empates.ImportRow(p);
                    }


                }

                chart3.DataSource = dtjuego1empates;

                chart3.Series[0].XValueMember = "Codigo_Jugador";
                chart3.Series[0].YValueMembers = "Empates";
                chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                chart3.ChartAreas[0].Area3DStyle.Enable3D = true;
                chart3.DataBind();
                //juego 2 empates

                chart6.DataSource = dtjuego2empates;

                chart6.Series[0].XValueMember = "Codigo_Jugador";
                chart6.Series[0].YValueMembers = "Empates";
                chart6.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                chart6.ChartAreas[0].Area3DStyle.Enable3D = true;
                chart6.DataBind();



                #endregion


            }
            catch (XmlException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

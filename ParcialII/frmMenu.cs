using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParcialII
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void jugadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmJugadores formJug = new frmJugadores();
            formJug.MdiParent = this;
            formJug.Show();
        }

        private void piedraPapelTijeraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPiedraPapelTijera formpiedra = new frmPiedraPapelTijera();
            formpiedra.MdiParent = this;
            formpiedra.Show();
        }

        private void tatetiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTateti formtateti = new frmTateti();
            formtateti.MdiParent = this;
            formtateti.Show();

        }

        private void juegoMasJugadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInforme forminforme = new frmInforme();
            forminforme.MdiParent = this;
            forminforme.Show();
        }
    }
}

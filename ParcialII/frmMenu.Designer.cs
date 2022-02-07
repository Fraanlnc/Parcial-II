
namespace ParcialII
{
    partial class frmMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jugadoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.juegosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.piedraPapelTijeraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tatetiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.juegoMasJugadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.gestionToolStripMenuItem,
            this.juegosToolStripMenuItem,
            this.informesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1117, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(121, 26);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // gestionToolStripMenuItem
            // 
            this.gestionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jugadoresToolStripMenuItem});
            this.gestionToolStripMenuItem.Name = "gestionToolStripMenuItem";
            this.gestionToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.gestionToolStripMenuItem.Text = "Gestion";
            // 
            // jugadoresToolStripMenuItem
            // 
            this.jugadoresToolStripMenuItem.Name = "jugadoresToolStripMenuItem";
            this.jugadoresToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.jugadoresToolStripMenuItem.Text = "Jugadores";
            this.jugadoresToolStripMenuItem.Click += new System.EventHandler(this.jugadoresToolStripMenuItem_Click);
            // 
            // juegosToolStripMenuItem
            // 
            this.juegosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.piedraPapelTijeraToolStripMenuItem,
            this.tatetiToolStripMenuItem});
            this.juegosToolStripMenuItem.Name = "juegosToolStripMenuItem";
            this.juegosToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.juegosToolStripMenuItem.Text = "Juegos";
            // 
            // piedraPapelTijeraToolStripMenuItem
            // 
            this.piedraPapelTijeraToolStripMenuItem.Name = "piedraPapelTijeraToolStripMenuItem";
            this.piedraPapelTijeraToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.piedraPapelTijeraToolStripMenuItem.Text = "Piedra-Papel-Tijera";
            this.piedraPapelTijeraToolStripMenuItem.Click += new System.EventHandler(this.piedraPapelTijeraToolStripMenuItem_Click);
            // 
            // tatetiToolStripMenuItem
            // 
            this.tatetiToolStripMenuItem.Name = "tatetiToolStripMenuItem";
            this.tatetiToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.tatetiToolStripMenuItem.Text = "Tateti";
            this.tatetiToolStripMenuItem.Click += new System.EventHandler(this.tatetiToolStripMenuItem_Click);
            // 
            // informesToolStripMenuItem
            // 
            this.informesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.juegoMasJugadoToolStripMenuItem});
            this.informesToolStripMenuItem.Name = "informesToolStripMenuItem";
            this.informesToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.informesToolStripMenuItem.Text = "Informes";
            // 
            // juegoMasJugadoToolStripMenuItem
            // 
            this.juegoMasJugadoToolStripMenuItem.Name = "juegoMasJugadoToolStripMenuItem";
            this.juegoMasJugadoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.juegoMasJugadoToolStripMenuItem.Text = "Informe";
            this.juegoMasJugadoToolStripMenuItem.Click += new System.EventHandler(this.juegoMasJugadoToolStripMenuItem_Click);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 553);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMenu";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jugadoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem juegosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem piedraPapelTijeraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tatetiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem juegoMasJugadoToolStripMenuItem;
    }
}


using RegistroDetalle2._0.UI.Consulta;
using RegistroDetalle2._0.UI.Registro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroDetalle2._0
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void PersonastoolStripButton_Click(object sender, EventArgs e)
        {
            rPersonas registro = new rPersonas();
            registro.MdiParent = this;
            registro.Show();
        }

        private void personasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rPersonas registro = new rPersonas();
            registro.MdiParent = this;
            registro.Show();
        }

        private void ArticulostoolStripButton_Click(object sender, EventArgs e)
        {
            rArticulos registro = new rArticulos();
            registro.MdiParent = this;
            registro.Show();
        }

        private void ArticulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rArticulos registro = new rArticulos();
            registro.MdiParent = this;
            registro.Show();
        }

        private void CotizacionestoolStripButton_Click(object sender, EventArgs e)
        {
            rCotizaciones registro = new rCotizaciones();
            registro.MdiParent = this;
            registro.Show();
        }

        private void CotizacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rCotizaciones registro = new rCotizaciones();
            registro.MdiParent = this;
            registro.Show();
        }

        private void PersonasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cPersonas consulta = new cPersonas();
            consulta.MdiParent = this;
            consulta.Show();
        }

        private void ArticulosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cArticulos consulta = new cArticulos();
            consulta.MdiParent = this;
            consulta.Show();
        }
    }
}

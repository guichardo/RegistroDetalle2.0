using RegistroDetalle2.DAL;
using RegistroDetalle2.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroDetalle2._0.UI.Registro
{
    public partial class rCotizaciones : Form
    {
        public rCotizaciones()
        {
            InitializeComponent();
            LlenarComboBox();
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(IdnumericUpDown.Value);
            Cotizaciones Cotizacion = BLL.CotizacionesBLL.Buscar(id);

            if (Cotizacion != null)
            {
                LlenarCampos(Cotizacion);
            }
            else
                MessageBox.Show("No se encontro!", "Fallo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            IdnumericUpDown.Value = 0;
            fechaDateTimePicker.Value = DateTime.Now;
            ObservacionestextBox.Clear();
            CantidadtextBox.Clear();
            PreciotextBox.Clear();
            ImportetextBox.Clear();
            TotalnumericUpDown.Value = 0;

            DetalledataGridView.DataSource = null;
            MyerrorProvider.Clear();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Cotizaciones cotizacion;
            bool Paso = false;

            if (HayErrores())
            {
                MessageBox.Show("Favor revisar todos los campos", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cotizacion = LlenaClase();

            //Determinar si es Guardar o Modificar
            if (IdnumericUpDown.Value == 0)
                Paso = BLL.CotizacionesBLL.Guardar(cotizacion);
            else
                //todo: validar que exista.
                Paso = BLL.CotizacionesBLL.Modificar(cotizacion);

            //Informar el resultado
            if (Paso)
            {
                NuevoButton.PerformClick();
                MessageBox.Show("Guardado!!", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("No se pudo guardar!!", "Fallo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(IdnumericUpDown.Value);

            //todo: validar que exista
            if (BLL.CotizacionesBLL.Eliminar(id))
                MessageBox.Show("Eliminado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo eliminar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Agregarbutton_Click(object sender, EventArgs e)
        {
            List<CotizacionesDetalle> detalle = new List<CotizacionesDetalle>();

            if (DetalledataGridView.DataSource != null)
            {
                detalle = (List<CotizacionesDetalle>)DetalledataGridView.DataSource;
            }

            //Agregar un nuevo detalle con los datos introducidos.
            detalle.Add(
                new CotizacionesDetalle(
                    id: 0,
                    cotizacioId: (int)IdnumericUpDown.Value,
                    personaId: (int)PersonacomboBox.SelectedValue,
                    articuloId: (int)ArticulocomboBox.SelectedValue,
                    cantidad: (int)Convert.ToInt32(CantidadtextBox.Text),
                    precio: (int)Convert.ToInt32(PreciotextBox.Text),
                    importe: (int)Convert.ToInt32(ImportetextBox.Text)

                ));

            //Cargar el detalle al Grid
            DetalledataGridView.DataSource = null;
            DetalledataGridView.DataSource = detalle;
        }
        private void LlenarComboBox()
        {
            Repositorio<Personas> repositorio = new Repositorio<Personas>(new Contexto());
            Repositorio<Articulos> repositori = new Repositorio<Articulos>(new Contexto());
            PersonacomboBox.DataSource = repositorio.GetList(c => true);
            PersonacomboBox.ValueMember = "PersonaId";
            PersonacomboBox.DisplayMember = "Nombres";

            ArticulocomboBox.DataSource = repositori.GetList(c => true);
            ArticulocomboBox.ValueMember = "ArticuloId";
            ArticulocomboBox.DisplayMember = "Descripcion";
        }

        private Cotizaciones LlenaClase()
        {
            Cotizaciones cotizacion = new Cotizaciones();

            cotizacion.CotizacionId = Convert.ToInt32(IdnumericUpDown.Value);
            cotizacion.Fecha = fechaDateTimePicker.Value;
            cotizacion.Comentario = ObservacionestextBox.Text;

            //Agregar cada linea del Grid al detalle
            foreach (DataGridViewRow item in DetalledataGridView.Rows)
            {
                cotizacion.AgregarDetalle(
                    ToInt(item.Cells["id"].Value),
                    ToInt(item.Cells["CotizacionId"].Value),
                    ToInt(item.Cells["PersonaId"].Value),
                    ToInt(item.Cells["ArticuloId"].Value),
                    ToInt(item.Cells["Cantidad"].Value),
                    ToInt(item.Cells["Precio"].Value),
                    ToInt(item.Cells["Importe"].Value)
                  );
            }
            return cotizacion;
        }
        private bool HayErrores()
        {
            bool HayErrores = false;

            if (String.IsNullOrWhiteSpace(ObservacionestextBox.Text))
            {
                MyerrorProvider.SetError(ObservacionestextBox,
                    "No debes dejar el Comentario vacio");
                HayErrores = true;
            }

            if (String.IsNullOrWhiteSpace(CantidadtextBox.Text))
            {
                MyerrorProvider.SetError(ObservacionestextBox,
                    "Debes introducir una cantidad");
                HayErrores = true;
            }

            if (String.IsNullOrWhiteSpace(PreciotextBox.Text))
            {
                MyerrorProvider.SetError(PreciotextBox,
                    "Debes introducir un precio");
                HayErrores = true;
            }

            if (String.IsNullOrWhiteSpace(ImportetextBox.Text))
            {
                MyerrorProvider.SetError(ImportetextBox,
                    "Debes introducir un importe");
                HayErrores = true;
            }

            if (DetalledataGridView.RowCount == 0)
            {
                MyerrorProvider.SetError(DetalledataGridView,
                    "Es obligatorio seleccionar las ciudades visitadas");
                HayErrores = true;
            }

            return HayErrores;
        }

        private int ToInt(object valor)
        {
            int retorno = 0;

            int.TryParse(valor.ToString(), out retorno);

            return retorno;
        }

        private void LlenarCampos(Cotizaciones cotizacion)
        {
            IdnumericUpDown.Value = cotizacion.CotizacionId;
            fechaDateTimePicker.Value = cotizacion.Fecha;
            ObservacionestextBox.Text = cotizacion.Comentario;

            //Cargar el detalle al Grid
            DetalledataGridView.DataSource = cotizacion.Detalle;

            //Ocultar columnas
            DetalledataGridView.Columns["Id"].Visible = false;
            DetalledataGridView.Columns["CotizacionId"].Visible = false;
        }

    }
}

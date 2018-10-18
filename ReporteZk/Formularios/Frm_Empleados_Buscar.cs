using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CapadeDatos;


namespace ReporteZk
{
    public partial class Frm_Empleados_Buscar : DevExpress.XtraEditors.XtraForm
    {
        public Frm_ReporteEmpleado FrmReporteEmpleado;
        public Frm_Empleados_Horario FrmEmpladosHorario;
        public Frm_Empleados_Buscar()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CLS_Empleados selE = new CLS_Empleados();
            selE.SSN = txtSSN.Text;
            selE.Name = txtNombre.Text;
            selE.lastname = txtApellido.Text;
            selE.TITLE = txtPuesto.Text;
            selE.MtdSeleccionarEmpleados();
            if(selE.Exito)
            {
                if(selE.Datos.Rows.Count>0)
                {
                    dtgEmpleados.DataSource = selE.Datos;
                }
                else
                {
                    dtgEmpleados.DataSource = selE.Datos;
                    XtraMessageBox.Show("No se encontraron Empleados con estos parametros de busqueda");
                }
            }
            else
            {
                XtraMessageBox.Show(selE.Mensaje);
            }
        }

        private void dtgEmpleados_Click(object sender, EventArgs e)
        {
            MtdSubeDatos();
        }
        private void MtdSubeDatos()
        {
            try
            {
                foreach (int i in this.dtgValEmpleados.GetSelectedRows())
                {
                    DataRow row = dtgValEmpleados.GetDataRow(i);
                    this.txtNombre.Tag= row["badgenumber"].ToString();
                    this.txtNombre.Text = row["Name"].ToString();
                    this.txtApellido.Text = row["lastname"].ToString();
                    this.txtSSN.Text= row["SSN"].ToString();
                    this.txtPuesto.Text = row["TITLE"].ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtSSN.Text = string.Empty;
            txtPuesto.Text = string.Empty;
            dtgEmpleados.DataSource = null;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (FrmReporteEmpleado != null)
            {
                FrmReporteEmpleado.txtNombreEmpleado.Tag = txtNombre.Tag;
                FrmReporteEmpleado.txtNombreEmpleado.Text = txtNombre.Text + " " + txtApellido.Text;
                this.Close();
            }
            else
            {
                FrmEmpladosHorario.txtNombreEmpleado.Tag = txtNombre.Tag;
                FrmEmpladosHorario.txtNombreEmpleado.Text = txtNombre.Text + " " + txtApellido.Text;
                this.Close();
            }
        }
    }
}
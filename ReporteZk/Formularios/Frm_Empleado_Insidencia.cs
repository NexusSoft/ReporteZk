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
    public partial class Frm_Empleado_Insidencia : DevExpress.XtraEditors.XtraForm
    {
        public bool isEdit { get; private set; }

        public Frm_Empleado_Insidencia()
        {
            InitializeComponent();
        }

        private void Frm_Empleado_Insidencia_Shown(object sender, EventArgs e)
        {
            CargarInsidencias(null);
            dtInicio.DateTime = DateTime.Now;
            dtFin.DateTime = DateTime.Now;
            dtgValInsidencias.OptionsSelection.EnableAppearanceFocusedCell = false;
            dtgValInsidencias.OptionsSelection.EnableAppearanceHideSelection = false;
            dtgValInsidencias.OptionsSelection.MultiSelect = true;
            dtgValInsidencias.OptionsView.ShowGroupPanel = false;
        }
        private void CargarInsidencias(int? Valor)
        {
            CLS_Insidencias obtener = new CLS_Insidencias();
            obtener.MtdSeleccionarInsidencias();
            if (obtener.Exito)
            {
                CargarComboSemanas(obtener.Datos, Valor);
            }
        }
        private void CargarComboSemanas(DataTable Datos, int? Valor)
        {
            cboInsidencias.Properties.DisplayMember = "NombreInsidencia";
            cboInsidencias.Properties.ValueMember = "IdInsidencia";
            cboInsidencias.EditValue = Valor;
            cboInsidencias.Properties.DataSource = Datos;
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            int Valor = Convert.ToInt32(txtNombreEmpleado.Tag);
            Frm_Empleados_Buscar frm = new Frm_Empleados_Buscar();
            frm.FrmEmpladosInsidencia = this;
            frm.ShowDialog();
            if(Valor!= Convert.ToInt32(txtNombreEmpleado.Tag))
            {
                dtInicio.DateTime = DateTime.Now;
                dtFin.DateTime = DateTime.Now;
                CargarInsidencias(null);
                dtgInsidencias.DataSource = null;
                isEdit = false;
            }
        }
        public string DosCeros(string sVal)
        {
            string str = "";
            if (sVal.Length == 1)
            {
                return (str = "0" + sVal);
            }
            return sVal;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(Convert.ToDateTime(dtInicio.EditValue), Convert.ToDateTime(dtFin.EditValue)) <= 0)
            {
                if (Convert.ToInt32(txtNombreEmpleado.Tag) != 0)
                {
                    DateTime FInicio = Convert.ToDateTime(dtInicio.EditValue.ToString());
                    DateTime FFin = Convert.ToDateTime(dtFin.EditValue.ToString());
                    CLS_Insidencias cls = new CLS_Insidencias();
                    cls.FechaInicio = string.Format("{0}{1}{2} 00:00:00", FInicio.Year, DosCeros(FInicio.Month.ToString()), DosCeros(FInicio.Day.ToString()));
                    cls.FechaFin = string.Format("{0}{1}{2} 23:59:59", FFin.Year, DosCeros(FFin.Month.ToString()), DosCeros(FFin.Day.ToString()));
                    cls.pin = Convert.ToInt32(txtNombreEmpleado.Tag);
                    cls.MtdSelectInsidencias();
                    if (cls.Exito)
                    {
                        if (cls.Datos.Rows.Count > 0)
                        {
                            for (int i = 0; i < cls.Datos.Rows.Count; i++)
                            {
                                dtgInsidencias.DataSource = cls.Datos;
                            }
                        }
                        else
                        {
                            dtgInsidencias.DataSource = cls.Datos;
                            if (isEdit == false)
                            {
                                XtraMessageBox.Show("No se encontraron datos para este empleado");
                            }
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show("Falta Seleccionar un empleado");
                }
            }
            else
            {
                XtraMessageBox.Show("La Fecha de inicio es mayor a la fecha Fin");
            }
        }

        private void dtgInsidencias_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (int i in this.dtgValInsidencias.GetSelectedRows())
                {
                    int FilaSelect = i;
                    DataRow row = dtgValInsidencias.GetDataRow(i);
                    dtInicio.EditValue =Convert.ToDateTime( row["fecha"].ToString());
                    dtFin.EditValue = Convert.ToDateTime(row["fecha"].ToString());
                    CargarInsidencias(Convert.ToInt32(row["IdInsidencia"].ToString()));
                    isEdit = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtNombreEmpleado.Text = string.Empty;
            txtNombreEmpleado.Tag = 0;
            dtInicio.DateTime = DateTime.Now;
            dtFin.DateTime = DateTime.Now;
            CargarInsidencias(null);
            dtgInsidencias.DataSource = null;
            isEdit = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(isEdit==true)
            {
                DialogResult = XtraMessageBox.Show("¿Desea eliminar la insidencia?\nLos cambios no se podran revertir", "Eliminar Insidencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (DialogResult == DialogResult.Yes)
                {
                    DateTime FInicio = Convert.ToDateTime(dtInicio.EditValue.ToString());
                    CLS_Insidencias del = new CLS_Insidencias();
                    del.pin = Convert.ToInt32(txtNombreEmpleado.Tag);
                    del.fecha = string.Format("{0}{1}{2} 00:00:00", FInicio.Year, DosCeros(FInicio.Month.ToString()), DosCeros(FInicio.Day.ToString()));
                    del.MtdDeleteEmpleadoInsidencias();
                    if (del.Exito)
                    {
                        btnBuscar.PerformClick();
                        XtraMessageBox.Show("Se ha elimindo el registro con exito");
                        isEdit = false;
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("No se ha seleccionado registro para eliminar");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(Convert.ToDateTime(dtInicio.EditValue), Convert.ToDateTime(dtFin.EditValue)) <= 0)
            {
                if (Convert.ToInt32(txtNombreEmpleado.Tag) != 0)
                {
                    if (cboInsidencias.EditValue != null)
                    {
                        if (isEdit == false)
                        {
                            DateTime FInicio = Convert.ToDateTime(dtInicio.EditValue.ToString());
                            DateTime FFin = Convert.ToDateTime(dtFin.EditValue.ToString());
                            string FechaInicio = string.Format("{0}{1}{2} 00:00:00", FInicio.Year, DosCeros(FInicio.Month.ToString()), DosCeros(FInicio.Day.ToString()));
                            string FechaFin = string.Format("{0}{1}{2} 00:00:00", FFin.Year, DosCeros(FFin.Month.ToString()), DosCeros(FFin.Day.ToString()));
                            if (FechaInicio == FechaFin)
                            {
                                CLS_Insidencias ins = new CLS_Insidencias();
                                ins.pin = Convert.ToInt32(txtNombreEmpleado.Tag);
                                ins.fecha = FechaInicio;
                                ins.Insidencia = cboInsidencias.Text;
                                ins.MtdInsertEmpleadoInsidencias();
                                if (ins.Exito)
                                {
                                    btnNuevo.PerformClick();
                                    XtraMessageBox.Show("El registro se ha insertado con exito");
                                    isEdit = false;
                                }
                                else
                                {
                                    XtraMessageBox.Show(ins.Mensaje);
                                }
                            }
                            else
                            {
                                int Valor = 0;
                                TimeSpan tSpan = FFin - FInicio;
                                for (int i = 0; i <= tSpan.Days; i++)
                                {
                                    DateTime fecha = FInicio.AddDays(i);
                                    FechaInicio = string.Format("{0}{1}{2} 00:00:00", fecha.Year, DosCeros(fecha.Month.ToString()), DosCeros(fecha.Day.ToString()));
                                    CLS_Insidencias ins = new CLS_Insidencias();
                                    ins.pin = Convert.ToInt32(txtNombreEmpleado.Tag);
                                    ins.fecha = FechaInicio;
                                    ins.Insidencia = cboInsidencias.Text;
                                    ins.MtdInsertEmpleadoInsidencias();
                                    if (ins.Exito)
                                    {
                                        Valor++;
                                    }
                                }
                                if ((Valor) == tSpan.Days + 1)
                                {
                                    btnNuevo.PerformClick();
                                    XtraMessageBox.Show("Los registros se ha insertado con exito");
                                }
                            }
                        }
                        else
                        {
                            DateTime FInicio = Convert.ToDateTime(dtInicio.EditValue.ToString());
                            CLS_Insidencias upd = new CLS_Insidencias();
                            upd.pin = Convert.ToInt32(txtNombreEmpleado.Tag);
                            upd.fecha = string.Format("{0}{1}{2} 00:00:00", FInicio.Year, DosCeros(FInicio.Month.ToString()), DosCeros(FInicio.Day.ToString()));
                            upd.Insidencia = cboInsidencias.Text.ToString();
                            upd.MtdupdateEmpleadoInsidencias();
                            if (upd.Exito)
                            {
                                btnBuscar.PerformClick();
                                XtraMessageBox.Show("Se ha actualizado el registro con exito");
                            }
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("No se ha seleccionado una Insidencia");
                    }
                }
                else
                {
                    XtraMessageBox.Show("No se ha seleccionado un empleado");
                }
            }
            else
            {
                XtraMessageBox.Show("La Fecha de inicio es mayor a la fecha Fin");
            }
        }
    }
}
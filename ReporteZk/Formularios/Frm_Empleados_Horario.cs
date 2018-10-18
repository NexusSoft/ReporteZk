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
    public partial class Frm_Empleados_Horario : DevExpress.XtraEditors.XtraForm
    {
        public bool ismodoedicion { get; private set; }
        public string vid { get; private set; }
        public string vLOGID { get; private set; }
        public DataRow row { get; private set; }
        public string vFecha { get; private set; }
        public string vHora { get; private set; }

        public Frm_Empleados_Horario()
        {
            InitializeComponent();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            Frm_Empleados_Buscar frm = new Frm_Empleados_Buscar();
            frm.FrmEmpladosHorario = this;
            frm.ShowDialog();
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
                DateTime FInicio = Convert.ToDateTime(dtInicio.EditValue.ToString());
                DateTime FFin = Convert.ToDateTime(dtFin.EditValue.ToString());
                CLS_Asistencia cls = new CLS_Asistencia();
                cls.FechaInicio = string.Format("{0}{1}{2} 00:00:00", FInicio.Year, DosCeros(FInicio.Month.ToString()), DosCeros(FInicio.Day.ToString()));
                cls.FechaFin = string.Format("{0}{1}{2} 23:59:59", FFin.Year, DosCeros(FFin.Month.ToString()), DosCeros(FFin.Day.ToString()));
                cls.pin = Convert.ToInt32(txtNombreEmpleado.Tag);
                cls.MtdSeleccionarEmpleadosHorarios();
                if (cls.Exito)
                {
                    if (cls.Datos.Rows.Count > 0)
                    {
                        for (int i = 0; i < cls.Datos.Rows.Count; i++)
                        {
                            dtgHorarios.DataSource = cls.Datos;
                        }
                    }
                    else
                    {
                        dtgHorarios.DataSource = cls.Datos;
                        XtraMessageBox.Show("No se encontraron datos para este empleado");
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("La Fecha de inicio es mayor a la fecha Fin");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txtNombreEmpleado.Tag.ToString()!="0")
            {
                if (radioGroup1.SelectedIndex == 0)
                {
                    CLS_Asistencia ins = new CLS_Asistencia();
                    ins.pin = Convert.ToInt32(txtNombreEmpleado.Tag.ToString());
                    DateTime VFecha = Convert.ToDateTime(dtFechaIngreso.EditValue.ToString());
                    DateTime VHora = Convert.ToDateTime(dtHoraIngreso.EditValue.ToString());
                    ins.CHECKTIME = string.Format("{0}{1}{2} {3}:{4}:{5}", VFecha.Year, DosCeros(VFecha.Month.ToString()), DosCeros(VFecha.Day.ToString()), DosCeros(VHora.Hour.ToString()), DosCeros(VHora.Minute.ToString()), DosCeros(VHora.Second.ToString()));
                    ins.MtdInsertar();
                    if (ins.Exito)
                    {
                        XtraMessageBox.Show("el registro se Agrego con Exito");
                        btnBuscar.PerformClick();
                    }
                    else
                    {
                        XtraMessageBox.Show(ins.Mensaje);
                    }
                }
                else
                {
                    CLS_Asistencia ins = new CLS_Asistencia();
                    ins.pin = Convert.ToInt32(txtNombreEmpleado.Tag.ToString());
                    DateTime VFecha = Convert.ToDateTime(dtFechaIngreso.EditValue.ToString());
                    DateTime VHora = Convert.ToDateTime(dtHoraIngreso.EditValue.ToString());
                    ins.CHECKTIME = string.Format("{0}{1}{2} {3}", VFecha.Year, DosCeros(VFecha.Month.ToString()), DosCeros(VFecha.Day.ToString()), "00:00:00");
                    ins.Notas = txtNotas.Text;
                    ins.MtdInsertarNotas();
                    if (ins.Exito)
                    {
                        XtraMessageBox.Show("el registro se Agrego con Exito");
                        btnBuscar.PerformClick();
                    }
                    else
                    {
                        XtraMessageBox.Show(ins.Mensaje);
                    }
                }
            }
        }

        private void Frm_Empleados_Horario_Shown(object sender, EventArgs e)
        {
            dtInicio.EditValue = DateTime.Now;
            dtFin.EditValue = DateTime.Now;
            dtFechaIngreso.EditValue = DateTime.Now;
            dtHoraIngreso.EditValue = DateTime.Now;
            radioGroup1.SelectedIndex = 0;
            dtHoraIngreso.Enabled = true;
            txtNotas.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ismodoedicion)
            {
                CLS_Asistencia udpAsis = new CLS_Asistencia();
                dtgHorarios.FocusedView.CloseEditor();
                vFecha = row["Fecha"].ToString();
                vHora = row["Hora"].ToString();
                udpAsis.id = Convert.ToInt32(vid);
                udpAsis.LOGID = Convert.ToInt32(vLOGID);
                DateTime VFecha = Convert.ToDateTime(vFecha);
                DateTime VHora = Convert.ToDateTime(vHora);

                udpAsis.CHECKTIME = string.Format("{0}{1}{2} {3}:{4}:{5}", VFecha.Year, DosCeros(VFecha.Month.ToString()), DosCeros(VFecha.Day.ToString()), DosCeros(VHora.Hour.ToString()), DosCeros(VHora.Minute.ToString()), DosCeros(VHora.Second.ToString())); ;
                udpAsis.MtdActualizar();
                if (udpAsis.Exito)
                {
                    XtraMessageBox.Show("Se ha actualizado el registro con exito");
                    btnBuscar.PerformClick();
                }
                ismodoedicion = false;
            }
        }

        private void dtgHorarios_Click(object sender, EventArgs e)
        {
            ismodoedicion = true;
            try
            {
                foreach (int i in this.dtgValHorarios.GetSelectedRows())
                {
                    row = dtgValHorarios.GetDataRow(i);
                    vid = row["id"].ToString();
                    vLOGID = row["LOGID"].ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void dtgValHorarios_Click(object sender, EventArgs e)
        {
            ismodoedicion = true;
            try
            {
                foreach (int i in this.dtgValHorarios.GetSelectedRows())
                {
                    row = dtgValHorarios.GetDataRow(i);
                    vid = row["id"].ToString();
                    vLOGID = row["LOGID"].ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void dtgValHorarios_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            ismodoedicion = true;
            try
            {
                foreach (int i in this.dtgValHorarios.GetSelectedRows())
                {
                    row = dtgValHorarios.GetDataRow(i);
                    vid = row["id"].ToString();
                    vLOGID = row["LOGID"].ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (ismodoedicion)
            {
                CLS_Asistencia udpAsis = new CLS_Asistencia();
                dtgHorarios.FocusedView.CloseEditor();
                udpAsis.id = Convert.ToInt32(vid);
                udpAsis.LOGID = Convert.ToInt32(vLOGID);
                
                udpAsis.MtdEliminar();
                if (udpAsis.Exito)
                {
                    XtraMessageBox.Show("Se ha Eliminado el registro con exito");
                    btnBuscar.PerformClick();
                }
                ismodoedicion = false;
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                dtHoraIngreso.Enabled = true;
                txtNotas.Enabled = false;
            }
            else
            {
                dtHoraIngreso.Enabled = false;
                txtNotas.Enabled = true;
            }
        }
    }
}
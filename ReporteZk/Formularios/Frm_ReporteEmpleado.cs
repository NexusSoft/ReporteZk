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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace ReporteZk
{
    public partial class Frm_ReporteEmpleado : DevExpress.XtraEditors.XtraForm
    {
        DataTable tb;
        ConnectionInfo oConexInfo = new ConnectionInfo();
        const string NombreProyecto = "ReporteZK";

        public string vpin { get; private set; }
        public int ncolumna { get; private set; }
        public string vFechaEntrada { get; set; }
        public string vFechaSalida { get; set; }
        public DateTime vFecha { get; private set; }
        public string vFechacorta { get; private set; }
        public DateTime vFechaInicio { get; private set; }
        public DateTime vFechaFin { get; private set; }
        public string vNombre { get; private set; }
        public string vNoEmpleadoInv { get; private set; }
        public string vPuesto { get; private set; }
        public string vDia { get; private set; }
        public string vNoEmpleadoSis { get; private set; }
        public string ParFechaInicio { get; private set; }
        public string ParFechaFin { get; private set; }
        public string vInsidencia { get; private set; }

        public Frm_ReporteEmpleado()
        {
            InitializeComponent();
        }

        private void btnConexion_Click(object sender, EventArgs e)
        {
            Frm_Conexiones frm = new Frm_Conexiones();
            frm.ShowDialog();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            Frm_Empleados_Buscar frm = new Frm_Empleados_Buscar();
            frm.FrmReporteEmpleado = this;
            frm.ShowDialog();
        }
        public void ImportarEmpleado(int idUsuario, string NombreEmpleado)
        {
            txtNombreEmpleado.Tag = idUsuario;
            txtNombreEmpleado.Text = NombreEmpleado;
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
        private void Parametros_basededatos()
        {
            MSRegistro RegOut = new MSRegistro();
            Crypto DesencriptarTexto = new Crypto();
            string ValServer = DesencriptarTexto.Desencriptar(RegOut.GetSetting("ConexionSQL", "Server"));
            string ValDBase = DesencriptarTexto.Desencriptar(RegOut.GetSetting("ConexionSQL", "DBase"));
            string ValUser = DesencriptarTexto.Desencriptar(RegOut.GetSetting("ConexionSQL", "User"));
            string ValPass = DesencriptarTexto.Desencriptar(RegOut.GetSetting("ConexionSQL", "Password"));

            oConexInfo.ServerName = ValServer;
            oConexInfo.DatabaseName = ValDBase;
            oConexInfo.UserID = ValUser;
            oConexInfo.Password = ValPass;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(Convert.ToDateTime(dtInicio.EditValue), Convert.ToDateTime(dtFin.EditValue)) <= 0)
            {
                DateTime FInicio =Convert.ToDateTime(dtInicio.EditValue.ToString());
                DateTime FFin = Convert.ToDateTime(dtFin.EditValue.ToString());
                CLS_Asistencia cls = new CLS_Asistencia();
                cls.FechaInicio = FInicio.Year.ToString() + DosCeros(FInicio.Month.ToString()) + DosCeros(FInicio.Day.ToString()) + " 00:00:00";
                cls.FechaFin = FFin.Year.ToString() + DosCeros(FFin.Month.ToString()) + DosCeros(FFin.Day.ToString())+ " 23:59:59";
                if (chkTodos.Checked != true)
                {
                    cls.pin = Convert.ToInt32(txtNombreEmpleado.Tag);
                }
                else
                {
                    cls.pin = 0;
                }
                cls.MtdEliminarRPT();
                cls.MtdSeleccionarAsistencia();
                if (cls.Exito)
                {
                    if (cls.Datos.Rows.Count > 0)
                    {
                        ncolumna = 1;
                        vpin=cls.Datos.Rows[0]["pin"].ToString();
                        vFecha=Convert.ToDateTime(cls.Datos.Rows[0]["time"].ToString());
                        vFechacorta = vFecha.Year + DosCeros(vFecha.Month.ToString()) + DosCeros(vFecha.Day.ToString());
                        for (int i = 0; i < cls.Datos.Rows.Count; i++)
                        {
                            if(vpin == cls.Datos.Rows[i]["pin"].ToString())
                            {
                                vNombre = string.Format("{0} {1}", cls.Datos.Rows[i]["lastname"], cls.Datos.Rows[i]["Name"]);
                                vNoEmpleadoInv = cls.Datos.Rows[i]["Minzu"].ToString();
                                vNoEmpleadoSis = cls.Datos.Rows[i]["pin"].ToString();
                                vPuesto = cls.Datos.Rows[i]["TITLE"].ToString();
                                vDia= cls.Datos.Rows[i]["Dia"].ToString();
                                vFecha = Convert.ToDateTime(cls.Datos.Rows[i]["time"].ToString());
                                if (vFechacorta == vFecha.Year + DosCeros(vFecha.Month.ToString()) + DosCeros(vFecha.Day.ToString()))
                                {
                                    if(ncolumna==1)
                                    {
                                        vFechaEntrada = string.Format("{0}:{1}:{2}", DosCeros(vFecha.Hour.ToString()), DosCeros(vFecha.Minute.ToString()), DosCeros(vFecha.Second.ToString()));
                                        vFechaInicio = vFecha;
                                        ncolumna++;
                                    }
                                    else
                                    {
                                        vFechaSalida = string.Format("{0}:{1}:{2}", DosCeros(vFecha.Hour.ToString()), DosCeros(vFecha.Minute.ToString()), DosCeros(vFecha.Second.ToString()));
                                        vFechaFin = vFecha;
                                        GuardarRegistro(vNoEmpleadoInv, vNoEmpleadoSis, vNombre, vFechaInicio, vFechaFin, vDia, vPuesto,"");
                                        ncolumna = 1;
                                    }
                                }
                                else if (vFechacorta != vFecha.Year + DosCeros(vFecha.Month.ToString()) + DosCeros(vFecha.Day.ToString()) && ncolumna == 1)
                                {
                                    vFechacorta = vFecha.Year + DosCeros(vFecha.Month.ToString()) + DosCeros(vFecha.Day.ToString());
                                    vFechaEntrada = string.Format("{0}:{1}:{2}", DosCeros(vFecha.Hour.ToString()), DosCeros(vFecha.Minute.ToString()), DosCeros(vFecha.Second.ToString()));
                                    vFechaInicio = vFecha;
                                    ncolumna = 2;
                                }
                                else if(vFechacorta != vFecha.Year + DosCeros(vFecha.Month.ToString()) + DosCeros(vFecha.Day.ToString()) && ncolumna == 2)
                                {
                                    GuardarRegistro(vNoEmpleadoInv, vNoEmpleadoSis, vNombre, vFechaInicio, vFechaInicio, vDia, vPuesto, "");
                                    vFechacorta = vFecha.Year + DosCeros(vFecha.Month.ToString()) + DosCeros(vFecha.Day.ToString());
                                    vFechaEntrada = string.Format("{0}:{1}:{2}", DosCeros(vFecha.Hour.ToString()), DosCeros(vFecha.Minute.ToString()), DosCeros(vFecha.Second.ToString()));
                                    vFechaInicio = vFecha;
                                    ncolumna = 2;
                                }
                            }
                            else
                            {
                                if (ncolumna == 2)
                                {
                                    GuardarRegistro(vNoEmpleadoInv, vNoEmpleadoSis, vNombre, vFechaInicio, vFechaInicio, vDia, vPuesto, "");
                                    vFecha = Convert.ToDateTime(cls.Datos.Rows[i]["time"].ToString());
                                    vFechacorta = vFecha.Year + DosCeros(vFecha.Month.ToString()) + DosCeros(vFecha.Day.ToString());
                                    vFechaEntrada = string.Format("{0}:{1}:{2}", DosCeros(vFecha.Hour.ToString()), DosCeros(vFecha.Minute.ToString()), DosCeros(vFecha.Second.ToString()));
                                    vFechaInicio = vFecha;
                                    vpin = cls.Datos.Rows[i]["pin"].ToString();
                                    vNombre = string.Format("{0} {1}", cls.Datos.Rows[i]["lastname"], cls.Datos.Rows[i]["Name"]);
                                    vNoEmpleadoInv = cls.Datos.Rows[i]["Minzu"].ToString();
                                    vNoEmpleadoSis = cls.Datos.Rows[i]["pin"].ToString();
                                    vPuesto = cls.Datos.Rows[i]["TITLE"].ToString();
                                    vDia = cls.Datos.Rows[i]["Dia"].ToString();
                                    vFecha = Convert.ToDateTime(cls.Datos.Rows[i]["time"].ToString());
                                    ncolumna = 2;
                                }
                                else
                                {
                                    vFecha = Convert.ToDateTime(cls.Datos.Rows[i]["time"].ToString());
                                    vFechacorta = vFecha.Year + DosCeros(vFecha.Month.ToString()) + DosCeros(vFecha.Day.ToString());
                                    vFechaEntrada = string.Format("{0}:{1}:{2}", DosCeros(vFecha.Hour.ToString()), DosCeros(vFecha.Minute.ToString()), DosCeros(vFecha.Second.ToString()));
                                    vFechaInicio = vFecha;
                                    vpin = cls.Datos.Rows[i]["pin"].ToString();
                                    vNombre = string.Format("{0} {1}", cls.Datos.Rows[i]["lastname"], cls.Datos.Rows[i]["Name"]);
                                    vNoEmpleadoInv = cls.Datos.Rows[i]["Minzu"].ToString();
                                    vNoEmpleadoSis = cls.Datos.Rows[i]["pin"].ToString();
                                    vPuesto = cls.Datos.Rows[i]["TITLE"].ToString();
                                    vDia = cls.Datos.Rows[i]["Dia"].ToString();
                                    vFecha = Convert.ToDateTime(cls.Datos.Rows[i]["time"].ToString());
                                    ncolumna = 2;
                                }
                            }
                        }
                        if(ncolumna==2)
                        {
                            GuardarRegistro(vNoEmpleadoInv, vNoEmpleadoSis, vNombre, vFechaInicio, vFechaInicio, vDia, vPuesto, "");
                        }
                        ParFechaInicio= DosCeros(FInicio.Day.ToString())+"/"+ DosCeros(FInicio.Month.ToString())+"/"+ FInicio.Year.ToString();
                        ParFechaFin = DosCeros(FFin.Day.ToString()) + "/" +DosCeros(FFin.Month.ToString()) + "/" + FFin.Year.ToString();
                        InsertarIncidencias(cls.FechaInicio, cls.FechaFin);
                        MostrarReporte(ParFechaInicio, ParFechaFin);
                    }
                    else
                    {
                        FInicio = Convert.ToDateTime(dtInicio.EditValue.ToString());
                        FFin = Convert.ToDateTime(dtFin.EditValue.ToString());
                        CLS_Asistencia dis = new CLS_Asistencia();
                        dis.FechaInicio = FInicio.Year.ToString() + DosCeros(FInicio.Month.ToString()) + DosCeros(FInicio.Day.ToString()) + " 00:00:00";
                        dis.FechaFin = FFin.Year.ToString() + DosCeros(FFin.Month.ToString()) + DosCeros(FFin.Day.ToString()) + " 23:59:59";
                        if (chkTodos.Checked != true)
                        {
                            dis.pin = Convert.ToInt32(txtNombreEmpleado.Tag);
                        }
                        else
                        {
                            dis.pin = 0;
                        }
                        dis.MtdSeleccionarInsidencias();
                        if (dis.Exito)
                        {
                            if (dis.Datos.Rows.Count > 0)
                            {
                                ParFechaInicio = DosCeros(FInicio.Day.ToString()) + "/" + DosCeros(FInicio.Month.ToString()) + "/" + FInicio.Year.ToString();
                                ParFechaFin = DosCeros(FFin.Day.ToString()) + "/" + DosCeros(FFin.Month.ToString()) + "/" + FFin.Year.ToString();
                                InsertarIncidencias(dis.FechaInicio, dis.FechaFin);
                                MostrarReporte(ParFechaInicio, ParFechaFin);
                            }
                            else
                            {
                                XtraMessageBox.Show("No se encontraron datos para este empleado");
                            }
                        }
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("La Fecha de inicio es mayor a la fecha Fin");
            }
        }

        private void InsertarIncidencias(string fechaInicio, string fechaFin)
        {
            DateTime FInicio = Convert.ToDateTime(dtInicio.EditValue.ToString());
            DateTime FFin = Convert.ToDateTime(dtFin.EditValue.ToString());
            CLS_Asistencia cls = new CLS_Asistencia();
            cls.FechaInicio = FInicio.Year.ToString() + DosCeros(FInicio.Month.ToString()) + DosCeros(FInicio.Day.ToString()) + " 00:00:00";
            cls.FechaFin = FFin.Year.ToString() + DosCeros(FFin.Month.ToString()) + DosCeros(FFin.Day.ToString()) + " 23:59:59";
            if (chkTodos.Checked != true)
            {
                cls.pin = Convert.ToInt32(txtNombreEmpleado.Tag);
            }
            else
            {
                cls.pin = 0;
            }
            cls.MtdSeleccionarInsidencias();
            if(cls.Exito)
            {
                if (cls.Datos.Rows.Count > 0)
                {
                    vFecha = Convert.ToDateTime(cls.Datos.Rows[0]["time"].ToString());
                    vFechacorta = vFecha.Year + DosCeros(vFecha.Month.ToString()) + DosCeros(vFecha.Day.ToString());
                    for (int i = 0; i < cls.Datos.Rows.Count; i++)
                    {
                        vpin = cls.Datos.Rows[0]["pin"].ToString();
                        vNombre = string.Format("{0} {1}", cls.Datos.Rows[i]["lastname"], cls.Datos.Rows[i]["Name"]);
                        vNoEmpleadoInv = cls.Datos.Rows[i]["Minzu"].ToString();
                        vNoEmpleadoSis = cls.Datos.Rows[i]["pin"].ToString();
                        vPuesto = cls.Datos.Rows[i]["TITLE"].ToString();
                        vDia = cls.Datos.Rows[i]["Dia"].ToString();
                        vFecha = Convert.ToDateTime(cls.Datos.Rows[i]["time"].ToString());
                        vInsidencia= cls.Datos.Rows[i]["Insidencia"].ToString();
                        GuardarRegistro(vNoEmpleadoInv, vNoEmpleadoSis, vNombre, vFecha, vFecha, vDia, vPuesto, vInsidencia);
                    }
                }
            }
        }

        private void MostrarReporte(string parFechaInicio, string parFechaFin)
        {
            Parametros_basededatos();
            if (radioGroup1.SelectedIndex == 1)
            {
                Reportes.RPTEmpleados RCatalogoP = new Reportes.RPTEmpleados();
                Tables RPTTablas = RCatalogoP.Database.Tables;

                foreach (Table oTabla in RPTTablas)
                {
                    TableLogOnInfo oTablaConexInfo = oTabla.LogOnInfo;
                    oTablaConexInfo.ConnectionInfo = oConexInfo;
                    oTabla.ApplyLogOnInfo(oTablaConexInfo);
                }
                try
                {
                    //RCatalogoP.Refresh();
                    //RCatalogoP.DataDefinition.FormulaFields["EsSemana"].Text = "'" + EsSemana + "'";
                    //RCatalogoP.DataDefinition.FormulaFields["EsFloracion"].Text = string.Format("'{0}'", EsFloracion);
                    //RCatalogoP.DataDefinition.FormulaFields["EsTipoCultivo"].Text = string.Format("'{0}'", EsTipoCultivo);
                    //RCatalogoP.DataDefinition.FormulaFields["EsClaseFruta"].Text = string.Format("'{0}'", EsClaseFruta);
                    //RCatalogoP.DataDefinition.FormulaFields["EsTipoBeneficiario"].Text = string.Format("'{0}'", EsTipoBeneficiario);
                    //RCatalogoP.DataDefinition.FormulaFields["EsTipoCorte"].Text = string.Format("'{0}'", EsTipoCorte);
                    //RCatalogoP.DataDefinition.FormulaFields["EsPais"].Text = string.Format("'{0}'", EsPais);
                    //RCatalogoP.DataDefinition.FormulaFields["EsGramaje"].Text = string.Format("'{0}'", EsGramaje);
                    ////RCatalogoP.Refresh();
                    RCatalogoP.Refresh();
                    RCatalogoP.SetParameterValue("FInicio", parFechaInicio);
                    RCatalogoP.SetParameterValue("FFin", parFechaFin);
                    CRViewer.ReportSource = RCatalogoP;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Descripcion : " + ex.Message);
                }
            }
            else if(radioGroup1.SelectedIndex == 0)
            {
                Reportes.RPTEmpleadosConcentrado RCatalogoP = new Reportes.RPTEmpleadosConcentrado();
                Tables RPTTablas = RCatalogoP.Database.Tables;

                foreach (Table oTabla in RPTTablas)
                {
                    TableLogOnInfo oTablaConexInfo = oTabla.LogOnInfo;
                    oTablaConexInfo.ConnectionInfo = oConexInfo;
                    oTabla.ApplyLogOnInfo(oTablaConexInfo);
                }
                try
                {
                    //RCatalogoP.Refresh();
                    //RCatalogoP.DataDefinition.FormulaFields["EsSemana"].Text = "'" + EsSemana + "'";
                    //RCatalogoP.DataDefinition.FormulaFields["EsFloracion"].Text = string.Format("'{0}'", EsFloracion);
                    //RCatalogoP.DataDefinition.FormulaFields["EsTipoCultivo"].Text = string.Format("'{0}'", EsTipoCultivo);
                    //RCatalogoP.DataDefinition.FormulaFields["EsClaseFruta"].Text = string.Format("'{0}'", EsClaseFruta);
                    //RCatalogoP.DataDefinition.FormulaFields["EsTipoBeneficiario"].Text = string.Format("'{0}'", EsTipoBeneficiario);
                    //RCatalogoP.DataDefinition.FormulaFields["EsTipoCorte"].Text = string.Format("'{0}'", EsTipoCorte);
                    //RCatalogoP.DataDefinition.FormulaFields["EsPais"].Text = string.Format("'{0}'", EsPais);
                    //RCatalogoP.DataDefinition.FormulaFields["EsGramaje"].Text = string.Format("'{0}'", EsGramaje);
                    ////RCatalogoP.Refresh();
                    RCatalogoP.Refresh();
                    RCatalogoP.SetParameterValue("FInicio", parFechaInicio);
                    RCatalogoP.SetParameterValue("FFin", parFechaFin);
                    CRViewer.ReportSource = RCatalogoP;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Descripcion : " + ex.Message);
                }
            }
        }

        private void GuardarRegistro(string vNoEmpleadoInv, string vNoEmpleadoSis, string vNombre, DateTime vFechaInicio, DateTime vFechaFin, string vDia, string vPuesto,string vInsidencia)
        {
            CLS_Asistencia ins = new CLS_Asistencia();
            if (vNoEmpleadoInv == string.Empty)
            {
                ins.NoEmpleadoInv = 0;
            }
            else
            {
                ins.NoEmpleadoInv = Convert.ToInt32(vNoEmpleadoInv);
            }
            ins.NoEmpleadoSis = Convert.ToInt32(vNoEmpleadoSis);
            ins.Nombre = vNombre;
            ins.FechaEntrada = string.Format("{0}{1}{2} {3}:{4}:{5}", vFechaInicio.Year, DosCeros(vFechaInicio.Month.ToString()), DosCeros(vFechaInicio.Day.ToString()), DosCeros(vFechaInicio.Hour.ToString()), DosCeros(vFechaInicio.Minute.ToString()), DosCeros(vFechaInicio.Second.ToString()));
            ins.FechaSalida = string.Format("{0}{1}{2} {3}:{4}:{5}", vFechaFin.Year, DosCeros(vFechaFin.Month.ToString()), DosCeros(vFechaFin.Day.ToString()), DosCeros(vFechaFin.Hour.ToString()), DosCeros(vFechaFin.Minute.ToString()), DosCeros(vFechaFin.Second.ToString()));
            ins.DiaSemana = vDia;
            ins.Notas = string.Empty;
            ins.Puesto = vPuesto;
            ins.Insidencia = vInsidencia;
            // A la hora final le restamos la hora de inicio
            TimeSpan diferencia = vFechaFin.Subtract(vFechaInicio);
            ins.HorasT = diferencia.Hours;
            ins.MinutosT = diferencia.Minutes;
            ins.MtdInsertarRPT();
            if(!ins.Exito==true)
            {
                XtraMessageBox.Show(ins.Mensaje);
            }
        }

        private void Frm_ReporteEmpleado_Shown(object sender, EventArgs e)
        {
            dtInicio.EditValue = DateTime.Now;
            dtFin.EditValue = DateTime.Now;
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            if(chkTodos.Checked==true)
            {
                txtNombreEmpleado.Text = string.Empty;
                txtNombreEmpleado.Enabled = false;
            }
            else
            {
                txtNombreEmpleado.Text = string.Empty;
                txtNombreEmpleado.Enabled = true;
                txtNombreEmpleado.Focus();
            }
        }

        private void btnHorarios_Click(object sender, EventArgs e)
        {
            Frm_Empleados_Horario frm = new Frm_Empleados_Horario();
            frm.ShowDialog();
        }

        private void btnInsidencias_Click(object sender, EventArgs e)
        {
            Frm_Empleado_Insidencia frm = new Frm_Empleado_Insidencia();
            frm.ShowDialog();
        }
    }
}
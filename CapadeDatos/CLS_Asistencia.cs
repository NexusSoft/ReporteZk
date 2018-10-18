using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapadeDatos
{
    public class CLS_Asistencia: ConexionBase
    {
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string NombreEmpleado { get; set; }
        public int pin { get; set; }
        public string CHECKTIME { get; set; }
        public int LOGID { get; set; }
        public int id { get; set; }
        public string Notas { get;  set; }


        public int NoEmpleadoInv { get; set; }
        public int NoEmpleadoSis { get; set; }
        public string Nombre { get; set; }
        public string FechaEntrada { get; set; }
        public string FechaSalida { get; set; }
        public int HorasT { get; set; }
        public int MinutosT { get; set; }
        public string DiaSemana { get; set; }
        public string Puesto { get; set; }

        public void MtdSeleccionarAsistencia()
        {
            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_AsistenciaEmpleados_Select";
                _dato.CadenaTexto = FechaInicio;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "FechaInicio");
                _dato.CadenaTexto = FechaFin;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "FechaFin");
                _dato.Entero = pin;
                _conexion.agregarParametro(EnumTipoDato.Entero, _dato, "pin");
                _conexion.EjecutarDataset();

                if (_conexion.Exito)
                {
                    Datos = _conexion.Datos;
                }
                else
                {
                    Mensaje = _conexion.Mensaje;
                    Exito = false;
                }
            }
            catch (Exception e)
            {
                Mensaje = e.Message;
                Exito = false;
            }


        }
        public void MtdSeleccionarEmpleadosHorarios()
        {
            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_Empleados_CheckInOut_Select";
                _dato.CadenaTexto = FechaInicio;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "FechaInicio");
                _dato.CadenaTexto = FechaFin;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "FechaFin");
                _dato.Entero = pin;
                _conexion.agregarParametro(EnumTipoDato.Entero, _dato, "pin");
                _conexion.EjecutarDataset();

                if (_conexion.Exito)
                {
                    Datos = _conexion.Datos;
                }
                else
                {
                    Mensaje = _conexion.Mensaje;
                    Exito = false;
                }
            }
            catch (Exception e)
            {
                Mensaje = e.Message;
                Exito = false;
            }


        }
        public void MtdInsertar()
        {
            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_Empleados_CheckInOut_Insert";

                _dato.Entero = pin;
                _conexion.agregarParametro(EnumTipoDato.Entero, _dato, "pin");
                _dato.CadenaTexto = CHECKTIME;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "CHECKTIME");
                _conexion.EjecutarDataset();

                if (_conexion.Exito)
                {
                    Datos = _conexion.Datos;
                }
                else
                {
                    Mensaje = _conexion.Mensaje;
                    Exito = false;
                }
            }
            catch (Exception e)
            {
                Mensaje = e.Message;
                Exito = false;
            }
        }
        public void MtdActualizar()
        {
            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_Empleados_CheckInOut_Update";

                _dato.Entero = id;
                _conexion.agregarParametro(EnumTipoDato.Entero, _dato, "id");
                _dato.Entero = LOGID;
                _conexion.agregarParametro(EnumTipoDato.Entero, _dato, "LOGID");
                _dato.CadenaTexto = CHECKTIME;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "CHECKTIME");
                _conexion.EjecutarDataset();

                if (_conexion.Exito)
                {
                    Datos = _conexion.Datos;
                }
                else
                {
                    Mensaje = _conexion.Mensaje;
                    Exito = false;
                }
            }
            catch (Exception e)
            {
                Mensaje = e.Message;
                Exito = false;
            }
        }
        public void MtdEliminar()
        {
            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_Empleados_CheckInOut_Delete";

                _dato.Entero = id;
                _conexion.agregarParametro(EnumTipoDato.Entero, _dato, "id");
                _dato.Entero = LOGID;
                _conexion.agregarParametro(EnumTipoDato.Entero, _dato, "LOGID");
                _conexion.EjecutarDataset();

                if (_conexion.Exito)
                {
                    Datos = _conexion.Datos;
                }
                else
                {
                    Mensaje = _conexion.Mensaje;
                    Exito = false;
                }
            }
            catch (Exception e)
            {
                Mensaje = e.Message;
                Exito = false;
            }
        }
        public void MtdInsertarNotas()
        {
            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_Empleados_CheckInOut_Insert";

                _dato.Entero = pin;
                _conexion.agregarParametro(EnumTipoDato.Entero, _dato, "pin");
                _dato.CadenaTexto = CHECKTIME;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "CHECKTIME");
                _dato.CadenaTexto = Notas;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "Notas");
                _conexion.EjecutarDataset();

                if (_conexion.Exito)
                {
                    Datos = _conexion.Datos;
                }
                else
                {
                    Mensaje = _conexion.Mensaje;
                    Exito = false;
                }
            }
            catch (Exception e)
            {
                Mensaje = e.Message;
                Exito = false;
            }
        }
        public void MtdInsertarRPT()
        {
            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_AsistenciaEmpleadosRPT_Insert";

                _dato.Entero = NoEmpleadoInv;
                _conexion.agregarParametro(EnumTipoDato.Entero, _dato, "NoEmpleadoInv");
                _dato.Entero = NoEmpleadoSis;
                _conexion.agregarParametro(EnumTipoDato.Entero, _dato, "NoEmpleadoSis");
                _dato.CadenaTexto = Nombre;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "Nombre");
                _dato.CadenaTexto = FechaEntrada;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "FechaEntrada");
                _dato.CadenaTexto = FechaSalida;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "FechaSalida");
                _dato.Entero = HorasT;
                _conexion.agregarParametro(EnumTipoDato.Entero, _dato, "HorasT");
                _dato.Entero = MinutosT;
                _conexion.agregarParametro(EnumTipoDato.Entero, _dato, "MinutosT");
                _dato.CadenaTexto = DiaSemana;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "DiaSemana");
                _dato.CadenaTexto = Notas;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "Notas");
                _dato.CadenaTexto = Puesto;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "Puesto");
                _conexion.EjecutarDataset();

                if (_conexion.Exito)
                {
                    Datos = _conexion.Datos;
                }
                else
                {
                    Mensaje = _conexion.Mensaje;
                    Exito = false;
                }
            }
            catch (Exception e)
            {
                Mensaje = e.Message;
                Exito = false;
            }
        }
        public void MtdEliminarRPT()
        {
            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_AsistenciaEmpleadosRPT_Delete";
                _conexion.EjecutarDataset();

                if (_conexion.Exito)
                {
                    Datos = _conexion.Datos;
                }
                else
                {
                    Mensaje = _conexion.Mensaje;
                    Exito = false;
                }
            }
            catch (Exception e)
            {
                Mensaje = e.Message;
                Exito = false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapadeDatos
{
    public class CLS_Insidencias:ConexionBase
    {
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public int pin { get; set; }
        public string fecha { get;  set; }
        public string Insidencia { get;  set; }

        public void MtdSeleccionarInsidencias()
        {

            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_Insidencias_Select";
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
        public void MtdSelectInsidencias()
        {
            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_Insidencia_Empleado_Select";
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
        public void MtdInsertEmpleadoInsidencias()
        {
            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_Insidencia_Empleado_Insert";
                _dato.CadenaTexto = fecha;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "fecha");
                _dato.CadenaTexto = Insidencia;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "Insidencia");
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
        public void MtdupdateEmpleadoInsidencias()
        {
            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_Insidencia_Empleado_Update";
                _dato.CadenaTexto = fecha;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "fecha");
                _dato.CadenaTexto = Insidencia;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "Insidencia");
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
        public void MtdDeleteEmpleadoInsidencias()
        {
            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_Insidencia_Empleado_Delete";
                _dato.CadenaTexto = fecha;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "fecha");
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapadeDatos
{
    public class CLS_Empleados : ConexionBase
    {
        public int USERID { get; set; }
        public string SSN { get; set; }
        public string Name { get; set; }
        public string lastname { get; set; }
        public string TITLE { get; set; }
       
        public void MtdSeleccionarEmpleados()
        {
            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_Empleados_Select";
                _dato.CadenaTexto = SSN;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "SSN");
                _dato.CadenaTexto = Name;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "Name");
                _dato.CadenaTexto = lastname;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "lastname");
                _dato.CadenaTexto = TITLE;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "TITLE");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapadeDatos
{
   public class CLS_Tratamiento:ConexionBase
    {
        public string c_codigo_tra { get; set; }
        public string v_nombre_tra { get; set; }
        public string c_codigo_usu { get; set; }

        public void MtdSeleccionar()
        {


            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_Rent_t_tratamiento_Select";

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
        public void MtdSeleccionarCodigoNombre()
        {
            TipoDato _dato = new TipoDato();
            Exito = true;
            try
            {
                _conexion.NombreProcedimiento = "usp_Rent_t_tratamiento_CN_select";
                _dato.CadenaTexto = c_codigo_tra;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "c_codigo_tra");
                _dato.CadenaTexto = v_nombre_tra;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "v_nombre_tra");
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
                _conexion.NombreProcedimiento = "usp_Rent_t_tratamiento_Delete";

                _dato.CadenaTexto = c_codigo_tra;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "c_codigo_tra");
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
                _conexion.NombreProcedimiento = "usp_Rent_t_tratamiento_Insert";

                _dato.CadenaTexto = c_codigo_tra;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "c_codigo_tra");
                _dato.CadenaTexto = v_nombre_tra;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "v_nombre_tra");
                _dato.CadenaTexto = c_codigo_usu;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "c_codigo_usu");
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
                _conexion.NombreProcedimiento = "usp_Rent_t_tratamiento_Update";

                _dato.CadenaTexto = c_codigo_tra;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "c_codigo_tra");
                _dato.CadenaTexto = v_nombre_tra;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "v_nombre_tra");
                _dato.CadenaTexto = c_codigo_usu;
                _conexion.agregarParametro(EnumTipoDato.CadenaTexto, _dato, "c_codigo_usu");
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

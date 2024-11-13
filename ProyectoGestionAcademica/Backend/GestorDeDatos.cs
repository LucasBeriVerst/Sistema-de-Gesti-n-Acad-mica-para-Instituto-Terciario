using ProyectoGestionAcademica.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGestionAcademica.Backend
{
    internal class GestorDeDatos
    {
        Conexion Instancia_SQL = new Conexion();
        #region Generales
        public bool ValidarCamposDeTexto(params string[] parametros)
        {
            List<string> listaParametros = new List<string>(parametros);

            foreach (string parametro in listaParametros)
            {
                if (string.IsNullOrEmpty(parametro))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region LogIn: Buscar usuario
        public int Form_LogIn_BuscarUsuario(string Usuario, string Contraseña)
        {
            int respuesta = 0;
            int perfil;
            if (Usuario.Length >= 25)
            {
                respuesta = 5;
            }
            else if (Contraseña.Length >= 35)
            {
                respuesta = 6;
            }
            else
            {
                var parametros = new Dictionary<string, object>
                {
                    { "@Usuario_Alumno", Usuario },
                    { "@Contrasenia_Alumno", Contraseña } 
                };

                // Ejecutar el procedimiento para los alumnos
                perfil = Convert.ToInt32(Instancia_SQL.EjecutarEscalar("sp_BuscarUsuarioYContraseñaAlumno", parametros));

                if (perfil == 4)
                {
                    respuesta = perfil;
                }
                else
                {
                    var parametros2 = new Dictionary<string, object>
                    {
                        { "@Usuario_Empleado", Usuario },
                        { "@Contrasenia_Empleado", Contraseña }
                    };

                    // Ejecutar el procedimiento para los empleados
                    perfil = Convert.ToInt32(Instancia_SQL.EjecutarEscalar("sp_BuscarUsuarioYContraseñaEmpleado", parametros2));
                    if (perfil > 0)
                    {
                        respuesta = perfil;
                    }
                    else
                    {
                        respuesta = 8;
                    }
                }
            }
            if (Usuario.Length >= 25 && Contraseña.Length >= 35) { respuesta = 7; }
            return respuesta;
        }
        #endregion
    }
}

using ProyectoGestionAcademica.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
        public DataTable BuscarAlumnosPorCategoria(string valorComboBox, string valorTextBox)
        {
            string parametroNombre;
            switch (valorComboBox)
            {
                case "MATRICULA":
                    parametroNombre = "@Matricula";
                    break;
                case "NOMBRE":
                    parametroNombre = "@Nombre_Alumno";
                    break;
                case "APELLIDO":
                    parametroNombre = "@Apellido_Alumno";
                    break;
                case "DNI":
                    parametroNombre = "@DNI_Alumno";
                    break;
                default:
                    throw new ArgumentException("El valor del ComboBox no es válido.");
            }
            var parametros = new Dictionary<string, object>
            {
                { parametroNombre, valorTextBox }
            };
            return Instancia_SQL.EjecutarQuery("sp_SeleccionarAlumnoAvanzado", parametros);
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
        #region Alumnos
        #region Agregar
        public int Form_Alumnos_AgregarAlumno(string nombre, string apellido, string dni, string calle, string numero, string telefono, string email, string matricula) 
        {
            int resultado = 0;
            if (!dni.All(char.IsDigit))
            {
                resultado = -9;
            }
            try 
            {
                new MailAddress(email);
            }
            catch (Exception ex) 
            {
                resultado = -10;
            }
            if (!matricula.All(char.IsDigit))
            {
                resultado = -11;
            }
            if (resultado == 0) 
            {
                var parametros = new Dictionary<string, object>
                {   { "@ID_Perfil", 4 },
                    { "@Matricula", int.Parse(matricula) },
                    { "@Nombre_Alumno", nombre },
                    { "@Apellido_Alumno", apellido },
                    { "@DNI_Alumno", int.Parse(dni) },
                    { "@Domicilio_Calle", calle ?? (object)DBNull.Value },
                    { "@Domicilio_Numero", string.IsNullOrEmpty(numero) ? (object)DBNull.Value : int.Parse(numero) },
                    { "@Telefono", telefono ?? (object)DBNull.Value },
                    { "@Email", email },
                    { "@Usuario_Alumno", dni },  // Usuario es igual a DNI
                    { "@Contrasenia_Alumno", dni },  // Contraseña es igual a DNI
                    { "@Fecha_Baja", DBNull.Value },
                    { "@Fecha_Alta", DBNull.Value }
                };
                resultado = Instancia_SQL.EjecutarNonQuery("sp_AgregarAlumno", parametros);      
            }
            return resultado;
        }
        #endregion
        #region Editar
        public int EditarAlumno(int idAlumno, int matricula, string nombre, string apellido, int dni, string domicilioCalle, int? domicilioNumero, string telefono, string email, string usuario, string contrasenia, DateTime? fechaBaja, DateTime? fechaAlta)
        {
            int error = 0;
            try
            {
                var parametros = new Dictionary<string, object>
                {
                    { "@ID_Alumno", idAlumno },
                    { "@Matricula", matricula },
                    { "@Nombre_Alumno", nombre },
                    { "@Apellido_Alumno", apellido },
                    { "@DNI_Alumno", dni },
                    { "@Domicilio_Calle", domicilioCalle },
                    { "@Domicilio_Numero", domicilioNumero.HasValue ? (object)domicilioNumero.Value : DBNull.Value }, // Verifica si el valor es null
                    { "@Telefono", telefono },
                    { "@Email", email },
                    { "@Usuario_Alumno", usuario },
                    { "@Contrasenia_Alumno", contrasenia },
                    { "@Fecha_Baja", fechaBaja ?? (object)DBNull.Value },
                    { "@Fecha_Alta", fechaAlta ?? (object)DBNull.Value }
                };
                /*
                 var parametrosNulos = new Dictionary<string, object>
                {
                    { "@ID_Alumno", idAlumno },
                    { "@Matricula", matricula },
                    { "@Nombre_Alumno", nombre },
                    { "@Apellido_Alumno", apellido },
                    { "@DNI_Alumno", dni },
                    { "@Domicilio_Calle", null },
                    { "@Domicilio_Numero", null }, // Verifica si el valor es null
                    { "@Telefono", null },
                    { "@Email", email },
                    { "@Usuario_Alumno", usuario },
                    { "@Contrasenia_Alumno", contrasenia },
                    { "@Fecha_Baja", null },
                    { "@Fecha_Alta", null }
                };
                */
                if (nombre.Length <= 0) { error = -1; }
                if (Regex.IsMatch(nombre, @"\d")) { error = -2; }
                if (apellido.Length <= 0) { error = -3; }
                if (Regex.IsMatch(apellido, @"\d")) { error = -4; }
                if (matricula <= 0) { error = -5; }
                if (dni <= 0) { error = -6; }
                if (email.Length <= 0) 
                {
                    error = -7;
                }
                else 
                {
                    try 
                    {
                        new MailAddress(email);
                    } 
                    catch (Exception e) 
                    { 
                        error = -8;                    
                    }
                }
                if (usuario.Length <= 0) { error = -9; }
                if (contrasenia.Length <= 0) { error = -10; }
                if (error == 0) 
                {
                    var resultado = Instancia_SQL.EjecutarNonQuery("sp_EditarAlumno", parametros);
                    if (resultado > 0)
                    {  
                        return resultado;
                    }
                    else if (resultado == 0)
                    {
                        return resultado;
                    }              
                }
                return error;
            }
            catch (Exception ex)
            {
                error = -11;
                return error;
            }
        }
        #endregion
        #region Eliminar
        public int EliminarAlumno(int id_alumno) 
        {
            int resultado;
            var parametros = new Dictionary<string, object> 
            {
                { "@ID_Alumno", id_alumno }
            };
            resultado = Instancia_SQL.EjecutarNonQuery("sp_EliminarAlumno", parametros);
            return resultado;
        }
        #endregion
        #endregion
    }
}

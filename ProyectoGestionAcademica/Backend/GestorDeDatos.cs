using ProyectoGestionAcademica.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
            int resultado;
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
            return resultado;
        }
        #endregion
        #region Editar
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
        public bool EditarAlumno(int idAlumno, int matricula, string nombre, string apellido, int dni, string domicilioCalle, int domicilioNumero, string telefono, string email, string usuario, string contrasenia, DateTime? fechaBaja, DateTime? fechaAlta)
        {
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
                    { "@Domicilio_Numero", domicilioNumero },
                    { "@Telefono", telefono },
                    { "@Email", email },
                    { "@Usuario_Alumno", usuario },
                    { "@Contrasenia_Alumno", contrasenia },
                    { "@Fecha_Baja", fechaBaja ?? (object)DBNull.Value },
                    { "@Fecha_Alta", fechaAlta ?? (object)DBNull.Value }
                };
                var resultado = Instancia_SQL.EjecutarNonQuery("sp_EditarAlumno", parametros);
                if (resultado > 0)
                {
                    MessageBox.Show("Alumno actualizado exitosamente.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("No se encontró el alumno o no se realizaron cambios." + resultado, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el alumno: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion
        #endregion
    }
}

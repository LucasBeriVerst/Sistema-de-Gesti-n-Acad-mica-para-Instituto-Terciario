using ProyectoGestionAcademica.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ProyectoGestionAcademica.Backend
{
    internal class GestorDeDatos
    {
        Conexion Instancia_SQL = new Conexion();
        private StringWriter consolaWriter;
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
            // Tabla vacía para devolver en caso de error
            DataTable tablaVacia = new DataTable();

            // Validar el valor seleccionado en el ComboBox
            switch (valorComboBox)
            {
                case "MATRICULA":
                    parametroNombre = "@Matricula";
                    // Validar que el valor sea un número entero
                    if (!int.TryParse(valorTextBox, out _))
                    {
                        MessageBox.Show("El valor ingresado para MATRICULA debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return tablaVacia;
                    }
                    break;

                case "DNI":
                    parametroNombre = "@DNI_Alumno";
                    // Validar que el valor sea un número entero
                    if (!int.TryParse(valorTextBox, out _))
                    {
                        MessageBox.Show("El valor ingresado para DNI debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return tablaVacia;
                    }
                    break;

                case "NOMBRE":
                case "APELLIDO":
                    parametroNombre = valorComboBox == "NOMBRE" ? "@Nombre_Alumno" : "@Apellido_Alumno";
                    // Validar que el valor no contenga números
                    if (valorTextBox.Any(char.IsDigit))
                    {
                        MessageBox.Show($"El valor ingresado para {valorComboBox} no debe contener números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return tablaVacia;
                    }
                    break;

                default:
                    throw new ArgumentException("El valor del ComboBox no es válido.");
            }

            // Si pasa las validaciones, ejecutar la consulta
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
        #region Asignar
        public void NombreParaCarrera(ComboBox comboBox) 
        {
            try
            {
                DataTable carreras = Instancia_SQL.EjecutarQuery("sp_ObtenerNombresCarreras");
                comboBox.Items.Clear();
                foreach (DataRow fila in carreras.Rows)
                {
                    comboBox.Items.Add(fila["Nombre_Carrera"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar las carreras: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CargarComboBoxAños(ComboBox comboBoxAños, int idCarrera)
        {
            try
            {
                var parametros = new Dictionary<string, object>
                {
                    { "@ID_Carrera", idCarrera }
                };
                DataTable años = Instancia_SQL.EjecutarQuery("sp_ObtenerAñosPorCarrera", parametros);
                comboBoxAños.Items.Clear();
                foreach (DataRow fila in años.Rows)
                {
                    comboBoxAños.Items.Add(fila["Año"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los años: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public int ObtenerIDCarreraPorNombre(string nombreCarrera)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@Nombre_Carrera", nombreCarrera }
            };

            object resultado = Instancia_SQL.EjecutarEscalar("sp_ObtenerIDCarreraPorNombre", parametros);

            return Convert.ToInt32(resultado);
        }
        public int ObtenerIDAñoDeCarrera(int IdCarrera, int Año)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@ID_Carrera", IdCarrera },
                { "@Año", Año }
            };
            object resultado = Instancia_SQL.EjecutarEscalar("ObtenerIDAñoDeCarrera", parametros);
            if (resultado != null && int.TryParse(resultado.ToString(), out int IdAñoDeCarrera))
            {
                return IdAñoDeCarrera;
            }
            throw new Exception("No se encontró un ID_AñoDeCarrera para los parámetros proporcionados.");
        }
        public DataTable ObtenerMateriasPorCarreraYAño(int IdCarrera, int IdAño)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@ID_Carrera", IdCarrera },
                { "@ID_AñoDeCarrera", IdAño }
            };
            DataTable dataTable = Instancia_SQL.EjecutarQuery("ObtenerMateriasPorCarreraYAño", parametros);
            return dataTable;
        }
        public int ObtenerIDMateriaPorNombre(string nombreMateria, int idCarrera)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@Nombre_Materia", nombreMateria },
                { "@ID_Carrera", idCarrera }
            };
            object resultado = Instancia_SQL.EjecutarEscalar("sp_ObtenerIDMateriaPorNombre", parametros);
            consolaWriter = new StringWriter();
            Console.SetOut(consolaWriter);
            Console.WriteLine(resultado.ToString());
            int valor = (int)resultado;
            //int numeroConvertido = int.TryParse(resultado.ToString());
            int respuesta = 0;
            string x = resultado.ToString();
            if (resultado != null)
            {
                respuesta = Convert.ToInt32(resultado);
                Debug.WriteLine("Resultado convertido a int: " + respuesta);
            }
            else
            {
                Debug.WriteLine("El resultado es nulo.");
            }
            return respuesta;
        }
        public int AgregarAlumnoEnMateria(int idAlumno, int idMateria)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@ID_Alumno", idAlumno },
                { "@ID_Materia", idMateria }
            };

            try
            {
                int filasAfectadas = Instancia_SQL.EjecutarNonQuery("sp_AgregarAlumnoEnMateria", parametros);
                return filasAfectadas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }
        public int EliminarAlumnoDeMateria(int idAlumno, int idMateria)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@ID_Alumno", idAlumno },
                { "@ID_Materia", idMateria }
            };

            try
            {
                int resultado = Instancia_SQL.EjecutarNonQuery("sp_EliminarAlumnoDeMateria", parametros);
                return resultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; 
            }
        }


        #endregion
        #region Informacion
        #endregion
        #endregion
        #region Materias
        #region Agregar
        public void AgregarMateriaSegunNombre(string nombre_Materia) 
        {
            int resultado = 0;
            var parametros = new Dictionary<string, object>
            {   
                { "@Nombre_Materia", nombre_Materia },
            };
            resultado = Instancia_SQL.EjecutarNonQuery("sp_AgregarMateria", parametros);
            if (resultado == 0) { MessageBox.Show($"Ocurrió un error al cargar la materia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else if (resultado == 1) { MessageBox.Show($"La Materia se ingreso correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
        #endregion
        #endregion
    }
}

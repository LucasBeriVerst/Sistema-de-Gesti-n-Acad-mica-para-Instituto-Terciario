using ProyectoGestionAcademica.Frondend.Form7_Usuarios;
using ProyectoGestionAcademica.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
                if (string.IsNullOrEmpty(parametro) || string.IsNullOrWhiteSpace(parametro))
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

        //Metodo generico para obtener una lista de IDs y Nombres (generalmente para cargar ComboBoxs)
        //Se usa una lista de pares clave-valor en vez de un diccionario xq el diccionario no es compaible con los ComboBox
        public List<KeyValuePair<object, string>> ObtenerIDsyNombresGenericos(string nombreTabla, string columnaID, string columnaNombre, string filtro = null)
        {
            //lista vacia para guardar los perfiles
            List<KeyValuePair<object, string>> listaIDsNombres = new List<KeyValuePair<object, string>>();

            //Parametros para el Stored Procedure
            var parametros = new Dictionary<string, object>
            {
                { "@NombreTabla", nombreTabla },
                { "@ColumnaID", columnaID },
                { "@ColumnaNombre", columnaNombre },
                { "@Filtro", filtro }
            };

            try
            {
                
                string nombreProcedimiento;

                if (!string.IsNullOrEmpty(filtro) && filtro.Contains("INNER JOIN"))  // Verifica si es una consulta con JOIN
                {
                    nombreProcedimiento = "sp_ObtenerIDsyNombresGenericosVERSION2";
                }
                else
                {
                    nombreProcedimiento = "sp_ObtenerIDsyNombresGenericos";
                }

                // Llamar al método EjecutarReader
                using (SqlDataReader lector = Instancia_SQL.EjecutarReader(nombreProcedimiento, parametros))
                {
                    while (lector.Read())
                    {
                        object id = lector[columnaID];
                        string nombre = lector[columnaNombre].ToString();
                        listaIDsNombres.Add(new KeyValuePair<object, string>(id, nombre));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listaIDsNombres;  //Retorna la lista completa de perfiles
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
        #region Carreras
        public void AgregarCarrera(string nombreCarrera, string resolucion, string programa)
        {
            // Crear el diccionario de parámetros
            var parametros = new Dictionary<string, object>
            {
                { "@Nombre_Carrera", nombreCarrera },
                { "@Resolucion", resolucion },
                { "@Programa", programa }
            };

            try
            {
                // Ejecutar el procedimiento almacenado
                int filasAfectadas = Instancia_SQL.EjecutarNonQuery("sp_AgregarCarrera", parametros);

                // Validar el resultado
                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Carrera agregada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo agregar la carrera. Verifique los datos ingresados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error al ejecutar el procedimiento almacenado: {ex.Message}", "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable ObtenerCarreraPorNombre(string nombreCarrera)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@Nombre_Carrera", nombreCarrera }
            };
            return Instancia_SQL.EjecutarQuery("sp_ObtenerCarreraPorNombre", parametros);
        }
        public int EditarCarrera(int idCarrera, string nombreCarrera, string resolucion, string programa)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@ID_Carrera", idCarrera },
                { "@Nombre_Carrera", nombreCarrera },
                { "@Resolucion", resolucion },
                { "@Programa", programa }
            };

            return Instancia_SQL.EjecutarNonQuery("sp_EditarCarrera", parametros);
        }
        public int EliminarCarrera(int idCarrera)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@ID_Carrera", idCarrera }
            };

            return Instancia_SQL.EjecutarNonQuery("sp_EliminarCarrera", parametros);
        }

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
        #region Editar
        public void EditarMateria(int idMateria, string nombreMateria)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@ID_Materia", idMateria },
                { "@Nombre_Materia", nombreMateria }
            };
            try
            {
                int filasAfectadas = Instancia_SQL.EjecutarNonQuery("sp_EditarMateria", parametros);
                if (filasAfectadas > 0)
                {
                    MessageBox.Show("La materia se ha actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontró la materia o no se realizaron cambios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } 
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Error SQL: {sqlEx.Message}", "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error general: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable ObtenerMaterias()
        {
            try
            {
                DataTable tablaMaterias = Instancia_SQL.EjecutarQuery("sp_SeleccionarMateriasAvanzado", null);
                return tablaMaterias;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Error SQL: {sqlEx.Message}", "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error general: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; 
            }
        }
        #endregion
        #region Eliminar
        public void EliminarMateria(int idMateria)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@ID_Materia", idMateria }
            };
            try
            {
                int filasAfectadas = Instancia_SQL.EjecutarNonQuery("sp_EliminarMateria", parametros);
                if (filasAfectadas > 0)
                {
                    MessageBox.Show("La materia ha sido eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontró la materia o no se pudo eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Error SQL: {sqlEx.Message}", "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error general: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region
        public int AsignarMateriaACarrera(int idMateria, int idCarrera, int idAñoDeCarrera)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@ID_Materia", idMateria },
                { "@ID_Carrera", idCarrera },
                { "@ID_AñoDeCarrera", idAñoDeCarrera }
            };

            try
            {
                // Ejecutar el procedimiento almacenado
                object resultado = Instancia_SQL.EjecutarNonQuery("sp_AgregarMateriaACarrera", parametros);

                // Convertir el resultado a entero y devolverlo
                return Convert.ToInt32(resultado);
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error SQL: {ex.Message}", "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -99; // Código de error para excepciones SQL
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error general: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -100; // Código de error para excepciones generales
            }
        }
        public void EliminarMateriaDeCarrera(int idMateria, int idCarrera, int idAñoDeCarrera)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@ID_Materia", idMateria },
                { "@ID_Carrera", idCarrera },
                { "@ID_AñoDeCarrera", idAñoDeCarrera }
            };

            try
            {
                // Ejecutar el procedimiento almacenado y obtener el valor de retorno
                int codigoRetorno = Convert.ToInt32(Instancia_SQL.EjecutarNonQuery("sp_EliminarMateriaDeCarrera", parametros));

                if (codigoRetorno == 1)
                {
                    // Éxito, la materia fue desvinculada correctamente
                    MessageBox.Show("La materia ha sido desvinculada correctamente de la carrera y el año especificado.",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (codigoRetorno == 0)
                {
                    // La relación no existe
                    MessageBox.Show("La materia no está asignada a esta carrera y año.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Error inesperado
                    MessageBox.Show("Ocurrió un error al intentar desvincular la materia.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejo de errores SQL
                MessageBox.Show($"Error SQL: {sqlEx.Message}", "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                MessageBox.Show($"Error general: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
        #endregion
        #region Perfil
        public string ObtenerNombreApellidoPorUsuario(string usuario)
        {
            string nombreCompleto = string.Empty;
            var parametros = new Dictionary<string, object>
            {
                { "@Usuario_Alumno", usuario }
            };

            try
            {
                // Ejecutar el procedimiento almacenado y obtener el resultado
                var resultado = Instancia_SQL.EjecutarQuery("sp_ObtenerNombreApellidoPorUsuario", parametros);

                if (resultado.Rows.Count > 0)
                {
                    // Si se encuentra el usuario, concatenar el nombre y apellido
                    string nombre = resultado.Rows[0]["Nombre_Alumno"].ToString();
                    string apellido = resultado.Rows[0]["Apellido_Alumno"].ToString();
                    nombreCompleto = $"{nombre} {apellido}";
                }
                else
                {
                    // Si no se encuentra el usuario
                    nombreCompleto = "Usuario no encontrado.";
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al obtener los datos del alumno: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return nombreCompleto;
        }
        public string ObtenerNombreApellidoPorUsuarioEmpleado(string usuario)
        {
            string nombreCompleto = string.Empty;
            var parametros = new Dictionary<string, object>
            {
                { "@Usuario_Empleado", usuario }
            };

            try
            {
                // Ejecutar el procedimiento almacenado y obtener el resultado en un DataTable
                DataTable resultado = Instancia_SQL.EjecutarQuery("sp_ObtenerNombreApellidoPorUsuarioEmpleado", parametros);

                if (resultado.Rows.Count > 0)
                {
                    string nombre = resultado.Rows[0]["Nombre_Empleado"].ToString();
                    string apellido = resultado.Rows[0]["Apellido_Empleado"].ToString();
                    nombreCompleto = $"{nombre} {apellido}";
                }
                else
                {
                    nombreCompleto = "Empleado no encontrado.";
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al obtener los datos del empleado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return nombreCompleto;
        }
        public string ObtenerContraseniaPorUsuarioAlumno(string usuarioAlumno)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@Usuario_Alumno", usuarioAlumno }
            };

            try
            {
                // Ejecutar el procedimiento almacenado y obtener el resultado
                DataTable resultado = Instancia_SQL.EjecutarQuery("sp_ObtenerContraseniaPorUsuarioAlumno", parametros);

                // Verificar si se encontraron registros
                if (resultado.Rows.Count > 0)
                {
                    string contrasenia = resultado.Rows[0]["Contrasenia_Alumno"].ToString();
                    return contrasenia; // Retorna la contraseña
                }
                else
                {
                    // Si no se encuentra el usuario, devuelve un valor vacío o nulo
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al obtener la contraseña: {ex.Message}");
                return null;
            }
        }
        public bool CambiarContraseniaAlumno(string usuarioAlumno, string nuevaContrasenia)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@Usuario_Alumno", usuarioAlumno },
                { "@Nueva_Contrasenia", nuevaContrasenia }
            };

            try
            {
                // Ejecutar el procedimiento almacenado y obtener el valor de retorno
                int resultado = Convert.ToInt32(Instancia_SQL.EjecutarEscalar("sp_CambiarContraseniaAlumno", parametros));

                // Si el resultado es 1, la contraseña fue cambiada con éxito
                return resultado == 1;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al cambiar la contraseña: {ex.Message}");
                return false;
            }
        }
        public string ObtenerContraseniaPorUsuarioEmpleado(string usuarioEmpleado)
        {
            // Diccionario con los parámetros a pasar al procedimiento almacenado
            var parametros = new Dictionary<string, object>
            {
                { "@Usuario_Empleado", usuarioEmpleado }
            };

            try
            {
                // Ejecutar el procedimiento almacenado y obtener el resultado (la contraseña)
                object resultado = Instancia_SQL.EjecutarEscalar("sp_ObtenerContraseniaPorUsuarioEmpleado", parametros);

                // Si el resultado no es null, devolver la contraseña como un string
                if (resultado != null)
                {
                    return resultado.ToString();
                }
                else
                {
                    return null; // Si no se encontró la contraseña, devolver null
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al obtener la contraseña: {ex.Message}");
                return null;
            }
        }
        public int CambiarContraseniaEmpleado(string usuarioEmpleado, string nuevaContrasenia)
        {
            // Diccionario con los parámetros a pasar al procedimiento almacenado
            var parametros = new Dictionary<string, object>
            {
                { "@Usuario_Empleado", usuarioEmpleado },
                { "@Nueva_Contrasenia_Empleado", nuevaContrasenia }
            };

            try
            {
                // Ejecutar el procedimiento almacenado y obtener el resultado (1 o 0)
                int resultado = Instancia_SQL.EjecutarNonQuery("sp_CambiarContraseniaEmpleado", parametros);

                // Retornar el resultado (1 si fue exitoso, 0 si no se encontró el usuario)
                return resultado;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al cambiar la contraseña: {ex.Message}");
                return 0;
            }
        }


        #endregion

        #region Usuarios
        #region Metodos Genericos Para Usuarios

        //Arma los parametros y se los pasa al Store BuscarUsuario, para cargar el DataGridView
        public DataTable BuscarUsuariosPorNombreApellidoDNI(string? valorNombre, string? valorApellido, string? valorDNI)
        {
            DataTable tablaVacia = new DataTable();

            //validacions para nombre
            if (valorNombre != null)
            {
                if (valorNombre.Length < 1 || valorNombre.Length > 30 || !valorNombre.All(char.IsLetter))
                {
                    MessageBox.Show("El nombre debe tener entre 1 y 30 letras y no puede contener números ni caracteres especiales.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return tablaVacia; // Se retorna una tabla vacía en caso de error.
                }
            }

            //validacions para apellido
            if (valorApellido != null)
            {
                if (valorApellido.Length < 1 || valorApellido.Length > 30 || !valorApellido.All(char.IsLetter))
                {
                    MessageBox.Show("El apellido debe tener entre 1 y 30 letras y no puede contener números ni caracteres especiales.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return tablaVacia; // Se retorna una tabla vacía en caso de error.
                }
            }

            //validacion para dni para ver si son digitos
            if (valorDNI != null)
            {
                if (!valorDNI.All(char.IsDigit) || valorDNI.Length < 7 || valorDNI.Length > 10)
                {
                    MessageBox.Show("El DNI debe contener entre 7 y 10 dígitos y solo Numeros Enteros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return tablaVacia; // Se retorna una tabla vacía en caso de error.
                }
            }

            //Si pasa todas las validaciones se ejecuta la consulta
            var parametros = new Dictionary<string, object>
            {
                {"@Nombre_Empleado", valorNombre ?? (object)DBNull.Value},      //Para verificar nulos. Sintaxis: variable ?? valorPorDefecto
                {"@Apellido_Empleado", valorApellido ?? (object)DBNull.Value},  //Verifica si la variable es null. Si lo es, retorna valorPorDefecto. Sino, retorna el valor de la variable
                {"@DNI_Empleado",  string.IsNullOrWhiteSpace(valorDNI) ? (object)DBNull.Value : Convert.ToInt32(valorDNI)}, //si es null o vacio indica a bdd que es null; sino convierte a entero el numero}
            };

            return Instancia_SQL.EjecutarQuery("sp_SeleccionarEmpleadoAvanzado", parametros);
        }

        #endregion
        #region Agregar

        public int Form_Usuarios_AgregarUsuario(string perfil, string nombre, string apellido, string dni, string calle, string numero, string telefono, string email, string usuario, string contraseña, DateTime? fechaAlta, DateTime? fechaBaja)
        {
            #region Validaciones de Campos
            
            int resultado = 0;

            // Validación para perfil (debe ser numérico)
            if (!int.TryParse(perfil, out int idPerfil) || idPerfil <= 0)
            {
                resultado = -20;  // Retorna error de perfil inválido
            }

            //validacion para nombre para que no agreguen espacios en blanco
            if (string.IsNullOrWhiteSpace(nombre) || !nombre.All(char.IsLetter) || nombre == "NOMBRE")
            {
                resultado = -21;
            }

            //validacion para apellido para que no agreguen espacios en blanco
            if (string.IsNullOrWhiteSpace(apellido) || !apellido.All(char.IsLetter) || apellido == "APELLIDO")
            {
                resultado = -22;
            }

            //validacion para dni para ver si son digitos
            if (!dni.All(char.IsDigit))
            {
                resultado = -9;
            }

            //validacion para calle para que no agreguen espacios en blanco
            if (calle == "DOMICILIO (CALLE)")
            {
                calle = null;
            }
            else if (string.IsNullOrWhiteSpace(calle) || !calle.All(char.IsLetter))
            {
                resultado = -23;
            }          

            //validacion para numero para ver si son digitos
            if (numero == "DOMICILIO (NUMERO)")
            {
                numero = null;
            }
            else if (!numero.All(char.IsDigit))
            {
                resultado = -24;
            }

            //validacion para telefono
            if (telefono == "TELEFONO")
            {
                telefono = null;
            }
            else if (telefono.All(char.IsLetter))
            {
                resultado = -28;
            }

            //validacion de formato de mail
            try
            {
                new MailAddress(email);
            }
            catch (Exception ex)
            {
                resultado = -10;
            }

            //validacion para usuario para que no sea nulo y que no sean espacios en blanco
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrWhiteSpace(usuario) || usuario == "USUARIO")
            {
                resultado = -25;
            }

            //validacion para contraseña para que no tenga espacios en blanco
            if (string.IsNullOrWhiteSpace(contraseña) || contraseña == "CONTRASEÑA")
            {
                resultado = -26;
            }

            //Validacion de fecha para que la fecha de baja no sea anterior a la fecha de alta
                //si fechaAlta tiene valor y fechaBaja tiene valor y fechaBaja es menor a fechaAlta entonces error
            if (fechaAlta.HasValue && fechaBaja.HasValue && fechaBaja.Value < fechaAlta.Value)
            {
                resultado = -27;                                
            }
            #endregion

            if (resultado == 0)
            {
                var parametros = new Dictionary<string, object>
                {
                    {"@ID_Perfil", idPerfil},   //idPerfil ya es int en esta parte
                    {"@Nombre_Empleado", nombre},
                    {"@Apellido_Empleado", apellido},
                    {"@DNI_Empleado", int.Parse(dni)},
                    {"@Domicilio_Calle", calle ?? (object)DBNull.Value},    //si NO es nulo devuelve calle, y si es nulo indica a la bdd que va a ser nulo
                    {"@Domicilio_Numero", string.IsNullOrEmpty(numero) ? (object)DBNull.Value : int.Parse(numero)}, //si es null o vacio indica a bdd que es null; sino convierte a entero el numero
                    {"@Telefono", telefono ?? (object)DBNull.Value},
                    {"@Email", email},
                    {"@Usuario_Empleado", usuario},
                    {"@Contrasenia_Empleado", contraseña},
                    {"@Fecha_Baja", fechaBaja ?? (object)DBNull.Value}, //Para verificar nulos. Sintaxis: variable ?? valorPorDefecto
                    {"@Fecha_Alta", fechaAlta ?? (object)DBNull.Value}  //Verifica si la variable es null. Si lo es, retorna valorPorDefecto. Sino, retorna el valor de la variable
                };
                resultado = Instancia_SQL.EjecutarNonQuery("sp_AgregarEmpleado", parametros);
            }
            return resultado;   //devuelve el numero de filas afectadas
        }

        #endregion
        #region Editar
        public int EditarUsuario(int id, string nombre, string apellido, string dni, string perfil, string calle, string numero, string telefono, string email, string usuario, string contraseña, DateTime? fechaAlta, DateTime? fechaBaja)
        {
            #region Validaciones de Campos

            int resultado = 0;

            // Validación para perfil (debe ser numérico)
            if (!int.TryParse(perfil, out int idPerfil) || idPerfil <= 0)
            {
                resultado = -20;  // Retorna error de perfil inválido
            }

            //validacion para nombre para que no agreguen espacios en blanco
            if (string.IsNullOrWhiteSpace(nombre) || !nombre.All(char.IsLetter) || nombre == "NOMBRE")
            {
                resultado = -21;
            }

            //validacion para apellido para que no agreguen espacios en blanco
            if (string.IsNullOrWhiteSpace(apellido) || !apellido.All(char.IsLetter) || apellido == "APELLIDO")
            {
                resultado = -22;
            }

            //validacion para dni para ver si son digitos
            if (!dni.All(char.IsDigit))
            {
                resultado = -9;
            }

            //validacion para calle para que no agreguen espacios en blanco
            if (calle == "DOMICILIO (CALLE)")
            {
                calle = null;
            }
            else if (string.IsNullOrWhiteSpace(calle) || !calle.All(char.IsLetter))
            {
                resultado = -23;
            }

            //validacion para numero para ver si son digitos
            if (numero == "DOMICILIO (NUMERO)")
            {
                numero = null;
            }
            else if (!numero.All(char.IsDigit))
            {
                resultado = -24;
            }

            //validacion para telefono
            if (telefono == "TELEFONO")
            {
                telefono = null;
            }
            else if (telefono.All(char.IsLetter))
            {
                resultado = -28;
            }

            //validacion de formato de mail
            try
            {
                new MailAddress(email);
            }
            catch (Exception ex)
            {
                resultado = -10;
            }

            //validacion para usuario para que no sea nulo y que no sean espacios en blanco
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrWhiteSpace(usuario) || usuario == "USUARIO")
            {
                resultado = -25;
            }

            //validacion para contraseña para que no tenga espacios en blanco
            if (string.IsNullOrWhiteSpace(contraseña) || contraseña == "CONTRASEÑA")
            {
                resultado = -26;
            }

            //Validacion de fecha para que la fecha de baja no sea anterior a la fecha de alta
            //si fechaAlta tiene valor y fechaBaja tiene valor y fechaBaja es menor a fechaAlta entonces error
            if (fechaAlta.HasValue && fechaBaja.HasValue && fechaBaja.Value < fechaAlta.Value)
            {
                resultado = -27;
            }
            #endregion

            if (resultado == 0)
            {
                var parametros = new Dictionary<string, object>
                {
                    {"@ID_Empleado", id}, // Se agrega el ID del empleado
                    {"@ID_Perfil", idPerfil},   //idPerfil ya es int en esta parte
                    {"@Nombre_Empleado", nombre},
                    {"@Apellido_Empleado", apellido},
                    {"@DNI_Empleado", int.Parse(dni)},
                    {"@Domicilio_Calle", calle ?? (object)DBNull.Value},    //si NO es nulo devuelve calle, y si es nulo indica a la bdd que va a ser nulo
                    {"@Domicilio_Numero", string.IsNullOrEmpty(numero) ? (object)DBNull.Value : int.Parse(numero)}, //si es null o vacio indica a bdd que es null; sino convierte a entero el numero
                    {"@Telefono", telefono ?? (object)DBNull.Value},
                    {"@Email", email},
                    {"@Usuario_Empleado", usuario},
                    {"@Contrasenia_Empleado", contraseña},
                    {"@Fecha_Baja", fechaBaja ?? (object)DBNull.Value}, //Para verificar nulos. Sintaxis: variable ?? valorPorDefecto
                    {"@Fecha_Alta", fechaAlta ?? (object)DBNull.Value}  //Verifica si la variable es null. Si lo es, retorna valorPorDefecto. Sino, retorna el valor de la variable
                };
                resultado = Instancia_SQL.EjecutarNonQuery("sp_EditarEmpleado", parametros);
            }
            return resultado;   //devuelve el numero de filas afectadas
        }

        #endregion
        #region Eliminar
        public int EliminarUsuario(int id)
        {
            int resultado;

            var parametros = new Dictionary<string, object>
            {
                { "@ID_Empleado", id }
            };
            resultado = Instancia_SQL.EjecutarNonQuery("sp_EliminarEmpleado", parametros);

            return resultado;
        }

        #endregion
        #region Asignar
        public int AsignarProfesorAMateria(int idProfesor, int idCarrera, int idAño, int idMateria)
        {
            int resultado;

            var parametros = new Dictionary<string, object>
            {
                { "@ID_Empleado", idProfesor },
                { "@ID_Carrera", idCarrera },
                { "@ID_AñoDeCarrera", idAño },
                { "@ID_Materia", idMateria }
            };

            try
            {
                resultado = Instancia_SQL.EjecutarNonQuery("sp_AsignarDocenteAMateria", parametros);
                return resultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }   
        }

        #endregion
        #endregion

        #region Examenes
        #region Metodos Genericos Para Examenes
        public DataTable BuscarAlumnosPorNombreApellidoDNIMatricula( string? valorNombre, string? valorApellido, string? valorDNI, string? valorMatricula)
        {
            DataTable tablaVacia = new DataTable();
            
            //validacions para nombre
            if (valorNombre != null)
            {
                if (valorNombre.Length < 1 || valorNombre.Length > 30 || !valorNombre.All(char.IsLetter))
                {
                    MessageBox.Show("El nombre debe tener entre 1 y 30 letras y no puede contener números ni caracteres especiales.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return tablaVacia; // Se retorna una tabla vacía en caso de error.
                }
            }
            
            //validacions para apellido
            if (valorApellido != null)
            {
                if (valorApellido.Length < 1 || valorApellido.Length > 30 || !valorApellido.All(char.IsLetter))
                {
                    MessageBox.Show("El apellido debe tener entre 1 y 30 letras y no puede contener números ni caracteres especiales.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return tablaVacia; // Se retorna una tabla vacía en caso de error.
                }
            }

            //validacion para dni para ver si son digitos
            if (valorDNI != null)
            {
                if (!valorDNI.All(char.IsDigit) || valorDNI.Length < 7 || valorDNI.Length > 10)
                {
                    MessageBox.Show("El DNI debe contener entre 7 y 10 dígitos y solo Numeros Enteros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return tablaVacia; // Se retorna una tabla vacía en caso de error..
                }
            }

            //validacion para Matricula para ver si son digitos
            if (valorMatricula != null)
            {
                if (!valorMatricula.All(char.IsDigit))
                {
                    MessageBox.Show("La Matricula debe contener solo Numeros Enteros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return tablaVacia; // Se retorna una tabla vacía en caso de error.
                }
            }
            
            //Si pasa todas las validaciones se ejecuta la consulta
            var parametros = new Dictionary<string, object>
            {
                {"@Matricula", string.IsNullOrWhiteSpace(valorMatricula) ? (object)DBNull.Value : Convert.ToInt32(valorMatricula)},
                {"@Nombre_Alumno", valorNombre ?? (object)DBNull.Value},      //Para verificar nulos. Sintaxis: variable ?? valorPorDefecto
                {"@Apellido_Alumno", valorApellido ?? (object)DBNull.Value},  //Verifica si la variable es null. Si lo es, retorna valorPorDefecto. Sino, retorna el valor de la variable
                {"@DNI_Alumno",  string.IsNullOrWhiteSpace(valorDNI) ? (object)DBNull.Value : Convert.ToInt32(valorDNI)}, //si es null o vacio indica a bdd que es null; sino convierte a entero el numero}
            };

            return Instancia_SQL.EjecutarQuery("sp_SeleccionarAlumnoAvanzado", parametros);
        }
        #endregion
        #endregion
    }
}

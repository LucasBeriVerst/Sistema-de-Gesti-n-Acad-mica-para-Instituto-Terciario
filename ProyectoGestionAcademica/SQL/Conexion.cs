using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoGestionAcademica.SQL
{
    internal class Conexion
    {
        private string cadena = "Data Source=DESKTOP-GO1RSK2\\SQLEXPRESS;Initial Catalog=Proyecto_Algoritmos_BDD;Integrated Security=True;TrustServerCertificate=True;";

        private SqlConnection conexion; //el objeto para manejar la conexión a la base de datos

        //constructor
        public Conexion()
        {
            conexion = new SqlConnection(cadena); //se crea la conexión con la cadena de conexión
        }

        public void Abrir()
        {
            // verifica si la conexión está cerrada antes de abrirla
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open(); //abre la conexión a la base de datos
            }
        }

        public void Cerrar()
        {
            // verifica si la conexión está abierta antes de cerrarla
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close(); //cierra la conexión a la base de datos
            }
        }
        // metodo que devuelve la conexión actual
        public SqlConnection ObtenerConexion()
        {
            return conexion; //retorna el objeto SqlConnection
        }
        public int EjecutarNonQuery(string nombreProcedimiento, Dictionary<string, object> parametros = null)
        {
            int filasAfectadas = 0;
            try
            {
                Abrir();
                using (SqlCommand comando = new SqlCommand(nombreProcedimiento, conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        foreach (var parametro in parametros)
                        {
                            comando.Parameters.AddWithValue(parametro.Key, parametro.Value);
                        }
                    }
                    filasAfectadas = comando.ExecuteNonQuery();
                }
            }
            finally
            {
                Cerrar();
            }
            return filasAfectadas;
        }

        public object EjecutarEscalar(string nombreProcedimiento, Dictionary<string, object> parametros = null)
        {
            object resultado = null;
            try
            {
                Abrir();
                using (SqlCommand comando = new SqlCommand(nombreProcedimiento, conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        foreach (var parametro in parametros)
                        {
                            comando.Parameters.AddWithValue(parametro.Key, parametro.Value);
                        }
                    }
                    resultado = comando.ExecuteScalar();
                }
            }
            finally
            {
                Cerrar();
            }
            return resultado;
        }
        public DataTable EjecutarQuery(string nombreProcedimiento, Dictionary<string, object> parametros = null)
        {
            DataTable tabla = new DataTable();
            try
            {
                Abrir();
                using (SqlCommand comando = new SqlCommand(nombreProcedimiento, conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        foreach (var parametro in parametros)
                        {
                            comando.Parameters.AddWithValue(parametro.Key, parametro.Value);
                        }
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        adapter.Fill(tabla);
                    }
                }
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }

    }

}


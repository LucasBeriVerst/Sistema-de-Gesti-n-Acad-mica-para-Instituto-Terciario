using ProyectoGestionAcademica.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoGestionAcademica.Frondend.Form4_Carreras
{
    public partial class Form4_DashBoardCarreras_2_Editar : Form, IConfiguracion
    {
        private GestorDeDatos gestorDeDatos = new GestorDeDatos();
        private string titulo = "CARRERAS: EDITAR";
        public Form4_DashBoardCarreras_2_Editar()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form4_DashBoardCarreras_2_Editar_Load(object sender, EventArgs e)
        {
            gestorDeDatos.NombreParaCarrera(Form4_DashBoardCarreras_2_Editar_ComboBox_SeleccionarCarrera);
            Form4_DashBoardCarreras_2_Editar_ComboBox_SeleccionarCarrera.SelectedIndex = 0;
        }

        private void Form4_DashBoardCarreras_2_Editar_ComboBox_SeleccionarCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre de la carrera seleccionada
            string nombreCarrera = Form4_DashBoardCarreras_2_Editar_ComboBox_SeleccionarCarrera.SelectedItem.ToString();

            // Obtener los datos de la carrera
            DataTable datosCarrera = gestorDeDatos.ObtenerCarreraPorNombre(nombreCarrera);

            if (datosCarrera.Rows.Count > 0)
            {
                // Asignar los valores a los TextBox
                Form4_DashBoardCarreras_2_Editar_TextBox_NombreCarrera.Text = datosCarrera.Rows[0]["Nombre_Carrera"].ToString();
                Form4_DashBoardCarreras_2_Editar_TextBox_Resolucion.Text = datosCarrera.Rows[0]["Resolucion"].ToString();
                Form4_DashBoardCarreras_2_Editar_TextBox_Programa.Text = datosCarrera.Rows[0]["Programa"].ToString();
            }
            else
            {
                // Limpiar los TextBox si no se encuentra la carrera
                Form4_DashBoardCarreras_2_Editar_TextBox_NombreCarrera.Text = string.Empty;
                Form4_DashBoardCarreras_2_Editar_TextBox_Resolucion.Text = string.Empty;
                Form4_DashBoardCarreras_2_Editar_TextBox_Programa.Text = string.Empty;

                MessageBox.Show("No se encontraron datos para la carrera seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Form4_DashBoardCarreras_2_Editar_PanelInferior_Button_Agregar_Click(object sender, EventArgs e)
        {
            int idCarrera = gestorDeDatos.ObtenerIDCarreraPorNombre(Form4_DashBoardCarreras_2_Editar_ComboBox_SeleccionarCarrera.SelectedItem.ToString());
            int resultado = gestorDeDatos.EditarCarrera(idCarrera, Form4_DashBoardCarreras_2_Editar_TextBox_NombreCarrera.Text, Form4_DashBoardCarreras_2_Editar_TextBox_Resolucion.Text, Form4_DashBoardCarreras_2_Editar_TextBox_Programa.Text);
            if (resultado > 0)
            {
                MessageBox.Show("Se modifico la carrera con exito.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gestorDeDatos.NombreParaCarrera(Form4_DashBoardCarreras_2_Editar_ComboBox_SeleccionarCarrera);
                Form4_DashBoardCarreras_2_Editar_ComboBox_SeleccionarCarrera.SelectedIndex = 0;
            }
        }

        private void Form4_DashBoardCarreras_2_Editar_PanelInferior_Button_Cancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seguro que queres cerrar la pagina actual. Se perderan los datos ingresados...", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}

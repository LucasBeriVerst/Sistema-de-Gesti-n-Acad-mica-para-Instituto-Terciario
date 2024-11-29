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
    public partial class Form4_DashBoardCarreras_1_Agregar : Form, IConfiguracion
    {
        private GestorDeDatos gestorDeDatos = new GestorDeDatos();
        private string titulo = "CARRERAS: AGREGAR";
        public Form4_DashBoardCarreras_1_Agregar()
        {
            InitializeComponent();
            Titulo = titulo;
        }

        public string Titulo { get => titulo; set => titulo = value; }

        private void Form4_DashBoardCarreras_1_Agregar_Load(object sender, EventArgs e)
        {

        }

        private void Form4_DashBoardCarreras_1_Agregar_PanelInferior_Button_Cancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seguro que queres cerrar la pagina actual. Se perderan los datos ingresados...", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Form4_DashBoardCarreras_1_Agregar_TextBox_NombreCarrera_Leave(object sender, EventArgs e)
        {
            if (Form4_DashBoardCarreras_1_Agregar_TextBox_NombreCarrera.Text == string.Empty) { Form4_DashBoardCarreras_1_Agregar_TextBox_NombreCarrera.Text = "NOMBRE"; }
            Form4_DashBoardCarreras_1_Agregar_TextBox_NombreCarrera.ForeColor = Color.DimGray;
        }

        private void Form4_DashBoardCarreras_1_Agregar_TextBox_NombreCarrera_Enter(object sender, EventArgs e)
        {
            if (Form4_DashBoardCarreras_1_Agregar_TextBox_NombreCarrera.Text == "NOMBRE") { Form4_DashBoardCarreras_1_Agregar_TextBox_NombreCarrera.Text = string.Empty; }
            Form4_DashBoardCarreras_1_Agregar_TextBox_NombreCarrera.ForeColor = Color.Black;
        }

        private void Form4_DashBoardCarreras_1_Agregar_TextBox_Resolucion_Enter(object sender, EventArgs e)
        {
            if (Form4_DashBoardCarreras_1_Agregar_TextBox_Resolucion.Text == "RESOLUCION") { Form4_DashBoardCarreras_1_Agregar_TextBox_Resolucion.Text = string.Empty; }
            Form4_DashBoardCarreras_1_Agregar_TextBox_Resolucion.ForeColor = Color.Black;
        }

        private void Form4_DashBoardCarreras_1_Agregar_TextBox_Resolucion_Leave(object sender, EventArgs e)
        {
            if (Form4_DashBoardCarreras_1_Agregar_TextBox_Resolucion.Text == string.Empty) { Form4_DashBoardCarreras_1_Agregar_TextBox_Resolucion.Text = "RESOLUCION"; }
            Form4_DashBoardCarreras_1_Agregar_TextBox_Resolucion.ForeColor = Color.DimGray;
        }

        private void Form4_DashBoardCarreras_1_Agregar_TextBox_Programa_Enter(object sender, EventArgs e)
        {
            if (Form4_DashBoardCarreras_1_Agregar_TextBox_Programa.Text == "PROGRAMA") { Form4_DashBoardCarreras_1_Agregar_TextBox_Programa.Text = string.Empty; }
            Form4_DashBoardCarreras_1_Agregar_TextBox_Programa.ForeColor = Color.Black;
        }

        private void Form4_DashBoardCarreras_1_Agregar_TextBox_Programa_Leave(object sender, EventArgs e)
        {
            if (Form4_DashBoardCarreras_1_Agregar_TextBox_Programa.Text == string.Empty) { Form4_DashBoardCarreras_1_Agregar_TextBox_Programa.Text = "PROGRAMA"; }
            Form4_DashBoardCarreras_1_Agregar_TextBox_Resolucion.ForeColor = Color.DimGray;
        }

        private void Form4_DashBoardCarreras_1_Agregar_PanelInferior_Button_Agregar_Click(object sender, EventArgs e)
        {
            // Declarar variables con valores iniciales
            string nombre = string.Empty;
            string resolucion = string.Empty;
            string programa = string.Empty;

            // Asignar valores si los campos no están vacíos y no contienen el texto por defecto
            if (!string.IsNullOrEmpty(Form4_DashBoardCarreras_1_Agregar_TextBox_NombreCarrera.Text) &&
                Form4_DashBoardCarreras_1_Agregar_TextBox_NombreCarrera.Text != "NOMBRE")
            {
                nombre = Form4_DashBoardCarreras_1_Agregar_TextBox_NombreCarrera.Text;
            }

            if (!string.IsNullOrEmpty(Form4_DashBoardCarreras_1_Agregar_TextBox_Resolucion.Text) &&
                Form4_DashBoardCarreras_1_Agregar_TextBox_Resolucion.Text != "RESOLUCION")
            {
                resolucion = Form4_DashBoardCarreras_1_Agregar_TextBox_Resolucion.Text;
            }

            if (!string.IsNullOrEmpty(Form4_DashBoardCarreras_1_Agregar_TextBox_Programa.Text) &&
                Form4_DashBoardCarreras_1_Agregar_TextBox_Programa.Text != "PROGRAMA")
            {
                programa = Form4_DashBoardCarreras_1_Agregar_TextBox_Programa.Text;
            }

            // Verificar que todos los campos necesarios tengan valores
            if (!string.IsNullOrEmpty(nombre) &&
                !string.IsNullOrEmpty(resolucion) &&
                !string.IsNullOrEmpty(programa))
            {
                // Llamar al método para agregar la carrera
                gestorDeDatos.AgregarCarrera(nombre, resolucion, programa);

                // Mensaje de confirmación
                MessageBox.Show("La carrera se agregó correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Opcional: limpiar los campos del formulario
                Form4_DashBoardCarreras_1_Agregar_TextBox_NombreCarrera.Text = "NOMBRE";
                Form4_DashBoardCarreras_1_Agregar_TextBox_Resolucion.Text = "RESOLUCION";
                Form4_DashBoardCarreras_1_Agregar_TextBox_Programa.Text = "PROGRAMA";
            }
            else
            {
                // Mensaje de error si algún campo está vacío
                MessageBox.Show("Por favor, complete todos los campos antes de agregar una carrera.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}

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

namespace ProyectoGestionAcademica.Frondend.Form5_Materias
{
    public partial class Form5_DashBoardMaterias_1_Agregar : Form, IConfiguracion
    {
        private GestorDeDatos gestorDeDatos = new GestorDeDatos();
        private string titulo = "MATERIAS: AGREGAR";
        public Form5_DashBoardMaterias_1_Agregar()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form5_DashBoardMaterias_1_Agregar_Load(object sender, EventArgs e)
        {

        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre_Enter(object sender, EventArgs e)
        {
            if (Form5_DashBoardMaterias_1_Agregar_TextBox_Materia.Text == "NOMBRE DE MATERIA") { Form5_DashBoardMaterias_1_Agregar_TextBox_Materia.Text = string.Empty; }
            Form5_DashBoardMaterias_1_Agregar_TextBox_Materia.ForeColor = Color.Black;
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre_Leave(object sender, EventArgs e)
        {
            if (Form5_DashBoardMaterias_1_Agregar_TextBox_Materia.Text == string.Empty) { Form5_DashBoardMaterias_1_Agregar_TextBox_Materia.Text = "NOMBRE DE MATERIA"; }
            Form5_DashBoardMaterias_1_Agregar_TextBox_Materia.ForeColor = Color.DimGray;
        }

        private void Form5_DashBoardMaterias_1_Agregar_Button_Cancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seguro que queres cerrar la pagina actual. Se perderan los datos ingresados...", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Form5_DashBoardMaterias_1_Agregar_Button_Agregar_Click(object sender, EventArgs e)
        {
            if (Form5_DashBoardMaterias_1_Agregar_TextBox_Materia.Text != "NOMBRE DE MATERIA" && Form5_DashBoardMaterias_1_Agregar_TextBox_Materia.Text != string.Empty) 
            {
                gestorDeDatos.AgregarMateriaSegunNombre(Form5_DashBoardMaterias_1_Agregar_TextBox_Materia.Text);
            }
        }
    }
}

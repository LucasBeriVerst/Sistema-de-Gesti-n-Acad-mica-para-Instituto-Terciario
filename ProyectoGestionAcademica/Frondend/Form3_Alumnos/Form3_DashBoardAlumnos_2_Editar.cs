using ProyectoGestionAcademica.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoGestionAcademica.Frondend
{
    public partial class Form3_DashBoardAlumnos_2_Editar : Form, IConfiguracion
    {
        private string titulo = "ALUMNOS: EDITAR";
        private GestorDeDatos gestorDeDatos = new GestorDeDatos();
        public Form3_DashBoardAlumnos_2_Editar()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form3_DashBoardAlumnos_2_Editar_Load(object sender, EventArgs e)
        {

        }

        private void Form3_DashBoardAlumnos_2_Editar_PanelInferior_Button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form3_DashBoardAlumnos_2_Editar_PanelInferior_Button_Buscar_Click(object sender, EventArgs e)
        {
            List<string> listaActual = new List<string>();
            List<string> listaNueva = new List<string>();
            if (Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Textbox_Dni.Text == string.Empty || Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Textbox_Matricula.Text == string.Empty) 
            {
                MessageBox.Show("Debe ingresar un id o un documento para buscar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
            else 
            {
                listaActual = gestorDeDatos.SeleccionarAlumnoPorDniOMatricula(int.Parse(Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Textbox_Dni.Text), int.Parse(Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Textbox_Matricula.Text));
            }
        }
    }
}

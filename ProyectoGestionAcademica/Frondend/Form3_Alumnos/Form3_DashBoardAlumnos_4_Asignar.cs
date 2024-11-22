using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoGestionAcademica.Frondend
{
    public partial class Form3_DashBoardAlumnos_4_Asignar : Form, IConfiguracion
    {
        private string titulo = "ALUMNOS: ASIGNAR";
        public Form3_DashBoardAlumnos_4_Asignar()
        {
            InitializeComponent();
            Titulo = titulo;
            Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_Combobox_TipoDeBusqueda.SelectedIndex = 0;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form3_DashBoardAlumnos_4_Asignar_Load(object sender, EventArgs e)
        {

        }

        private void Form3_DashBoardAlumnos_4_Asignar_PanelInferior_Button_Cancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seguro que queres cerrar la pagina actual. Se perderan los datos ingresados...", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_Textbox_ValorDeBusqueda_Enter(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_Textbox_ValorDeBusqueda.Text == "VALOR DE BUSQUEDA") { Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_Textbox_ValorDeBusqueda.Text = string.Empty; }
            Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_Textbox_ValorDeBusqueda.ForeColor = Color.Black;
        }

        private void Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_Textbox_ValorDeBusqueda_Leave(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_Textbox_ValorDeBusqueda.Text == string.Empty) { Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_Textbox_ValorDeBusqueda.Text = "VALOR DE BUSQUEDA"; }
            Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_Textbox_ValorDeBusqueda.ForeColor = Color.DimGray;
        }
    }
}

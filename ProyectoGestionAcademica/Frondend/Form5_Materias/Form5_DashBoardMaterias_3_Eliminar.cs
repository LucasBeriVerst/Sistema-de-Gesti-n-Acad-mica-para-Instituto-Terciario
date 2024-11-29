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
    public partial class Form5_DashBoardMaterias_3_Eliminar : Form, IConfiguracion
    {
        private GestorDeDatos gestorDeDatos = new GestorDeDatos();
        private string titulo = "MATERIAS: ELIMINAR";
        public Form5_DashBoardMaterias_3_Eliminar()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form5_DashBoardMaterias_3_Eliminar_Load(object sender, EventArgs e)
        {
            Form5_DashBoardMaterias_3_Eliminar_PanelIsquierdo_DataGridView.DataSource = gestorDeDatos.ObtenerMaterias();
            if (Form5_DashBoardMaterias_3_Eliminar_PanelIsquierdo_DataGridView.Rows.Count > 0)
            {
                Form5_DashBoardMaterias_3_Eliminar_PanelIsquierdo_DataGridView.Rows[0].Selected = true;
            }
        }

        private void Form5_DashBoardMaterias_3_Eliminar_PanelIsquierdo_DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (Form5_DashBoardMaterias_3_Eliminar_PanelIsquierdo_DataGridView.SelectedRows.Count > 0)
            {
                Form5_DashBoardMaterias_3_Eliminar_TextBox_Materia.Text = Form5_DashBoardMaterias_3_Eliminar_PanelIsquierdo_DataGridView.SelectedRows[0].Cells[1].Value?.ToString();
            }
            else
            {
                Form5_DashBoardMaterias_3_Eliminar_TextBox_Materia.Text = string.Empty;
            }
        }

        private void Form3_DashBoardAlumnos_3_Eliminar_PanelInferior_Button_Cancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seguro que queres cerrar la pagina actual. Se perderan los datos ingresados...", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Form3_DashBoardAlumnos_3_Eliminar_PanelInferior_Button_Eliminar_Click(object sender, EventArgs e)
        {
            int idMateria = Convert.ToInt32(Form5_DashBoardMaterias_3_Eliminar_PanelIsquierdo_DataGridView.SelectedRows[0].Cells[0].Value?.ToString());
            gestorDeDatos.EliminarMateria(idMateria);
            Form5_DashBoardMaterias_3_Eliminar_PanelIsquierdo_DataGridView.DataSource = null;
            Form5_DashBoardMaterias_3_Eliminar_PanelIsquierdo_DataGridView.DataSource = gestorDeDatos.ObtenerMaterias();
        }
    }
}

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
    public partial class Form5_DashBoardMaterias_2_Editar : Form, IConfiguracion
    {
        private GestorDeDatos gestorDeDatos = new GestorDeDatos();
        private string titulo = "MATERIAS: EDITAR";
        public Form5_DashBoardMaterias_2_Editar()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form5_DashBoardMaterias_2_Editar_Load(object sender, EventArgs e)
        {
            Form5_DashBoardMaterias_2_Editar_PanelIsquierdo_DataGridView.DataSource = gestorDeDatos.ObtenerMaterias();
            Form5_DashBoardMaterias_2_Editar_PanelIsquierdo_DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (Form5_DashBoardMaterias_2_Editar_PanelIsquierdo_DataGridView.Rows.Count > 0)
            {
                Form5_DashBoardMaterias_2_Editar_PanelIsquierdo_DataGridView.Rows[0].Selected = true;
            }
        }

        private void Form5_DashBoardMaterias_2_Editar_PanelIsquierdo_DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            Form5_DashBoardMaterias_2_Editar_TextBox_Materia.Enabled = true;
            if (Form5_DashBoardMaterias_2_Editar_PanelIsquierdo_DataGridView.SelectedRows.Count > 0)
            {
                Form5_DashBoardMaterias_2_Editar_TextBox_Materia.Text = Form5_DashBoardMaterias_2_Editar_PanelIsquierdo_DataGridView.SelectedRows[0].Cells[1].Value?.ToString();
            }
            else
            {
                Form5_DashBoardMaterias_2_Editar_TextBox_Materia.Text = string.Empty;
            }
        }

        private void Form5_DashBoardMaterias_2_Editar_PanelInferior_Button_Aceptar_Click(object sender, EventArgs e)
        {
            string nombreActualMateria = Form5_DashBoardMaterias_2_Editar_PanelIsquierdo_DataGridView.SelectedRows[0].Cells[1].Value?.ToString();
            string nombreNuevoMateria = Form5_DashBoardMaterias_2_Editar_TextBox_Materia.Text;
            if (nombreActualMateria != nombreNuevoMateria)
            {
                int idMateria = Convert.ToInt32(Form5_DashBoardMaterias_2_Editar_PanelIsquierdo_DataGridView.SelectedRows[0].Cells[0].Value?.ToString());
                gestorDeDatos.EditarMateria(idMateria, nombreNuevoMateria);
                Form5_DashBoardMaterias_2_Editar_PanelIsquierdo_DataGridView.DataSource = null;
                Form5_DashBoardMaterias_2_Editar_PanelIsquierdo_DataGridView.DataSource = gestorDeDatos.ObtenerMaterias();
            }
            else
            {
                MessageBox.Show("No hay cambios realizados.", "Redundancia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form5_DashBoardMaterias_2_Editar_PanelInferior_Button_Cancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seguro que queres cerrar la pagina actual. Se perderan los datos ingresados...", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}

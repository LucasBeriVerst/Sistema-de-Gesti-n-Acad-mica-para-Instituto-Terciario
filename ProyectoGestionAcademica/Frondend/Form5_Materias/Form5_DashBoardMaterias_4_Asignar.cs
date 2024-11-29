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
    public partial class Form5_DashBoardMaterias_4_Asignar : Form, IConfiguracion
    {
        private GestorDeDatos gestorDeDatos = new GestorDeDatos();
        private string titulo = "MATERIAS: ASIGNAR";
        public Form5_DashBoardMaterias_4_Asignar()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form5_DashBoardMaterias_4_Asignar_Load(object sender, EventArgs e)
        {
            Form5_DashBoardMaterias_4_Asignar_PanelIsquierdo_DataGridView.DataSource = gestorDeDatos.ObtenerMaterias();
            Form5_DashBoardMaterias_4_Asignar_PanelIsquierdo_DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (Form5_DashBoardMaterias_4_Asignar_PanelIsquierdo_DataGridView.Rows.Count > 0)
            {
                Form5_DashBoardMaterias_4_Asignar_PanelIsquierdo_DataGridView.Rows[0].Selected = true;
            }
            Form5_DashBoardMaterias_4_Asignar_PanelDerecho_ComboBox_Carreras.Enabled = true;
            gestorDeDatos.NombreParaCarrera(Form5_DashBoardMaterias_4_Asignar_PanelDerecho_ComboBox_Carreras);
            Form5_DashBoardMaterias_4_Asignar_PanelDerecho_ComboBox_Carreras.SelectedIndex = 0;
        }
        private void Form5_DashBoardMaterias_4_Asignar_PanelIsquierdo_DataGridView_SelectionChanged_1(object sender, EventArgs e)
        {
            if (Form5_DashBoardMaterias_4_Asignar_PanelIsquierdo_DataGridView.SelectedRows.Count > 0)
            {
                Form5_DashBoardMaterias_4_Asignar_TextBox_Materia.Text = Form5_DashBoardMaterias_4_Asignar_PanelIsquierdo_DataGridView.SelectedRows[0].Cells[1].Value?.ToString();
            }
            else
            {
                Form5_DashBoardMaterias_4_Asignar_TextBox_Materia.Text = "NOMBRE DE MATERIA";
            }
        }

        private void Form5_DashBoardMaterias_4_Asignar_PanelDerecho_ComboBox_Carreras_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Form5_DashBoardMaterias_4_Asignar_PanelDerecho_ComboBox_Años.Enabled = true;
                int idCarreraSeleccionada = gestorDeDatos.ObtenerIDCarreraPorNombre(Form5_DashBoardMaterias_4_Asignar_PanelDerecho_ComboBox_Carreras.SelectedItem.ToString());
                gestorDeDatos.CargarComboBoxAños(Form5_DashBoardMaterias_4_Asignar_PanelDerecho_ComboBox_Años, idCarreraSeleccionada);
                Form5_DashBoardMaterias_4_Asignar_PanelDerecho_ComboBox_Años.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al seleccionar la carrera: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form5_DashBoardMaterias_4_Asignar_PanelInferior_Button_Asignar_Click(object sender, EventArgs e)
        {
            int idCarrera = gestorDeDatos.ObtenerIDCarreraPorNombre(Form5_DashBoardMaterias_4_Asignar_PanelDerecho_ComboBox_Carreras.SelectedItem.ToString());
            int idAño = gestorDeDatos.ObtenerIDAñoDeCarrera(idCarrera, Convert.ToInt32(Form5_DashBoardMaterias_4_Asignar_PanelDerecho_ComboBox_Años.SelectedItem.ToString()));
            int idMateria = Convert.ToInt32(Form5_DashBoardMaterias_4_Asignar_PanelIsquierdo_DataGridView.SelectedRows[0].Cells[0].Value?.ToString());
            int resultado = gestorDeDatos.AsignarMateriaACarrera(idMateria, idCarrera, idAño);

            if (resultado == 1)
            {
                MessageBox.Show("La materia ha sido asignada correctamente a la carrera y el año especificado.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                MessageBox.Show("La materia ya está asignada a esta carrera y año.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };
       

        }

        private void Form5_DashBoardMaterias_4_Asignar_PanelInferior_Button_Cancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seguro que queres cerrar la pagina actual. Se perderan los datos ingresados...", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Form5_DashBoardMaterias_4_Asignar_PanelInferior_Button_Desvincular_Click(object sender, EventArgs e)
        {
            int idCarrera = gestorDeDatos.ObtenerIDCarreraPorNombre(Form5_DashBoardMaterias_4_Asignar_PanelDerecho_ComboBox_Carreras.SelectedItem.ToString());
            int idAño = gestorDeDatos.ObtenerIDAñoDeCarrera(idCarrera, Convert.ToInt32(Form5_DashBoardMaterias_4_Asignar_PanelDerecho_ComboBox_Años.SelectedItem.ToString()));
            int idMateria = Convert.ToInt32(Form5_DashBoardMaterias_4_Asignar_PanelIsquierdo_DataGridView.SelectedRows[0].Cells[0].Value?.ToString());
            gestorDeDatos.EliminarMateriaDeCarrera(idMateria, idCarrera, idAño);
        }
    }
}

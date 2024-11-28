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

namespace ProyectoGestionAcademica.Frondend
{
    public partial class Form3_DashBoardAlumnos_4_Asignar : Form, IConfiguracion
    {
        private string titulo = "ALUMNOS: ASIGNAR";
        private GestorDeDatos gestorDeDatos = new GestorDeDatos();
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

        private void Form3_DashBoardAlumnos_4_Asignar_PanelInferior_Button_Buscar_Click(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_Textbox_ValorDeBusqueda.Text == string.Empty || Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_Textbox_ValorDeBusqueda.Text == "VALOR DE BUSQUEDA")
            {
                MessageBox.Show("No hay ningun valor ingresado para buscar...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_DataGridView.Enabled = true;
                DataTable resultados = gestorDeDatos.BuscarAlumnosPorCategoria(Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_Combobox_TipoDeBusqueda.Text, Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_Textbox_ValorDeBusqueda.Text);
                if (resultados.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos iguales al ingresado en la categoria seleccionada...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_DataGridView.DataSource = resultados;
                }
            }
        }
        private void HabilitarCampos()
        {
            if (Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_DataGridView.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Seguro que quieres utilizar el alumno elegido?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DataGridViewRow filaSeleccionada = Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_DataGridView.SelectedRows[0];
                    Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_TextBox_Nombre.Text = filaSeleccionada.Cells["Nombre_Alumno"].Value?.ToString();
                    Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_TextBox_Apellidos.Text = filaSeleccionada.Cells["Apellido_Alumno"].Value?.ToString();
                    Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_TextBox_Dni.Text = filaSeleccionada.Cells["DNI_Alumno"].Value?.ToString();

                    Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Carreras.Enabled = true;
                    gestorDeDatos.NombreParaCarrera(Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Carreras);
                    Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Carreras.SelectedIndex = 0;
                    Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Años.Enabled = true;
                }
            }
        }



        private void Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            HabilitarCampos();
        }

        private void Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Carreras_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID de la carrera seleccionada
                int idCarreraSeleccionada = gestorDeDatos.ObtenerIDCarreraPorNombre(Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Carreras.SelectedItem.ToString());
                gestorDeDatos.CargarComboBoxAños(Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Años, idCarreraSeleccionada);
                Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Años.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al seleccionar la carrera: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Años_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Carreras.Enabled = true;
            try
            {
                if (Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Carreras.SelectedIndex == -1 ||
                    Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Años.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, seleccione una carrera y un año antes de proceder.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Materias.DataSource = null;

                int idCarrera = gestorDeDatos.ObtenerIDCarreraPorNombre(Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Carreras.SelectedItem.ToString());
                int idAño = gestorDeDatos.ObtenerIDAñoDeCarrera(idCarrera, Convert.ToInt32(Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Años.SelectedItem.ToString()));
                DataTable materias = gestorDeDatos.ObtenerMateriasPorCarreraYAño(idCarrera, idAño);
                Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Materias.Enabled = true;
                Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Materias.DataSource = materias;
                Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Materias.DisplayMember = "Nombre_Materia";     // La columna que se mostrará en el ComboBox // La columna que será el valor interno
                Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Materias.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar las materias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Form3_DashBoardAlumnos_4_Asignar_PanelInferior_Button_Asignar_Click(object sender, EventArgs e)
        {
            int id_alumno = Convert.ToInt32(Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_DataGridView.SelectedRows[0].Cells[0].Value);
            int idCarreraSeleccionada = gestorDeDatos.ObtenerIDCarreraPorNombre(Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Carreras.SelectedItem.ToString());
            string nombre_Materia = Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Materias.Text;
            int id_Materia = gestorDeDatos.ObtenerIDMateriaPorNombre(nombre_Materia, idCarreraSeleccionada);
            int respuesta = gestorDeDatos.AgregarAlumnoEnMateria(id_alumno, id_Materia);
            if (respuesta == 1)
            {
                MessageBox.Show("Alumno ingreado a la materia", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            { 
                MessageBox.Show("El alumno ya se encuentra ingresado en la materia", "Redundancia", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }
        private void Form3_DashBoardAlumnos_4_Asignar_PanelInferior_Button_Desvincular_Click(object sender, EventArgs e)
        {
            int id_alumno = Convert.ToInt32(Form3_DashBoardAlumnos_4_Asignar_PanelIsquierdo_DataGridView.SelectedRows[0].Cells[0].Value);
            int idCarreraSeleccionada = gestorDeDatos.ObtenerIDCarreraPorNombre(Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Carreras.SelectedItem.ToString());
            string nombre_Materia = Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Materias.Text;
            int id_Materia = gestorDeDatos.ObtenerIDMateriaPorNombre(nombre_Materia, idCarreraSeleccionada);
            int respuesta = gestorDeDatos.EliminarAlumnoDeMateria(id_alumno, id_Materia);
            if (respuesta == 1)
            {
                MessageBox.Show("Alumno desvinculado de la materia", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El alumno no se encuentra ingresado en la materia", "Redundancia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Form3_DashBoardAlumnos_4_Asignar_PanelDerecho_ComboBox_Materias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}

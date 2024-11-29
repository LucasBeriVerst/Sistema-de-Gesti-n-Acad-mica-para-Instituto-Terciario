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
    public partial class Form3_DashBoardAlumnos_3_Eliminar : Form, IConfiguracion
    {
        private string titulo = "ALUMNOS: ELIMINAR";
        private GestorDeDatos gestorDeDatos = new GestorDeDatos();
        public Form3_DashBoardAlumnos_3_Eliminar()
        {
            InitializeComponent();
            Titulo = titulo;
            Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Combobox_TipoDeBusqueda.SelectedIndex = 0;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form3_DashBoardAlumnos_3_Eliminar_Load(object sender, EventArgs e)
        {

        }

        private void Form3_DashBoardAlumnos_3_Eliminar_PanelInferior_Button_Cancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seguro que queres cerrar la pagina actual. Se perderan los datos ingresados...", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Form3_DashBoardAlumnos_3_Eliminar_PanelInferior_Button_Buscar_Click(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_3_Eliminar_PanelIsquierdo_Textbox_Valor.Text == string.Empty || Form3_DashBoardAlumnos_3_Eliminar_PanelIsquierdo_Textbox_Valor.Text == "VALOR A BUSCAR:")
            {
                MessageBox.Show("No hay ningun valor ingresado para buscar...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataTable resultados = gestorDeDatos.BuscarAlumnosPorCategoria(Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Combobox_TipoDeBusqueda.Text, Form3_DashBoardAlumnos_3_Eliminar_PanelIsquierdo_Textbox_Valor.Text);
                if (resultados.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos iguales al ingresado en la categoria seleccionada...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Form3_DashBoardAlumnos_3_Eliminar_PanelDerecho_DataGridView.DataSource = resultados;
                    if (Form3_DashBoardAlumnos_3_Eliminar_PanelDerecho_DataGridView.Rows.Count > 0)
                    {
                        Form3_DashBoardAlumnos_3_Eliminar_PanelDerecho_DataGridView.Rows[0].Selected = true; //Selecciona la primera fila                    
                    }
                }
            }
        }

        private void Form3_DashBoardAlumnos_3_Eliminar_PanelIsquierdo_Textbox_Valor_Enter(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_3_Eliminar_PanelIsquierdo_Textbox_Valor.Text == string.Empty) { Form3_DashBoardAlumnos_3_Eliminar_PanelIsquierdo_Textbox_Valor.Text = "VALOR A BUSCAR:"; }
            Form3_DashBoardAlumnos_3_Eliminar_PanelIsquierdo_Textbox_Valor.ForeColor = Color.DimGray;
        }

        private void Form3_DashBoardAlumnos_3_Eliminar_PanelIsquierdo_Textbox_Valor_Leave(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_3_Eliminar_PanelIsquierdo_Textbox_Valor.Text == string.Empty) { Form3_DashBoardAlumnos_3_Eliminar_PanelIsquierdo_Textbox_Valor.Text = "VALOR A BUSCAR:"; }
            Form3_DashBoardAlumnos_3_Eliminar_PanelIsquierdo_Textbox_Valor.ForeColor = Color.DimGray;
        }

        private void Form3_DashBoardAlumnos_3_Eliminar_PanelInferior_Button_Eliminar_Click(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_3_Eliminar_PanelDerecho_DataGridView.SelectedRows.Count == 0) 
            {
                MessageBox.Show("No hay ningun alumno seleccionado para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            else 
            {
                DialogResult result = MessageBox.Show("Seguro que queres eliminar al alumno seleccionado. Esta accion no se puede revertir...", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    int respuesta = gestorDeDatos.EliminarAlumno(Convert.ToInt32(Form3_DashBoardAlumnos_3_Eliminar_PanelDerecho_DataGridView.SelectedRows[0].Cells[0].Value));
                    if (respuesta == 0)
                    {
                        MessageBox.Show("Hubo un error en la eliminacion del alumno...", "Fallo en la eliminacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else 
                    {
                        MessageBox.Show("El alumno se elimino correctamente.", "Eliminado con exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}

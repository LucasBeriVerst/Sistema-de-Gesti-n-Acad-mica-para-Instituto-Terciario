﻿using ProyectoGestionAcademica.Backend;
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
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProyectoGestionAcademica.Frondend
{
    public partial class Form3_DashBoardAlumnos_2_Editar : Form, IConfiguracion
    {
        private string titulo = "ALUMNOS: EDITAR";
        private bool editable = false;
        private GestorDeDatos gestorDeDatos = new GestorDeDatos();
        public Form3_DashBoardAlumnos_2_Editar()
        {
            InitializeComponent();
            Titulo = titulo;
            Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Combobox_TipoDeBusqueda.SelectedIndex = 0;
            ConfiguracionDeDateTimePicker();
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form3_DashBoardAlumnos_2_Editar_Load(object sender, EventArgs e)
        {

        }

        private void Form3_DashBoardAlumnos_2_Editar_PanelInferior_Button_Cancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seguro que queres cerrar la pagina actual. Se perderan los datos ingresado...", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Form3_DashBoardAlumnos_2_Editar_PanelInferior_Button_Buscar_Click(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Textbox_ValorDeBusqueda.Text == string.Empty || Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Textbox_ValorDeBusqueda.Text == "VALOR DE BUSQUEDA")
            {
                MessageBox.Show("No hay ningun valor ingresado para buscar...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataTable resultados = gestorDeDatos.BuscarAlumnosPorCategoria(Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Combobox_TipoDeBusqueda.Text, Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Textbox_ValorDeBusqueda.Text);
                if (resultados.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos iguales al ingresado en la categoria seleccionada...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_DataGridView.DataSource = resultados;
                    /*if (Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_DataGridView.Columns.Count > 1) //Esconde id_alumno y Id_Perfil
                    {
                        Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_DataGridView.Columns[0].Visible = false;
                        Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_DataGridView.Columns[1].Visible = false;
                    }*/
                    if (Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_DataGridView.Rows.Count > 0) 
                    {
                        Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_DataGridView.Rows[0].Selected = true; //Selecciona la primera fila                    
                    }
                    HabilitarCampos();
                }
            }







            /*Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_DataGridView.Rows.Clear();
            Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_DataGridView.DataSource = resultados;
            Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_DataGridView.Refresh();
            if (resultados.Rows.Count == 0)
            {
                MessageBox.Show("No se encontró ningún alumno con los criterios de búsqueda.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                editable = true;
                HabilitarCampos();
            }*/

        }
        private void HabilitarCampos()
        {
            if (Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_DataGridView.SelectedRows.Count > 0)
            {
                editable = true;
                ActualizarTextBoxPorDataGridView();
            }
            else
            {
                MessageBox.Show("Elija una fila de la tabla para cargar los datos y habilitar los textboxs", "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ProbarYRestablecerCampos()
        {
            if (!editable)
            {
                if (Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Textbox_ValorDeBusqueda.Text == "VALOR DE BUSQUEDA")
                {
                    Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Textbox_ValorDeBusqueda.Text = string.Empty;
                    Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Textbox_ValorDeBusqueda.ForeColor = Color.Black;
                }
                else if (Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Textbox_ValorDeBusqueda.Text == string.Empty)
                {
                    Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Textbox_ValorDeBusqueda.Text = "VALOR DE BUSQUEDA";
                    Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Textbox_ValorDeBusqueda.ForeColor = Color.DimGray;
                }
            }
        }
        private void ActualizarTextBoxPorDataGridView()
        {
            if (editable)
            {
                try
                {
                    DataGridViewRow filaSeleccionada = Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_DataGridView.SelectedRows[0];

                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Nombre.Enabled = editable;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Nombre.Text = filaSeleccionada.Cells["Nombre_Alumno"].Value?.ToString();
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Nombre.ForeColor = Color.Black;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Apellidos.Enabled = editable;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Apellidos.Text = filaSeleccionada.Cells["Apellido_Alumno"].Value?.ToString();
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Apellidos.ForeColor = Color.Black;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Dni.Enabled = editable;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Dni.Text = filaSeleccionada.Cells["DNI_Alumno"].Value?.ToString();
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Dni.ForeColor = Color.Black;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Calle.Enabled = editable;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Calle.Text = filaSeleccionada.Cells["Domicilio_Calle"].Value?.ToString();
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Calle.ForeColor = Color.Black;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Numero.Enabled = editable;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Numero.Text = filaSeleccionada.Cells["Domicilio_Numero"].Value?.ToString();
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Numero.ForeColor = Color.Black;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Telefono.Enabled = editable;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Telefono.Text = filaSeleccionada.Cells["Telefono"].Value?.ToString();
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Telefono.ForeColor = Color.Black;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Email.Enabled = editable;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Email.Text = filaSeleccionada.Cells["Email"].Value?.ToString();
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Email.ForeColor = Color.Black;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Matricula.Enabled = editable;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Matricula.Text = filaSeleccionada.Cells["Matricula"].Value?.ToString();
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Matricula.ForeColor = Color.Black;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Usuario.Enabled = editable;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Usuario.Text = filaSeleccionada.Cells["Usuario_Alumno"].Value?.ToString();
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Usuario.ForeColor = Color.Black;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Contraseña.Enabled = editable;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Contraseña.Text = filaSeleccionada.Cells["Contrasenia_Alumno"].Value?.ToString();
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Contraseña.ForeColor = Color.Black;

                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Alta.Enabled = editable;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Alta.ShowCheckBox = true;

                    if (DateTime.TryParse(filaSeleccionada.Cells["Fecha_Alta"].Value?.ToString(), out DateTime fechaAlta))
                    {
                        Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Alta.Value = fechaAlta;
                        Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Alta.Checked = true;  // Selecciona la fecha
                    }
                    else
                    {
                        Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Alta.Checked = false; // No hay fecha seleccionada
                    }

                    // Para la fecha de baja
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Baja.Enabled = editable;
                    Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Baja.ShowCheckBox = true;

                    if (DateTime.TryParse(filaSeleccionada.Cells["Fecha_Baja"].Value?.ToString(), out DateTime fechaBaja))
                    {
                        Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Baja.Value = fechaBaja;
                        Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Baja.Checked = true; // Selecciona la fecha
                    }
                    else
                    {
                        Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Baja.Checked = false; // No hay fecha seleccionada
                    }
                }
                catch(Exception e) 
                { }
            }
        }
        private void Form3_DashBoardAlumnos_2_Editar_PanelInferior_Button_Aceptar_Click(object sender, EventArgs e)
        {
            DateTime? fechaBaja = Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Baja.Checked
                ? (DateTime?)Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Baja.Value
                : null;
            DateTime? fechaAlta = Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Alta.Checked
                ? (DateTime?)Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Alta.Value
                : null;
            string? calle = string.IsNullOrWhiteSpace(Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Calle.Text)
                ? null
                : Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Calle.Text;
            string? telefono = string.IsNullOrWhiteSpace(Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Telefono.Text)
                ? null
                : Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Telefono.Text;
            int? numero = string.IsNullOrWhiteSpace(Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Numero.Text)
                ? (int?)null
                : int.Parse(Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Numero.Text);

            int respuesta = gestorDeDatos.EditarAlumno(
                int.Parse(Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_DataGridView.SelectedRows[0].Cells["ID_Alumno"].Value.ToString()),
                int.Parse(Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Matricula.Text),
                Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Nombre.Text,
                Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Apellidos.Text,
                int.Parse(Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Dni.Text),
                calle,
                numero, 
                telefono,
                Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Email.Text,
                Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Usuario.Text,
                Form3_DashBoardAlumnos_2_Editar_PanelDerecho_TextBox_Contraseña.Text,
                fechaBaja,
                fechaAlta);

            switch (respuesta)
            {
                case -1:
                    MessageBox.Show("El campo 'Nombre' no puede guardarse como vacio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -2:
                    MessageBox.Show("El campo 'Nombre' no puede contener numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -3:
                    MessageBox.Show("El campo 'Apellido' no puede guardarse como vacio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -4:
                    MessageBox.Show("El campo 'Apellido' no puede contener numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -5:
                    MessageBox.Show("El campo 'Matricula' no puede ser menor o igual a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -6:
                    MessageBox.Show("El campo 'Dni' no puede ser menor o igual a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -7:
                    MessageBox.Show("El campo 'Email' no puede guardarse como vacio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -8:
                    MessageBox.Show("El campo 'Email' no contiene el formato correcto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -9:
                    MessageBox.Show("El campo 'Usuario' no puede guardarse como vacio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -10:
                    MessageBox.Show("El campo 'Contraseña' no puede guardarse como vacio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -11:
                    MessageBox.Show("Error desconocido en la carga de alumno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 0:
                    MessageBox.Show("El Identificador del alumno no se encuentra guardado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("La Carga del alumno se realizo con exito.", "Carga exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form3_DashBoardAlumnos_2_Editar_PanelInferior_Button_Buscar.PerformClick();
                    break;
            };

        }

        private void ConfiguracionDeDateTimePicker()
        {
            Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Alta.CalendarMonthBackground = Color.FromArgb(214, 208, 209);
            Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Alta.ForeColor = Color.DimGray;
            Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Baja.CalendarMonthBackground = Color.FromArgb(214, 208, 209);
            Form3_DashBoardAlumnos_2_Editar_PanelDerecho_DateTimePicker_Baja.ForeColor = Color.DimGray;
        }
        private void Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Textbox_ValorDeBusqueda_Enter(object sender, EventArgs e)
        {
            ProbarYRestablecerCampos();
        }

        private void Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_Textbox_ValorDeBusqueda_Leave(object sender, EventArgs e)
        {
            ProbarYRestablecerCampos();
        }
        private void Form3_DashBoardAlumnos_2_Editar_PanelIsquierdo_DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarTextBoxPorDataGridView();
        }
    }
}

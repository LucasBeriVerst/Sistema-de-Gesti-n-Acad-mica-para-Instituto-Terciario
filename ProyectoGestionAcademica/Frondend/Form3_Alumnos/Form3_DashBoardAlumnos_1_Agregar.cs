using ProyectoGestionAcademica.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoGestionAcademica.Frondend
{
    public partial class Form3_DashBoardAlumnos_1_Agregar : Form, IConfiguracion
    {
        private GestorDeDatos GestorDeDatos = new GestorDeDatos();
        private string titulo = "ALUMNOS: AGREGAR";
        public Form3_DashBoardAlumnos_1_Agregar()
        {
            InitializeComponent();
            Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre.Focus();
            Titulo = titulo;
        }

        public string Titulo { get => titulo; set => titulo = value; }

        private void Form3_DashBoardAlumnos_1_Agregar_Load(object sender, EventArgs e)
        {

        }

        private void Form3_DashBoardAlumnos_1_Agregar_PanelInferior_Button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form3_DashBoardAlumnos_1_Agregar_PanelInferior_Button_Agregar_Click(object sender, EventArgs e)
        {
            #region Logica de verificacion y agregado de alumnos
            
            ProbarYRestablecerCampos();
            if (GestorDeDatos.ValidarCamposDeTexto(Form3_DashBoardAlumnos_1_Agregar_TextBox_Apellido.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Dni.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Email.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Matricula.Text))
            {
                /*if (GestorDeDatos.Form_Alumnos_AgregarAlumno(Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Apellido.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Dni.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Calle.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Numero.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Telefono.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Email.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Matricula.Text) == 1)
                {
                    MessageBox.Show("El nuevo alumno ha sido ingresado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //MessageBox.Show("Campos de texto importantes no se han proporcionado para para agreagar un nuevo alumno...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else 
                {*/
                    int error = GestorDeDatos.Form_Alumnos_AgregarAlumno(Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Apellido.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Dni.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Calle.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Numero.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Telefono.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Email.Text, Form3_DashBoardAlumnos_1_Agregar_TextBox_Matricula.Text);
                    switch (error)
                    {
                        case -1:
                            MessageBox.Show("El alumno ya existe (DNI o matrícula duplicados).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case -2:
                            MessageBox.Show("El campo 'Nombre' es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case -3:
                            MessageBox.Show("El campo 'Apellido' es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case -4:
                            MessageBox.Show("El campo 'DNI' es obligatorio.(Solo numeros)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case -5:
                            MessageBox.Show("El campo 'Email' es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case -6:
                            MessageBox.Show("El campo 'Usuario' es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case -7:
                            MessageBox.Show("El campo 'Contraseña' es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case -8:
                            MessageBox.Show("El alumno ya existe (DNI o matrícula duplicados).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case -9:
                            MessageBox.Show("El campo 'DNI' solo puede tener numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case -10:
                            MessageBox.Show("El campo 'Email' no tiene el formato correcto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case -11:
                            MessageBox.Show("El campo 'Matricula' solo puede tener numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    case 1:
                            MessageBox.Show("El alumno se ha agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        default:
                            MessageBox.Show("Ocurrió un error desconocido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    };
                //}
            }
            else
            {
                MessageBox.Show("Faltan Completar campos de texto para agreagar un nuevo alumno...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ProbarYRestablecerCampos();

            #endregion
        }
        private void ProbarYRestablecerCampos()
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Apellido.Text == "APELLIDO") { Form3_DashBoardAlumnos_1_Agregar_TextBox_Apellido.Text = string.Empty; } else if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Apellido.Text == string.Empty) { Form3_DashBoardAlumnos_1_Agregar_TextBox_Apellido.Text = "APELLIDO"; }
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Calle.Text == "DOMICILIO (CALLE)") { Form3_DashBoardAlumnos_1_Agregar_TextBox_Calle.Text = string.Empty; } else if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Calle.Text == string.Empty) { Form3_DashBoardAlumnos_1_Agregar_TextBox_Calle.Text = "DOMICILIO (CALLE)"; }
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Dni.Text == "DNI") { Form3_DashBoardAlumnos_1_Agregar_TextBox_Dni.Text = string.Empty; } else if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Dni.Text == string.Empty) { Form3_DashBoardAlumnos_1_Agregar_TextBox_Dni.Text = "DNI"; }
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Email.Text == "EMAIL") { Form3_DashBoardAlumnos_1_Agregar_TextBox_Email.Text = string.Empty; } else if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Email.Text == string.Empty) { Form3_DashBoardAlumnos_1_Agregar_TextBox_Email.Text = "EMAIL"; }
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre.Text == "NOMBRE") { Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre.Text = string.Empty; } else if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre.Text == string.Empty) { Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre.Text = "NOMBRE"; }
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Numero.Text == "DOMICILIO (NUMERO)") { Form3_DashBoardAlumnos_1_Agregar_TextBox_Numero.Text = string.Empty; } else if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Numero.Text == string.Empty) { Form3_DashBoardAlumnos_1_Agregar_TextBox_Numero.Text = "DOMICILIO (NUMERO)"; }
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Telefono.Text == "TELEFONO") { Form3_DashBoardAlumnos_1_Agregar_TextBox_Telefono.Text = string.Empty; } else if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Telefono.Text == string.Empty) { Form3_DashBoardAlumnos_1_Agregar_TextBox_Telefono.Text = "TELEFONO"; }
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Matricula.Text == "MATRICULA") { Form3_DashBoardAlumnos_1_Agregar_TextBox_Matricula.Text = string.Empty; } else if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Matricula.Text == string.Empty) { Form3_DashBoardAlumnos_1_Agregar_TextBox_Matricula.Text = "MATRICULA"; }
        }
        #region Texto en botones
        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre_Enter(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre.Text == "NOMBRE")
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre.Text = string.Empty;
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre.ForeColor = Color.Black;
            }
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre_Leave(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre.Text == string.Empty)
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre.Text = "NOMBRE";
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Nombre.ForeColor = Color.DimGray;
            }
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Apellido_Enter(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Apellido.Text == "APELLIDO")
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Apellido.Text = string.Empty;
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Apellido.ForeColor = Color.Black;
            }
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Apellido_Leave(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Apellido.Text == string.Empty)
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Apellido.Text = "APELLIDO";
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Apellido.ForeColor = Color.DimGray;
            }
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Dni_Enter(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Dni.Text == "DNI")
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Dni.Text = string.Empty;
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Dni.ForeColor = Color.Black;
            }
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Dni_Leave(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Dni.Text == string.Empty)
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Dni.Text = "DNI";
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Dni.ForeColor = Color.DimGray;
            }
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Calle_Enter(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Calle.Text == "DOMICILIO (CALLE)")
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Calle.Text = string.Empty;
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Calle.ForeColor = Color.Black;
            }
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Calle_Leave(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Calle.Text == string.Empty)
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Calle.Text = "DOMICILIO (CALLE)";
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Calle.ForeColor = Color.DimGray;
            }
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Numero_Enter(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Numero.Text == "DOMICILIO (NUMERO)")
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Numero.Text = string.Empty;
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Numero.ForeColor = Color.Black;
            }
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Numero_Leave(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Numero.Text == string.Empty)
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Numero.Text = "DOMICILIO (NUMERO)";
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Numero.ForeColor = Color.DimGray;
            }
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Telefono_Enter(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Telefono.Text == "TELEFONO")
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Telefono.Text = string.Empty;
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Telefono.ForeColor = Color.Black;
            }
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Telefono_Leave(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Telefono.Text == string.Empty)
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Telefono.Text = "TELEFONO";
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Telefono.ForeColor = Color.DimGray;
            }
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Email_Enter(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Email.Text == "EMAIL")
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Email.Text = string.Empty;
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Email.ForeColor = Color.Black;
            }
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Email_Leave(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Email.Text == string.Empty)
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Email.Text = "EMAIL";
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Email.ForeColor = Color.DimGray;
            }
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Matricula_Enter(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Matricula.Text == "MATRICULA")
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Matricula.Text = string.Empty;
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Matricula.ForeColor = Color.Black;
            }
        }

        private void Form3_DashBoardAlumnos_1_Agregar_TextBox_Matricula_Leave(object sender, EventArgs e)
        {
            if (Form3_DashBoardAlumnos_1_Agregar_TextBox_Matricula.Text == string.Empty)
            {
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Matricula.Text = "MATRICULA";
                Form3_DashBoardAlumnos_1_Agregar_TextBox_Matricula.ForeColor = Color.DimGray;
            }
        }
        #endregion
    }
}

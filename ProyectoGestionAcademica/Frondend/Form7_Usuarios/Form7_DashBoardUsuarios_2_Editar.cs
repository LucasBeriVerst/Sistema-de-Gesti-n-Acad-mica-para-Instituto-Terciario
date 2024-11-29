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

namespace ProyectoGestionAcademica.Frondend.Form7_Usuarios
{
    public partial class Form7_DashBoardUsuarios_2_Editar : Form, IConfiguracion
    {
        private GestorDeDatos GestorDeDatos = new GestorDeDatos();
        private bool editable = false;
        private string titulo = "USUARIOS: EDITAR";
        public Form7_DashBoardUsuarios_2_Editar()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }

        private void Form7_DashBoardUsuarios_2_Editar_Load(object sender, EventArgs e)
        {
            //pasamos los datos para cargar el combobox, incluido el filtro para excluir el perfil 'Alumno'
            CargarComboBoxConIDsNombresGenericos(Form7_DashBoardUsuarios_2_Editar_PanelDerecho_ComboBox_Perfil, "Perfiles", "ID_Perfil", "Nombre_Perfil", "WHERE Nombre_Perfil != 'Alumno'");

            // Suscripciónes de los eventos Enter y Leave para cada TextBox
            //Suscribir quiere decir que cuando ocurra ese evento (Enter o Leave) se ejecuta algo especifico; en
            //este caso el metodo TextBox_EnterLeave
            #region Suscripciones
            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Nombre.Enter += TextBox_Enter;
            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Nombre.Leave += TextBox_Leave;

            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Apellido.Enter += TextBox_Enter;
            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Apellido.Leave += TextBox_Leave;

            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Dni.Enter += TextBox_Enter;
            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Dni.Leave += TextBox_Leave;

            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Calle.Enter += TextBox_Enter;
            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Calle.Leave += TextBox_Leave;

            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Numero.Enter += TextBox_Enter;
            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Numero.Leave += TextBox_Leave;

            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Telefono.Enter += TextBox_Enter;
            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Telefono.Leave += TextBox_Leave;

            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Email.Enter += TextBox_Enter;
            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Email.Leave += TextBox_Leave;

            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Usuario.Enter += TextBox_Enter;
            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Usuario.Leave += TextBox_Leave;

            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Contraseña.Enter += TextBox_Enter;
            Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Contraseña.Leave += TextBox_Leave;


            Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarNombre.Enter += TextBox_Enter;
            Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarNombre.Leave += TextBox_Leave;

            Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarApellido.Enter += TextBox_Enter;
            Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarApellido.Leave += TextBox_Leave;

            Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarDni.Enter += TextBox_Enter;
            Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarDni.Leave += TextBox_Leave;
            #endregion
        }

        private void Form7_DashBoardUsuarios_2_Editar_PanelInferior_Button_Cancelar_Click(object sender, EventArgs e)
        {
            DialogResult resutl = MessageBox.Show("Seguro que quiere cerrar la pestaña actual? Se perderan los datos ingresados...", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resutl == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void CargarComboBoxConIDsNombresGenericos(ComboBox ComboBoxGenerico, string nombreTabla, string columnaID, string columnaNombre, string filtro = null)
        {
            var listaIDsNombres = GestorDeDatos.ObtenerIDsyNombresGenericos(nombreTabla, columnaID, columnaNombre, filtro);

            // Agregar 'Seleccione uno...' al principio de la lista con indice 0
            listaIDsNombres.Insert(0, new KeyValuePair<object, string>(0, "Seleccione uno..."));

            ComboBoxGenerico.DisplayMember = "Value";  //Especifica qué propiedad del objeto de la lista se muestra en el ComboBox
            ComboBoxGenerico.ValueMember = "Key";      //qué propiedad está asociada al valor seleccionado. Aquí es Key, el ID.
            ComboBoxGenerico.DataSource = null;     //Limpia el ComboBox antes de asignar la fuente de datos
            ComboBoxGenerico.DataSource = listaIDsNombres;    //Asigna la lista de datos al ComboBox

            ComboBoxGenerico.SelectedIndex = 0; // Selecciona el primer elemento (Texto 'Seleccione uno...')
        }

        //Metodo que gestiona el comportamiento de los TextBox cuando reciben o pierden el foco
        private string ObtenerTextoPredeterminado(string textBoxName)
        {
            #region Texto Predeterminado de los TextBoxs
            switch (textBoxName)
            {
                case "Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Nombre":
                    return "NOMBRE";
                case "Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Apellido":
                    return "APELLIDO";
                case "Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Dni":
                    return "DNI";
                case "Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Calle":
                    return "DOMICILIO (CALLE)";
                case "Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Numero":
                    return "DOMICILIO (NUMERO)";
                case "Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Telefono":
                    return "TELEFONO";
                case "Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Email":
                    return "EMAIL";
                case "Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Usuario":
                    return "USUARIO";
                case "Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Contraseña":
                    return "CONTRASEÑA";
                case "Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarNombre":
                    return "Buscar por Nombre:";
                case "Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarApellido":
                    return "Buscar por Apellido:";
                case "Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarDni":
                    return "Buscar por DNI:";
                default:
                    return string.Empty;
            }
            #endregion
        }

        // Método para manejar el evento Enter del TextBox (recibe el foco)
        private void TextBox_Enter(object sender,EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                string defaultText = ObtenerTextoPredeterminado(textBox.Name);

                // Lógica para el evento 'Enter' (cuando el control recibe el foco)
                if (textBox.Text == defaultText)  // Si el texto es el predeterminado
                {
                    textBox.Text = string.Empty;  // Limpiamos el TextBox
                    textBox.ForeColor = Color.Black;  // Cambiamos el color del texto a negro (para indicar que el usuario puede escribir)
                }
            }
        }

        // Método para manejar el evento Leave del TextBox (pierde el foco)
        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string defaultText = ObtenerTextoPredeterminado(textBox.Name);

                // Lógica para el evento 'Leave' (cuando el control pierde el foco)
                if (string.IsNullOrEmpty(textBox.Text))  // Si el TextBox está vacío
                {
                    textBox.Text = defaultText;  // Restauramos el texto predeterminado
                    textBox.ForeColor = Color.DimGray;  // Ponemos el texto en color gris, como marcador de posición
                }
            }
        }

        private void Form7_DashBoardUsuarios_2_Editar_PanelInferior_Button_Buscar_Click(object sender, EventArgs e)
        {
            //Valida que al menos uno de los campos no esté vacío o compuesto solo por espacios en blanco, y que tampoco
            //contenga el texto predeterminado
            bool alMenosUnCampoValido =
                !string.IsNullOrWhiteSpace(Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarNombre.Text) &&
                Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarNombre.Text != "Buscar por Nombre:" ||

                !string.IsNullOrWhiteSpace(Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarApellido.Text) &&
                Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarApellido.Text != "Buscar por Apellido:" ||

                !string.IsNullOrWhiteSpace(Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarDni.Text) &&
                Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarDni.Text != "Buscar por DNI:";

            if (!alMenosUnCampoValido)
            {
                MessageBox.Show("No hay ningún valor ingresado que sea correcto para buscar el Usuario...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //El trim elimina espacios en blanco innecesarios
                string valorNombre = Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarNombre.Text.Trim();
                string valorApellido = Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarApellido.Text.Trim();
                string valorDNI = Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_Textbox_BuscarDni.Text.Trim();

                // Verifica si el valor es el texto predeterminado y lo reemplaza por null
                if (valorNombre == "Buscar por Nombre:") valorNombre = null;
                if (valorApellido == "Buscar por Apellido:") valorApellido = null;
                if (valorDNI == "Buscar por DNI:") valorDNI = null;

                //se pasan los datos para cargar el DataGrid con los Usuarios
                DataTable resultados = GestorDeDatos.BuscarUsuariosPorNombreApellidoDNI(valorNombre, valorApellido, valorDNI);

                if (resultados.Rows.Count == 0)
                {
                    MessageBox.Show("No hay Usuarios con esos Datos en el Sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Cargo el DataGridView
                    Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_DataGridView_Usuarios.DataSource = resultados;

                    // Oculto las columnas del DataGridView para que no aparezcan todas
                    Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_DataGridView_Usuarios.Columns["ID_Empleado"].Visible = false;
                    Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_DataGridView_Usuarios.Columns["ID_Perfil"].Visible = false;
                    Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_DataGridView_Usuarios.Columns["Domicilio_Calle"].Visible = false;
                    Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_DataGridView_Usuarios.Columns["Domicilio_Numero"].Visible = false;
                    Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_DataGridView_Usuarios.Columns["Usuario"].Visible = false;
                    Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_DataGridView_Usuarios.Columns["Contraseña"].Visible = false;
                    Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_DataGridView_Usuarios.Columns["Fecha_Baja"].Visible = false;
                    Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_DataGridView_Usuarios.Columns["Fecha_Alta"].Visible = false;

                    //si la cantidad de filas es > 0
                    if (Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_DataGridView_Usuarios.Rows.Count > 0)
                    {
                        Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_DataGridView_Usuarios.Rows[0].Selected = true; //Selecciona la primera fila                    
                    }
                    HabilitarCampos();      //paso los datos a la derecha para poder editarlos
                }
            }
        }

        //Habilita los textbox de la derecha pasandole los datos del usuario seleccionado del DataGrid. Para luego poder editarlos
        private void HabilitarCampos()
        {
            if (Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_DataGridView_Usuarios.SelectedRows.Count > 0)
            {
                editable = true;
                ActualizarTextBoxPorDataGridView();                
            }
            else
            {
                MessageBox.Show("Elija una fila de la Tabla para mostrar los datos a la derecha y asi poder editarlos.", "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //De acuerdo a la Usuario seleccionado del DatagridView, se muestran los datos a la derecha para modificarlos
        private void ActualizarTextBoxPorDataGridView()
        {
            #region Actualizar TextBoxs con DataGridView
            if (editable)
            {
                try
                {
                    //Indica que estás accediendo solo a la primera fila seleccionada (índice 0). Si se seleccionan varias toma la primera
                    DataGridViewRow filaSeleccionada = Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_DataGridView_Usuarios.SelectedRows[0];

                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Nombre.Enabled = editable;
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Nombre.Text = filaSeleccionada.Cells["Nombre"].Value?.ToString();
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Nombre.ForeColor = Color.Black;

                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Apellido.Enabled = editable;
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Apellido.Text = filaSeleccionada.Cells["Apellido"].Value?.ToString();
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Apellido.ForeColor = Color.Black;

                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Dni.Enabled = editable;
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Dni.Text = filaSeleccionada.Cells["DNI"].Value?.ToString();
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Dni.ForeColor = Color.Black;

                    //ComboBox Tipo de Perfil
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_ComboBox_Perfil.Enabled = editable;
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_ComboBox_Perfil.SelectedValue = filaSeleccionada.Cells["ID_Perfil"].Value;
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_ComboBox_Perfil.ForeColor = Color.Black;

                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Calle.Enabled = editable;
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Calle.Text = filaSeleccionada.Cells["Domicilio_Calle"].Value?.ToString() ?? "DOMICILIO (CALLE)";
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Calle.ForeColor = Color.Black;

                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Numero.Enabled = editable;
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Numero.Text = filaSeleccionada.Cells["Domicilio_Numero"].Value?.ToString() ?? "DOMICILIO (NUMERO)";
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Numero.ForeColor = Color.Black;

                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Telefono.Enabled = editable;
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Telefono.Text = filaSeleccionada.Cells["Telefono"].Value?.ToString() ?? "TELEFONO";
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Telefono.ForeColor = Color.Black;

                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Email.Enabled = editable;
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Email.Text = filaSeleccionada.Cells["Email"].Value?.ToString();
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Email.ForeColor = Color.Black;

                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Usuario.Enabled = editable;
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Usuario.Text = filaSeleccionada.Cells["Usuario"].Value?.ToString();
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Usuario.ForeColor = Color.Black;

                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Contraseña.Enabled = editable;
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Contraseña.Text = filaSeleccionada.Cells["Contraseña"].Value?.ToString();
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Contraseña.ForeColor = Color.Black;

                    //Para Fecha Alta
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_DataTimePicker_FechaAlta.Enabled = editable;
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_DataTimePicker_FechaAlta.ShowCheckBox = true;
                    if (DateTime.TryParse(filaSeleccionada.Cells["Fecha_Alta"].Value?.ToString(), out DateTime fechaAlta))
                    {
                        Form7_DashBoardUsuarios_2_Editar_PanelDerecho_DataTimePicker_FechaAlta.Value = fechaAlta;
                        Form7_DashBoardUsuarios_2_Editar_PanelDerecho_DataTimePicker_FechaAlta.Checked = true;  // Selecciona la fecha
                    }
                    else
                    {
                        Form7_DashBoardUsuarios_2_Editar_PanelDerecho_DataTimePicker_FechaAlta.Checked = false; // No hay fecha seleccionada
                    }

                    //Para Fecha Baja
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_DataTimePicker_FechaBaja.Enabled = editable;
                    Form7_DashBoardUsuarios_2_Editar_PanelDerecho_DataTimePicker_FechaBaja.ShowCheckBox = true;
                    if (DateTime.TryParse(filaSeleccionada.Cells["Fecha_Baja"].Value?.ToString(), out DateTime fechaBaja))
                    {
                        Form7_DashBoardUsuarios_2_Editar_PanelDerecho_DataTimePicker_FechaBaja.Value = fechaBaja;
                        Form7_DashBoardUsuarios_2_Editar_PanelDerecho_DataTimePicker_FechaBaja.Checked = true;  // Selecciona la fecha
                    }
                    else
                    {
                        Form7_DashBoardUsuarios_2_Editar_PanelDerecho_DataTimePicker_FechaBaja.Checked = false; // No hay fecha seleccionada
                    }
                }
                catch (Exception ex)
                {

                }
            }
            #endregion
        }

        //Sirve para que cada vez que seleccionemos una fila del DataGridView, se actualicen los TextBox para editarlos
        private void Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_DataGridView_Usuarios_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarTextBoxPorDataGridView();
        }

        private void Form7_DashBoardUsuarios_2_Editar_PanelInferior_Button_Editar_Click(object sender, EventArgs e)
        {
            //Si fecha Alta/Baja esta seleccionado (.Checked) entonces se guarda el '.Value'; sino se guarda nulo
            DateTime? fechaAlta = Form7_DashBoardUsuarios_2_Editar_PanelDerecho_DataTimePicker_FechaAlta.Checked
                ? (DateTime?)Form7_DashBoardUsuarios_2_Editar_PanelDerecho_DataTimePicker_FechaAlta.Value
                : null;
            DateTime? fechaBaja = Form7_DashBoardUsuarios_2_Editar_PanelDerecho_DataTimePicker_FechaBaja.Checked
                ? (DateTime?)Form7_DashBoardUsuarios_2_Editar_PanelDerecho_DataTimePicker_FechaBaja.Value
                : null;

            // Asignamos la columna oculta siendo que la columna ID está en la posición 0.
            int idEmpleado = Convert.ToInt32(Form7_DashBoardUsuarios_2_Editar_PanelIzquierdo_DataGridView_Usuarios.SelectedRows[0].Cells["ID_Empleado"].Value);

            int error = GestorDeDatos.EditarUsuario(
                idEmpleado,
                Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Nombre.Text,
                Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Apellido.Text,
                Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Dni.Text,
                Form7_DashBoardUsuarios_2_Editar_PanelDerecho_ComboBox_Perfil.SelectedValue.ToString(),
                Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Calle.Text,
                Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Numero.Text,
                Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Telefono.Text,
                Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Email.Text,
                Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Usuario.Text,
                Form7_DashBoardUsuarios_2_Editar_PanelDerecho_TextBox_Contraseña.Text,
                fechaAlta,
                fechaBaja
                );

            switch (error)
            {
                case -20:
                    MessageBox.Show("Por favor seleccione un 'Tipo de Perfil' del campo desplegable.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case -21:
                    MessageBox.Show("Por favor complete el campo 'Nombre' correctamente. No puede contener números ni caracteres especiales.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -22:
                    MessageBox.Show("Por favor complete el campo 'Apellido' correctamente. No puede contener números ni caracteres especiales.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -9:
                    MessageBox.Show("El campo 'DNI' solo puede contener numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -23:
                    MessageBox.Show("Por favor complete el campo 'Domicilio (Calle)' correctamente. No puede contener números ni caracteres especiales.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -24:
                    MessageBox.Show("El campo 'Domicilio (Numero)' solo puede contener numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -10:
                    MessageBox.Show("El campo 'Email' no tiene el formato correcto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -25:
                    MessageBox.Show("Por favor complete el campo 'Usuario' correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -26:
                    MessageBox.Show("Por favor ingrese una contraseña valida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -27:
                    MessageBox.Show("La Fecha de Baja no puede ser Anterior a la Fecha de Alta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -28:
                    MessageBox.Show("Por favor complete el campo 'Telefono' correctamente. No ingrese letras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                // Case:1 seria correcto, xq al ejecutar la query devuelve el numero de filas afectadas. Y al ser una insercion
                // la fila afectada/ingresada seria siempre 1 en este caso. Lo que se traduce a una insercion exitosa
                case 1:
                    MessageBox.Show("El Usuario se ha ingresado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                default:
                    MessageBox.Show("Ocurrió un error desconocido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }



    }
}

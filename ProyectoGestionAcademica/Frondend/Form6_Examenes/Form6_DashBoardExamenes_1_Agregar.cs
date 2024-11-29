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

namespace ProyectoGestionAcademica.Frondend.Form6_Examenes
{
    public partial class Form6_DashBoardExamenes_1_Agregar : Form, IConfiguracion
    {
        private GestorDeDatos GestorDeDatos = new GestorDeDatos();
        private string titulo = "MATERIAS: AGREGAR";
        bool editable = false;
        public Form6_DashBoardExamenes_1_Agregar()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }

        private void Form6_DashBoardExamenes_1_Agregar_Load(object sender, EventArgs e)
        {
            //pasamos los datos para cargar el combobox, incluido el filtro
            CargarComboBoxConIDsNombresGenericos(Form6_DashBoardExamenes_1_Agregar_PanelDerecho_ComboBox_Materias, "Materias", "ID_Materia", "Nombre_Materia");
            CargarComboBoxConIDsNombresGenericos(Form6_DashBoardExamenes_1_Agregar_PanelDerecho_ComboBox_Instancias, "Instancias", "ID_Instancia", "Nombre_Instancia");

            // Suscripciónes de los eventos Enter y Leave para cada TextBox
            //Suscribir quiere decir que cuando ocurra ese evento (Enter o Leave) se ejecuta algo especifico; en
            //este caso el metodo TextBox_EnterLeave
            #region Suscripciones
            Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Nombre.Enter += TextBox_Enter;
            Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Nombre.Leave += TextBox_Leave;

            Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Apellido.Enter += TextBox_Enter;
            Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Apellido.Leave += TextBox_Leave;

            Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Nota.Enter += TextBox_Enter;
            Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Nota.Leave += TextBox_Leave;

            Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Libro.Enter += TextBox_Enter;
            Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Libro.Leave += TextBox_Leave;

            Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Folio.Enter += TextBox_Enter;
            Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Folio.Leave += TextBox_Leave;


            Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarNombre.Enter += TextBox_Enter;
            Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarNombre.Leave += TextBox_Leave;

            Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarApellido.Enter += TextBox_Enter;
            Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarApellido.Leave += TextBox_Leave;

            Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarDni.Enter += TextBox_Enter;
            Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarDni.Leave += TextBox_Leave;

            Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarMatricula.Enter += TextBox_Enter;
            Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarMatricula.Leave += TextBox_Leave;
            #endregion
        }

        private void Form6_DashBoardExamenes_1_Agregar_PanelInferior_Button_Cancelar_Click(object sender, EventArgs e)
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
                case "Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Nombre":
                    return "NOMBRE";
                case "Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Apellido":
                    return "APELLIDO";
                case "Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Nota":
                    return "NOTA";
                case "Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Libro":
                    return "LIBRO";
                case "Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Folio":
                    return "FOLIO";

                case "Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarNombre":
                    return "Buscar por Nombre:";
                case "Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarApellido":
                    return "Buscar por Apellido:";
                case "Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarDni":
                    return "Buscar por DNI:";
                case "Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarMatricula":
                    return "Buscar por Matricula:";
                default:
                    return string.Empty;
            }
            #endregion
        }

        // Método para manejar el evento Enter del TextBox (recibe el foco)
        private void TextBox_Enter(object sender, EventArgs e)
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

        private void Form6_DashBoardExamenes_1_Agregar_PanelInferior_Button_Buscar_Click(object sender, EventArgs e)
        {
            //Valida que al menos uno de los campos no esté vacío o compuesto solo por espacios en blanco, y que tampoco
            //contenga el texto predeterminado
            bool alMenosUnCampoValido =
                !string.IsNullOrWhiteSpace(Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarNombre.Text) &&
                Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarNombre.Text != "Buscar por Nombre:" ||

                !string.IsNullOrWhiteSpace(Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarApellido.Text) &&
                Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarApellido.Text != "Buscar por Apellido:" ||

                !string.IsNullOrWhiteSpace(Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarDni.Text) &&
                Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarDni.Text != "Buscar por DNI:" ||

                !string.IsNullOrWhiteSpace(Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarMatricula.Text) &&
                Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarMatricula.Text != "Buscar por Matricula:";

            if (!alMenosUnCampoValido)
            {
                MessageBox.Show("No hay ningún valor ingresado que sea correcto para buscar el Alumno...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //El trim elimina espacios en blanco innecesarios
                string valorNombre = Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarNombre.Text.Trim();
                string valorApellido = Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarApellido.Text.Trim();
                string valorDNI = Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarDni.Text.Trim();
                string valorMatricula = Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_Textbox_BuscarMatricula.Text.Trim();

                // Verifica si el valor es el texto predeterminado y lo reemplaza por null
                if (valorNombre == "Buscar por Nombre:") valorNombre = null;
                if (valorApellido == "Buscar por Apellido:") valorApellido = null;
                if (valorDNI == "Buscar por DNI:") valorDNI = null;
                if (valorMatricula == "Buscar por Matricula:") valorMatricula = null;

                //se pasan los datos para cargar el DataGrid con los Usuarios
                DataTable resultados = GestorDeDatos.BuscarAlumnosPorNombreApellidoDNIMatricula(valorNombre, valorApellido, valorDNI, valorMatricula);

                if (resultados.Rows.Count == 0)
                {
                    MessageBox.Show("No hay Alumnos con esos Datos en el Sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Cargo el DataGridView
                    Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_DataGridView_Alumnos.DataSource = resultados;

                    // Oculto las columnas del DataGridView para que no aparezcan todas
                    Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_DataGridView_Alumnos.Columns["ID_Alumno"].Visible = false;
                    Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_DataGridView_Alumnos.Columns["ID_Perfil"].Visible = false;
                    Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_DataGridView_Alumnos.Columns["Domicilio_Calle"].Visible = false;
                    Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_DataGridView_Alumnos.Columns["Domicilio_Numero"].Visible = false;
                    Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_DataGridView_Alumnos.Columns["Usuario"].Visible = false;
                    Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_DataGridView_Alumnos.Columns["Contraseña"].Visible = false;
                    Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_DataGridView_Alumnos.Columns["Fecha_Baja"].Visible = false;
                    Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_DataGridView_Alumnos.Columns["Fecha_Alta"].Visible = false;

                    //si la cantidad de filas es > 0
                    if (Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_DataGridView_Alumnos.Rows.Count > 0)
                    {
                        Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_DataGridView_Alumnos.Rows[0].Selected = true; //Selecciona la primera fila                    
                    }
                    HabilitarCampos();      //paso los datos a la derecha para poder editarlos
                }
            }
        }

        //Habilita los textbox de la derecha pasandole los datos del usuario seleccionado del DataGrid. Para luego poder editarlos
        private void HabilitarCampos()
        {
            if (Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_DataGridView_Alumnos.SelectedRows.Count > 0)
            {
                editable = true;
                ActualizarTextBoxPorDataGridView();
            }
            else
            {
                MessageBox.Show("Elija una fila de la Tabla para mostrar los datos a la derecha.", "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //De acuerdo a la Usuario seleccionado del DatagridView, se muestran los datos a la derecha
        private void ActualizarTextBoxPorDataGridView()
        {
            #region Actualizar TextBoxs con DataGridView
            if (editable)
            {
                try
                {
                    //Indica que estás accediendo solo a la primera fila seleccionada (índice 0). Si se seleccionan varias toma la primera
                    DataGridViewRow filaSeleccionada = Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_DataGridView_Alumnos.SelectedRows[0];

                    //Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Nombre.Enabled = editable;
                    Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Nombre.Text = filaSeleccionada.Cells["Nombre"].Value?.ToString();
                    Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Nombre.ForeColor = Color.Black;

                    //Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Apellido.Enabled = editable;
                    Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Apellido.Text = filaSeleccionada.Cells["Apellido"].Value?.ToString();
                    Form6_DashBoardExamenes_1_Agregar_PanelDerecho_TextBox_Apellido.ForeColor = Color.Black;
                }
                catch (Exception ex)
                {

                }
            }
            #endregion
        }

        //Sirve para que cada vez que seleccionemos una fila del DataGridView, se actualicen los TextBox de la derecha
        private void Form6_DashBoardExamenes_1_Agregar_PanelIzquierdo_DataGridView_Alumnos_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarTextBoxPorDataGridView();
        }
    }
}

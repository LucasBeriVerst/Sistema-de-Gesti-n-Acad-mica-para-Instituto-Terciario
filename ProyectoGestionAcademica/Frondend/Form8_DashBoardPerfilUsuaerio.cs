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
    public partial class Form8_DashBoardPerfilUsuaerio : Form, IConfiguracion
    {
        private string titulo = "PERFIL DE USUARIO";
        private GestorDeDatos gestorDeDatos = new GestorDeDatos();
        public int IdPerfil { get; set; }
        public string NombreUsuario { get; set; }

        private Form2_DashboardGeneral instanciaForm;
        public Form8_DashBoardPerfilUsuaerio()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form8_DashBoardPerfilUsuaerio_Load(object sender, EventArgs e)
        {
            int perfil = IdPerfil;
            string usuario = NombreUsuario;
            Form8_DashBoardPerfilUsuaerio_Label_Usuario.Text += " " + usuario;
            if (perfil == 4)
            {
                Form8_DashBoardPerfilUsuaerio_Label_Identidad.Text += " " + gestorDeDatos.ObtenerNombreApellidoPorUsuario(usuario);
            }
            else
            {
                Form8_DashBoardPerfilUsuaerio_Label_Identidad.Text += " " + gestorDeDatos.ObtenerNombreApellidoPorUsuarioEmpleado(usuario);
            }
        }

        private void Form8_DashBoardPerfilUsuaerio_Button_Cancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seguro que queres cerrar la pagina actual. Se perderan los datos ingresados...", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Form8_DashBoardPerfilUsuaerio_Button_Agregar_Click(object sender, EventArgs e)
        {
            if (IdPerfil == 4)
            {
                string contraseña = gestorDeDatos.ObtenerContraseniaPorUsuarioAlumno(NombreUsuario);
                if (Form8_DashBoardPerfilUsuaerio_TextBox_NuevaContraseña.Text != contraseña)
                {
                    bool cambio = gestorDeDatos.CambiarContraseniaAlumno(NombreUsuario, Form8_DashBoardPerfilUsuaerio_TextBox_NuevaContraseña.Text);
                    if (cambio)
                    {
                        DialogResult result = MessageBox.Show("Se actualizo la contraseña.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {

                }
            }
            else
            {
                string contraseña = gestorDeDatos.ObtenerContraseniaPorUsuarioEmpleado(NombreUsuario);
                if (Form8_DashBoardPerfilUsuaerio_TextBox_NuevaContraseña.Text != contraseña)
                {
                    int cambio = gestorDeDatos.CambiarContraseniaEmpleado(NombreUsuario, Form8_DashBoardPerfilUsuaerio_TextBox_NuevaContraseña.Text);
                    if (cambio == 1)
                    {
                        DialogResult result = MessageBox.Show("Se actualizo la contraseña.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {

                }
            }
        }

        private void Form8_DashBoardPerfilUsuaerio_TextBox_Contraseña_Enter(object sender, EventArgs e)
        {
            if (Form8_DashBoardPerfilUsuaerio_TextBox_Contraseña.Text == "CONTRASEÑA ACTUAL") { Form8_DashBoardPerfilUsuaerio_TextBox_Contraseña.Text = string.Empty; }
            Form8_DashBoardPerfilUsuaerio_TextBox_Contraseña.ForeColor = Color.Black;
            Form8_DashBoardPerfilUsuaerio_TextBox_Contraseña.PasswordChar = '*';

        }

        private void Form8_DashBoardPerfilUsuaerio_TextBox_Contraseña_Leave(object sender, EventArgs e)
        {
            if (Form8_DashBoardPerfilUsuaerio_TextBox_Contraseña.Text == string.Empty) { Form8_DashBoardPerfilUsuaerio_TextBox_Contraseña.Text = "CONTRASEÑA ACTUAL"; }
            Form8_DashBoardPerfilUsuaerio_TextBox_Contraseña.ForeColor = Color.DimGray;
            Form8_DashBoardPerfilUsuaerio_TextBox_Contraseña.PasswordChar = '\0';
        }

        private void Form8_DashBoardPerfilUsuaerio_TextBox_NuevaContraseña_Leave(object sender, EventArgs e)
        {
            if (Form8_DashBoardPerfilUsuaerio_TextBox_NuevaContraseña.Text == string.Empty) { Form8_DashBoardPerfilUsuaerio_TextBox_NuevaContraseña.Text = "NUEVA CONTRASEÑA"; }
            Form8_DashBoardPerfilUsuaerio_TextBox_NuevaContraseña.ForeColor = Color.DimGray;
            Form8_DashBoardPerfilUsuaerio_TextBox_NuevaContraseña.PasswordChar = '\0';
        }

        private void Form8_DashBoardPerfilUsuaerio_TextBox_NuevaContraseña_Enter(object sender, EventArgs e)
        {
            if (Form8_DashBoardPerfilUsuaerio_TextBox_NuevaContraseña.Text == "NUEVA CONTRASEÑA") { Form8_DashBoardPerfilUsuaerio_TextBox_NuevaContraseña.Text = string.Empty; }
            Form8_DashBoardPerfilUsuaerio_TextBox_NuevaContraseña.ForeColor = Color.Black;
            Form8_DashBoardPerfilUsuaerio_TextBox_NuevaContraseña.PasswordChar = '*';
        }
    }
    
}

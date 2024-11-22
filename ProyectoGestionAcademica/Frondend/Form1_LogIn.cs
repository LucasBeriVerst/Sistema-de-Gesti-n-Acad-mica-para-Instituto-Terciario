using ProyectoGestionAcademica.Backend;
using ProyectoGestionAcademica.Frondend;
using System.Windows.Forms;

namespace ProyectoGestionAcademica
{
    public partial class Form1_LogIn : Form
    {
        //Intentos disponibles para el usuario
        private int intentetos = 3;
        private int respuestaDePerfil;
        GestorDeDatos Instancia_GestorDeDatos = new GestorDeDatos();
        Dictionary<int, string> Perfiles = new Dictionary<int, string>();
        public Form1_LogIn()
        {
            InitializeComponent();
            Form1_LogIn_Labell_Intentos.Text = "Intentos disponibles : " + intentetos;
            Form1_LogIn_LinkLabell_InformarUsuario.TabStop = false;
            Form1_LogIn_LinkLabell_InformarContraseņa.TabStop = false;
            Perfiles.Add(1, "Administrador");
            Perfiles.Add(2, "Empleado Adminirativo");
            Perfiles.Add(3, "Profesor");
            Perfiles.Add(4, "Alumno");
        }
        private void Form1_LogIn_Button_Acceder_Click(object sender, EventArgs e)
        {
            respuestaDePerfil = Instancia_GestorDeDatos.Form_LogIn_BuscarUsuario(Form1_LogIn_TextBox_Usuario.Text, Form1_LogIn_TextBox_Contraseņa.Text);
            if (respuestaDePerfil > 0 && respuestaDePerfil <= 4)
            {
                Form2_DashboardGeneral Form2 = new Form2_DashboardGeneral(respuestaDePerfil, Form1_LogIn_TextBox_Usuario.Text);
                MessageBox.Show("Entraste como " + Perfiles[respuestaDePerfil], "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form2.Show();
                this.Hide();
            }
            else
            {
                if (respuestaDePerfil >= 5 && respuestaDePerfil <= 7)
                {
                    if (respuestaDePerfil == 5)
                    {
                        errorProvider1.SetError(Form1_LogIn_TextBox_Usuario, "Procurar que el usuario no sea mas largo que 8 caracteres");
                    }
                    else if (respuestaDePerfil == 6)
                    {
                        errorProvider1.SetError(Form1_LogIn_TextBox_Contraseņa, "Procurar que el contraseņa no sea mas largo que 35 caracteres");
                    }
                    else
                    {
                        errorProvider1.SetError(Form1_LogIn_TextBox_Usuario, "Procurar que el usuario no sea mas largo que 8 caracteres");
                        errorProvider1.SetError(Form1_LogIn_TextBox_Contraseņa, "Procurar que el contraseņa no sea mas largo que 35 caracteres");
                    }
                }
                else
                {
                    intentetos = intentetos - 1;
                    if (intentetos > 1)
                    {
                        MessageBox.Show("El usuario no esta registrado o hay una discrepancia entre USUARIO y CONTRASEŅA...", "Error: " + respuestaDePerfil, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Form1_LogIn_Labell_Intentos.Text = "Intentos disponibles : " + intentetos;
                    }
                    if (intentetos == 1)
                    {
                        MessageBox.Show("El usuario no esta registrado o hay una discrepancia entre USUARIO y CONTRASEŅA. Un fallo mas y el sistema se bloqueara...", "Error: " + respuestaDePerfil, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Form1_LogIn_Labell_Intentos.Text = "Intentos disponibles : " + intentetos;
                    }
                    if (intentetos == 0)
                    {
                        MessageBox.Show("El usuario no esta registrado o hay una discrepancia entre USUARIO y CONTRASEŅA. Cierre la aplicacion para volver a intentarlo...", "Error: " + respuestaDePerfil, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Form1_LogIn_Button_Acceder.Enabled = false;
                        Form1_LogIn_LinkLabell_BorrarCampos.Enabled = false;
                        Form1_LogIn_LinkLabell_InformarContraseņa.Enabled = false;
                        Form1_LogIn_LinkLabell_InformarUsuario.Enabled = false;
                        Form1_LogIn_TextBox_Contraseņa.Enabled = false;
                        Form1_LogIn_TextBox_Usuario.Enabled = false;
                        Form1_LogIn_Labell_Intentos.Text = "Intentos disponibles : " + intentetos;
                    }
                }
            }
        }
        private void Form1_LogIn_TextBox_Usuario_Enter(object sender, EventArgs e)
        {
            if (Form1_LogIn_TextBox_Usuario.Text == "USUARIO")
            {
                Form1_LogIn_TextBox_Usuario.Text = string.Empty;
                Form1_LogIn_TextBox_Usuario.ForeColor = Color.Black;
            }
        }
        private void Form1_LogIn_TextBox_Usuario_Leave(object sender, EventArgs e)
        {
            if (Form1_LogIn_TextBox_Usuario.Text == string.Empty)
            {
                Form1_LogIn_TextBox_Usuario.Text = "USUARIO";
                Form1_LogIn_TextBox_Usuario.ForeColor = Color.DimGray;
            }
        }
        private void Form1_LogIn_TextBox_Contraseņa_Enter(object sender, EventArgs e)
        {
            Form1_LogIn_TextBox_Contraseņa.PasswordChar = '*';
            if (Form1_LogIn_TextBox_Contraseņa.Text == "CONTRASEŅA")
            {
                Form1_LogIn_TextBox_Contraseņa.Text = string.Empty;
                Form1_LogIn_TextBox_Contraseņa.ForeColor = Color.Black;
            }
        }
        private void Form1_LogIn_TextBox_Contraseņa_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Form1_LogIn_TextBox_Contraseņa.Text))
            {
                Form1_LogIn_TextBox_Contraseņa.PasswordChar = '\0';
                Form1_LogIn_TextBox_Contraseņa.UseSystemPasswordChar = false;
                Form1_LogIn_TextBox_Contraseņa.Text = "CONTRASEŅA";
                Form1_LogIn_TextBox_Contraseņa.ForeColor = Color.DimGray;
            }
        }
        private void Form1_LogIn_LinkLabell_CerrarApp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult = MessageBox.Show("ŋ Seguro que queres salir ?", "Salir...", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        private void Form1_LogIn_LinkLabell_MinimizarApp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Form1_LogIn_LinkLabell_BorrarCampos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Form1_LogIn_TextBox_Usuario.Text != "USUARIO" || Form1_LogIn_TextBox_Contraseņa.Text != "CONTRASEŅA")
            {
                Form1_LogIn_TextBox_Usuario.Text = string.Empty;
                Form1_LogIn_TextBox_Contraseņa.Text = string.Empty;
                Form1_LogIn_TextBox_Usuario.Focus();
            }
        }
        private void Form1_LogIn_LinkLabell_InformarUsuario_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1_LogIn_ToolTip_Contexto.SetToolTip(Form1_LogIn_LinkLabell_InformarUsuario, "- El usuario suele ser el DNI de la persona ingresante.");
        }
        private void Form1_LogIn_LinkLabell_InformarContraseņa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1_LogIn_ToolTip_Contexto.SetToolTip(Form1_LogIn_LinkLabell_InformarContraseņa, "- La contraseņa tiene como maximo 12 caracteres.\n- Si no recuerda su contraseņa, comuniquese con algun personal administrativo.");
        }
    }
}

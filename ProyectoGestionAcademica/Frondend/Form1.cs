using ProyectoGestionAcademica.Frondend;

namespace ProyectoGestionAcademica
{
    public partial class Form1_LogIn : Form
    {
        private int intentetos = 3;
        Form2_DashboardAlumnos Form2 = new Form2_DashboardAlumnos();
        public Form1_LogIn()
        {
            InitializeComponent();
            Form1_LogIn_Labell_Intentos.Text = "Intentos disponibles : " + intentetos;
            Form1_LogIn_LinkLabell_InformarUsuario.TabStop = false;
            Form1_LogIn_LinkLabell_InformarContraseña.TabStop = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form1_LogIn_TextBox_Usuario.Text == "admin" && Form1_LogIn_TextBox_Contraseña.Text == "admin")
            {
                MessageBox.Show("Entraste como admin...", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form2.Show();
                this.Hide();
            }
            else
            {
                intentetos = intentetos - 1;
                if (intentetos > 1)
                {
                    MessageBox.Show("El usuario o la contraseña son incorectos...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Form1_LogIn_Labell_Intentos.Text = "Intentos disponibles : " + intentetos;
                }
                if (intentetos == 1)
                {
                    MessageBox.Show("El usuario o la contraseña son incorectos. Un fallo mas y el sistema se bloqueara...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Form1_LogIn_Labell_Intentos.Text = "Intentos disponibles : " + intentetos;
                }
                if (intentetos == 0)
                {
                    MessageBox.Show("El usuario o la contraseña son incorectos. Cierre la aplicacion para volver a intentarlo...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Form1_LogIn_Button_Acceder.Enabled = false;
                    Form1_LogIn_LinkLabell_BorrarCampos.Enabled = false;
                    Form1_LogIn_LinkLabell_InformarContraseña.Enabled = false;
                    Form1_LogIn_LinkLabell_InformarUsuario.Enabled = false;
                    Form1_LogIn_TextBox_Contraseña.Enabled = false;
                    Form1_LogIn_TextBox_Usuario.Enabled = false;
                    Form1_LogIn_Labell_Intentos.Text = "Intentos disponibles : " + intentetos;
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

        private void Form1_LogIn_TextBox_Contraseña_Enter(object sender, EventArgs e)
        {
            if (Form1_LogIn_TextBox_Contraseña.Text == "CONTRASEÑA")
            {
                Form1_LogIn_TextBox_Contraseña.Text = string.Empty;
                Form1_LogIn_TextBox_Contraseña.ForeColor = Color.Black;
            }
            Form1_LogIn_TextBox_Contraseña.PasswordChar = '*';
        }

        private void Form1_LogIn_TextBox_Contraseña_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Form1_LogIn_TextBox_Contraseña.Text))
            {
                Form1_LogIn_TextBox_Contraseña.UseSystemPasswordChar = false;
                Form1_LogIn_TextBox_Contraseña.Text = "CONTRASEÑA";
                Form1_LogIn_TextBox_Contraseña.ForeColor = Color.DimGray;
            }
        }

        private void Form1_LogIn_LinkLabell_CerrarApp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult = MessageBox.Show("¿ Seguro que queres salir ?", "Salir...", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
            if (Form1_LogIn_TextBox_Usuario.Text != "USUARIO" || Form1_LogIn_TextBox_Contraseña.Text != "CONTRASEÑA")
            {
                Form1_LogIn_TextBox_Usuario.Text = string.Empty;
                Form1_LogIn_TextBox_Contraseña.Text = string.Empty;
                Form1_LogIn_TextBox_Usuario.Focus();
            }
        }

        private void Form1_LogIn_ToolTip_ContextoUsuario_Popup(object sender, PopupEventArgs e)
        {

        }

        private void Form1_LogIn_LinkLabell_InformarUsuario_Enter(object sender, EventArgs e)
        {
            Form1_LogIn_ToolTip_Contexto.SetToolTip(Form1_LogIn_LinkLabell_InformarUsuario, "La contraseña suele ser el DNI del usuario");
        }

        private void Form1_LogIn_LinkLabell_InformarUsuario_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1_LogIn_ToolTip_Contexto.SetToolTip(Form1_LogIn_LinkLabell_InformarUsuario, "- El usuario suele ser el DNI de la persona ingresante.");
        }

        private void Form1_LogIn_LinkLabell_InformarContraseña_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1_LogIn_ToolTip_Contexto.SetToolTip(Form1_LogIn_LinkLabell_InformarContraseña, "- La contraseña tiene como maximo 12 caracteres.\n- Si no recuerda su contraseña, comuniquese con algun personal administrativo.");
        }

        private void Form1_LogIn_LinkLabell_BorrarCampos_Enter(object sender, EventArgs e)
        {
            Form1_LogIn_ToolTip_Contexto.SetToolTip(Form1_LogIn_LinkLabell_BorrarCampos, "- Devuelve los campos de texto a su estado original.");
        }

        private void Form1_LogIn_LinkLabell_InformarUsuario_Leave(object sender, EventArgs e)
        {
            Form1_LogIn_ToolTip_Contexto.SetToolTip(Form1_LogIn_LinkLabell_InformarUsuario, "La contraseña suele ser el DNI del usuario");
        }

        private void Form1_LogIn_LinkLabell_InformarContraseña_Enter(object sender, EventArgs e)
        {
            Form1_LogIn_ToolTip_Contexto.SetToolTip(Form1_LogIn_LinkLabell_InformarContraseña, "- La contraseña tiene como maximo 12 caracteres.\n- Si no recuerda su contraseña, comuniquese con algun personal administrativo.");
        }

        private void Form1_LogIn_PictureBox_HiletLogo_Click(object sender, EventArgs e)
        {

        }
    }
}

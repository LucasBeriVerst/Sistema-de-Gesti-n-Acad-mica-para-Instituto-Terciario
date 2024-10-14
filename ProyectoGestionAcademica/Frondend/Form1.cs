namespace ProyectoGestionAcademica
{
    public partial class Form1_LogIn : Form
    {
        public Form1_LogIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

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
                Form1_LogIn_TextBox_Contraseña.UseSystemPasswordChar = true;
            }
        }

        private void Form1_LogIn_TextBox_Contraseña_Leave(object sender, EventArgs e)
        {
            if (Form1_LogIn_TextBox_Contraseña.Text == string.Empty)
            {
                Form1_LogIn_TextBox_Contraseña.Text = "CONTRASEÑA";
                Form1_LogIn_TextBox_Contraseña.ForeColor = Color.DimGray;
                Form1_LogIn_TextBox_Contraseña.UseSystemPasswordChar = false;
                Form1_LogIn_LinkLabell_InformarContraseña.Focus();
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
    }
}

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
    }
}

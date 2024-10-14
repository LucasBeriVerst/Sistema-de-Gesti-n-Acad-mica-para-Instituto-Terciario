namespace ProyectoGestionAcademica
{
    partial class Form1_LogIn
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Form1_LogIn_Panel_Izquierdo = new Panel();
            Form1_LogIn_TextBox_Titulo = new TextBox();
            Form1_LogIn_PictureBox_HiletLogo = new PictureBox();
            Form1_LogIn_Panel_Derecho = new Panel();
            Form1_LogIn_Panel_Izquierdo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Form1_LogIn_PictureBox_HiletLogo).BeginInit();
            SuspendLayout();
            // 
            // Form1_LogIn_Panel_Izquierdo
            // 
            Form1_LogIn_Panel_Izquierdo.BackColor = Color.FromArgb(36, 60, 100);
            Form1_LogIn_Panel_Izquierdo.Controls.Add(Form1_LogIn_TextBox_Titulo);
            Form1_LogIn_Panel_Izquierdo.Controls.Add(Form1_LogIn_PictureBox_HiletLogo);
            Form1_LogIn_Panel_Izquierdo.Dock = DockStyle.Left;
            Form1_LogIn_Panel_Izquierdo.Location = new Point(0, 0);
            Form1_LogIn_Panel_Izquierdo.Name = "Form1_LogIn_Panel_Izquierdo";
            Form1_LogIn_Panel_Izquierdo.Size = new Size(269, 400);
            Form1_LogIn_Panel_Izquierdo.TabIndex = 0;
            // 
            // Form1_LogIn_TextBox_Titulo
            // 
            Form1_LogIn_TextBox_Titulo.BackColor = Color.FromArgb(36, 60, 100);
            Form1_LogIn_TextBox_Titulo.BorderStyle = BorderStyle.None;
            Form1_LogIn_TextBox_Titulo.Font = new Font("Fira Sans Medium", 16F, FontStyle.Bold);
            Form1_LogIn_TextBox_Titulo.ForeColor = Color.FromArgb(188, 196, 212);
            Form1_LogIn_TextBox_Titulo.Location = new Point(34, 283);
            Form1_LogIn_TextBox_Titulo.Name = "Form1_LogIn_TextBox_Titulo";
            Form1_LogIn_TextBox_Titulo.ReadOnly = true;
            Form1_LogIn_TextBox_Titulo.Size = new Size(198, 39);
            Form1_LogIn_TextBox_Titulo.TabIndex = 1;
            Form1_LogIn_TextBox_Titulo.TabStop = false;
            Form1_LogIn_TextBox_Titulo.Text = "LOG-IN";
            Form1_LogIn_TextBox_Titulo.TextAlign = HorizontalAlignment.Center;
            // 
            // Form1_LogIn_PictureBox_HiletLogo
            // 
            Form1_LogIn_PictureBox_HiletLogo.BackgroundImageLayout = ImageLayout.None;
            Form1_LogIn_PictureBox_HiletLogo.Image = Properties.Resources.Captura_de_pantalla_2024_10_13_2230491;
            Form1_LogIn_PictureBox_HiletLogo.Location = new Point(34, 80);
            Form1_LogIn_PictureBox_HiletLogo.Name = "Form1_LogIn_PictureBox_HiletLogo";
            Form1_LogIn_PictureBox_HiletLogo.Size = new Size(198, 95);
            Form1_LogIn_PictureBox_HiletLogo.TabIndex = 0;
            Form1_LogIn_PictureBox_HiletLogo.TabStop = false;
            // 
            // Form1_LogIn_Panel_Derecho
            // 
            Form1_LogIn_Panel_Derecho.BackColor = Color.FromArgb(177, 173, 189);
            Form1_LogIn_Panel_Derecho.Dock = DockStyle.Fill;
            Form1_LogIn_Panel_Derecho.Location = new Point(269, 0);
            Form1_LogIn_Panel_Derecho.Name = "Form1_LogIn_Panel_Derecho";
            Form1_LogIn_Panel_Derecho.Size = new Size(531, 400);
            Form1_LogIn_Panel_Derecho.TabIndex = 1;
            // 
            // Form1_LogIn
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 400);
            Controls.Add(Form1_LogIn_Panel_Derecho);
            Controls.Add(Form1_LogIn_Panel_Izquierdo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1_LogIn";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Form1_LogIn_Panel_Izquierdo.ResumeLayout(false);
            Form1_LogIn_Panel_Izquierdo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Form1_LogIn_PictureBox_HiletLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel Form1_LogIn_Panel_Izquierdo;
        private PictureBox Form1_LogIn_PictureBox_HiletLogo;
        private Panel Form1_LogIn_Panel_Derecho;
        private TextBox Form1_LogIn_TextBox_Titulo;
    }
}

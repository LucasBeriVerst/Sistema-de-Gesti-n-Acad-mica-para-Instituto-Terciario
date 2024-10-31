namespace ProyectoGestionAcademica.Frondend
{
    partial class Form3_DashBoardAlumnos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Form3_DashBoardAlumnos_DataGrigViewAlumnos = new DataGridView();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)Form3_DashBoardAlumnos_DataGrigViewAlumnos).BeginInit();
            SuspendLayout();
            // 
            // Form3_DashBoardAlumnos_DataGrigViewAlumnos
            // 
            Form3_DashBoardAlumnos_DataGrigViewAlumnos.AllowUserToOrderColumns = true;
            Form3_DashBoardAlumnos_DataGrigViewAlumnos.BackgroundColor = SystemColors.Control;
            Form3_DashBoardAlumnos_DataGrigViewAlumnos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Form3_DashBoardAlumnos_DataGrigViewAlumnos.GridColor = Color.Yellow;
            Form3_DashBoardAlumnos_DataGrigViewAlumnos.Location = new Point(18, 16);
            Form3_DashBoardAlumnos_DataGrigViewAlumnos.Name = "Form3_DashBoardAlumnos_DataGrigViewAlumnos";
            Form3_DashBoardAlumnos_DataGrigViewAlumnos.Size = new Size(713, 526);
            Form3_DashBoardAlumnos_DataGrigViewAlumnos.TabIndex = 0;
            Form3_DashBoardAlumnos_DataGrigViewAlumnos.CellContentClick += Form3_DashBoardAlumnos_DataGrigViewAlumnos_CellContentClick;
            // 
            // button1
            // 
            button1.Location = new Point(18, 570);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(198, 570);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(409, 570);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 3;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(656, 570);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 4;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            // 
            // Form3_DashBoardAlumnos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(177, 173, 189);
            ClientSize = new Size(1040, 605);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(Form3_DashBoardAlumnos_DataGrigViewAlumnos);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form3_DashBoardAlumnos";
            Text = "Form3";
            Load += Form3_Load;
            ((System.ComponentModel.ISupportInitialize)Form3_DashBoardAlumnos_DataGrigViewAlumnos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView Form3_DashBoardAlumnos_DataGrigViewAlumnos;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
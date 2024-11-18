namespace ProyectoGestionAcademica.Frondend.Form6_Examenes
{
    partial class Form6_DashBoardExamenes_4_Asignar
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
            label1 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 22F);
            label1.Location = new Point(346, 348);
            label1.Name = "label1";
            label1.Size = new Size(458, 50);
            label1.TabIndex = 0;
            label1.Text = "NO VA ESTE FORMULARIO";
            // 
            // Form6_DashBoardExamenes_4_Asignar
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(177, 173, 189);
            ClientSize = new Size(1189, 807);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form6_DashBoardExamenes_4_Asignar";
            Text = "Form6_DashBoardExamenes_4_Asignar";
            Load += Form6_DashBoardExamenes_4_Asignar_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
    }
}
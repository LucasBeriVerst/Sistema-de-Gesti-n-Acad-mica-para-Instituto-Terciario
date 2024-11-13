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
    public partial class Form3_DashBoardAlumnos_2_Editar : Form, IConfiguracion
    {
        private string titulo = "ALUMNOS: EDITAR";
        public Form3_DashBoardAlumnos_2_Editar()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form3_DashBoardAlumnos_2_Editar_Load(object sender, EventArgs e)
        {

        }

        private void Form3_DashBoardAlumnos_2_Editar_PanelInferior_Button_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

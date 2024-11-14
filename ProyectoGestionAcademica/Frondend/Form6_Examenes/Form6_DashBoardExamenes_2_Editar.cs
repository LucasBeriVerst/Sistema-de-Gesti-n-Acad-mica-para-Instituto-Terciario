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
    public partial class Form6_DashBoardExamenes_2_Editar : Form, IConfiguracion
    {
        private string titulo = "MATERIAS: EDITAR";
        public Form6_DashBoardExamenes_2_Editar()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form6_DashBoardExamenes_2_Editar_Load(object sender, EventArgs e)
        {

        }
    }
}

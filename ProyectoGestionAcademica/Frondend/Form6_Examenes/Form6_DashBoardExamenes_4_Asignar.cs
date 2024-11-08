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
    public partial class Form6_DashBoardExamenes_4_Asignar : Form, IConfiguracion
    {
        private string titulo = "MATERIAS: ASIGNAR";
        public Form6_DashBoardExamenes_4_Asignar()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form6_DashBoardExamenes_4_Asignar_Load(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoGestionAcademica.Frondend.Form5_Materias
{
    public partial class Form5_DashBoardMaterias_5_Informacion : Form, IConfiguracion
    {
        private string titulo = "MATERIAS: INFORMACIÓN";
        public Form5_DashBoardMaterias_5_Informacion()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form5_DashBoardMaterias_5_Informacion_Load(object sender, EventArgs e)
        {

        }
    }
}

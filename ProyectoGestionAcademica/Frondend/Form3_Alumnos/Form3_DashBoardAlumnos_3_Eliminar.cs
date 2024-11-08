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
    public partial class Form3_DashBoardAlumnos_3_Eliminar : Form, IConfiguracion
    {
        private string titulo = "ALUMNOS: ELIMINAR";
        public Form3_DashBoardAlumnos_3_Eliminar()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form3_DashBoardAlumnos_3_Eliminar_Load(object sender, EventArgs e)
        {

        }
    }
}
